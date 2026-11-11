from app.services.exceptions import (
    VehicleNotFoundError,
    TripAlreadyProcessedError,
    SensorValidationError,
    SensorTimeoutError,
)
from app.services.sensor_client import ISensorClient, HttpSensorClient
from app.services.diagnostics_service import DiagnosticsService