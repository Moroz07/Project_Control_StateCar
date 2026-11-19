from fastapi import APIRouter, Depends, HTTPException, Path, Query, status
from fastapi.responses import JSONResponse

from app.api.dependencies import get_telemetry_service
from app.errors import TelemetryStorageError
from app.models import (
    EngineSensorPayload,
    ElectricalSensorPayload,
    DtcSensorPayload,
    EngineTelemetryResponse,
    ElectricalTelemetryResponse,
    ObdTelemetryResponse,
    SensorType,
    TelemetryHistoryResponse,
)
from app.services import TelemetryService, VehicleNotFoundError

router = APIRouter(prefix="/api/telemetry", tags=["Телеметрия"])


@router.post(
    "/engine",
    summary="Приём данных от датчика двигателя",
    responses={
        200: {
            "description": "Показания сохранены, отклонений не обнаружено",
            "content": {"application/json": {"example": {
                "message": "Телеметрия двигателя сохранена, показатели в пределах нормы"
            }}},
        },
        201: {
            "description": "Телеметрия сохранена, обнаружены отклонения",
            "content": {"application/json": {"example": {
                "vin": "XTA12345678901231",
                "sensor_id": "engine-01",
                "anomalies": [
                    "Критический перегрев охлаждающей жидкости: 120.0 °C (норма до 105.0 °C)"
                ],
            }}},
        },
        404: {"description": "Автомобиль с указанным VIN не зарегистрирован"},
        503: {"description": "База данных временно недоступна"},
        422: {"description": "Некорректный формат телеметрии"},
    },
)
def receive_engine_telemetry(
    payload: EngineSensorPayload,
    service: TelemetryService = Depends(get_telemetry_service),
):
    try:
        anomalies = service.process_engine_data(payload)
        if anomalies:
            return JSONResponse(
                status_code=status.HTTP_201_CREATED,
                content={"vin": payload.vin, "sensor_id": payload.sensor_id,
                         "anomalies": anomalies},
            )
        return {"message": "Телеметрия двигателя сохранена, показатели в пределах нормы"}
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))
    except TelemetryStorageError as e:
        raise HTTPException(status_code=status.HTTP_503_SERVICE_UNAVAILABLE, detail=str(e))


@router.post(
    "/electrical",
    summary="Приём данных от датчика бортовой сети и одометра",
    responses={
        200: {
            "description": "Показания сохранены, отклонений не обнаружено",
            "content": {"application/json": {"example": {
                "message": "Телеметрия бортовой сети сохранена, показатели в пределах нормы"
            }}},
        },
        201: {
            "description": "Телеметрия сохранена, обнаружены отклонения",
            "content": {"application/json": {"example": {
                "vin": "XTA12345678901231",
                "sensor_id": "electrical-01",
                "anomalies": [
                    "Критически низкое напряжение бортовой сети (недозаряд): "
                    "10.5 В (норма от 11.8 В)"
                ],
            }}},
        },
        404: {"description": "Автомобиль с указанным VIN не зарегистрирован"},
        503: {"description": "База данных временно недоступна"},
        422: {"description": "Некорректный формат телеметрии"},
    },
)
def receive_electrical_telemetry(
    payload: ElectricalSensorPayload,
    service: TelemetryService = Depends(get_telemetry_service),
):
    try:
        anomalies = service.process_electrical_data(payload)
        if anomalies:
            return JSONResponse(
                status_code=status.HTTP_201_CREATED,
                content={"vin": payload.vin, "sensor_id": payload.sensor_id,
                         "anomalies": anomalies},
            )
        return {"message": "Телеметрия бортовой сети сохранена, показатели в пределах нормы"}
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))
    except TelemetryStorageError as e:
        raise HTTPException(status_code=status.HTTP_503_SERVICE_UNAVAILABLE, detail=str(e))


