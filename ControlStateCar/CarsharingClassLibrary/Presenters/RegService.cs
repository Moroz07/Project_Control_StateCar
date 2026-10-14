using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlStateCar;

namespace CarsharingClassLibrary
{
    public class RegService
    {
        private readonly IDBConnection userRepository_;
        private readonly UserValidator validator_;

        public RegService(IDBConnection userRepository, UserValidator validator)
        {
            userRepository_ = userRepository;
            validator_ = validator;
        }

        public bool CheckLoginUniqueness(string login)
        {
            return !userRepository_.CheckIfLoginExists(login);
        }

        public bool Register(User user, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (user == null)
            {
                errorMessage = "Данные сотрудника не указаны.";
                return false;
            }

            if (!validator_.ValidateLogin(user.Login))
            {
                errorMessage = "Логин должен быть от 3 до 50 символов (только латиница и цифры).";
                return false;
            }

            if (!validator_.ValidatePassword(user.Password))
            {
                errorMessage = "Пароль должен быть от 6 до 100 символов без пробелов.";
                return false;
            }

            if (!validator_.ValidateFullName(user.FullName))
            {
                errorMessage = "ФИО не должно быть пустым (допустимы буквы, дефисы и пробелы).";
                return false;
            }

            if (!validator_.ValidateRole(user.Role))
            {
                errorMessage = "Указана некорректная роль.";
                return false;
            }

            if (!validator_.ValidateEmail(user.Email))
            {
                errorMessage = "Некорректный формат email (имя@домен.зона).";
                return false;
            }

            if (!validator_.ValidatePhone(user.Phone))
            {
                errorMessage = "Номер телефона должен соответствовать маске +7 (XXX) XXX-XX-XX.";
                return false;
            }

            if (userRepository_.CheckIfLoginExists(user.Login))
            {
                errorMessage = "Пользователь с таким логином уже существует в системе.";
                return false;
            }

            if (!userRepository_.AddUser(user))
            {
                errorMessage = "Произошла ошибка записи в базу данных.";
                return false;
            }

            return true;
        }
    }
}

