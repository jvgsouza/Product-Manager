using Usuario.Domain.Entities;

namespace Usuario.Domain.Repositories
{
    public interface IUser
    {
        User Login();
    }
}
