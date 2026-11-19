import uvicorn
from fastapi import FastAPI

from app.api.telemetry import router as telemetry_router

app = FastAPI(
    title="Телеметрия автомобиля",
    description="Приём показаний датчиков двигателя, бортовой сети и кодов OBD",
    version="1.0.0",
)

app.include_router(telemetry_router)


if __name__ == "__main__":
    uvicorn.run("main:app", host="127.0.0.1", port=8000, reload=True)
