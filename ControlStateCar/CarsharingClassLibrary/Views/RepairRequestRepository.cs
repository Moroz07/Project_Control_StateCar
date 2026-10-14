using System;
using System.Collections.Generic;
using Npgsql;

namespace ControlStateCar
{
    public class RepairRequestRepository : IRepairRequestRepository
    {
        private const string connectionString_ = "Host=localhost;Port=5432;Database=CompanyDB;Username=postgres;Password=123456";

        public List<RepairRequest> GetRecentRepairRequests(Role role, string userLogin, int count = 15)
        {
            var requests = new List<RepairRequest>();

            try
            {
                using (var conn = new NpgsqlConnection(connectionString_))
                {
                    conn.Open();

                    // Если техник — выводим только его заявки. Если администратор — выводим все.
                    string sql;
                    if (role == Role.техник)
                    {
                        sql = @"
                            SELECT id, car_name, car_number, description, status, created_at, technician_login 
                            FROM repair_requests 
                            WHERE technician_login = @technicianLogin
                            ORDER BY created_at DESC 
                            LIMIT @count";
                    }
                    else
                    {
                        sql = @"
                            SELECT id, car_name, car_number, description, status, created_at, technician_login 
                            FROM repair_requests 
                            ORDER BY created_at DESC 
                            LIMIT @count";
                    }

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

                                string rawStatus = reader.GetString(4).Replace(" ", "_");
                                RepairStatus parsedStatus;
                                if (Enum.TryParse(rawStatus, true, out parsedStatus))
                                {
                                    request.Status = parsedStatus;
                                }
                                else
                                {
                                    request.Status = RepairStatus.новый;
                                }

                                request.CreatedAt = reader.GetDateTime(5);
                                request.TechnicianLogin = reader.IsDBNull(6) ? "Не назначен" : reader.GetString(6);

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
                using (var conn = new NpgsqlConnection(connectionString_))
                {
                    conn.Open();
                    string sql = @"
                        UPDATE repair_requests 
                        SET technician_login = @technicianLogin, status = 'в_работе' 
                        WHERE id = @requestid";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@technicianLogin", technicianLogin);
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
    }
}