using Npgsql;

namespace ControlStateCar
{
    public interface IDBConnection
    {
        NpgsqlConnection Connection { get; }
        bool CheckIfLoginExists(string login);
        bool AddUser(User user);
        User FindByLogin(string login);
    }
}