using Moq;
using Usuario.Application.Services;
using Usuario.Domain.DTOs;
using Usuario.Domain.Entities;
using Usuario.Domain.Repositories;
using Usuario.Domain.Services;

namespace User.Test
{
    public class UserServiceTest
    {
        private readonly IUserService _userService;
        public readonly Mock<IUserRepository> _userRepository = new();
        private readonly Usuario.Domain.Entities.User _user;

        public UserServiceTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _userService = new UserService(_userRepository.Object);
            _user = new Usuario.Domain.Entities.User 
            {
                Email = "teste@gmail.com",
                Password = "12345678",
                Id = 1
            };
        }

        [Fact]
        public void SuccessLogin()
        {
            var login = new Login()
            {
                Email = "teste@gmail.com",
                Password = "12345678",
            };
            _userRepository.Setup(a => a.Login(login)).Returns(_user);

            var user = _userService.Login(login);
            
            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
        }
    }
}
