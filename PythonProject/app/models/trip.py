import re
from datetime import datetime
from enum import Enum
from typing import Any, Dict, Optional

from pydantic import BaseModel, Field, field_validator

VIN_PATTERN = re.compile(r"^[A-HJ-NPR-Z0-9]{17}$")


class VehicleStatus(str, Enum):
    OK = "исправен"
    READY = "готов к эксплуатации"
    NEEDS_SERVICE = "требует обслуживания"
    DIAGNOSTICS = "на диагностике"
    IN_REPAIR = "в ремонте"


class SensorType(str, Enum):
    ENGINE = "engine"
    ELECTRICAL = "electrical"
    CHASSIS = "chassis"


class TripCompletedPayload(BaseModel):
    vin: str = Field(..., description="VIN автомобиля (17 символов, без I, O, Q)")
    trip_id: str = Field(..., description="Идентификатор завершённой поездки")

    @field_validator("vin")
    @classmethod
    def validate_vin(cls, v: str) -> str:
        vin = v.strip().upper()
        if not VIN_PATTERN.match(vin):
            raise ValueError("VIN должен состоять из 17 символов (латинские буквы без I, O, Q и цифры)")
        return vin

    @field_validator("trip_id")
    @classmethod
    def validate_trip_id(cls, v: str) -> str:
        if not v or not v.strip():
            raise ValueError("Укажите trip_id")
        return v.strip()


class SensorResponseBase(BaseModel):
    sensor_id: str

    @field_validator("sensor_id")
    @classmethod
    def validate_sensor_id(cls, v: str) -> str:
        if not v or not v.strip():
            raise ValueError("Пустой sensor_id")
        return v.strip()


class EngineSensorResponse(SensorResponseBase):
    coolant_temp: float = Field(..., ge=-60, le=250)
    oil_temp: float = Field(..., ge=-60, le=250)
    oil_pressure: float = Field(..., ge=0)
    max_rpm: int = Field(..., ge=0)


class ElectricalSensorResponse(SensorResponseBase):
    battery_voltage: float = Field(..., ge=0)
    battery_charge: float = Field(..., ge=0, le=100)
    current_mileage: int = Field(..., ge=0)


class ChassisSensorResponse(SensorResponseBase):
    fuel_level: float = Field(..., ge=0)
    tire_pressure: float = Field(..., ge=0)
    brake_wear: float = Field(..., ge=0, le=100)


# Какая схема проверяет ответ какого датчика
SENSOR_SCHEMAS = {
    SensorType.ENGINE: EngineSensorResponse,
    SensorType.ELECTRICAL: ElectricalSensorResponse,
    SensorType.CHASSIS: ChassisSensorResponse,
}


class SensorReadingRecord(BaseModel):
    id: int
    vin: str
    trip_id: str
    sensor_id: str
    sensor_type: SensorType
    payload: Dict[str, Any]
    recorded_at: datetime


class VehicleStateInfo(BaseModel):
    vin: str
    car_number: str
    status: VehicleStatus
    coolant_temp: Optional[float] = None
    oil_temp: Optional[float] = None
    oil_pressure: Optional[float] = None
    max_rpm: Optional[int] = None
    battery_voltage: Optional[float] = None
    battery_charge: Optional[float] = None
    current_mileage: Optional[int] = None
    fuel_level: Optional[float] = None
    tire_pressure: Optional[float] = None
    brake_wear: Optional[float] = None
    updated_at: Optional[datetime] = None


class StatusHistoryItem(BaseModel):
    status: VehicleStatus
    comment: Optional[str] = None
    changed_at: datetime


class TripProcessingResult(BaseModel):
    vin: str
    trip_id: str
    status: VehicleStatus
    status_changed: bool
    comment: str
    readings_saved: int
    message: str