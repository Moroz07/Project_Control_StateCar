from typing import List, Optional

import psycopg2
from psycopg2.extras import RealDictCursor

from app.config import DBConnection
from app.errors import TelemetryStorageError
from app.models import (
    DtcEventRecord,
    ElectricalSensorPayload,
    EngineSensorPayload,
    VehicleNormAndState,
)
from app.repositories.base import ITelemetryRepository


class TelemetryRepository(ITelemetryRepository):
    def get_vehicle_norms(self, vin: str) -> Optional[VehicleNormAndState]:
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor(cursor_factory=RealDictCursor) as cur:
                    cur.execute("SELECT * FROM v_vehicle_state WHERE vin = %s;", (vin,))
                    row = cur.fetchone()
                    return VehicleNormAndState(**row) if row else None
        except psycopg2.Error as exc:
            raise TelemetryStorageError("Не удалось прочитать данные автомобиля") from exc

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
                    cur.execute(
                        query,
                        (payload.engine_rpm, payload.coolant_temp, payload.oil_temp,
                         payload.oil_pressure, vin),
                    )
                    return cur.rowcount == 1
        except psycopg2.Error as exc:
            raise TelemetryStorageError("Не удалось сохранить телеметрию двигателя") from exc

    def update_electrical_state(self, vin: str, payload: ElectricalSensorPayload) -> bool:
        query = """
            UPDATE vehicles
            SET battery_voltage = %s, current_mileage = %s, updated_at = NOW()
            WHERE vin = %s;
        """
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor() as cur:
                    cur.execute(
                        query,
                        (payload.battery_voltage, payload.current_mileage, vin),
                    )
                    return cur.rowcount == 1
        except psycopg2.Error as exc:
            raise TelemetryStorageError(
                "Не удалось сохранить телеметрию бортовой сети"
            ) from exc

    def save_dtc_errors(self, vin: str, sensor_id: str, errors: List[str]) -> None:
        if not errors:
            return

        query = """
            INSERT INTO dtc_events (vin, sensor_id, error_code)
            VALUES (%s, %s, %s);
        """
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor() as cur:
                    cur.executemany(
                        query,
                        [(vin, sensor_id, error_code) for error_code in errors],
                    )
        except psycopg2.Error as exc:
            raise TelemetryStorageError("Не удалось сохранить коды OBD") from exc

    def get_dtc_events(self, vin: str) -> List[DtcEventRecord]:
        query = """
            SELECT sensor_id, error_code, received_at
            FROM dtc_events
            WHERE vin = %s
            ORDER BY received_at DESC
            LIMIT 50;
        """
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor(cursor_factory=RealDictCursor) as cur:
                    cur.execute(query, (vin,))
                    return [DtcEventRecord(**row) for row in cur.fetchall()]
        except psycopg2.Error as exc:
            raise TelemetryStorageError("Не удалось прочитать коды OBD") from exc
