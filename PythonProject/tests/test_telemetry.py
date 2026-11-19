from datetime import datetime, timezone

import pytest
from fastapi.testclient import TestClient

from app.api.dependencies import get_telemetry_service
from app.errors import TelemetryStorageError
from app.models import ElectricalSensorPayload, EngineSensorPayload, VehicleNormAndState
from app.models import DtcEventRecord
from app.repositories import ITelemetryRepository
from app.services import TelemetryService
from main import app


VIN = "1HGCM82633A004352"


class InMemoryTelemetryRepository(ITelemetryRepository):
    def __init__(self):
        self.vehicle = VehicleNormAndState(
            vin=VIN,
            car_number="A123AA",
            current_mileage=100_000,
            updated_at=datetime.now(timezone.utc),
            max_coolant_temp=105.0,
            max_oil_temp=130.0,
            min_oil_pressure=1.0,
            min_voltage=11.8,
            max_voltage=14.8,
        )
        self.engine_updates = []
        self.electrical_updates = []
        self.dtc_updates = []
        self.dtc_events = []
        self.fail_engine_update = False

    def get_vehicle_norms(self, vin: str):
        return self.vehicle if vin == VIN else None

    def update_engine_state(self, vin: str, payload: EngineSensorPayload) -> bool:
        if self.fail_engine_update:
            raise TelemetryStorageError("storage unavailable")
        self.engine_updates.append((vin, payload))
        self.vehicle.engine_rpm = payload.engine_rpm
        self.vehicle.coolant_temp = payload.coolant_temp
        self.vehicle.oil_temp = payload.oil_temp
        self.vehicle.oil_pressure = payload.oil_pressure
        return True

    def update_electrical_state(self, vin: str, payload: ElectricalSensorPayload) -> bool:
        self.electrical_updates.append((vin, payload))
        self.vehicle.battery_voltage = payload.battery_voltage
        self.vehicle.current_mileage = payload.current_mileage
        return True

    def save_dtc_errors(self, vin: str, sensor_id: str, errors: list[str]) -> None:
        self.dtc_updates.append((vin, sensor_id, errors))
        now = datetime.now(timezone.utc)
        self.dtc_events.extend(
            DtcEventRecord(sensor_id=sensor_id, error_code=error, received_at=now)
            for error in errors
        )

    def get_dtc_events(self, vin: str) -> list[DtcEventRecord]:
        return self.dtc_events


@pytest.fixture
def telemetry_repository():
    return InMemoryTelemetryRepository()


@pytest.fixture
def client(telemetry_repository):
    app.dependency_overrides[get_telemetry_service] = lambda: TelemetryService(
        telemetry_repository
    )
    with TestClient(app) as test_client:
        yield test_client
    app.dependency_overrides.clear()


def test_engine_telemetry_is_saved_when_values_are_normal(client, telemetry_repository):
    response = client.post(
        "/api/telemetry/engine",
        json={
            "sensor_id": "engine-1",
            "vin": VIN,
            "engine_rpm": 750,
            "coolant_temp": 90.0,
            "oil_temp": 100.0,
            "oil_pressure": 2.5,
        },
    )

    assert response.status_code == 200
    assert "сохранена" in response.json()["message"]
    assert len(telemetry_repository.engine_updates) == 1


def test_engine_telemetry_reports_anomalies(client):
    response = client.post(
        "/api/telemetry/engine",
        json={
            "sensor_id": "engine-1",
            "vin": VIN,
            "engine_rpm": 900,
            "coolant_temp": 120.0,
            "oil_temp": 140.0,
            "oil_pressure": 0.5,
        },
    )

    assert response.status_code == 201
    assert len(response.json()["anomalies"]) == 3


