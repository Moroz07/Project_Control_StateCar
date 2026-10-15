from abc import ABC, abstractmethod
from typing import List, Optional

from app.models import (
    VehicleNormAndState,
    EngineSensorPayload,
    ElectricalSensorPayload,
    RepairRequest,
    RepairStatus,
)


class IDiagnosticsRepository(ABC):
    @abstractmethod
    def get_vehicle_norms(self, vin: str) -> Optional[VehicleNormAndState]:
        pass

    @abstractmethod
    def update_engine_state(self, vin: str, payload: EngineSensorPayload) -> bool:
        pass

    @abstractmethod
    def update_electrical_state(self, vin: str, payload: ElectricalSensorPayload) -> bool:
        pass

    @abstractmethod
    def create_repair_request(self, request: RepairRequest) -> int:
        pass

    @abstractmethod
    def get_requests_by_vin(self, vin: str) -> List[RepairRequest]:
        pass

    @abstractmethod
    def update_request_status(self, request_id: int, new_status: RepairStatus) -> bool:
        pass