using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Comment
{
    public class AddCommentInputValidator : AbstractValidator<AddCommentInput>
    {
        public AddCommentInputValidator()
        {
            RuleFor(x => x.Texto)
            .NotEmpty().WithMessage("Campo obrigatório")
            .Length(5, 500).WithMessage("Mínimo de {MinLength} e máximo de {MaxLength} caracteres no nome do projeto");

            RuleFor(x => x.TaskId).NotEmpty().WithMessage("Campo obrigatório")
           .GreaterThan(0)
           .WithMessage("O ID deve ser maior que 0.");

            RuleFor(x => x.UsuarioId).NotEmpty().WithMessage("Campo obrigatório")
            .GreaterThan(0)
            .WithMessage("O Usuario deve ser maior que 0.");
        }
    }
}
