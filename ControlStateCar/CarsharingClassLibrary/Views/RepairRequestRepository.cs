using System;
using System.Collections.Generic;
using CarsharingClassLibrary.Models;
using Npgsql;

namespace ControlStateCar
{
    public class RepairRequestRepository : IRepairRequestRepository
    {


        public List<RepairRequest> GetAllRepairRequests()
        {
            var requests = new List<RepairRequest>();

            try
            {
                var conn = DBConnection.GetOpenConnection();

                string sql = @"
                    SELECT 
                        r.id, 
                        COALESCE(v.brand, '') || ' ' || COALESCE(v.model, 'Автомобиль') AS car_name, 
                        COALESCE(v.car_number, 'Без номера') AS car_number, 
                        r.description, 
                        r.status, 
                        r.created_at, 
                        COALESCE(e.login, 'Не назначен') AS technician_login 
                    FROM repair_requests r
                    LEFT JOIN vehicles v ON r.vehicle_id = v.id
                    LEFT JOIN employees e ON r.employee_id = e.id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var req = new RepairRequest
                        {
                            Id = reader.GetInt32(0),
                            CarName = reader.GetString(1),
                            CarNumber = reader.GetString(2),
                            Description = reader.GetString(3)
                        };

                        string rawStatus = "";
                        if (!reader.IsDBNull(4))
                        {
                            rawStatus = reader.GetString(4).ToLower();
                        }
                        else
                        {
                            rawStatus = "open";
                        }

                        switch (rawStatus)
                        {
                            case "open":
                            case "новый":
                                req.Status = RepairStatus.New;
                                break;
                            case "in_progress":
                            case "в работе":
                            case "в_работе":
                                req.Status = RepairStatus.InProgress;
                                break;
                            case "closed":
                            case "завершён":
                            case "завершен":
                                req.Status = RepairStatus.Completed;
                                break;
                            case "cancelled":
                            case "отменена":
                                req.Status = RepairStatus.Cancelled;
                                break;
                            default:
                                req.Status = RepairStatus.New;
                                break;
                        }

                        req.CreatedAt = reader.GetDateTime(5);

                        if (!reader.IsDBNull(6))
                        {
                            req.TechnicianLogin = reader.GetString(6);
                        }
                        else
                        {
                            req.TechnicianLogin = "Не назначен";
                        }

                        requests.Add(req);
                    }
                }
            }
            catch (Exception)
            {
                return new List<RepairRequest>();
            }

