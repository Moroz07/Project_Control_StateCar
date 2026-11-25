using System;
using System.Collections.Generic;
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
    }
    
}