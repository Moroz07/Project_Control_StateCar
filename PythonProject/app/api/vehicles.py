from typing import List

from fastapi import APIRouter, Depends, HTTPException, status

from app.api.dependencies import get_diagnostics_service
from app.models import VehicleNormAndState, RepairRequest
from app.services import DiagnosticsService, VehicleNotFoundError

router = APIRouter(prefix="/api", tags=["Автомобили и заявки"])


@router.get("/vehicles/{vin}", response_model=VehicleNormAndState,
            summary="Получить актуальное состояние автомобиля")
def get_vehicle_status(
    vin: str,
    service: DiagnosticsService = Depends(get_diagnostics_service),
):
    try:
        return service.get_vehicle_state(vin)
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))


@router.get("/requests/{vin}", response_model=List[RepairRequest],
            summary="Получить список заявок по автомобилю")
def get_requests_for_vehicle(
    vin: str,
    service: DiagnosticsService = Depends(get_diagnostics_service),
):
    try:
        return service.get_vehicle_requests(vin)
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))