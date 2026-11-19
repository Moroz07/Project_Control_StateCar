from typing import List

from app.models import (
    DtcSensorPayload,
    ElectricalSensorPayload,
    EngineSensorPayload,
    EngineTelemetryResponse,
    ElectricalTelemetryResponse,
    ObdTelemetryResponse,
    SensorType,
    TelemetryHistoryResponse,
    VehicleNormAndState,
)
from app.repositories import ITelemetryRepository
from app.services.exceptions import VehicleNotFoundError


class TelemetryService:
    """Принимает телеметрию и проверяет её по нормам автомобиля."""

    def __init__(self, repository: ITelemetryRepository):
        self._repository = repository

    def _get_norm_or_raise(self, vin: str) -> VehicleNormAndState:
        norm = self._repository.get_vehicle_norms(vin)
        if norm is None:
            raise VehicleNotFoundError(
                f"Автомобиль с VIN {vin} не зарегистрирован в системе"
            )
        return norm

    def process_engine_data(self, payload: EngineSensorPayload) -> List[str]:
        norm = self._get_norm_or_raise(payload.vin)
        if not self._repository.update_engine_state(payload.vin, payload):
            raise VehicleNotFoundError(
                f"Автомобиль с VIN {payload.vin} не зарегистрирован в системе"
            )

        anomalies = []
        if norm.max_coolant_temp is not None and payload.coolant_temp > norm.max_coolant_temp:
            anomalies.append(
                f"Критический перегрев охлаждающей жидкости: {payload.coolant_temp} °C "
                f"(норма до {norm.max_coolant_temp} °C)"
            )
        if norm.max_oil_temp is not None and payload.oil_temp > norm.max_oil_temp:
            anomalies.append(
                f"Превышение температуры моторного масла: {payload.oil_temp} °C "
                f"(норма до {norm.max_oil_temp} °C)"
            )
        if payload.engine_rpm > 600 and payload.oil_pressure < norm.min_oil_pressure:
            anomalies.append(
                f"Аварийное падение давления масла: {payload.oil_pressure} бар "
                f"при {payload.engine_rpm} об/мин "
                f"(норма от {norm.min_oil_pressure} бар)"
            )
        return anomalies

    def process_electrical_data(self, payload: ElectricalSensorPayload) -> List[str]:
        norm = self._get_norm_or_raise(payload.vin)
        if not self._repository.update_electrical_state(payload.vin, payload):
            raise VehicleNotFoundError(
                f"Автомобиль с VIN {payload.vin} не зарегистрирован в системе"
            )

        if payload.battery_voltage < norm.min_voltage:
            return [
                f"Критически низкое напряжение бортовой сети (недозаряд): "
                f"{payload.battery_voltage} В (норма от {norm.min_voltage} В)"
            ]
        if payload.battery_voltage > norm.max_voltage:
            return [
                f"Превышение напряжения бортовой сети (перезаряд): "
                f"{payload.battery_voltage} В (норма до {norm.max_voltage} В)"
            ]
        return []

    def process_dtc_data(self, payload: DtcSensorPayload) -> List[str]:
        self._get_norm_or_raise(payload.vin)
        errors = [code.strip().upper() for code in payload.dtc_errors if code.strip()]
        self._repository.save_dtc_errors(payload.vin, payload.sensor_id, errors)
        return errors

    def get_engine_telemetry(self, vin: str) -> EngineTelemetryResponse:
        clean_vin = vin.strip().upper()
        state = self._get_norm_or_raise(clean_vin)
        return EngineTelemetryResponse(
            vin=state.vin,
            car_number=state.car_number,
            updated_at=state.updated_at,
            engine={
                "engine_rpm": state.engine_rpm,
                "coolant_temp": state.coolant_temp,
                "oil_temp": state.oil_temp,
                "oil_pressure": state.oil_pressure,
            },
        )

    def get_electrical_telemetry(self, vin: str) -> ElectricalTelemetryResponse:
        clean_vin = vin.strip().upper()
        state = self._get_norm_or_raise(clean_vin)
        return ElectricalTelemetryResponse(
            vin=state.vin,
            car_number=state.car_number,
            updated_at=state.updated_at,
            electrical={
                "battery_voltage": state.battery_voltage,
                "current_mileage": state.current_mileage,
            },
        )

    def get_obd_telemetry(self, vin: str) -> ObdTelemetryResponse:
        clean_vin = vin.strip().upper()
        state = self._get_norm_or_raise(clean_vin)
        dtc_events = self._repository.get_dtc_events(clean_vin)
        return ObdTelemetryResponse(
            vin=state.vin,
            car_number=state.car_number,
            dtc_events=dtc_events,
        )

    def get_telemetry_history(
        self, vin: str, sensor_type: SensorType
    ) -> TelemetryHistoryResponse:
        clean_vin = vin.strip().upper()
        state = self._get_norm_or_raise(clean_vin)
        readings = self._repository.get_telemetry_readings(clean_vin, sensor_type)
        return TelemetryHistoryResponse(
            vin=state.vin,
            sensor_type=sensor_type,
            readings=readings,
        )
