from abc import ABC, abstractmethod
from typing import List, Optional

from app.models import (
    DtcEventRecord,
    ElectricalSensorPayload,
    EngineSensorPayload,
    SensorType,
    TelemetryReadingRecord,
    VehicleNormAndState,
)


class ITelemetryRepository(ABC):
    @abstractmethod
    def get_vehicle_norms(self, vin: str) -> Optional[VehicleNormAndState]:
        raise NotImplementedError

    @abstractmethod
    def update_engine_state(self, vin: str, payload: EngineSensorPayload) -> bool:
        raise NotImplementedError

    @abstractmethod
    def update_electrical_state(self, vin: str, payload: ElectricalSensorPayload) -> bool:
        raise NotImplementedError

    @abstractmethod
    def save_dtc_errors(self, vin: str, sensor_id: str, errors: List[str]) -> None:
        raise NotImplementedError

    @abstractmethod
    def get_dtc_events(self, vin: str) -> List[DtcEventRecord]:
        raise NotImplementedError

    @abstractmethod
    def get_telemetry_readings(
        self, vin: str, sensor_type: SensorType
    ) -> List[TelemetryReadingRecord]:
        raise NotImplementedError
