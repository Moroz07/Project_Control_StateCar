from unittest.mock import Mock

import pytest
from pydantic import ValidationError

from app.models import SensorType, TripCompletedPayload, VehicleStateInfo, VehicleStatus
from app.repositories import IDiagnosticsRepository
from app.services import (
    DiagnosticsService,
    ISensorClient,
    SensorTimeoutError,
    SensorValidationError,
    TripAlreadyProcessedError,
    VehicleNotFoundError,
)

VIN = "XTA210930Y1234567"

NORMAL_RESPONSES = {
    SensorType.ENGINE: {"sensor_id": "ENG-01", "coolant_temp": 90.0, "oil_temp": 100.0,
                        "oil_pressure": 2.5, "max_rpm": 3000},
    SensorType.ELECTRICAL: {"sensor_id": "EL-01", "battery_voltage": 13.8,
                            "battery_charge": 80, "current_mileage": 54320},
    SensorType.CHASSIS: {"sensor_id": "CH-01", "fuel_level": 30.0,
                         "tire_pressure": 2.3, "brake_wear": 30},
}


def make_state(status=VehicleStatus.READY, **overrides) -> VehicleStateInfo:
    values = dict(vin=VIN, car_number="Е777КХ77", status=status,
                  coolant_temp=90.0, oil_temp=100.0, oil_pressure=2.5, max_rpm=3000,
                  battery_voltage=13.8, battery_charge=80.0, current_mileage=54000,
                  fuel_level=30.0, tire_pressure=2.3, brake_wear=30.0)
    values.update(overrides)
    return VehicleStateInfo(**values)


@pytest.fixture
def repo():
    r = Mock(spec=IDiagnosticsRepository)
    r.get_vehicle_state.return_value = make_state()
    r.is_trip_processed.return_value = False
    r.mark_trip_processed.return_value = True
    r.save_sensor_reading.return_value = 1
    r.update_vehicle_state.return_value = True
    r.set_vehicle_status.return_value = True
    return r


@pytest.fixture
def client():
    c = Mock(spec=ISensorClient)
    c.request_sensor.side_effect = lambda vin, sensor_type: dict(NORMAL_RESPONSES[sensor_type])
    return c


@pytest.fixture
def service(repo, client):
    return DiagnosticsService(repo, client)


def trip(trip_id="TRIP-1"):
    return TripCompletedPayload(vin=VIN, trip_id=trip_id)


# ---------- process_trip_completion ----------

def test_trip_with_deviations_changes_status(service, repo, client):
    repo.get_vehicle_state.return_value = make_state(coolant_temp=118.0)

    result = service.process_trip_completion(trip())

    requested = [call.args[1] for call in client.request_sensor.call_args_list]
    assert requested == [SensorType.ENGINE, SensorType.ELECTRICAL, SensorType.CHASSIS]
    assert repo.save_sensor_reading.call_count == 3
    assert repo.update_vehicle_state.call_count == 3
    repo.set_vehicle_status.assert_called_once()
    assert result.status_changed is True
    assert result.status == VehicleStatus.IN_REPAIR


def test_trip_normal_status_not_changed(service, repo):
    result = service.process_trip_completion(trip())

    assert repo.save_sensor_reading.call_count == 3
    repo.set_vehicle_status.assert_not_called()
    assert result.status_changed is False
    assert result.message == "Опрос датчиков завершён, статус автомобиля не изменён"


def test_trip_already_processed(service, repo, client):
    repo.is_trip_processed.return_value = True

    with pytest.raises(TripAlreadyProcessedError):
        service.process_trip_completion(trip())
    client.request_sensor.assert_not_called()


def test_trip_vehicle_not_found(service, repo):
    repo.get_vehicle_state.return_value = None

    with pytest.raises(VehicleNotFoundError):
        service.process_trip_completion(trip())


# ---------- evaluate_vehicle_status ----------

@pytest.mark.parametrize("overrides", [{"fuel_level": 3.0}, {"battery_charge": 15.0}])
def test_evaluate_warning_needs_service(service, repo, overrides):
    repo.get_vehicle_state.return_value = make_state(**overrides)

    status, _, changed = service.evaluate_vehicle_status(VIN)

    assert status == VehicleStatus.NEEDS_SERVICE
    assert changed is True


@pytest.mark.parametrize("overrides", [{"coolant_temp": 118.0}, {"oil_pressure": 0.5},
                                       {"tire_pressure": 1.2}])
def test_evaluate_critical_in_repair(service, repo, overrides):
    repo.get_vehicle_state.return_value = make_state(**overrides)

    status, _, changed = service.evaluate_vehicle_status(VIN)

    assert status == VehicleStatus.IN_REPAIR
    assert changed is True


# ---------- poll_*_sensor ----------

def test_poll_engine_timeout_sets_diagnostics(service, repo, client):
    client.request_sensor.side_effect = SensorTimeoutError("Датчик не ответил на запрос сервиса")

    with pytest.raises(SensorTimeoutError):
        service.poll_engine_sensor(VIN, "TRIP-1")

    assert repo.set_vehicle_status.call_args.args[1] == VehicleStatus.DIAGNOSTICS
    repo.save_sensor_reading.assert_not_called()


# ---------- валидация ----------

@pytest.mark.parametrize("vin, trip_id", [("A123", "TRIP-1"), (VIN, ""), (VIN, "   ")])
def test_trip_payload_validation(vin, trip_id):
    with pytest.raises(ValidationError):
        TripCompletedPayload(vin=vin, trip_id=trip_id)


@pytest.mark.parametrize("sensor_type, overrides", [
    (SensorType.ENGINE, {"coolant_temp": -100}),
    (SensorType.ENGINE, {"sensor_id": ""}),
    (SensorType.ELECTRICAL, {"current_mileage": -100}),
    (SensorType.ELECTRICAL, {"current_mileage": 1000}),   # меньше прошлого пробега 54000
    (SensorType.CHASSIS, {"brake_wear": 150}),
])
def test_sensor_response_validation(service, repo, client, sensor_type, overrides):
    client.request_sensor.side_effect = lambda vin, t: {**NORMAL_RESPONSES[t], **overrides}

    with pytest.raises(SensorValidationError):
        service._poll_sensor(VIN, "TRIP-1", sensor_type)

    assert repo.set_vehicle_status.call_args.args[1] == VehicleStatus.DIAGNOSTICS
    repo.save_sensor_reading.assert_not_called()


def test_unsupported_sensor_type():
    with pytest.raises(ValueError):
        SensorType("broken")