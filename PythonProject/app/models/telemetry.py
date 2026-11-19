from datetime import datetime
from enum import Enum
from typing import Any, Dict, List, Optional

from pydantic import BaseModel


class EngineTelemetryData(BaseModel):
    engine_rpm: Optional[int] = None
    coolant_temp: Optional[float] = None
    oil_temp: Optional[float] = None
    oil_pressure: Optional[float] = None


class ElectricalTelemetryData(BaseModel):
    battery_voltage: Optional[float] = None
    current_mileage: int


class DtcEventRecord(BaseModel):
    sensor_id: str
    error_code: str
    received_at: datetime


class EngineTelemetryResponse(BaseModel):
    vin: str
    car_number: str
    updated_at: datetime
    engine: EngineTelemetryData


class ElectricalTelemetryResponse(BaseModel):
    vin: str
    car_number: str
    updated_at: datetime
    electrical: ElectricalTelemetryData


class ObdTelemetryResponse(BaseModel):
    vin: str
    car_number: str
    dtc_events: List[DtcEventRecord]


class SensorType(str, Enum):
    ENGINE = "engine"
    ELECTRICAL = "electrical"
    OBD = "obd"


class TelemetryReadingRecord(BaseModel):
    id: int
    vin: str
    sensor_type: SensorType
    recorded_at: datetime
    data: Dict[str, Any]


class TelemetryHistoryResponse(BaseModel):
    vin: str
    sensor_type: SensorType
    readings: List[TelemetryReadingRecord]
