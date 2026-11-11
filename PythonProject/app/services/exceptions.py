class VehicleNotFoundError(Exception):
    pass


class TripAlreadyProcessedError(Exception):
    pass


class SensorValidationError(Exception):
    pass


class SensorTimeoutError(Exception):
    pass