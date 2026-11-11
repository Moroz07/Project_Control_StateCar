from typing import List

from fastapi import APIRouter, Depends, HTTPException, status
from fastapi.responses import JSONResponse

from app.api.dependencies import get_diagnostics_service
from app.models import (
    TripCompletedPayload,
    TripProcessingResult,
    VehicleStateInfo,
    StatusHistoryItem,
    SensorReadingRecord,
    SensorType,
)
from app.services import (
    DiagnosticsService,
    VehicleNotFoundError,
    TripAlreadyProcessedError,
    SensorValidationError,
    SensorTimeoutError,
)

router = APIRouter(tags=["Опрос датчиков после поездки"])


@router.post("/api/trips/complete", response_model=TripProcessingResult,
             summary="Уведомление о завершении поездки: опрос датчиков и пересчёт статуса")
def complete_trip(payload: TripCompletedPayload,
                  service: DiagnosticsService = Depends(get_diagnostics_service)):
    try:
        result = service.process_trip_completion(payload)
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))
    except TripAlreadyProcessedError as e:
        raise HTTPException(status_code=status.HTTP_409_CONFLICT, detail=str(e))
    except SensorValidationError:
        raise HTTPException(status_code=status.HTTP_422_UNPROCESSABLE_ENTITY,
                            detail="Ошибка валидации данных датчика")
    except SensorTimeoutError:
        raise HTTPException(status_code=status.HTTP_504_GATEWAY_TIMEOUT,
                            detail="Датчик не ответил на запрос сервиса")

    code = status.HTTP_201_CREATED if result.status_changed else status.HTTP_200_OK
    return JSONResponse(status_code=code, content=result.model_dump(mode="json"))


@router.get("/api/vehicles/{vin}", response_model=VehicleStateInfo,
            summary="Сводка показателей и текущий статус автомобиля")
def get_vehicle_summary(vin: str, service: DiagnosticsService = Depends(get_diagnostics_service)):
    try:
        return service.get_vehicle_summary(vin)
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))


@router.get("/api/vehicles/{vin}/readings", response_model=List[SensorReadingRecord],
            summary="История замеров по типу датчика")
def get_readings(vin: str, sensor_type: SensorType,
                 service: DiagnosticsService = Depends(get_diagnostics_service)):
    try:
        return service.get_sensor_readings(vin, sensor_type)
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))


@router.get("/api/vehicles/{vin}/status-history", response_model=List[StatusHistoryItem],
            summary="История смены статусов (новые первыми)")
def get_status_history(vin: str, service: DiagnosticsService = Depends(get_diagnostics_service)):
    try:
        return service.get_status_history(vin)
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))