from app.repositories import DiagnosticsRepository
from app.services import DiagnosticsService


def get_diagnostics_service() -> DiagnosticsService:
    repo = DiagnosticsRepository()
    return DiagnosticsService(repo)