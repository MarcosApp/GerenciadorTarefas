﻿using FluentValidation;
using System;

namespace WebAPI.Models.Task
{
    public class DeleteTaskInputValidator : AbstractValidator<int>
    {
        public DeleteTaskInputValidator()
        {
            RuleFor(id => id).NotEmpty().WithMessage("Campo obrigatório")
            .GreaterThan(0)
            .WithMessage("O ID deve ser maior que 0.");
        }
    }
}