@router.post(
    "/obd",
    summary="Приём кодов ошибок от диагностического модуля OBD",
    responses={
        200: {
            "description": "Коды ошибок отсутствуют",
            "content": {"application/json": {"example": {
                "message": "Телеметрия OBD принята, ошибок не обнаружено"
            }}},
        },
        201: {
            "description": "Коды ошибок OBD приняты и сохранены",
            "content": {"application/json": {"example": {
                "vin": "XTA12345678901231",
                "sensor_id": "obd-01",
                "dtc_errors": ["P0420", "U0100"],
            }}},
        },
        404: {"description": "Автомобиль с указанным VIN не зарегистрирован"},
        503: {"description": "База данных временно недоступна"},
        422: {"description": "Некорректный формат телеметрии"},
    },
)
def receive_dtc_telemetry(
    payload: DtcSensorPayload,
    service: TelemetryService = Depends(get_telemetry_service),
):
    try:
        errors = service.process_dtc_data(payload)
        if errors:
            return JSONResponse(
                status_code=status.HTTP_201_CREATED,
                content={"vin": payload.vin, "sensor_id": payload.sensor_id,
                         "dtc_errors": errors},
            )
        return {"message": "Телеметрия OBD принята, ошибок не обнаружено"}
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))
    except TelemetryStorageError as e:
        raise HTTPException(status_code=status.HTTP_503_SERVICE_UNAVAILABLE, detail=str(e))


def _vin_path():
    return Path(
        ...,
        min_length=17,
        max_length=17,
        pattern=r"(?i)^[A-HJ-NPR-Z0-9]{17}$",
        description="VIN автомобиля",
    )


@router.get("/{vin}/engine", response_model=EngineTelemetryResponse,
           summary="Получить сохранённые показания двигателя по VIN")
def get_engine_telemetry(
    vin: str = _vin_path(),
    service: TelemetryService = Depends(get_telemetry_service),
):
    try:
        return service.get_engine_telemetry(vin)
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))
    except TelemetryStorageError as e:
        raise HTTPException(status_code=status.HTTP_503_SERVICE_UNAVAILABLE, detail=str(e))


@router.get("/{vin}/electrical", response_model=ElectricalTelemetryResponse,
           summary="Получить сохранённые показания электрики по VIN")
def get_electrical_telemetry(
    vin: str = _vin_path(),
    service: TelemetryService = Depends(get_telemetry_service),
):
    try:
        return service.get_electrical_telemetry(vin)
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))
    except TelemetryStorageError as e:
        raise HTTPException(status_code=status.HTTP_503_SERVICE_UNAVAILABLE, detail=str(e))


@router.get("/{vin}/obd", response_model=ObdTelemetryResponse,
           summary="Получить сохранённые коды OBD по VIN")
def get_obd_telemetry(
    vin: str = _vin_path(),
    service: TelemetryService = Depends(get_telemetry_service),
):
    try:
        return service.get_obd_telemetry(vin)
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))
    except TelemetryStorageError as e:
        raise HTTPException(status_code=status.HTTP_503_SERVICE_UNAVAILABLE, detail=str(e))


@router.get(
    "/{vin}/readings",
    response_model=TelemetryHistoryResponse,
    summary="Получить историю замеров телеметрии по VIN",
    responses={
        200: {
            "description": "История замеров выбранного типа датчика, от новых к старым",
            "content": {"application/json": {"example": {
                "vin": "XTA12345678901231",
                "sensor_type": "engine",
                "readings": [{
                    "id": 42,
                    "vin": "XTA12345678901231",
                    "sensor_type": "engine",
                    "recorded_at": "2026-11-19T12:00:00Z",
                    "data": {
                        "parameter": "coolant_temp",
                        "value": 92.0,
                        "unit": "°C",
                    },
                }],
            }}},
        },
        404: {"description": "Автомобиль с указанным VIN не зарегистрирован"},
        422: {"description": "Некорректный VIN или тип датчика"},
        503: {"description": "База данных временно недоступна"},
    },
)
def get_telemetry_history(
    vin: str = _vin_path(),
    sensor_type: SensorType = Query(
        ...,
        description="Тип датчика: engine, electrical или obd",
    ),
    service: TelemetryService = Depends(get_telemetry_service),
):
    try:
        return service.get_telemetry_history(vin, sensor_type)
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))
    except TelemetryStorageError as e:
        raise HTTPException(status_code=status.HTTP_503_SERVICE_UNAVAILABLE, detail=str(e))
