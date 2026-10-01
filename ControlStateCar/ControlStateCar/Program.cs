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

            string connectionString = "Host=localhost;Port=5432;Database=CompanyDB;Username=postgres;Password=123456;";

            IUserRepository userRepository = new DBConnection(connectionString);
            UserValidator validator = new UserValidator();
            AuthService authService = new AuthService(userRepository);
            

            while (true)
            {
                using (var loginForm = new LoginForm(authService, validator))
                {
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        break;
                    }
                }

                using (var profileForm = new ProfileForm(userRepository, validator))
                {
                    profileForm.ShowDialog();
                }

                Session.EndSession();
            }
        }
    }
}
