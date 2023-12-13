using Xunit;
using Moq;
using Usuario.Application.Exceptions;
using Usuario.Application.Services;
using Usuario.Domain.DTOs;
using Usuario.Domain.Repositories;
using Usuario.Domain.Services;

namespace Usuario.Test.Service
{
    public class UserServiceTestUT
    {
        private readonly IUserService _userService;
        public readonly Mock<IUserRepository> _userRepository = new();
        private readonly Domain.Entities.User _user;
        private readonly Login _login;

        public UserServiceTestUT()
        {
            _userRepository = new Mock<IUserRepository>();
            _userService = new UserService(_userRepository.Object);
            _user = new Domain.Entities.User
            {
                Email = "teste@gmail.com",
                Password = "12345678",
                Id = 1
            };
            _login = new Login()
            {
                Email = "teste@gmail.com",
                Password = "12345678",
            };
        }

        [Fact]
        public void SuccessLogin()
        {
            _userRepository.Setup(a => a.Login(_login)).Returns(_user);

            var user = _userService.Login(_login);

            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public void FailLogin()
        {
            _login.Password = "1234567891";
            _userRepository.Setup(a => a.Login(_login));

            var user = _userService.Login(_login);

            Assert.Null(user);
        }
    }
}
