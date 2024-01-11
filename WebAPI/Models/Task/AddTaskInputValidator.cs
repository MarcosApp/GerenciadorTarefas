using FluentValidation;
using System;

namespace WebAPI.Models.Task
{
    public class AddTaskInputValidator : AbstractValidator<AddTaskInput>
    {
        public AddTaskInputValidator()
        {
            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Campo obrigatório")
            .Length(5, 50).WithMessage("Mínimo de {MinLength} e máximo de {MaxLength} caracteres no nome do projeto");

            RuleFor(x => x.Descricao).NotEmpty().WithMessage("Campo obrigatório")
            .Length(5, 500).WithMessage("Mínimo de {MinLength} e máximo de {MaxLength} caracteres na descrição do projeto");

            RuleFor(x => x.Prioridade).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(x => x.Status).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(x => x.UsuarioId).NotEmpty().WithMessage("Campo obrigatório")
            .GreaterThan(0)
            .WithMessage("O ID deve ser maior que 0.");
        }
    }
}
