using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlStateCar
{
    public partial class LoginForm : Form
    {
        private  AuthService _authService;
        private  UserValidator _validator;

        public LoginForm(AuthService authService, UserValidator validator)
        {
            InitializeComponent();
            _authService = authService;
            _validator = validator;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (!_validator.ValidateLogin(login))
            {
                MessageBox.Show("Некорректный формат логина.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_validator.ValidatePassword(password))
            {
                MessageBox.Show("Некорректный формат пароля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string errorMessage;
            User user = _authService.Login(login, password, out errorMessage);

            if (user != null)
            {
                ProfileForm profileForm = new ProfileForm(user, null, _validator);

                profileForm.FormClosed += (s, args) => this.Close();

                Hide();
                profileForm.Show();
            }
            else
            {
                MessageBox.Show(errorMessage, "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

