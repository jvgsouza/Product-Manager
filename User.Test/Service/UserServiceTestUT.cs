using Bogus;
using Moq;
using Usuario.Application.Services;
using Usuario.Domain.DTOs;
using Usuario.Domain.Exceptions;
using Usuario.Domain.Repositories;
using Usuario.Domain.Services;
using Xunit;

namespace Usuario.Test.Service
{
    public class UserServiceTestUT
    {
        private readonly IUserService _userService;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Domain.Entities.User _user;
        private readonly Login _login;

        public UserServiceTestUT()
        {
            _userRepository = new Mock<IUserRepository>();
            _userService = new UserService(_userRepository.Object);

            _user = new Faker<Domain.Entities.User>()
                 .RuleFor(p => p.Email, p => p.Person.Email)
                 .RuleFor(p => p.Password, p => p.Internet.Password(8))
                 .RuleFor(p => p.Id, p => p.Random.Int());

            _login = new Faker<Login>()
                 .RuleFor(p => p.Email, p => p.Person.Email)
                 .RuleFor(p => p.Password, p => p.Internet.Password(8));
        }

        [Fact]
        public void SuccessLogin()
        {
            _userRepository.Setup(a => a.Login(_login)).Returns(_user);

            var user = _userService.Login(_login);

            Assert.NotNull(user);
        }

        [Fact]
        public void FailLogin()
        {
            _login.Password = "";

            Assert.Throws<ApiException>(() => _userService.Login(_login));
        }
    }
}
