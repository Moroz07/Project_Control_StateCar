class DBConnection:
    # Укажите ваши реальные данные подключения к PostgreSQL
    connection_string: str = "postgresql://postgres:123@localhost:5432/car_diagnostics_db"


class SensorConfig:
    # Адрес бортовой системы (сейчас это встроенный эмулятор датчиков)
    base_url: str = "http://127.0.0.1:8000"
    # Сколько секунд ждать ответа датчика
    timeout_seconds: float = 3.0


class SensorNorms:
    # Критические пороги -> статус «в ремонте»
    MAX_COOLANT_TEMP = 105.0   # °C
    MAX_OIL_TEMP = 130.0       # °C
    MIN_OIL_PRESSURE = 1.0     # бар
    MIN_TIRE_PRESSURE = 1.8    # бар
    # Предупреждающие пороги -> статус «требует обслуживания»
    MIN_FUEL_LEVEL = 5.0       # л
    MIN_BATTERY_CHARGE = 30.0  # %
    MIN_VOLTAGE = 11.8         # В
    MAX_VOLTAGE = 14.8         # В
    MAX_BRAKE_WEAR = 80.0      # %