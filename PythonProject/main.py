import uvicorn
from fastapi import FastAPI

from app.api import telemetry_router, vehicles_router, register_exception_handlers

app = FastAPI(
    title="Диагностика & ремонт транспорта REST Service",
    description="Сервис приёма телеметрии от датчиков, актуализации БД и "
                "автоматического создания заявок на ремонт",
    version="1.0.0",
)

register_exception_handlers(app)
app.include_router(telemetry_router)
app.include_router(vehicles_router)


if __name__ == "__main__":
    uvicorn.run("main:app", host="127.0.0.1", port=8000, reload=True)
