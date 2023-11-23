using Usuario.Domain.DTOs;
using FluentValidation;

namespace Usuario.Application.Validator
{
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(login => login.Email).NotEmpty();
            RuleFor(login => login.Email).Length(12, 255);
            RuleFor(login => login.Email).EmailAddress();

            RuleFor(login => login.Password).NotEmpty();
            RuleFor(login => login.Password).Length(8, 30);
        }
    }
}
