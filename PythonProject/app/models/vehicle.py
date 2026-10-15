from datetime import datetime
from typing import Optional
from pydantic import BaseModel


class VehicleNormAndState(BaseModel):
    vin: str
    car_number: str
    current_mileage: int
    engine_rpm: Optional[int] = None
    coolant_temp: Optional[float] = None
    oil_temp: Optional[float] = None
    oil_pressure: Optional[float] = None
    battery_voltage: Optional[float] = None
    updated_at: datetime
    max_coolant_temp: float
    max_oil_temp: float
    min_oil_pressure: float
    min_voltage: float
    max_voltage: float