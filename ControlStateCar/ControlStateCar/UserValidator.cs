using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControlStateCar
{
    public class UserValidator
    {
        public bool ValidateLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login) || login.Length < 3 || login.Length > 50)
                return false;

            return Regex.IsMatch(login, @"^[a-zA-Z0-9]+$");
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6 || password.Length > 100)
                return false;

            return !password.Contains(" ");
        }

        public bool ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return false;

            return Regex.IsMatch(fullName, @"^[a-zA-Zа-яА-ЯёЁ\s\-]+$");
        }

        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return Regex.IsMatch(phone, @"^\+7\s\(\d{3}\)\s\d{3}-\d{2}-\d{2}$");
        }

        public bool ValidateRole(Role role) => Enum.IsDefined(typeof(Role), role);

        public bool ValidateAll(User user)
        {
            if (user == null) return false;

            bool isRoleValid = Enum.TryParse(user.Role, out Role parsedRole) && ValidateRole(parsedRole);

            return ValidateLogin(user.Login) &&
                   ValidatePassword(user.Password) &&
                   ValidateFullName(user.FullName) &&
                   ValidateEmail(user.Email) &&
                   ValidatePhone(user.Phone) &&
                   isRoleValid;
        }
    }
}
