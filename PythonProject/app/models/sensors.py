import re
from typing import List

from pydantic import BaseModel, ConfigDict, Field, field_validator


class BaseSensorPayload(BaseModel):
    sensor_id: str = Field(..., description="Уникальный идентификатор датчика")
    vin: str = Field(
        ...,
        min_length=17,
        max_length=17,
        pattern=r"^[A-HJ-NPR-Z0-9]{17}$",
        description="VIN-номер автомобиля (17 символов, без I, O и Q)",
    )

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
        if not re.fullmatch(r"[A-HJ-NPR-Z0-9]{17}", clean_vin):
            raise ValueError(
                "VIN должен состоять из 17 символов A–Z и 0–9, без букв I, O и Q"
            )
        return clean_vin


class EngineSensorPayload(BaseSensorPayload):
    model_config = ConfigDict(json_schema_extra={"examples": [{
        "sensor_id": "engine-01",
        "vin": "XTA12345678901231",
        "engine_rpm": 850,
        "coolant_temp": 92.0,
        "oil_temp": 105.0,
        "oil_pressure": 2.4,
    }]})

    engine_rpm: int = Field(..., ge=0, description="Обороты двигателя (об/мин)")
    coolant_temp: float = Field(..., description="Температура ОЖ (°C)")
    oil_temp: float = Field(..., description="Температура масла (°C)")
    oil_pressure: float = Field(..., ge=0.0, description="Давление масла (бар)")


class ElectricalSensorPayload(BaseSensorPayload):
    model_config = ConfigDict(json_schema_extra={"examples": [{
        "sensor_id": "electrical-01",
        "vin": "XTA12345678901231",
        "battery_voltage": 13.8,
        "current_mileage": 250,
    }]})

    battery_voltage: float = Field(..., gt=0.0, description="Напряжение сети (В)")
    current_mileage: int = Field(..., ge=0, description="Общий пробег (км)")


class DtcSensorPayload(BaseSensorPayload):
    model_config = ConfigDict(json_schema_extra={"examples": [{
        "sensor_id": "obd-01",
        "vin": "XTA12345678901231",
        "dtc_errors": ["P0420", "U0100"],
    }]})

    dtc_errors: List[str] = Field(default_factory=list, description="Коды ошибок OBD")
