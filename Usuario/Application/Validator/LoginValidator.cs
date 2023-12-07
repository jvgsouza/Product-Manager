using Usuario.Domain.DTOs;
using FluentValidation;

namespace Usuario.Application.Validator
{
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(login => login.Email)
                .NotEmpty().WithMessage("Preencha o campo de email!")
                .Length(12, 80).WithMessage("O campo email deve conter de 12 a 80 caracteres")
                .EmailAddress().WithMessage("Informe um email válido!");

            RuleFor(login => login.Password)
                .NotEmpty().WithMessage("Preencha o campo de senha!")
                .Length(8, 30).WithMessage("O campo senha deve conter de 8 a 30 caracteres");
        }
    }
}
