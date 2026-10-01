using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStateCar
{
    public class RegistrationService
    {
        private readonly IUserRepository userRepository_;

        public RegistrationService(IUserRepository userRepository)
        {
            userRepository_ = userRepository;
        }
        public bool CheckLoginUniqueness(string login)
        {
            return userRepository_.CheckIfLoginExists(login);
        }

        public User CreateNewEmployee(string login, string password, string role, string fullName, string email, string phone)
        {
            var user = new User();
            user.SetLogin(login);
            user.SetPassword(password);
            user.SetRole(role);
            user.FullName = fullName;
            user.SetContactInfo(email, phone);

            return user;
        }
        public bool SaveUser(User user)
        {
            return userRepository_.AddUser(user);
        }
    }
}
