using Domain.Contracts.UseCases.AddTask;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Error;
using WebAPI.Models.Task;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly ITaskUseCase _taskUseCase;
        private readonly IValidator<AddTaskInput> _taskInputValidator;
        public TaskController(ITaskUseCase taskUseCase, IValidator<AddTaskInput> taskInputValidator)
        {
            _taskUseCase = taskUseCase;
            _taskInputValidator = taskInputValidator;
        }

        [HttpPost]
        public IActionResult AddTask(AddTaskInput input)
        {
            var validationResult = _taskInputValidator.Validate(input);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());

            var task = new Domain.Entities.Task(0, input.Nome, input.Descricao, input.Status, input.Prioridade, 
                                                    input.DataCriacao, input.DataAtualizacao, input.ProjectId
                                                );

            var valueId = _taskUseCase.AddTask(task);

            task.Id = valueId;

            return Created("", task);
        }
    }
}
