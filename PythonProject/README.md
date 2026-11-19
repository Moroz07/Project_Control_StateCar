# Телеметрия автомобиля

Сервис принимает показания двигателя, бортовой сети и OBD через HTTP API.

## Запуск

Из корневой папки проекта запустите приложение:

```powershell
python main.py
```

Swagger UI будет доступен по адресу <http://127.0.0.1:8000/docs>. В раскрывающемся списке каждого метода есть пример JSON для кнопки **Try it out**.

Для успешной проверки VIN должен быть зарегистрирован в таблице `vehicles`, а подключение в `app/config.py` должно указывать на нужный сервер и базу PostgreSQL.

## Полный цикл запроса

1. API принимает `POST` и FastAPI проверяет JSON по Pydantic-модели из `app/models/sensors.py`. Неверные поля или VIN возвращают `422` до вызова обработчика.
2. Обработчик в `app/api/telemetry.py` передаёт проверенную модель в `TelemetryService`.
3. Сервис в `app/services/telemetry_service.py` проверяет, что VIN зарегистрирован, нормализует DTC-коды и сравнивает показания с порогами.
4. Репозиторий в `app/repositories/telemetry_repository.py` читает нормы из `v_vehicle_state`, обновляет показания автомобиля и записывает DTC-события. Триггер БД добавляет изменённые числовые показания в `measurements`.
5. API возвращает результат: `200` — данные сохранены без отклонений, `201` — сохранённые данные содержат отклонения или DTC-коды, `404` — VIN не зарегистрирован, `422` — JSON не прошёл проверку, `503` — БД недоступна или запись не удалась.
6. Чтобы получить сохранённые данные, вызовите один из трёх GET: `/api/telemetry/{vin}/engine`, `/api/telemetry/{vin}/electrical` или `/api/telemetry/{vin}/obd`. Каждый маршрут возвращает показания только своего датчика.
7. Историю замеров можно получить через `GET /api/telemetry/{vin}/readings?sensor_type=engine`, указав `electrical` или `obd` для других датчиков. Записи идут от новых к старым; числовые показания читаются из `measurements`, OBD-коды — из `dtc_events`.

VIN проверяется как 17 символов без букв I, O и Q и является первичным ключом автомобиля в схеме телеметрии.

## Проверка через Swagger

1. Откройте `/docs` и раскройте `POST /api/telemetry/engine`.
2. Нажмите **Try it out**, оставьте пример с нормальными значениями и нажмите **Execute**. Ожидается HTTP `200`.
3. Для проверки обнаружения отклонений измените `coolant_temp` на `120` и `oil_pressure` на `0.5`. Ожидается HTTP `201` и массив `anomalies`.
4. Проверьте `POST /api/telemetry/electrical`: при `battery_voltage: 13.8` ожидается `200`; при `battery_voltage: 10.5` — `201` с предупреждением.
5. Проверьте `POST /api/telemetry/obd` с `dtc_errors: ["p0420", "u0100"]`. Ожидается `201`; коды вернутся как `P0420` и `U0100`.
6. Неверный формат запроса должен вернуть `422`. VIN автомобиля, которого нет в `vehicles`, должен вернуть `404`.

## Проверка через curl в PowerShell

Выполняйте команды при запущенном сервисе. Примеры используют зарегистрированный VIN `XTA12345678901231` (автомобиль `A123BC77`).

Нормальные показания двигателя, ожидается `200`:

```powershell
curl.exe -i -X POST "http://127.0.0.1:8000/api/telemetry/engine" `
  -H "Content-Type: application/json" `
  -d '{"sensor_id":"engine-01","vin":"XTA12345678901231","engine_rpm":850,"coolant_temp":92,"oil_temp":105,"oil_pressure":2.4}'
```

Отклонения двигателя, ожидается `201` и `anomalies` в ответе:

```powershell
curl.exe -i -X POST "http://127.0.0.1:8000/api/telemetry/engine" `
  -H "Content-Type: application/json" `
  -d '{"sensor_id":"engine-01","vin":"XTA12345678901231","engine_rpm":850,"coolant_temp":120,"oil_temp":105,"oil_pressure":0.5}'
```

Низкое напряжение бортовой сети, ожидается `201`:

```powershell
curl.exe -i -X POST "http://127.0.0.1:8000/api/telemetry/electrical" `
  -H "Content-Type: application/json" `
  -d '{"sensor_id":"electrical-01","vin":"XTA12345678901231","battery_voltage":10.5,"current_mileage":250}'
```

Коды OBD, ожидается `201`:

```powershell
curl.exe -i -X POST "http://127.0.0.1:8000/api/telemetry/obd" `
  -H "Content-Type: application/json" `
  -d '{"sensor_id":"obd-01","vin":"XTA12345678901231","dtc_errors":["p0420","u0100"]}'
```

Получить сохранённые данные каждого датчика по VIN:

```powershell
curl.exe -i "http://127.0.0.1:8000/api/telemetry/XTA12345678901231/engine"
curl.exe -i "http://127.0.0.1:8000/api/telemetry/XTA12345678901231/electrical"
curl.exe -i "http://127.0.0.1:8000/api/telemetry/XTA12345678901231/obd"
```

Каждый запрос вернёт только данные выбранного датчика. Отправьте POST для этого датчика заранее, иначе текущие поля могут быть пустыми, а список кодов OBD — пустым.

Получить историю замеров:

```powershell
curl.exe -i "http://127.0.0.1:8000/api/telemetry/XTA12345678901231/readings?sensor_type=engine"
curl.exe -i "http://127.0.0.1:8000/api/telemetry/XTA12345678901231/readings?sensor_type=electrical"
curl.exe -i "http://127.0.0.1:8000/api/telemetry/XTA12345678901231/readings?sensor_type=obd"
```

Ответ содержит `vin`, `sensor_type` и массив `readings` с данными и временем записи. Пустая история возвращается как HTTP `200` с пустым массивом.

## Автоматические тесты

```powershell
python -m pytest -q
```

Тесты используют временный репозиторий в памяти и проверяют нормальные значения, отклонения, нормализацию DTC, неизвестный VIN и некорректный запрос. Для них не требуется PostgreSQL.
