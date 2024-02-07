using Bogus;
using FluentValidation.TestHelper;
using Usuario.Application.Validator;
using Usuario.Domain.DTOs;
using Xunit;

namespace Usuario.Test.Validator
{
    public class LoginValidatorTestUT
    {
        private readonly LoginValidator _loginValidator;
        private readonly Login _login;

        public LoginValidatorTestUT()
        {
            _loginValidator = new LoginValidator();
            _login = new Faker<Login>()
                .RuleFor(p => p.Email, p => p.Person.Email)
                .RuleFor(p => p.Password, p => p.Internet.Password(8));
        }

        [Fact]
        public void EmptyEmail()
        {
            _login.Email = "";

            var result = _loginValidator.TestValidate(_login);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Preencha o campo de email!");
        }

        [Fact]
        public void InvalidEmail()
        {
            _login.Email = "teste.gmail.com";

            var result = _loginValidator.TestValidate(_login);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Informe um email válido!");
        }

        [Theory]
        [InlineData("tt@gmail.m")]
        [InlineData("abcdefghijklmnopqrstuvwxyabcdefghijklmnopqrstuvwxyabcdefghijklmnopqrstuvwxyz@gmail.com")]
        public void NumberOfInvalidEmailCharacters(string email)
        {
            _login.Email = email;

            var result = _loginValidator.TestValidate(_login);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("O campo email deve conter de 12 a 80 caracteres");
        }

        [Fact]
        public void NullEmail()
        {
            _login.Email = string.Empty;

            var result = _loginValidator.TestValidate(_login);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Preencha o campo de email!");
        }

        [Fact]
        public void EmptyPassword()
        {
            _login.Password = "";

            var result = _loginValidator.TestValidate(_login);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Preencha o campo de senha!");
        }

        [Theory]
        [InlineData("1234567")]
        [InlineData("1234567891234567891234567891234")]
        public void NumberOfInvalidPasswordCharacters(string password)
        {
            _login.Password = password;

            var result = _loginValidator.TestValidate(_login);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("O campo senha deve conter de 8 a 30 caracteres");
        }

        [Fact]
        public void NullPassword()
        {
            _login.Password = string.Empty;

            var result = _loginValidator.TestValidate(_login);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage("Preencha o campo de senha!");
        }
    }
}