def test_electrical_telemetry_is_saved_when_voltage_is_normal(
    client, telemetry_repository
):
    response = client.post(
        "/api/telemetry/electrical",
        json={
            "sensor_id": "electrical-1",
            "vin": VIN,
            "battery_voltage": 13.8,
            "current_mileage": 100_100,
        },
    )

    assert response.status_code == 200
    assert len(telemetry_repository.electrical_updates) == 1


def test_electrical_telemetry_reports_low_voltage(client):
    response = client.post(
        "/api/telemetry/electrical",
        json={
            "sensor_id": "electrical-1",
            "vin": VIN,
            "battery_voltage": 10.5,
            "current_mileage": 100_100,
        },
    )

    assert response.status_code == 201
    assert "недозаряд" in response.json()["anomalies"][0]


def test_dtc_codes_are_normalized_and_saved(client, telemetry_repository):
    response = client.post(
        "/api/telemetry/obd",
        json={
            "sensor_id": "obd-1",
            "vin": VIN,
            "dtc_errors": [" p0420 ", "u0100", "  "],
        },
    )

    assert response.status_code == 201
    assert response.json()["dtc_errors"] == ["P0420", "U0100"]
    assert telemetry_repository.dtc_updates == [(VIN, "obd-1", ["P0420", "U0100"])]


def test_unknown_vehicle_is_rejected(client):
    response = client.post(
        "/api/telemetry/obd",
        json={
            "sensor_id": "obd-1",
            "vin": "2HGCM82633A004352",
            "dtc_errors": ["P0420"],
        },
    )

    assert response.status_code == 404


def test_invalid_telemetry_payload_is_rejected(client):
    response = client.post(
        "/api/telemetry/engine",
        json={"sensor_id": "engine-1", "vin": VIN},
    )

    assert response.status_code == 422


def test_invalid_vin_characters_are_rejected(client):
    response = client.post(
        "/api/telemetry/obd",
        json={
            "sensor_id": "obd-1",
            "vin": "1HGCM82633A00435I",
            "dtc_errors": ["P0420"],
        },
    )

    assert response.status_code == 422


def test_database_write_failure_is_not_reported_as_success(
    client, telemetry_repository
):
    telemetry_repository.fail_engine_update = True
    response = client.post(
        "/api/telemetry/engine",
        json={
            "sensor_id": "engine-1",
            "vin": VIN,
            "engine_rpm": 750,
            "coolant_temp": 90.0,
            "oil_temp": 100.0,
            "oil_pressure": 2.5,
        },
    )

    assert response.status_code == 503


def test_sensor_get_endpoints_return_saved_data_by_vin(client):
    client.post(
        "/api/telemetry/engine",
        json={
            "sensor_id": "engine-1",
            "vin": VIN,
            "engine_rpm": 850,
            "coolant_temp": 92.0,
            "oil_temp": 105.0,
            "oil_pressure": 2.4,
        },
    )
    client.post(
        "/api/telemetry/electrical",
        json={
            "sensor_id": "electrical-1",
            "vin": VIN,
            "battery_voltage": 13.8,
            "current_mileage": 100_250,
        },
    )
    client.post(
        "/api/telemetry/obd",
        json={"sensor_id": "obd-1", "vin": VIN, "dtc_errors": ["P0420"]},
    )

    engine = client.get(f"/api/telemetry/{VIN}/engine")
    electrical = client.get(f"/api/telemetry/{VIN}/electrical")
    obd = client.get(f"/api/telemetry/{VIN}/obd")

    assert engine.status_code == 200
    assert engine.json()["engine"]["coolant_temp"] == 92.0
    assert electrical.status_code == 200
    assert electrical.json()["electrical"]["battery_voltage"] == 13.8
    assert obd.status_code == 200
    assert obd.json()["dtc_events"][0]["error_code"] == "P0420"


def test_sensor_get_endpoints_reject_unknown_vin(client):
    for sensor in ("engine", "electrical", "obd"):
        response = client.get(f"/api/telemetry/2HGCM82633A004352/{sensor}")
        assert response.status_code == 404
