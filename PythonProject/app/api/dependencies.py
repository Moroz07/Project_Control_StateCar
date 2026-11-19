from app.repositories import TelemetryRepository
from app.services import TelemetryService


def get_telemetry_service() -> TelemetryService:
    return TelemetryService(TelemetryRepository())
