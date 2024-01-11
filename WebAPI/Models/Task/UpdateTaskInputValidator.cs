using FluentValidation;
using System;

namespace WebAPI.Models.Task
{
    public class UpdateTaskInputValidator : AbstractValidator<UpdateTaskInput>
    {
        public UpdateTaskInputValidator()
        {
            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Campo obrigatório")
            .Length(5, 50).WithMessage("Mínimo de {MinLength} e máximo de {MaxLength} caracteres no nome do projeto");

            RuleFor(x => x.Descricao).NotEmpty().WithMessage("Campo obrigatório")
            .Length(5, 500).WithMessage("Mínimo de {MinLength} e máximo de {MaxLength} caracteres na descrição do projeto");

            RuleFor(x => x.Status).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(x => x.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
