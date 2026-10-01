using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStateCar
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public void SetLogin(string login) => Login = login;
        public void SetPassword(string password) => Password = password;
        public void SetRole(string role) => Role = role;

        public void SetContactInfo(string email, string phone)
        {
            Email = email;
            Phone = phone;
        }

        public string GetEmployeeInfo() => $"{FullName} | Роль: {Role} | Логин: {Login}";

        public bool CheckPassword(string password) => Password == password;
    }
}
