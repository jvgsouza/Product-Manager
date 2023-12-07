using Moq;
using Usuario.Application.Exceptions;
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
        private readonly Login _login;

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
        public void EmptyEmail()
        {
            _login.Email = "";
            _userRepository.Setup(a => a.Login(_login)).Returns(_user);

            string message = Assert.Throws<APIException>(() => _userService.Login(_login)).Message;

            Assert.Equal("Preencha o campo de email!, O campo email deve conter de 12 a 80 caracteres, Informe um email válido!", message);
        }

        [Fact]
        public void InvalidEmail()
        {
            _login.Email = "teste.gmail.com";
            _userRepository.Setup(a => a.Login(_login)).Returns(_user);

            string message = Assert.Throws<APIException>(() => _userService.Login(_login)).Message;

            Assert.Equal("Informe um email válido!", message);
        }

        [Theory]
        [InlineData("tt@gmail.m")]
        [InlineData("abcdefghijklmnopqrstuvwxyabcdefghijklmnopqrstuvwxyabcdefghijklmnopqrstuvwxyz@gmail.com")]
        public void NumberOfInvalidEmailCharacters(string email)
        {
            _login.Email = email;
            _userRepository.Setup(a => a.Login(_login)).Returns(_user);

            string message = Assert.Throws<APIException>(() => _userService.Login(_login)).Message;

            Assert.Equal("O campo email deve conter de 12 a 80 caracteres", message);
        }

        [Fact]
        public void NullEmail()
        {
            _login.Email = null;
            _userRepository.Setup(a => a.Login(_login)).Returns(_user);

            string message = Assert.Throws<APIException>(() => _userService.Login(_login)).Message;

            Assert.Equal("Preencha o campo de email!", message);
        }

        [Fact]
        public void EmptyPassword()
        {
            _login.Password = "";
            _userRepository.Setup(a => a.Login(_login)).Returns(_user);

            string message = Assert.Throws<APIException>(() => _userService.Login(_login)).Message;

            Assert.Equal("Preencha o campo de senha!, O campo senha deve conter de 8 a 30 caracteres", message);
        }

        [Theory]
        [InlineData("1234567")]
        [InlineData("1234567891234567891234567891234")]
        public void NumberOfInvalidPasswordCharacters(string password)
        {
            _login.Password = password;
            _userRepository.Setup(a => a.Login(_login)).Returns(_user);

            string message = Assert.Throws<APIException>(() => _userService.Login(_login)).Message;

            Assert.Equal("O campo senha deve conter de 8 a 30 caracteres", message);
        }

        [Fact]
        public void NullPassword()
        {
            _login.Password = null;
            _userRepository.Setup(a => a.Login(_login)).Returns(_user);

            string message = Assert.Throws<APIException>(() => _userService.Login(_login)).Message;

            Assert.Equal("Preencha o campo de senha!", message);
        }
    }
}
