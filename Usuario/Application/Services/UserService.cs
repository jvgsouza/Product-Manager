using Usuario.Application.Validator;
using Usuario.Domain.DTOs;
using Usuario.Domain.Entities;
using Usuario.Domain.Exceptions;
using Usuario.Domain.Repositories;
using Usuario.Domain.Services;

namespace Usuario.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }

        public User Login(Login login)
        {
            var loginValidator = new LoginValidator();
            var valid = loginValidator.Validate(login);
            if (valid.IsValid)
            {
                return _userRepository.Login(login);
            }
            else
            {
                var errors = string.Join(", ", valid.Errors.Select( x => x.ErrorMessage));
                throw new APIException(errors!);
            }
        }
    }
}
