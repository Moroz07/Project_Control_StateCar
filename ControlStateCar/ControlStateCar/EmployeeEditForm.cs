using System;
using System.Windows.Forms;

namespace ControlStateCar
{
    public partial class EmployeeEditForm : Form
    {
        private IUserRepository userRepository_;
        private UserValidator validator_;

        public EmployeeEditForm(IUserRepository userRepository, UserValidator validator)
        {
            InitializeComponent();
            userRepository_ = userRepository;
            validator_ = validator;

            cmbRole.DataSource = Enum.GetValues(typeof(Role));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            var user = new User
            {
                Login = txtLogin.Text.Trim(),
                Password = txtPassword.Text,
                FullName = txtFullName.Text.Trim(),
                Role = cmbRole.SelectedItem?.ToString(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim()
            };

            if (!validator_.ValidateLogin(user.Login))
            {
                errorProvider.SetError(txtLogin, "Логин должен быть от 3 до 50 символов (только латиница и цифры)");
                txtLogin.Focus();
                return;
            }

            if (!validator_.ValidatePassword(user.Password))
            {
                errorProvider.SetError(txtPassword, "Пароль должен быть от 6 до 100 символов без пробелов");
                txtPassword.Focus();
                return;
            }

            if (!validator_.ValidateFullName(user.FullName))
            {
                errorProvider.SetError(txtFullName, "ФИО не должно быть пустым (только буквы, дефисы и пробелы)");
                txtFullName.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(user.Email) && !validator_.ValidateEmail(user.Email))
            {
                errorProvider.SetError(txtEmail, "Некорректный формат email");
                txtEmail.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(user.Phone) && !validator_.ValidatePhone(user.Phone))
            {
                errorProvider.SetError(txtPhone, "Формат телефона: +7 (XXX) XXX-XX-XX");
                txtPhone.Focus();
                return;
            }

            if (!userRepository_.CheckIfLoginExists(user.Login))
            {
                errorProvider.SetError(txtLogin, "Пользователь с таким логином уже существует");
                txtLogin.Focus();
                return;
            }
            if (userRepository_.AddUser(user))
            {
                MessageBox.Show("Сотрудник успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка записи в базу данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