            return requests;
        }

        public bool AssignTechnician(int requestId, string technicianLogin)
        {
            try
            {
                var conn = DBConnection.GetOpenConnection();

                string getEmpIdSql = "SELECT id FROM employees WHERE login = @login";
                int employeeId = 0;

                using (var cmdEmp = new NpgsqlCommand(getEmpIdSql, conn))
                {
                    cmdEmp.Parameters.AddWithValue("@login", technicianLogin);
                    var result = cmdEmp.ExecuteScalar();
                    if (result != null)
                    {
                        employeeId = Convert.ToInt32(result);
                    }
                    else
                    {
                        return false;
                    }
                }

                string sql = @"
                    UPDATE repair_requests 
                    SET employee_id = @employeeId, status = 'in_progress' 
                    WHERE id = @requestid";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);
                    cmd.Parameters.AddWithValue("@requestid", requestId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CancelRequest(int requestId, string technicianLogin)
        {
            try
            {
                var conn = DBConnection.GetOpenConnection();

                string sql = @"
                    UPDATE repair_requests 
                    SET status = 'cancelled', closed_at = NOW() 
                    WHERE id = @requestid 
                      AND employee_id = (SELECT id FROM employees WHERE login = @login)";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@requestid", requestId);
                    cmd.Parameters.AddWithValue("@login", technicianLogin);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<string> GetAllTechnicianLogins()
        {
            var logins = new List<string>();

            try
            {
                var conn = DBConnection.GetOpenConnection();
                string sql = "SELECT login, role FROM employees";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string login = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        string role = reader.IsDBNull(1) ? "" : reader.GetString(1).ToLower();

                        if (login != "" && (role == "техник" || role == "technician"))
                        {
                            logins.Add(login);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<string>();
            }

            return logins;
        }

        public List<RepairRequestDetails> GetAllRepairRequestDetails()
        {
            var detailsList = new List<RepairRequestDetails>();

            try
            {
                var conn = DBConnection.GetOpenConnection();

                string sql = @"
                    SELECT 
                        r.id,
                        r.status,
                        r.created_at,
                        r.closed_at,
                        r.description,
                        r.customer_name,
                        r.customer_phone,
                        r.work_done,
                        v.brand,
                        v.model,
                        v.car_number,
                        v.vin,
                        v.year,
                        e.login,
                        e.full_name,
                        e.phone
                    FROM repair_requests r
                    LEFT JOIN vehicles v ON r.vehicle_id = v.id
                    LEFT JOIN employees e ON r.employee_id = e.id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var details = new RepairRequestDetails
                        {
                            Id = reader.GetInt32(0),
                            CreatedAt = reader.GetDateTime(2)
                        };

                        string rawStatus = reader.IsDBNull(1) ? "open" : reader.GetString(1).ToLower();
                        switch (rawStatus)
                        {
                            case "open":
                            case "новый":
                                details.Status = RepairStatus.New;
                                break;
                            case "in_progress":
                            case "в работе":
                            case "в_работе":
                                details.Status = RepairStatus.InProgress;
                                break;
                            case "closed":
                            case "завершён":
                            case "завершен":
                                details.Status = RepairStatus.Completed;
                                break;
                            case "cancelled":
                            case "отменена":
                                details.Status = RepairStatus.Cancelled;
                                break;
                            default:
                                details.Status = RepairStatus.New;
                                break;
                        }

                        details.ClosedAt = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);
                        details.Description = reader.IsDBNull(4) ? "Описание отсутствует" : reader.GetString(4);
                        details.CustomerName = reader.IsDBNull(5) ? "Не указан" : reader.GetString(5);
                        details.CustomerPhone = reader.IsDBNull(6) ? "Не указан" : reader.GetString(6);
                        details.WorkDone = reader.IsDBNull(7) ? "" : reader.GetString(7);

                        details.CarBrand = reader.IsDBNull(8) ? "Не указана" : reader.GetString(8);
                        details.CarModel = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        details.CarNumber = reader.IsDBNull(10) ? "Без номера" : reader.GetString(10);
                        details.Vin = reader.IsDBNull(11) ? "Не указан" : reader.GetString(11);
                        details.Year = reader.IsDBNull(12) ? 0 : Convert.ToInt32(reader.GetValue(12));

                        details.TechnicianLogin = reader.IsDBNull(13) ? null : reader.GetString(13);
                        details.TechnicianFullName = reader.IsDBNull(14) ? "Не назначен" : reader.GetString(14);
                        details.TechnicianPhone = reader.IsDBNull(15) ? "Не назначен" : reader.GetString(15);

                        detailsList.Add(details);
                    }
                }
            }
            catch (Exception)
            {
                return new List<RepairRequestDetails>();
            }

            return detailsList;
        }

        public bool CloseRequest(int requestId, string workDone, DateTime closedAt)
        {
            try
            {
                var conn = DBConnection.GetOpenConnection();
                string sql = @"
                    UPDATE repair_requests 
                    SET status = 'closed', 
                        work_done = @workDone, 
                        closed_at = @closedAt 
                    WHERE id = @id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@workDone", workDone ?? "");
                    cmd.Parameters.AddWithValue("@closedAt", closedAt);
                    cmd.Parameters.AddWithValue("@id", requestId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateClosedDate(int requestId, DateTime closedAt, string workDone)
        {
            try
            {
                var conn = DBConnection.GetOpenConnection();
                string sql = @"
                    UPDATE repair_requests 
                    SET closed_at = @closedAt, 
                        work_done = @workDone 
                    WHERE id = @id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@closedAt", closedAt);
                    cmd.Parameters.AddWithValue("@workDone", workDone ?? "");
                    cmd.Parameters.AddWithValue("@id", requestId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    

    public List<RepairRequestHistoryItem> GetAllRequestHistory()
        {
            var items = new List<RepairRequestHistoryItem>();

            try
            {
                var conn = DBConnection.GetOpenConnection();

                string sql = @"
                    SELECT
                        h.id,
                        h.request_id,
                        h.changed_at,
                        s.name,
                        e.full_name,
                        e.login,
                        st.name,
                        h.comment
                    FROM status_history h
                    LEFT JOIN statuses s ON h.status_id = s.id
                    LEFT JOIN employees e ON h.employee_id = e.id
                    LEFT JOIN service_types st ON h.service_type_id = st.id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string empFullName = reader.IsDBNull(4) ? "" : reader.GetString(4).Trim();
                        string empLogin = reader.IsDBNull(5) ? "" : reader.GetString(5).Trim();

                        string empDisplay;
                        if (!string.IsNullOrEmpty(empFullName) && !string.IsNullOrEmpty(empLogin))
                        {
                            empDisplay = $"{empFullName} ({empLogin})";
                        }
                        else if (!string.IsNullOrEmpty(empFullName))
                        {
                            empDisplay = empFullName;
                        }
                        else if (!string.IsNullOrEmpty(empLogin))
                        {
                            empDisplay = empLogin;
                        }
                        else
                        {
                            empDisplay = "Не указан";
                        }

                        items.Add(new RepairRequestHistoryItem
                        {
                            Id = reader.GetInt32(0),
                            RequestId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                            ChangedAt = reader.IsDBNull(2) ? DateTime.MinValue : reader.GetDateTime(2),
                            StatusName = reader.IsDBNull(3) ? "Изменение" : reader.GetString(3),
                            EmployeeDisplay = empDisplay,
                            ServiceTypeName = reader.IsDBNull(6) ? "" : reader.GetString(6),
                            Comment = reader.IsDBNull(7) ? "" : reader.GetString(7)
                        });
                    }
                }
            }
            catch (Exception)
            {
                return new List<RepairRequestHistoryItem>();
            }

            return items;
        }

        public bool AddRequestHistory(int requestId, int? employeeId, string statusLabel, string comment, DateTime changedAt)
        {
            try
            {
                var conn = DBConnection.GetOpenConnection();

                int? vehicleId = null;
                string getReqSql = "SELECT vehicle_id, employee_id FROM repair_requests WHERE id = @requestId";
                using (var getReqCmd = new NpgsqlCommand(getReqSql, conn))
                {
                    getReqCmd.Parameters.AddWithValue("@requestId", requestId);
                    using (var reader = getReqCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                vehicleId = reader.GetInt32(0);
                            }
                            if (!employeeId.HasValue && !reader.IsDBNull(1))
                            {
                                employeeId = reader.GetInt32(1);
                            }
                        }
                    }
                }

                int? statusId = null;
                string getStatusSql = "SELECT id, name FROM statuses";
                using (var stCmd = new NpgsqlCommand(getStatusSql, conn))
                using (var stReader = stCmd.ExecuteReader())
                {
                    int? fallbackId = null;
                    string target = (statusLabel ?? "").Trim().ToLower();

                    while (stReader.Read())
                    {
                        int id = stReader.GetInt32(0);
                        string name = stReader.IsDBNull(1) ? "" : stReader.GetString(1).Trim().ToLower();

                        if (!fallbackId.HasValue)
                        {
                            fallbackId = id;
                        }

                        if (!string.IsNullOrEmpty(target) &&
                            (name == target ||
                             (target.Contains("заверш") && (name.Contains("заверш") || name.Contains("closed") || name.Contains("completed"))) ||
                             (target.Contains("дат") && (name.Contains("измен") || name.Contains("work") || name.Contains("progress")))))
                        {
                            statusId = id;
                            break;
                        }
                    }

                    if (!statusId.HasValue)
                    {
                        statusId = fallbackId;
                    }
                }

                string insertSql = @"
                    INSERT INTO status_history (vehicle_id, status_id, request_id, employee_id, comment, changed_at)
                    VALUES (@vehicleId, @statusId, @requestId, @employeeId, @comment, @changedAt)";

                using (var insCmd = new NpgsqlCommand(insertSql, conn))
                {
                    insCmd.Parameters.AddWithValue("@vehicleId", (object)vehicleId ?? DBNull.Value);
                    insCmd.Parameters.AddWithValue("@statusId", (object)statusId ?? DBNull.Value);
                    insCmd.Parameters.AddWithValue("@requestId", requestId);
                    insCmd.Parameters.AddWithValue("@employeeId", (object)employeeId ?? DBNull.Value);
                    insCmd.Parameters.AddWithValue("@comment", comment ?? "");
                    insCmd.Parameters.AddWithValue("@changedAt", changedAt);

                    return insCmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    } 
}