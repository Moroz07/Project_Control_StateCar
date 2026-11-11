from app.repositories import DiagnosticsRepository
from app.services import DiagnosticsService, HttpSensorClient


def get_diagnostics_service() -> DiagnosticsService:
    return DiagnosticsService(DiagnosticsRepository(), HttpSensorClient())