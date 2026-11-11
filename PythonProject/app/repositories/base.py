from abc import ABC, abstractmethod
from typing import Any, Dict, List, Optional

from app.models import (
    VehicleNormAndState,
    EngineSensorPayload,
    ElectricalSensorPayload,
    RepairRequest,
    RepairStatus,
    SensorType,
    VehicleStatus,
    VehicleStateInfo,
    StatusHistoryItem,
    SensorReadingRecord,
)


class IDiagnosticsRepository(ABC):
    # ---------- прежние методы ----------
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

    # ---------- опрос датчиков после поездки ----------
    @abstractmethod
    def is_trip_processed(self, trip_id: str) -> bool:
        pass

    @abstractmethod
    def mark_trip_processed(self, vin: str, trip_id: str) -> bool:
        pass

    @abstractmethod
    def save_sensor_reading(self, vin: str, trip_id: str, sensor_type: SensorType,
                            data: Dict[str, Any]) -> int:
        pass

    @abstractmethod
    def update_vehicle_state(self, vin: str, sensor_type: SensorType, data: Dict[str, Any]) -> bool:
        pass

    @abstractmethod
    def get_vehicle_state(self, vin: str) -> Optional[VehicleStateInfo]:
        pass

    @abstractmethod
    def set_vehicle_status(self, vin: str, status: VehicleStatus, comment: str) -> bool:
        pass

    @abstractmethod
    def get_status_history(self, vin: str) -> List[StatusHistoryItem]:
        pass

    @abstractmethod
    def get_sensor_readings(self, vin: str, sensor_type: SensorType) -> List[SensorReadingRecord]:
        pass