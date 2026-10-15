from app.models.enums import RepairStatus
from app.models.sensors import (
    BaseSensorPayload,
    EngineSensorPayload,
    ElectricalSensorPayload,
    DtcSensorPayload,
)
from app.models.vehicle import VehicleNormAndState
from app.models.repair import RepairRequest, StatusUpdatePayload