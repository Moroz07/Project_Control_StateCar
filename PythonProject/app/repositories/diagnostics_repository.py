from typing import List, Optional

import psycopg2
from psycopg2.extras import RealDictCursor

from app.config import DBConnection
from app.models import (
    VehicleNormAndState,
    EngineSensorPayload,
    ElectricalSensorPayload,
    RepairRequest,
    RepairStatus,
)
from app.repositories.base import IDiagnosticsRepository


class DiagnosticsRepository(IDiagnosticsRepository):
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
            SET engine_rpm = %s,
                coolant_temp = %s,
                oil_temp = %s,
                oil_pressure = %s,
                updated_at = NOW()
            WHERE vin = %s;
        """
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor() as cur:
                    cur.execute(
                        query,
                        (payload.engine_rpm, payload.coolant_temp,
                         payload.oil_temp, payload.oil_pressure, vin)
                    )
                    return cur.rowcount == 1
        except psycopg2.Error:
            return False

    def update_electrical_state(self, vin: str, payload: ElectricalSensorPayload) -> bool:
        query = """
            UPDATE vehicles
            SET battery_voltage = %s,
                current_mileage = %s,
                updated_at = NOW()
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
                ORDER BY r.created_at DESC; \
                """
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor(cursor_factory=RealDictCursor) as cur:
                    cur.execute(query, (vin,))
                    return [RepairRequest(**row) for row in cur.fetchall()]
        except psycopg2.Error:
            return []

    def update_request_status(self, request_id: int, new_status: RepairStatus) -> bool:
        query = """
                UPDATE repair_requests
                SET status    = %s,
                    closed_at = CASE WHEN %s IN ('closed', 'cancelled') THEN NOW() ELSE NULL END
                WHERE id = %s;
                """
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor() as cur:
                    cur.execute(query, (new_status.value, new_status.value, request_id))
                    return cur.rowcount == 1
        except psycopg2.Error:
            return False