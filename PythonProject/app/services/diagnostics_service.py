from datetime import datetime
from typing import Any, Dict, List, Optional, Tuple

from pydantic import BaseModel, ValidationError

from app.config import SensorNorms
from app.models import (
    EngineSensorPayload,
    ElectricalSensorPayload,
    DtcSensorPayload,
    VehicleNormAndState,
    RepairRequest,
    RepairStatus,
    VehicleStatus,
    SensorType,
    SENSOR_SCHEMAS,
    TripCompletedPayload,
    TripProcessingResult,
    VehicleStateInfo,
    StatusHistoryItem,
    SensorReadingRecord,
)
from app.repositories import IDiagnosticsRepository
from app.services.exceptions import (
    VehicleNotFoundError,
    TripAlreadyProcessedError,
    SensorValidationError,
    SensorTimeoutError,
)
from app.services.sensor_client import ISensorClient

NOT_FOUND_MESSAGE = "Автомобиль с указанным VIN не зарегистрирован в системе"


class DiagnosticsService:
    _SENSOR_NAMES = {
        SensorType.ENGINE: "двигателя",
        SensorType.ELECTRICAL: "бортовой сети",
        SensorType.CHASSIS: "ходовой части",
    }

    def __init__(self, repository: IDiagnosticsRepository,
                 sensor_client: Optional[ISensorClient] = None):
        self._repository = repository
        self._sensor_client = sensor_client

    # ==================== прежняя логика (приём телеметрии) ====================

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

    def process_engine_data(self, payload: EngineSensorPayload) -> Optional[RepairRequest]:
        norm = self._get_norm_or_raise(payload.vin)
        self._repository.update_engine_state(payload.vin, payload)

        anomalies = []
        if norm.max_coolant_temp is not None and payload.coolant_temp > norm.max_coolant_temp:
            anomalies.append(f"Критический перегрев охлаждающей жидкости: {payload.coolant_temp} °C "
                             f"(норма до {norm.max_coolant_temp} °C)")
        if norm.max_oil_temp is not None and payload.oil_temp > norm.max_oil_temp:
            anomalies.append(f"Превышение температуры моторного масла: {payload.oil_temp} °C "
                             f"(норма до {norm.max_oil_temp} °C)")
        if payload.engine_rpm > 600 and payload.oil_pressure < norm.min_oil_pressure:
            anomalies.append(f"Аварийное падение давления масла: {payload.oil_pressure} бар при "
                             f"{payload.engine_rpm} об/мин (норма от {norm.min_oil_pressure} бар)")

        if not anomalies:
            return None
        return self._create_request(norm, f"[Датчик {payload.sensor_id}] " + "; ".join(anomalies))

    def process_electrical_data(self, payload: ElectricalSensorPayload) -> Optional[RepairRequest]:
        norm = self._get_norm_or_raise(payload.vin)
        self._repository.update_electrical_state(payload.vin, payload)

        anomalies = []
        if payload.battery_voltage < norm.min_voltage:
            anomalies.append(f"Критически низкое напряжение бортовой сети (недозаряд): "
                             f"{payload.battery_voltage} В (норма от {norm.min_voltage} В)")
        elif payload.battery_voltage > norm.max_voltage:
            anomalies.append(f"Превышение напряжения бортовой сети (перезаряд): "
                             f"{payload.battery_voltage} В (норма до {norm.max_voltage} В)")

        if not anomalies:
            return None
        return self._create_request(norm, f"[Датчик {payload.sensor_id}] " + "; ".join(anomalies))

    def process_dtc_data(self, payload: DtcSensorPayload) -> Optional[RepairRequest]:
        norm = self._get_norm_or_raise(payload.vin)
        cleaned_errors = [code.strip().upper() for code in payload.dtc_errors if code.strip()]
        if not cleaned_errors:
            return None
        return self._create_request(
            norm,
            f"[Модуль {payload.sensor_id}] Обнаружены диагностические коды ошибок DTC: "
            f"{', '.join(cleaned_errors)}",
        )

    def get_vehicle_state(self, vin: str) -> VehicleNormAndState:
        state = self._repository.get_vehicle_norms(vin.strip().upper())
        if not state:
            raise VehicleNotFoundError(NOT_FOUND_MESSAGE)
        return state

    def get_vehicle_requests(self, vin: str) -> List[RepairRequest]:
        clean_vin = vin.strip().upper()
        if not self._repository.get_vehicle_norms(clean_vin):
            raise VehicleNotFoundError(NOT_FOUND_MESSAGE)
        return self._repository.get_requests_by_vin(clean_vin)

    # ==================== опрос датчиков после поездки ====================

    def process_trip_completion(self, payload: TripCompletedPayload) -> TripProcessingResult:
        """Полный цикл: проверка -> опрос 3 датчиков -> пересчёт статуса."""
        if self._repository.get_vehicle_state(payload.vin) is None:
            raise VehicleNotFoundError(NOT_FOUND_MESSAGE)
        if self._repository.is_trip_processed(payload.trip_id):
            raise TripAlreadyProcessedError("Опрос датчиков по данной поездке уже выполнен")
        if not self._repository.mark_trip_processed(payload.vin, payload.trip_id):
            raise TripAlreadyProcessedError("Опрос датчиков по данной поездке уже выполнен")

        # Три отдельных запроса — строго по очереди
        self.poll_engine_sensor(payload.vin, payload.trip_id)
        self.poll_electrical_sensor(payload.vin, payload.trip_id)
        self.poll_chassis_sensor(payload.vin, payload.trip_id)

        status, comment, changed = self.evaluate_vehicle_status(payload.vin)
        message = ("Опрос датчиков завершён, статус автомобиля изменён" if changed
                   else "Опрос датчиков завершён, статус автомобиля не изменён")
        return TripProcessingResult(
            vin=payload.vin,
            trip_id=payload.trip_id,
            status=status,
            status_changed=changed,
            comment=comment,
            readings_saved=3,
            message=message,
        )

    def poll_engine_sensor(self, vin: str, trip_id: str) -> BaseModel:
        return self._poll_sensor(vin, trip_id, SensorType.ENGINE)

    def poll_electrical_sensor(self, vin: str, trip_id: str) -> BaseModel:
        return self._poll_sensor(vin, trip_id, SensorType.ELECTRICAL)

    def poll_chassis_sensor(self, vin: str, trip_id: str) -> BaseModel:
        return self._poll_sensor(vin, trip_id, SensorType.CHASSIS)

    def _poll_sensor(self, vin: str, trip_id: str, sensor_type: SensorType) -> BaseModel:
        if self._sensor_client is None:
            raise RuntimeError("Клиент опроса датчиков не настроен")
        sensor_name = self._SENSOR_NAMES[sensor_type]

        # 1. Отдельный запрос к датчику
        try:
            raw: Dict[str, Any] = self._sensor_client.request_sensor(vin, sensor_type)
        except SensorTimeoutError:
            self._apply_status(vin, VehicleStatus.DIAGNOSTICS,
                               f"Датчик {sensor_name} не ответил на запрос сервиса")
            raise
        except SensorValidationError:
            raise self._sensor_error(vin, sensor_name, "ответ не в формате JSON") from None

        # 2. Проверка формата ответа
        try:
            data = SENSOR_SCHEMAS[sensor_type].model_validate(raw)
        except ValidationError as e:
            fields = ", ".join(".".join(str(x) for x in err["loc"]) or "ответ" for err in e.errors())
            raise self._sensor_error(vin, sensor_name, f"некорректные поля: {fields}") from None

        # 3. Пробег не может уменьшиться
        if sensor_type == SensorType.ELECTRICAL:
            state = self._repository.get_vehicle_state(vin)
            if state and state.current_mileage is not None and data.current_mileage < state.current_mileage:
                raise self._sensor_error(
                    vin, sensor_name,
                    f"пробег {data.current_mileage} км меньше прошлого {state.current_mileage} км")

        # 4. Сохранение в историю замеров и в текущее состояние
        values = data.model_dump()
        self._repository.save_sensor_reading(vin, trip_id, sensor_type, values)
        self._repository.update_vehicle_state(vin, sensor_type, values)
        return data

    def _sensor_error(self, vin: str, sensor_name: str, reason: str) -> SensorValidationError:
        """Ставит статус «на диагностике» и возвращает исключение 422."""
        self._apply_status(vin, VehicleStatus.DIAGNOSTICS,
                           f"Датчик {sensor_name}: некорректный ответ ({reason})")
        return SensorValidationError("Ошибка валидации данных датчика")

    def evaluate_vehicle_status(self, vin: str) -> Tuple[VehicleStatus, str, bool]:
        """Рассчитывает статус по последним показателям. Возвращает (статус, комментарий, changed)."""
        state = self._repository.get_vehicle_state(vin)
        if state is None:
            raise VehicleNotFoundError(NOT_FOUND_MESSAGE)
        n = SensorNorms
        critical, warnings = [], []

        if state.coolant_temp is not None and state.coolant_temp > n.MAX_COOLANT_TEMP:
            critical.append(f"датчик двигателя: перегрев ОЖ {state.coolant_temp} °C (норма до {n.MAX_COOLANT_TEMP})")
        if state.oil_temp is not None and state.oil_temp > n.MAX_OIL_TEMP:
            critical.append(f"датчик двигателя: перегрев масла {state.oil_temp} °C (норма до {n.MAX_OIL_TEMP})")
        if state.oil_pressure is not None and state.oil_pressure < n.MIN_OIL_PRESSURE:
            critical.append(f"датчик двигателя: давление масла {state.oil_pressure} бар (норма от {n.MIN_OIL_PRESSURE})")
        if state.tire_pressure is not None and state.tire_pressure < n.MIN_TIRE_PRESSURE:
            critical.append(f"датчик ходовой части: давление в шинах {state.tire_pressure} бар (норма от {n.MIN_TIRE_PRESSURE})")

        if state.fuel_level is not None and state.fuel_level < n.MIN_FUEL_LEVEL:
            warnings.append(f"датчик ходовой части: мало топлива {state.fuel_level} л")
        if state.battery_charge is not None and state.battery_charge < n.MIN_BATTERY_CHARGE:
            warnings.append(f"датчик бортовой сети: низкий заряд АКБ {state.battery_charge} %")
        if state.battery_voltage is not None and not (n.MIN_VOLTAGE <= state.battery_voltage <= n.MAX_VOLTAGE):
            warnings.append(f"датчик бортовой сети: напряжение {state.battery_voltage} В "
                            f"(норма {n.MIN_VOLTAGE}–{n.MAX_VOLTAGE})")
        if state.brake_wear is not None and state.brake_wear > n.MAX_BRAKE_WEAR:
            warnings.append(f"датчик ходовой части: износ колодок {state.brake_wear} %")

        if critical:
            status, comment = VehicleStatus.IN_REPAIR, "Критические отклонения: " + "; ".join(critical)
        elif warnings:
            status, comment = VehicleStatus.NEEDS_SERVICE, "Требуется обслуживание: " + "; ".join(warnings)
        else:
            status, comment = VehicleStatus.READY, "Все датчики ответили, показатели в норме"

        changed = self._apply_status(vin, status, comment)
        return status, comment, changed

    def _apply_status(self, vin: str, status: VehicleStatus, comment: str) -> bool:
        """Меняет статус, только если он отличается от текущего. Возвращает changed."""
        state = self._repository.get_vehicle_state(vin)
        current = state.status if state else None
        if current == status:
            return False
        # «исправен» и «готов к эксплуатации» считаем одинаково исправным состоянием
        if status == VehicleStatus.READY and current == VehicleStatus.OK:
            return False
        self._repository.set_vehicle_status(vin, status, comment)
        return True

    # ==================== чтение данных ====================

    def get_vehicle_summary(self, vin: str) -> VehicleStateInfo:
        state = self._repository.get_vehicle_state(vin.strip().upper())
        if state is None:
            raise VehicleNotFoundError(NOT_FOUND_MESSAGE)
        return state

    def get_status_history(self, vin: str) -> List[StatusHistoryItem]:
        clean_vin = vin.strip().upper()
        if self._repository.get_vehicle_state(clean_vin) is None:
            raise VehicleNotFoundError(NOT_FOUND_MESSAGE)
        return self._repository.get_status_history(clean_vin)

    def get_sensor_readings(self, vin: str, sensor_type: SensorType) -> List[SensorReadingRecord]:
        clean_vin = vin.strip().upper()
        if self._repository.get_vehicle_state(clean_vin) is None:
            raise VehicleNotFoundError(NOT_FOUND_MESSAGE)
        return self._repository.get_sensor_readings(clean_vin, sensor_type)