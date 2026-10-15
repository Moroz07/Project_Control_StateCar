from typing import List

from pydantic import BaseModel, Field, field_validator


class BaseSensorPayload(BaseModel):
    sensor_id: str = Field(..., description="Уникальный идентификатор датчика")
    vin: str = Field(..., description="VIN-номер автомобиля (17 символов)")

    @field_validator("sensor_id")
    @classmethod
    def validate_sensor_id(cls, v: str) -> str:
        if not v or not v.strip():
            raise ValueError("Укажите корректный sensor_id")
        return v.strip()

    @field_validator("vin")
    @classmethod
    def validate_vin(cls, v: str) -> str:
        clean_vin = v.strip().upper()
        if len(clean_vin) != 17:
            raise ValueError("VIN-номер должен состоять ровно из 17 символов")
        return clean_vin


class EngineSensorPayload(BaseSensorPayload):
    engine_rpm: int = Field(..., ge=0, description="Обороты двигателя (об/мин)")
    coolant_temp: float = Field(..., description="Температура ОЖ (°C)")
    oil_temp: float = Field(..., description="Температура масла (°C)")
    oil_pressure: float = Field(..., ge=0.0, description="Давление масла (бар)")


class ElectricalSensorPayload(BaseSensorPayload):
    battery_voltage: float = Field(..., gt=0.0, description="Напряжение сети (В)")
    current_mileage: int = Field(..., ge=0, description="Общий пробег (км)")


class DtcSensorPayload(BaseSensorPayload):
    dtc_errors: List[str] = Field(default_factory=list, description="Коды ошибок OBD")