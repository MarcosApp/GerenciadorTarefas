using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Project
{
    public class AddProjectInputValidator : AbstractValidator<AddProjectInput>
    {
        public AddProjectInputValidator()
        {
            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Campo obrigatório")
            .Length(5, 50).WithMessage("Mínimo de {MinLength} e máximo de {MaxLength} caracteres no nome do projeto");

            RuleFor(x => x.Descricao).NotEmpty().WithMessage("Campo obrigatório")
            .Length(5, 500).WithMessage("Mínimo de {MinLength} e máximo de {MaxLength} caracteres na descrição do projeto");

            RuleFor(x => x.DataInicio).NotEmpty().WithMessage("Campo obrigatório")
            .LessThan(p => DateTime.Now).WithMessage("A data deve estar no passado");

            RuleFor(x => x.DataFim).NotEmpty().WithMessage("Campo obrigatório")
            .LessThan(p => DateTime.Now).WithMessage("A data deve estar no passado");
        }
    }
}
