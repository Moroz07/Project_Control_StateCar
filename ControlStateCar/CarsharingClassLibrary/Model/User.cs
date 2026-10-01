using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStateCar
{
    public enum Role { администратор, техник }
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }        
        public Role Role { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string GetEmployeeInfo()
        {
            return $"{FullName} | Роль: {Convert.ToString(Role)} | Логин: {Login}";
        }

        public bool CheckPassword(string password) => Password == password;
    }
}
