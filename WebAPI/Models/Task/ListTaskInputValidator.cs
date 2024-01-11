using FluentValidation;
using System;

namespace WebAPI.Models.Task
{
    public class ListTaskInputValidator : AbstractValidator<int>
    {
        public ListTaskInputValidator()
        {
            RuleFor(id => id).NotEmpty().WithMessage("Campo obrigatório")
            .GreaterThan(0)
            .WithMessage("O ID deve ser maior que 0.");
        }
    }
}
