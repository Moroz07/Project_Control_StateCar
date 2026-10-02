using Npgsql;
using System;

namespace ControlStateCar
{
    public class DBConnection : IUserRepository
    {
        private const string connectionString_ = "Host=192.168.1.48;Port=5432;Database=CompanyDB;Username=st403-7;Password=4037";

        public bool AddUser(User user)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString_))
                {
                    connection.Open();
                    string query = @"INSERT INTO users (login, password, role, full_name, email, phone) 
                                     VALUES (@login, @password, @role, @full_name, @email, @phone)";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", user.Login);
                        command.Parameters.AddWithValue("@password", user.Password);

                        string roleDb = user.Role == Role.администратор ? "admin" : "technician";
                        command.Parameters.AddWithValue("@role", roleDb);

                        command.Parameters.AddWithValue("@full_name", user.FullName);
                        command.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(user.Email) ? (object)DBNull.Value : user.Email);
                        command.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(user.Phone) ? (object)DBNull.Value : user.Phone);

                        int execute = command.ExecuteNonQuery();
                        return execute > 0;
                    }
                }
            }
            catch (NpgsqlException)
            {
                return false;
            }
        }

        public bool CheckIfLoginExists(string login)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString_))
                {
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM users WHERE login = @login";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        long count = (long)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (NpgsqlException)
            {
                return false;
            }
        }

        public User FindByLogin(string login)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString_))
                {
                    connection.Open();
                    string query = "SELECT login, password, role, full_name, email, phone FROM users WHERE login = @login";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roleDb = reader["role"].ToString();
                                Role userRole = (roleDb == "admin" || roleDb == "администратор")
                                    ? Role.администратор
                                    : Role.техник;

                                return new User
                                {
                                    Login = reader.GetString(0),
                                    Password = reader.GetString(1),
                                    Role = userRole,
                                    FullName = reader["full_name"].ToString(),
                                    Email = reader["email"] != DBNull.Value ? reader["email"].ToString() : null,
                                    Phone = reader["phone"] != DBNull.Value ? reader["phone"].ToString() : null
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (NpgsqlException)
            {
                return null;
            }
        }
    }
}