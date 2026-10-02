using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ControlStateCar;

namespace TAuthorization
{
    [TestClass]
    public class TAuthorization
    {
        // 1. Успешная проверка (верная пара логин/пароль)
        [TestMethod]
        public void TestMethod_ValidateCredentials_Success()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.FindByLogin("ivanov"))
                .Returns(new User
                {
                    Login = "ivanov",
                    Password = "EmpPass123!",
                    Role = Role.техник,
                    FullName = "Иванов Иван Иванович"
                });

            IUserRepository repository = mock.Object;
            AuthService authService = new AuthService(repository);

            bool flag = authService.ValidateCredentials("ivanov", "EmpPass123!");

            Assert.IsTrue(flag);
        }

        // 2. Передача неверного пароля
        [TestMethod]
        public void TestMethod_ValidateCredentials_WrongPassword()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.FindByLogin("ivanov"))
                .Returns(new User
                {
                    Login = "ivanov",
                    Password = "EmpPass123!",
                    Role = Role.техник,
                    FullName = "Иванов Иван Иванович"
                });

            IUserRepository repository = mock.Object;
            AuthService authService = new AuthService(repository);

            bool flag = authService.ValidateCredentials("ivanov", "WrongPassword");

            Assert.IsFalse(flag);
        }

        // 3. Передача несуществующего логина
        [TestMethod]
        public void TestMethod_ValidateCredentials_UserNotFound()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.FindByLogin("unknown_user"))
                .Returns((User)null);

            IUserRepository repository = mock.Object;
            AuthService authService = new AuthService(repository);

            bool flag = authService.ValidateCredentials("unknown_user", "EmpPass123!");

            Assert.IsFalse(flag);
        }

        // 4. Определение роли администратора
        [TestMethod]
        public void TestMethod_GetUserRole_Admin()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.FindByLogin("admin_petr"))
                .Returns(new User
                {
                    Login = "admin_petr",
                    Password = "AdminRoot777",
                    Role = Role.администратор,
                    FullName = "Петров Пётр Петрович"
                });

            IUserRepository repository = mock.Object;
            AuthService authService = new AuthService(repository);

            string role = authService.GetUserRole("admin_petr");

            Assert.AreEqual("администратор", role);
        }

        // 5. Успешный вход и получение объекта пользователя
        [TestMethod]
        public void TestMethod_Login_Success()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(repo => repo.FindByLogin("ivanov"))
                .Returns(new User
                {
                    Login = "ivanov",
                    Password = "EmpPass123!",
                    Role = Role.техник,
                    FullName = "Иванов Иван Иванович"
                });

            IUserRepository repository = mock.Object;
            AuthService authService = new AuthService(repository);

            string errorMessage;
            User user = authService.Login("ivanov", "EmpPass123!", out errorMessage);

            Assert.IsNotNull(user);
            Assert.AreEqual("ivanov", user.Login);
            Assert.AreEqual(Role.техник, user.Role);
            Assert.AreEqual(string.Empty, errorMessage);
        }
    }
}