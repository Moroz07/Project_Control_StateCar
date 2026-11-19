from typing import List, Optional

import psycopg2
from psycopg2.extras import RealDictCursor

from app.config import DBConnection
from app.errors import TelemetryStorageError
from app.models import (
    DtcEventRecord,
    ElectricalSensorPayload,
    EngineSensorPayload,
    SensorType,
    TelemetryReadingRecord,
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

    def get_telemetry_readings(
        self, vin: str, sensor_type: SensorType
    ) -> List[TelemetryReadingRecord]:
        try:
            with psycopg2.connect(DBConnection.connection_string) as conn:
                with conn.cursor(cursor_factory=RealDictCursor) as cur:
                    if sensor_type == SensorType.OBD:
                        cur.execute(
                            """
                            SELECT id, vin, sensor_id, error_code, received_at
                            FROM dtc_events
                            WHERE vin = %s
                            ORDER BY received_at DESC, id DESC;
                            """,
                            (vin,),
                        )
                        return [
                            TelemetryReadingRecord(
                                id=row["id"],
                                vin=row["vin"].strip(),
                                sensor_type=sensor_type,
                                recorded_at=row["received_at"],
                                data={
                                    "sensor_id": row["sensor_id"],
                                    "error_code": row["error_code"],
                                },
                            )
                            for row in cur.fetchall()
                        ]

                    parameter_codes = {
                        SensorType.ENGINE: (
                            "engine_rpm", "coolant_temp", "oil_temp", "oil_pressure"
                        ),
                        SensorType.ELECTRICAL: (
                            "battery_voltage", "current_mileage"
                        ),
                    }[sensor_type]
                    cur.execute(
                        """
                        SELECT m.id, m.vin, t.parameter_code, t.unit,
                               m.value, m.measured_at
                        FROM measurements AS m
                        JOIN thresholds AS t ON t.id = m.threshold_id
                        WHERE m.vin = %s AND t.parameter_code = ANY(%s)
                        ORDER BY m.measured_at DESC, m.id DESC;
                        """,
                        (vin, list(parameter_codes)),
                    )
                    return [
                        TelemetryReadingRecord(
                            id=row["id"],
                            vin=row["vin"].strip(),
                            sensor_type=sensor_type,
                            recorded_at=row["measured_at"],
                            data={
                                "parameter": row["parameter_code"],
                                "value": float(row["value"]),
                                "unit": row["unit"],
                            },
                        )
                        for row in cur.fetchall()
                    ]
        except psycopg2.Error as exc:
            raise TelemetryStorageError("Не удалось прочитать историю телеметрии") from exc
