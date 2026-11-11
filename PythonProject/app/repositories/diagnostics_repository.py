from typing import Any, Dict, List, Optional

import psycopg2
from psycopg2.extras import Json, RealDictCursor

from app.config import DBConnection
from app.models import (
    VehicleNormAndState,
    EngineSensorPayload,
    ElectricalSensorPayload,
    RepairRequest,
    RepairStatus,
    SensorType,
    VehicleStatus,
    VehicleStateInfo,
    StatusHistoryItem,
    SensorReadingRecord,
)
from app.repositories.base import IDiagnosticsRepository


class DiagnosticsRepository(IDiagnosticsRepository):
    # Какие столбцы vehicle_state обновляет каждый датчик
    _STATE_COLUMNS = {
        SensorType.ENGINE: ["coolant_temp", "oil_temp", "oil_pressure", "max_rpm"],
        SensorType.ELECTRICAL: ["battery_voltage", "battery_charge", "current_mileage"],
        SensorType.CHASSIS: ["fuel_level", "tire_pressure", "brake_wear"],
    }

    # ==================== прежние методы ====================

    def get_vehicle_norms(self, vin: str) -> Optional[VehicleNormAndState]:
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor(cursor_factory=RealDictCursor) as cur:
                    cur.execute("SELECT * FROM v_vehicle_state WHERE vin = %s;", (vin,))
                    row = cur.fetchone()
                    return VehicleNormAndState(**row) if row else None
        except psycopg2.Error:
            return None

    def update_engine_state(self, vin: str, payload: EngineSensorPayload) -> bool:
        query = """
            UPDATE vehicles
            SET engine_rpm = %s, coolant_temp = %s, oil_temp = %s, oil_pressure = %s,
                updated_at = NOW()
            WHERE vin = %s;
        """
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor() as cur:
                    cur.execute(query, (payload.engine_rpm, payload.coolant_temp,
                                        payload.oil_temp, payload.oil_pressure, vin))
                    return cur.rowcount == 1
        except psycopg2.Error:
            return False

    def update_electrical_state(self, vin: str, payload: ElectricalSensorPayload) -> bool:
        query = """
            UPDATE vehicles
            SET battery_voltage = %s, current_mileage = %s, updated_at = NOW()
            WHERE vin = %s;
        """
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor() as cur:
                    cur.execute(query, (payload.battery_voltage, payload.current_mileage, vin))
                    return cur.rowcount == 1
        except psycopg2.Error:
            return False

    def create_repair_request(self, request: RepairRequest) -> int:
        query = """
            INSERT INTO repair_requests (vehicle_id, description, status, created_at)
            SELECT id, %s, %s, %s FROM vehicles WHERE vin = %s
            RETURNING id;
        """
        with psycopg2.connect(DBConnection.connection_string) as conn:
            with conn.cursor() as cur:
                cur.execute(query, (request.description, request.status.value,
                                    request.created_at, request.vin))
                return cur.fetchone()[0]

    def get_requests_by_vin(self, vin: str) -> List[RepairRequest]:
        query = """
            SELECT r.id, v.vin, v.car_number, r.description, r.status, r.created_at
            FROM repair_requests r
            JOIN vehicles v ON v.id = r.vehicle_id
            WHERE v.vin = %s
            ORDER BY r.created_at DESC;
        """
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor(cursor_factory=RealDictCursor) as cur:
                    cur.execute(query, (vin,))
                    return [RepairRequest(**row) for row in cur.fetchall()]
        except psycopg2.Error:
            return []

    def update_request_status(self, request_id: int, new_status: RepairStatus) -> bool:
        is_closed = new_status in (RepairStatus.COMPLETED, RepairStatus.CANCELLED)
        query = """
            UPDATE repair_requests
            SET status = %s,
                closed_at = CASE WHEN %s THEN NOW() ELSE NULL END
            WHERE id = %s;
        """
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor() as cur:
                    cur.execute(query, (new_status.value, is_closed, request_id))
                    return cur.rowcount == 1
        except psycopg2.Error:
            return False

    # ==================== опрос датчиков после поездки ====================

    def is_trip_processed(self, trip_id: str) -> bool:
        with psycopg2.connect(DBConnection.connection_string) as conn:
            with conn.cursor() as cur:
                cur.execute("SELECT 1 FROM trips WHERE trip_id = %s;", (trip_id,))
                return cur.fetchone() is not None

    def mark_trip_processed(self, vin: str, trip_id: str) -> bool:
        query = "INSERT INTO trips (trip_id, vin) VALUES (%s, %s) ON CONFLICT DO NOTHING;"
        with psycopg2.connect(DBConnection.connection_string) as conn:
            with conn.cursor() as cur:
                cur.execute(query, (trip_id, vin))
                return cur.rowcount == 1

    def save_sensor_reading(self, vin: str, trip_id: str, sensor_type: SensorType,
                            data: Dict[str, Any]) -> int:
        query = """
            INSERT INTO sensor_readings (vin, trip_id, sensor_id, sensor_type, payload)
            VALUES (%s, %s, %s, %s, %s)
            RETURNING id;
        """
        with psycopg2.connect(DBConnection.connection_string) as conn:
            with conn.cursor() as cur:
                cur.execute(query, (vin, trip_id, data["sensor_id"], sensor_type.value, Json(data)))
                return cur.fetchone()[0]

    def update_vehicle_state(self, vin: str, sensor_type: SensorType, data: Dict[str, Any]) -> bool:
        columns = self._STATE_COLUMNS[sensor_type]
        column_list = ", ".join(columns)
        placeholders = ", ".join(["%s"] * len(columns))
        updates = ", ".join(f"{c} = EXCLUDED.{c}" for c in columns)
        # Имена столбцов берутся только из _STATE_COLUMNS, значения передаются параметрами
        query = f"""
            INSERT INTO vehicle_state (vin, {column_list}, updated_at)
            VALUES (%s, {placeholders}, NOW())
            ON CONFLICT (vin) DO UPDATE SET {updates}, updated_at = NOW();
        """
        values = [vin] + [data[c] for c in columns]
        with psycopg2.connect(DBConnection.connection_string) as conn:
            with conn.cursor() as cur:
                cur.execute(query, values)
                return cur.rowcount == 1

    def get_vehicle_state(self, vin: str) -> Optional[VehicleStateInfo]:
        query = """
            SELECT v.vin, v.car_number, s.name AS status,
                   vs.coolant_temp, vs.oil_temp, vs.oil_pressure, vs.max_rpm,
                   vs.battery_voltage, vs.battery_charge, vs.current_mileage,
                   vs.fuel_level, vs.tire_pressure, vs.brake_wear, vs.updated_at
            FROM vehicles v
            JOIN statuses s ON s.id = v.status_id
            LEFT JOIN vehicle_state vs ON vs.vin = v.vin
            WHERE v.vin = %s;
        """
        with psycopg2.connect(DBConnection.connection_string) as conn:
            with conn.cursor(cursor_factory=RealDictCursor) as cur:
                cur.execute(query, (vin,))
                row = cur.fetchone()
                return VehicleStateInfo(**row) if row else None

    def set_vehicle_status(self, vin: str, status: VehicleStatus, comment: str) -> bool:
        # Обновление статуса и запись истории — одна транзакция
        with psycopg2.connect(DBConnection.connection_string) as conn:
            with conn.cursor() as cur:
                cur.execute(
                    """
                    UPDATE vehicles
                    SET status_id = (SELECT id FROM statuses WHERE name = %s)
                    WHERE vin = %s
                    RETURNING id;
                    """,
                    (status.value, vin),
                )
                row = cur.fetchone()
                if not row:
                    return False
                cur.execute(
                    """
                    INSERT INTO status_history (vehicle_id, status_id, comment)
                    SELECT %s, id, %s FROM statuses WHERE name = %s;
                    """,
                    (row[0], comment, status.value),
                )
                return True

    def get_status_history(self, vin: str) -> List[StatusHistoryItem]:
        query = """
            SELECT s.name AS status, h.comment, h.changed_at
            FROM status_history h
            JOIN vehicles v ON v.id = h.vehicle_id
            JOIN statuses s ON s.id = h.status_id
            WHERE v.vin = %s
            ORDER BY h.changed_at DESC, h.id DESC;
        """
        with psycopg2.connect(DBConnection.connection_string) as conn:
            with conn.cursor(cursor_factory=RealDictCursor) as cur:
                cur.execute(query, (vin,))
                return [StatusHistoryItem(**row) for row in cur.fetchall()]

    def get_sensor_readings(self, vin: str, sensor_type: SensorType) -> List[SensorReadingRecord]:
        query = """
            SELECT id, vin, trip_id, sensor_id, sensor_type, payload, recorded_at
            FROM sensor_readings
            WHERE vin = %s AND sensor_type = %s
            ORDER BY recorded_at DESC;
        """
        with psycopg2.connect(DBConnection.connection_string) as conn:
            with conn.cursor(cursor_factory=RealDictCursor) as cur:
                cur.execute(query, (vin, sensor_type.value))
                return [SensorReadingRecord(**row) for row in cur.fetchall()]