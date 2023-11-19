using Usuario.Domain.DTOs;
using Usuario.Domain.Entities;

namespace Usuario.Domain.Repositories
{
    public interface IUserRepository
    {
        User Login(Login login);
    }
}
