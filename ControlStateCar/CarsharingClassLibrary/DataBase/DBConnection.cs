using System;
using System.IO;
using System.Data;
using Npgsql;

namespace ControlStateCar
{
    public class DBConnection : IDBConnection
    {
        private static NpgsqlConnection _connection;
        private static string _connectionString;

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = LoadConnectionStringFromIni();
                }
                return _connectionString;
            }
        }

        public NpgsqlConnection Connection => GetOpenConnection();

        public static NpgsqlConnection GetOpenConnection()
        {
            if (_connection == null)
            {
                _connection = new NpgsqlConnection(ConnectionString);
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            return _connection;
        }

        private static string LoadConnectionStringFromIni()
        {
            string host = "localhost";
            string port = "5432";
            string database = "CompanyDB";
            string username = "postgres";
            string password = "123";

            string iniPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
            if (File.Exists(iniPath))
            {
                string[] lines = File.ReadAllLines(iniPath);
                foreach (var rawLine in lines)
                {
                    string line = rawLine.Trim();
                    if (string.IsNullOrEmpty(line) || line.StartsWith(";") || line.StartsWith("#") || line.StartsWith("["))
                        continue;

                    string[] parts = line.Split(new[] { '=' }, 2);
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim().ToLower();
                        string val = parts[1].Trim();

                        switch (key)
                        {
                            case "host": host = val; break;
                            case "port": port = val; break;
                            case "database": database = val; break;
                            case "username": username = val; break;
                            case "password": password = val; break;
                        }
                    }
                }
            }

            return $"Host={host};Port={port};Database={database};Username={username};Password={password}";
        }

        public bool AddUser(User user)
        {
            try
            {
                var conn = Connection;
                string query = @"INSERT INTO employees (login, password_hash, role, full_name, email, phone) 
                                 VALUES (@login, @password, @role, @full_name, @email, @phone)";

                using (var command = new NpgsqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@login", user.Login);
                    command.Parameters.AddWithValue("@password", user.Password);

                    string roleDb = user.Role == Role.администратор ? "admin" : "technician";
                    command.Parameters.AddWithValue("@role", roleDb);

                    command.Parameters.AddWithValue("@full_name", user.FullName);
                    command.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(user.Email) ? (object)DBNull.Value : user.Email);
                    command.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(user.Phone) ? (object)DBNull.Value : user.Phone);

                    return command.ExecuteNonQuery() > 0;
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
                var conn = Connection;
                string query = "SELECT COUNT(1) FROM employees WHERE login = @login";

                using (var command = new NpgsqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@login", login);
                    long count = Convert.ToInt64(command.ExecuteScalar());
                    return count > 0;
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
                var conn = Connection;
                string query = "SELECT login, password_hash, role, full_name, email, phone FROM employees WHERE login = @login";

                using (var command = new NpgsqlCommand(query, conn))
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
                return null;
            }
            catch (NpgsqlException)
            {
                return null;
            }
        }
    }
}