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
            txtPhone.Mask = "+7 (999) 999-99-99";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (cmbRole.SelectedItem == null)
            {
                errorProvider.SetError(cmbRole, "Выберите роль сотрудника");
                return;
            }

            Role selectedRole = (Role)cmbRole.SelectedItem;

            var user = new User
            {
                Login = txtLogin.Text.Trim(),
                Password = txtPassword.Text,
                FullName = txtFullName.Text.Trim(),
                Role = selectedRole,
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim()
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
                errorProvider.SetError(txtFullName, "ФИО не должно быть пустым (буквы, дефисы и пробелы)");
                txtFullName.Focus();
                return;
            }

            if (!validator_.ValidateRole(user.Role))
            {
                errorProvider.SetError(cmbRole, "Указана недопустимая роль");
                cmbRole.Focus();
                return;
            }

            if (!validator_.ValidateEmail(user.Email))
            {
                errorProvider.SetError(txtEmail, "Некорректный формат email (имя@домен.зона)");
                txtEmail.Focus();
                return;
            }

            if (!validator_.ValidatePhone(user.Phone))
            {
                errorProvider.SetError(txtPhone, "Номер должен соответствовать маске +7 (XXX) XXX-XX-XX");
                txtPhone.Focus();
                return;
            }

            if (userRepository_.CheckIfLoginExists(user.Login))
            {
                errorProvider.SetError(txtLogin, "Пользователь с таким логином уже существует");
                txtLogin.Focus();
                return;
            }

            if (userRepository_.AddUser(user))
            {
                MessageBox.Show("Сотрудник успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка записи в базу данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EmployeeEditForm_Load(object sender, EventArgs e)
        {
        }
    }
}