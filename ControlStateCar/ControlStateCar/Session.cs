using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStateCar
{
    public class Session
    {
        public static string EmployeeLogin { get; private set; }
        public static string Role { get; private set; }
        public static DateTime LoginTime { get; private set; }

        public static void CreateSession(string login, string role)
        {
            EmployeeLogin = login;
            Role = role;
            LoginTime = DateTime.Now;
        }

        public static string GetCurrentUser() => EmployeeLogin;

        public static void EndSession()
        {
            EmployeeLogin = null;
            Role = null;
            LoginTime = DateTime.MinValue;
        }
    }
}
