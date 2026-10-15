from fastapi import APIRouter, Depends, HTTPException, status
from fastapi.responses import JSONResponse

from app.api.dependencies import get_diagnostics_service
from app.models import EngineSensorPayload, ElectricalSensorPayload, DtcSensorPayload
from app.services import DiagnosticsService, VehicleNotFoundError

router = APIRouter(prefix="/api/telemetry", tags=["Телеметрия"])


@router.post("/engine", summary="Приём данных от датчика двигателя")
def receive_engine_telemetry(
    payload: EngineSensorPayload,
    service: DiagnosticsService = Depends(get_diagnostics_service),
):
    try:
        created_request = service.process_engine_data(payload)
        if created_request:
            return JSONResponse(
                status_code=status.HTTP_201_CREATED,
                content=created_request.model_dump(mode="json"),
            )
        return {"message": "Данные актуализированы, показатели в пределах нормы"}
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))


@router.post("/electrical", summary="Приём данных от датчика бортовой сети и одометра")
def receive_electrical_telemetry(
    payload: ElectricalSensorPayload,
    service: DiagnosticsService = Depends(get_diagnostics_service),
):
    try:
        created_request = service.process_electrical_data(payload)
        if created_request:
            return JSONResponse(
                status_code=status.HTTP_201_CREATED,
                content=created_request.model_dump(mode="json"),
            )
        return {"message": "Данные актуализированы, показатели в пределах нормы"}
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))


@router.post("/dtc", summary="Приём кодов ошибок от диагностического модуля OBD")
def receive_dtc_telemetry(
    payload: DtcSensorPayload,
    service: DiagnosticsService = Depends(get_diagnostics_service),
):
    try:
        created_request = service.process_dtc_data(payload)
        if created_request:
            return JSONResponse(
                status_code=status.HTTP_201_CREATED,
                content=created_request.model_dump(mode="json"),
            )
        return {"message": "Ошибок не обнаружено, заявка не требуется"}
    except VehicleNotFoundError as e:
        raise HTTPException(status_code=status.HTTP_404_NOT_FOUND, detail=str(e))