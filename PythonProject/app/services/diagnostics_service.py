from datetime import datetime
from typing import List, Optional

from app.models import (
    EngineSensorPayload,
    ElectricalSensorPayload,
    DtcSensorPayload,
    VehicleNormAndState,
    RepairRequest,
    RepairStatus,
)
from app.repositories import IDiagnosticsRepository
from app.services.exceptions import VehicleNotFoundError


class DiagnosticsService:
    def __init__(self, repository: IDiagnosticsRepository):
        self._repository = repository

    # ---------- вспомогательные методы ----------

    def _get_norm_or_raise(self, vin: str) -> VehicleNormAndState:
        norm = self._repository.get_vehicle_norms(vin)
        if not norm:
            raise VehicleNotFoundError(f"Автомобиль с VIN {vin} не зарегистрирован в системе")
        return norm

    def _create_request(self, norm: VehicleNormAndState, description: str) -> RepairRequest:
        request = RepairRequest(
            vin=norm.vin,
            car_number=norm.car_number,
            description=description,
            status=RepairStatus.NEW,
            created_at=datetime.utcnow(),
        )
        request.id = self._repository.create_repair_request(request)
        return request

    # ---------- обработка телеметрии ----------

    def process_engine_data(self, payload: EngineSensorPayload) -> Optional[RepairRequest]:
        norm = self._get_norm_or_raise(payload.vin)

        # 1. Актуализируем состояние автомобиля в БД
        self._repository.update_engine_state(payload.vin, payload)

        # 2. Проверяем критерии тревоги по нормативам
        anomalies = []

        if payload.coolant_temp > norm.max_coolant_temp:
            anomalies.append(
                f"Критический перегрев охлаждающей жидкости: {payload.coolant_temp} °C "
                f"(норма до {norm.max_coolant_temp} °C)"
            )

        if payload.oil_temp > norm.max_oil_temp:
            anomalies.append(
                f"Превышение температуры моторного масла: {payload.oil_temp} °C "
                f"(норма до {norm.max_oil_temp} °C)"
            )

        # Давление масла проверяется только при заведённом двигателе (> 600 об/мин)
        if payload.engine_rpm > 600 and payload.oil_pressure < norm.min_oil_pressure:
            anomalies.append(
                f"Аварийное падение давления масла: {payload.oil_pressure} бар при "
                f"{payload.engine_rpm} об/мин (норма от {norm.min_oil_pressure} бар)"
            )

        if not anomalies:
            return None

        # 3. Формируем заявку
        return self._create_request(
            norm, f"[Датчик {payload.sensor_id}] " + "; ".join(anomalies)
        )

    def process_electrical_data(self, payload: ElectricalSensorPayload) -> Optional[RepairRequest]:
        norm = self._get_norm_or_raise(payload.vin)

        # 1. Актуализируем пробег и напряжение
        self._repository.update_electrical_state(payload.vin, payload)

        # 2. Проверяем границы напряжения сети
        anomalies = []
        if payload.battery_voltage < norm.min_voltage:
            anomalies.append(
                f"Критически низкое напряжение бортовой сети (недозаряд): "
                f"{payload.battery_voltage} В (норма от {norm.min_voltage} В)"
            )
        elif payload.battery_voltage > norm.max_voltage:
            anomalies.append(
                f"Превышение напряжения бортовой сети (перезаряд): "
                f"{payload.battery_voltage} В (норма до {norm.max_voltage} В)"
            )

        if not anomalies:
            return None

        return self._create_request(
            norm, f"[Датчик {payload.sensor_id}] " + "; ".join(anomalies)
        )

    def process_dtc_data(self, payload: DtcSensorPayload) -> Optional[RepairRequest]:
        norm = self._get_norm_or_raise(payload.vin)

        cleaned_errors = [code.strip().upper() for code in payload.dtc_errors if code.strip()]
        if not cleaned_errors:
            return None

        return self._create_request(
            norm,
            f"[Модуль {payload.sensor_id}] Обнаружены диагностические коды ошибок DTC: "
            f"{', '.join(cleaned_errors)}"
        )

    # ---------- чтение ----------

    def get_vehicle_state(self, vin: str) -> VehicleNormAndState:
        state = self._repository.get_vehicle_norms(vin.strip().upper())
        if not state:
            raise VehicleNotFoundError("Автомобиль с указанным VIN не зарегистрирован в системе")
        return state

    def get_vehicle_requests(self, vin: str) -> List[RepairRequest]:
        clean_vin = vin.strip().upper()
        if not self._repository.get_vehicle_norms(clean_vin):
            raise VehicleNotFoundError("Автомобиль с указанным VIN не зарегистрирован в системе")
        return self._repository.get_requests_by_vin(clean_vin)