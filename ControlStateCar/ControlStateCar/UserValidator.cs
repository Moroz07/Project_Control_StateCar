using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ControlStateCar
{
    public class UserValidator
    {
        public bool ValidateLogin(string login)
        {
            if (login == null || login.Trim() == "" || login.Length < 3 || login.Length > 50)
                return false;

            return Regex.IsMatch(login, @"^[a-zA-Z0-9]+$");
        }

        public bool ValidatePassword(string password)
        {
            if (password == null || password.Trim() == "" || password.Length < 6 || password.Length > 100)
                return false;

            return !password.Contains(" ");
        }

        public bool ValidateFullName(string fullName)
        {
            if (fullName == null || fullName.Trim() == "")
                return false;

            return Regex.IsMatch(fullName, @"^[a-zA-Zа-яА-ЯёЁ\s\-]+$");
        }

        public bool ValidateEmail(string email)
        {
            if (email == null || email.Trim() == "")
                return false;

            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public bool ValidatePhone(string phone)
        {
            if (phone == null || phone.Trim() == "")
                return false;

            int digits = phone.Count(char.IsDigit);
            return digits == 11;
        }

        public bool ValidateRole(Role role)
        {
            return role == Role.admin || role == Role.employee;
        }

        public bool ValidateAll(User user)
        {
            if (user == null) return false;

            Role parsedRole;
            if (user.Role == "admin")
                parsedRole = Role.admin;
            else if (user.Role == "employee")
                parsedRole = Role.employee;
            else
                return false;

            return ValidateLogin(user.Login) &&
                   ValidatePassword(user.Password) &&
                   ValidateFullName(user.FullName) &&
                   ValidateEmail(user.Email) &&
                   ValidatePhone(user.Phone) &&
                   ValidateRole(parsedRole);
        }
    }
}