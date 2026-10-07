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
            {
                return false;
            }

            return Regex.IsMatch(login, @"^[a-zA-Z0-9]+$");
        }

        public bool ValidatePassword(string password)
        {
            if (password == null || password.Trim() == "" || password.Length < 6 || password.Length > 100)
            {
                return false;
            }

            return !password.Contains(" ");
        }

        public bool ValidateFullName(string fullName)
        {
            if (fullName == null || fullName.Trim() == "")
            {
                return false;
            }

            return Regex.IsMatch(fullName, @"^[a-zA-Zа-яА-ЯёЁ\s\-]+$");
        }

        public bool ValidateEmail(string email)
        {
            if (email == null || email.Trim() == "")
            {
                return false;
            }

            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }

            int digits = phone.Count(char.IsDigit);
            if (digits != 11)
            {
                return false;
            }

            return Regex.IsMatch(phone, @"^\+7\s?\(\d{3}\)\s?\d{3}-\d{2}-\d{2}$");
        }
        public bool ValidateRole(Role role)
        {
            return role == Role.администратор || role == Role.техник;
        }

        public bool ValidateRole(User u)
        {
            if (u == null)
            {
                return false;
            }

            return ValidateRole(u.Role);
        }

        public bool ValidateAll(User user)
        {
            if (user == null)
            {
                return false;
            }

            return ValidateLogin(user.Login) &&
                   ValidatePassword(user.Password) &&
                   ValidateFullName(user.FullName) &&
                   ValidateEmail(user.Email) &&
                   ValidatePhone(user.Phone) &&
                   ValidateRole(user.Role);
        }
    }
}