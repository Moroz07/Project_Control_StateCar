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
        private readonly AuthService authService_;
        private readonly UserValidator validator_;

        public LoginForm(AuthService authService, UserValidator validator)
        {
            InitializeComponent();
            authService_ = authService;
            validator_ = validator;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (!validator_.ValidateLogin(login))
            {
                errorProvider.SetError(txtLogin, "Логин должен быть от 3 до 50 символов (только латиница и цифры)");
                txtLogin.Focus();
                return;
            }

            if (!validator_.ValidatePassword(password))
            {
                errorProvider.SetError(txtPassword, "Пароль должен быть от 6 до 100 символов без пробелов");
                txtPassword.Focus();
                return;
            }

            if (authService_.Authorize(login, password, out string errorMessage))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(errorMessage, "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
    }
}
