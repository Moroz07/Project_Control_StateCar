import asyncio
import random
import time
from typing import Literal

from fastapi import APIRouter

from app.config import SensorConfig
from app.models import SensorType

router = APIRouter(prefix="/sensors", tags=["Эмулятор бортовых датчиков"])

SensorMode = Literal["normal", "warning", "critical", "timeout", "broken"]
_modes = {t: "normal" for t in SensorType}


def _mileage() -> int:
    # Пробег растёт со временем и никогда не уменьшается (даже после перезапуска)
    return 40000 + int((time.time() - 1_700_000_000) // 60)


@router.put("/mode", summary="Переключить режим датчика (для проверки сценариев)")
def set_sensor_mode(sensor_type: SensorType, mode: SensorMode):
    _modes[sensor_type] = mode
    return {"modes": {t.value: m for t, m in _modes.items()}}


@router.get("/{sensor_type}", summary="Ответ бортового датчика")
async def read_sensor(sensor_type: SensorType, vin: str):
    mode = _modes[sensor_type]
    sensor_id = f"{sensor_type.value.upper()}-{vin[-4:]}"

    if mode == "timeout":
        await asyncio.sleep(SensorConfig.timeout_seconds + 2)  # сервис не дождётся
    if mode == "broken":
        return {"sensor_id": "", "coolant_temp": "###", "current_mileage": -100}

    if sensor_type == SensorType.ENGINE:
        data = {"sensor_id": sensor_id,
                "coolant_temp": round(random.uniform(85, 95), 1),
                "oil_temp": round(random.uniform(90, 110), 1),
                "oil_pressure": round(random.uniform(2.0, 3.5), 2),
                "max_rpm": random.randint(2500, 4500)}
        if mode == "critical":
            data.update(coolant_temp=118.0, oil_pressure=0.5)
    elif sensor_type == SensorType.ELECTRICAL:
        data = {"sensor_id": sensor_id,
                "battery_voltage": round(random.uniform(13.5, 14.4), 2),
                "battery_charge": random.randint(70, 100),
                "current_mileage": _mileage()}
        if mode in ("warning", "critical"):
            data.update(battery_voltage=11.2, battery_charge=20)
    else:
        data = {"sensor_id": sensor_id,
                "fuel_level": round(random.uniform(15, 45), 1),
                "tire_pressure": round(random.uniform(2.1, 2.5), 2),
                "brake_wear": random.randint(10, 50)}
        if mode == "warning":
            data.update(fuel_level=3.0, brake_wear=85)
        if mode == "critical":
            data.update(tire_pressure=1.2)
    return data