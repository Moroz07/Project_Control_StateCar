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
    public partial class ProfileForm : Form
    {
        private readonly IUserRepository _userRepository;
        private readonly UserValidator _validator;

        public ProfileForm(IUserRepository userRepository, UserValidator validator)
        {
            InitializeComponent();
            _userRepository = userRepository;
            _validator = validator;

            LoadProfileData();
        }

        private void LoadProfileData()
        {
            lblUserInfo.Text = $"Пользователь: {Session.GetCurrentUser()} | Роль: {Session.Role}";
            btnRegisterEmployee.Visible = (Session.Role == Role.Admin.ToString());
        }

        private void btnRegisterEmployee_Click(object sender, EventArgs e)
        {
            using (var regForm = new EmployeeEditForm(_userRepository, _validator))
            {
                regForm.ShowDialog();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
