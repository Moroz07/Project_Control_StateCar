using System;
using System.Windows.Forms;

namespace ControlStateCar
{
    public partial class ProfileForm : Form
    {
        private User _currentUser;
        private IUserRepository _userRepository;
        private UserValidator _validator;

        public ProfileForm(User user, IUserRepository userRepository = null, UserValidator validator = null)
        {
            InitializeComponent();
            _currentUser = user;
            _userRepository = userRepository;
            _validator = validator;

            LoadProfileData();
        }

        private void LoadProfileData()
        {
            if (_currentUser == null)
            {
                return;
            }

            string displayName;
            if (_currentUser.FullName != null && _currentUser.FullName.Trim() != "")
            {
                displayName = _currentUser.FullName;
            }
            else
            {
                displayName = _currentUser.Login;
            }

            lblUserInfo.Text = $"Пользователь: {displayName} | Роль: {_currentUser.Role}";

            if (_currentUser.Role == Role.администратор)
            {
                btnRegisterEmployee.Visible = true;
            }
            else
            {
                btnRegisterEmployee.Visible = false;
            }
        }

        private void btnRegisterEmployee_Click(object sender, EventArgs e)
        {
            // using (var regForm = new EmployeeEditForm(_userRepository, _validator))
            // {
            //     regForm.ShowDialog();
            // }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}