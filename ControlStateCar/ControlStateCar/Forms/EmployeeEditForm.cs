using System;
using System.Windows.Forms;
using CarsharingClassLibrary;

namespace ControlStateCar
{
    public partial class EmployeeEditForm : Form
    {
        private RegService _regService;

        public EmployeeEditForm(IDBConnection userRepository, UserValidator validator)
        {
            InitializeComponent();
            _regService = new RegService(userRepository, validator);

            cmbRole.DataSource = Enum.GetValues(typeof(Role));
            txtPhone.Mask = "+7 (999) 999-99-99";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var user = new User
            {
                Login = txtLogin.Text.Trim(),
                Password = txtPassword.Text,
                FullName = txtFullName.Text.Trim(),
                Role = cmbRole.SelectedItem != null ? (Role)cmbRole.SelectedItem : Role.техник,
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim()
            };

            string errorMessage;
            if (!_regService.Register(user, out errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Сотрудник успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}