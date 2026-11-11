using System;
using System.Collections.Generic;
using CarsharingClassLibrary.Models;
using Npgsql;

namespace ControlStateCar
{
    public class RepairRequestRepository : IRepairRequestRepository
    {
        public List<RepairRequest> GetRecentRepairRequests(Role role, string userLogin, int count = 15)
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

                if (role == Role.техник)
                {
                    sql += " WHERE e.login = @technicianLogin";
                }

                sql += " ORDER BY r.created_at DESC LIMIT @count";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@count", count);

                    if (role == Role.техник)
                    {
                        cmd.Parameters.AddWithValue("@technicianLogin", userLogin);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var request = new RepairRequest
                            {
                                Id = reader.GetInt32(0),
                                CarName = reader.GetString(1),
                                CarNumber = reader.GetString(2),
                                Description = reader.GetString(3)
                            };

                            string rawStatus = reader.IsDBNull(4) ? "open" : reader.GetString(4).ToLower();
                            switch (rawStatus)
                            {
                                case "open":
                                case "новый":
                                    request.Status = RepairStatus.New;
                                    break;
                                case "in_progress":
                                case "в работе":
                                case "в_работе":
                                    request.Status = RepairStatus.InProgress;
                                    break;
                                case "closed":
                                case "завершён":
                                case "завершен":
                                    request.Status = RepairStatus.Completed;
                                    break;
                                case "cancelled":
                                case "отменена":
                                    request.Status = RepairStatus.Cancelled;
                                    break;
                                default:
                                    request.Status = RepairStatus.New;
                                    break;
                            }

                            request.CreatedAt = reader.GetDateTime(5);
                            request.TechnicianLogin = reader.IsDBNull(6) ? "Не назначен" : reader.GetString(6);

                            requests.Add(request);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<RepairRequest>();
            }

            return requests;
        }

        public List<RepairRequest> FilterRepairRequests(Role userRole, string currentUserLogin, RepairRequestFilterParameters filterParams)
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
                    LEFT JOIN employees e ON r.employee_id = e.id
                    WHERE 1=1";

                using (var cmd = new NpgsqlCommand())
                {
                    if (userRole == Role.техник)
                    {
                        sql += " AND e.login = @currentTechLogin";
                        cmd.Parameters.AddWithValue("@currentTechLogin", currentUserLogin);
                    }

                    if (filterParams != null)
                    {
                        if (filterParams.CarNumber != null && filterParams.CarNumber.Trim() != "")
                        {
                            sql += " AND v.car_number ILIKE @carNumber";
                            cmd.Parameters.AddWithValue("@carNumber", "%" + filterParams.CarNumber.Trim() + "%");
                        }

                        if (filterParams.Status.HasValue)
                        {
                            string statusDb;
                            switch (filterParams.Status.Value)
                            {
                                case RepairStatus.New:
                                    statusDb = "open";
                                    break;
                                case RepairStatus.InProgress:
                                    statusDb = "in_progress";
                                    break;
                                case RepairStatus.Completed:
                                    statusDb = "closed";
                                    break;
                                case RepairStatus.Cancelled:
                                    statusDb = "cancelled";
                                    break;
                                default:
                                    statusDb = "open";
                                    break;
                            }

                            sql += " AND r.status = @status";
                            cmd.Parameters.AddWithValue("@status", statusDb);
                        }

                        if (userRole == Role.администратор)
                        {
                            if (filterParams.TechnicianLogin != null && filterParams.TechnicianLogin.Trim() != "")
                            {
                                sql += " AND e.login ILIKE @techSearch";
                                cmd.Parameters.AddWithValue("@techSearch", "%" + filterParams.TechnicianLogin.Trim() + "%");
                            }
                        }

                        if (filterParams.CreatedDate.HasValue)
                        {
                            sql += " AND r.created_at::date = @createdDate";
                            cmd.Parameters.AddWithValue("@createdDate", filterParams.CreatedDate.Value.Date);
                        }
                    }

                    sql += " ORDER BY r.created_at DESC";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;

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
    }
}