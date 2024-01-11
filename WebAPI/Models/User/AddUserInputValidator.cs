using FluentValidation;
using System;
using WebAPI.Models.User;

namespace WebAPI.Models.User
{
    public class AddUserInputValidator : AbstractValidator<AddUserInput>
    {
        public AddUserInputValidator()
        {
            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Campo obrigatório")
            .Length(5, 50).WithMessage("Mínimo de {MinLength} e máximo de {MaxLength} caracteres no nome do projeto");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Campo obrigatório")
            .Length(5, 100).WithMessage("Mínimo de {MinLength} e máximo de {MaxLength} caracteres na descrição do projeto");

            RuleFor(x => x.Senha).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(x => x.Perfil).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
