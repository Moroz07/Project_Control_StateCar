using Npgsql;
using System;
using System.Windows.Forms;

namespace ControlStateCar
{
    public class DBConnection : IUserRepository
    {
        private string connectionString_;

        public DBConnection(string connectionString)
        {
            connectionString_ = connectionString;
        }

        public bool AddUser(User user)
        {
            try
            {
                bool result = false;
                var connection = new NpgsqlConnection(connectionString_);
                connection.Open();
                string query = @"INSERT INTO users (login, password, role, full_name, email, phone) 
                                 VALUES (@login, @password, @role, @full_name, @email, @phone)";
                var command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@login", user.Login);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@role", user.Role);
                command.Parameters.AddWithValue("@full_name", user.FullName);
                command.Parameters.AddWithValue("@email", (object)user.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@phone", (object)user.Phone ?? DBNull.Value);
                int execute = command.ExecuteNonQuery();
                if (execute > 0)
                {
                    result = true;
                }
                return result;
            }
            catch (NpgsqlException exception)
            {
                MessageBox.Show($"Ошибка: {exception.Message}");
                return false;
            }
        }

        public bool CheckIfLoginExists(string login)
        {
            try
            {
                var connection = new NpgsqlConnection(connectionString_);
                connection.Open();
                string query = "SELECT COUNT(1) FROM users WHERE login = @login";
                var command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@login", login);
                long count = (long)command.ExecuteScalar();
                return count > 0;
            }
            catch (NpgsqlException exception)
            {
                MessageBox.Show($"Ошибка: {exception.Message}");
                return false;
            }
        }

        public User FindByLogin(string login)
        {
            try
            {
                var connection = new NpgsqlConnection(connectionString_);
                connection.Open();
                string query = "SELECT login, password, role, full_name, email, phone FROM users WHERE login = @login";
                var command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@login", login);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        Login = reader["login"].ToString(),
                        Password = reader["password"].ToString(),
                        Role = reader["role"].ToString(),
                        FullName = reader["full_name"].ToString(),
                        Email = reader["email"] != DBNull.Value ? reader["email"].ToString() : null,
                        Phone = reader["phone"] != DBNull.Value ? reader["phone"].ToString() : null
                    };
                }
                return null;
            }
            catch (NpgsqlException exception)
            {
                MessageBox.Show($"Ошибка: {exception.Message}");
                return null;
            }
        }
    }
}