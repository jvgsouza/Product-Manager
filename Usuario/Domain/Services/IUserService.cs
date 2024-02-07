using Usuario.Domain.DTOs;
using Usuario.Domain.Entities;

namespace Usuario.Domain.Services
{
    public interface IUserService
    {
        User Login(Login login);
    }
}
