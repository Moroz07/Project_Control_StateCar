using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStateCar
{
    public class AuthService
    {
        private IUserRepository userRepository_;

        public AuthService(IUserRepository userRepository)
        {
            userRepository_ = userRepository;
        }

        public bool ValidateCredentials(string login, string password, out string errorMessage)
        {
            errorMessage = string.Empty;
            User user = userRepository_.FindByLogin(login);

            if (user == null)
            {
                errorMessage = "Пользователь с таким логином не найден.";
                return false;
            }

            if (!user.CheckPassword(password))
            {
                errorMessage = "Неверный пароль.";
                return false;
            }

            return true;
        }

        public string GetUserRole(string login)
        {
            User user = userRepository_.FindByLogin(login);
            return user?.Role;
        }

        public bool Authorize(string login, string password, out string errorMessage)
        {
            if (!ValidateCredentials(login, password, out errorMessage))
                return false;

            string role = GetUserRole(login);
            Session.CreateSession(login, role);
            return true;
        }
    }
}
