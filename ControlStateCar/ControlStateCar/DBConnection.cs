using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var connection = new NpgsqlConnection(connectionString_);
            {
                string query = @"INSERT INTO users (login, password, role, full_name, email, phone) 
                                 VALUES (@login, @password, @role, @full_name, @email, @phone)";
                var command = new NpgsqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@login", user.Login);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@role", user.Role);
                command.Parameters.AddWithValue("@full_name", user.FullName);
                command.Parameters.AddWithValue("@email", (object)user.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@phone", (object)user.Phone ?? DBNull.Value);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
                
            }
        }

        public bool CheckIfLoginExists(string login)
        {
            var connection = new NpgsqlConnection(connectionString_);
            string query = "SELECT COUNT(1) FROM users WHERE login = @login";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@login", login);
            connection.Open();
            long count = (long)command.ExecuteScalar();
            return count == 0;
        }

        public User FindByLogin(string login)
        {
            var connection = new NpgsqlConnection(connectionString_);
            string query = "SELECT login, password, role, full_name, email, phone FROM users WHERE login = @login";
            var command = new NpgsqlCommand(query, connection);               
            command.Parameters.AddWithValue("@login", login);
            connection.Open();
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
    }
}
