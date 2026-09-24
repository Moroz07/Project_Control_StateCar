using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlStateCar
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Параметры подключения к PostgreSQL
            string connectionString = "Host=localhost;Port=5432;Database=CompanyDB;Username=postgres;Password=your_password;";

            // Инициализация сервисов и слоя данных
            IUserRepository userRepository = new DBConnection(connectionString);
            AuthService authService = new AuthService(userRepository);
            UserValidator validator = new UserValidator();

            // Основной цикл жизненного цикла сессий
            while (true)
            {
                // 1. Отображение формы авторизации
                using (var loginForm = new LoginForm(authService, validator))
                {
                    // Если пользователь закрыл форму крестиком — корректный выход из приложения
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        break;
                    }
                }

                // 2. При успешном входе открывается форма профиля
                using (var profileForm = new ProfileForm(userRepository, validator))
                {
                    profileForm.ShowDialog();
                }

                // 3. После закрытия формы профиля (нажатие кнопки «Выйти») очищаются данные сессии
                Session.EndSession();
            }
        }
    }
}
