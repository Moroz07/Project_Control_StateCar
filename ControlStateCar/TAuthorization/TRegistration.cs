using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlStateCar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TAuthorization
{
    [TestClass]
    public class TRegistration
    {
        [TestMethod]
        public void TestMethod_AddUser_Success()
        {
            var user = new User
            {
                Login = "sidorov_i",
                Password = "StrongPass123",
                FullName = "Сидоров Иван Сергеевич",
                Email = "sidorov@company.ru",
                Phone = "+79991112233",
                Role = Role.техник
            };

            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.AddUser(user)).Returns(true);

            IUserRepository repository = mock.Object;
            bool result = repository.AddUser(user);

            Assert.IsTrue(result);
            Assert.AreEqual(Role.техник, user.Role);
        }



        // занятый логин → false
        [TestMethod]
        public void TestMethod_CheckIfLoginExists_AlreadyExists()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.CheckIfLoginExists("sidorov_i")).Returns(false);

            IUserRepository repository = mock.Object;
            bool exists = repository.CheckIfLoginExists("sidorov_i");

            Assert.IsFalse(exists);
        }

        // свободный логин → true
        [TestMethod]
        public void TestMethod_CheckIfLoginExists_NewLogin()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.CheckIfLoginExists("new_unique_user")).Returns(true);

            IUserRepository repository = mock.Object;
            bool exists = repository.CheckIfLoginExists("new_unique_user");

            Assert.IsTrue(exists);
        }


        //Пустой логин / пробелы
        [TestMethod]
        public void TestMethod_ValidateLogin_EmptyOrWhitespace()
        {
            var validator = new UserValidator();

            Assert.IsFalse(validator.ValidateLogin(" "));
            Assert.IsFalse(validator.ValidateLogin(""));
            Assert.IsFalse(validator.ValidateLogin(null));
        }

        // Пароль короче 6 символов
        [TestMethod]
        public void TestMethod_ValidatePassword_TooShort()
        {
            var validator = new UserValidator();

            bool result = validator.ValidatePassword("12345");

            Assert.IsFalse(result);
        }

        // Отсутствие роли (null user)
        [TestMethod]
        public void TestMethod_ValidateRole_NullUser()
        {
            var validator = new UserValidator();

            bool result = validator.ValidateRole((User)null);

            Assert.IsFalse(result);
        }

        // Некорректный email
        [TestMethod]
        public void TestMethod_ValidateEmail_Incorrect()
        {
            var validator = new UserValidator();

            bool result = validator.ValidateEmail("not_an_email");

            Assert.IsFalse(result);
        }

        // Все поля пустые
        [TestMethod]
        public void TestMethod_ValidateAll_EmptyUser()
        {
            var validator = new UserValidator();

            var user = new User
            {
                Login = "",
                Password = "",
                FullName = "",
                Email = "",
                Phone = "",
                Role = Role.техник
            };

            bool result = validator.ValidateAll(user);

            Assert.IsFalse(result);
        }

        // Пустое ФИО
        [TestMethod]
        public void TestMethod_ValidateFullName_Empty()
        {
            var validator = new UserValidator();

            var user = new User
            {
                Login = "sidorov_i",
                Password = "StrongPass123",
                FullName = "",
                Email = "sidorov@company.ru",
                Phone = "+79991112233",
                Role = Role.техник
            };

            Assert.IsFalse(validator.ValidateFullName(user.FullName));
            Assert.IsFalse(validator.ValidateAll(user));
        }
    }
}
