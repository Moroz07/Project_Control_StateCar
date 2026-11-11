from abc import ABC, abstractmethod
from typing import Any, Dict

import httpx

from app.config import SensorConfig
from app.models import SensorType
from app.services.exceptions import SensorTimeoutError, SensorValidationError


class ISensorClient(ABC):
    """Клиент опроса бортовых датчиков автомобиля."""

    @abstractmethod
    def request_sensor(self, vin: str, sensor_type: SensorType) -> Dict[str, Any]:
        pass


class HttpSensorClient(ISensorClient):
    """Отправляет отдельный HTTP-запрос к каждому датчику: GET /sensors/{тип}?vin=..."""

    def __init__(self, base_url: str = SensorConfig.base_url,
                 timeout: float = SensorConfig.timeout_seconds):
        self._base_url = base_url.rstrip("/")
        self._timeout = timeout

    def request_sensor(self, vin: str, sensor_type: SensorType) -> Dict[str, Any]:
        url = f"{self._base_url}/sensors/{sensor_type.value}"
        try:
            response = httpx.get(url, params={"vin": vin}, timeout=self._timeout)
        except httpx.HTTPError:
            raise SensorTimeoutError("Датчик не ответил на запрос сервиса")

        if response.status_code != 200:
            raise SensorTimeoutError("Датчик не ответил на запрос сервиса")

        try:
            data = response.json()
        except ValueError:
            raise SensorValidationError("Ошибка валидации данных датчика")

        if not isinstance(data, dict):
            raise SensorValidationError("Ошибка валидации данных датчика")
        return data