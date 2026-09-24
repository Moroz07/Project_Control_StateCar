using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlStateCar
{
    public interface IUserRepository
    {
        bool CheckIfLoginExists(string login);
        bool AddUser(User user);
        User FindByLogin(string login);
    }
}
