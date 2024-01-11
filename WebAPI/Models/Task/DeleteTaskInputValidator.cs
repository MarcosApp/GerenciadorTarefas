using FluentValidation;
using System;

namespace WebAPI.Models.Task
{
    public class DeleteTaskInputValidator : AbstractValidator<DeleteTaskInput>
    {
        public DeleteTaskInputValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
