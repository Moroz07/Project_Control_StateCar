import uvicorn
from fastapi import FastAPI

from app.api import (
    telemetry_router,
    vehicles_router,
    vehicle_management_router,
    sensor_emulator_router,
    register_exception_handlers,
)

app = FastAPI(
    title="Диагностика и ремонт транспорта",
    description="Автоматический опрос датчиков после поездки, учёт состояния и статусов автомобилей",
    version="2.0.0",
)

register_exception_handlers(app)
app.include_router(vehicle_management_router)
app.include_router(telemetry_router)
app.include_router(vehicles_router)
app.include_router(sensor_emulator_router)


if __name__ == "__main__":
    uvicorn.run("main:app", host="127.0.0.1", port=8000, reload=True)