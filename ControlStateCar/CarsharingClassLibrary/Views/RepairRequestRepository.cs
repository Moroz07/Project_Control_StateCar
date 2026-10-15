using System;
using System.Collections.Generic;
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
                using (var conn = new NpgsqlConnection(DBConnection.connectionString_))
                {
                    conn.Open();

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

                                // Маппинг английских статусов из базы в наш Enum
                                string dbStatus = reader.GetString(4).ToLower();
                                switch (dbStatus)
                                {
                                    case "open":
                                        request.Status = RepairStatus.новый;
                                        break;
                                    case "in_progress":
                                        request.Status = RepairStatus.в_работе;
                                        break;
                                    case "closed":
                                        request.Status = RepairStatus.завершён;
                                        break;
                                    case "cancelled":
                                        request.Status = RepairStatus.отменена;
                                        break;
                                    default:
                                        request.Status = RepairStatus.новый;
                                        break;
                                }

                                request.CreatedAt = reader.GetDateTime(5);
                                request.TechnicianLogin = reader.GetString(6);

                                requests.Add(request);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException)
            {
                return new List<RepairRequest>();
            }

            return requests;
        }

        public bool AssignTechnician(int requestId, string technicianLogin)
        {
            try
            {
                using (var conn = new NpgsqlConnection(DBConnection.connectionString_))
                {
                    conn.Open();
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

                    // При назначении меняем статус на английский 'in_progress' (согласно ограничениям БД)
                    string sql = @"
                        UPDATE repair_requests 
                        SET employee_id = @employeeId, status = 'in_progress' 
                        WHERE id = @requestid";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@employeeId", employeeId);
                        cmd.Parameters.AddWithValue("@requestid", requestId);

                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
            }
            catch (NpgsqlException)
            {
                return false;
            }
        }

        public bool CancelRequest(int requestId, string technicianLogin)
        {
            try
            {
                using (var conn = new NpgsqlConnection(DBConnection.connectionString_))
                {
                    conn.Open();
                    // Проверяем, что заявка действительно принадлежит этому технику и находится в работе
                    string sql = @"
                        UPDATE repair_requests 
                        SET status = 'cancelled', closed_at = NOW() 
                        WHERE id = @requestid 
                          AND employee_id = (SELECT id FROM employees WHERE login = @login)";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@requestid", requestId);
                        cmd.Parameters.AddWithValue("@login", technicianLogin);

                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
            }
            catch (NpgsqlException)
            {
                return false;
            }
        }
    }
}