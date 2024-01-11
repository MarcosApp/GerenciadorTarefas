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
        private readonly IValidator<UpdateTaskInput> _updateTaskInputValidator;
        public TaskController(ITaskUseCase taskUseCase, IValidator<AddTaskInput> taskInputValidator, IValidator<UpdateTaskInput> updateTaskInputValidator)
        {
            _taskUseCase = taskUseCase;
            _taskInputValidator = taskInputValidator;
            _updateTaskInputValidator = updateTaskInputValidator;
        }

        [HttpGet]
        public IActionResult ListTask()
        {
            var projects = _taskUseCase.ListTask();

            if (projects.Count == 0) return Ok("Nenhuma task criado.");

            return Ok(projects);
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

        [HttpPut]
        public IActionResult UpdateTask(UpdateTaskInput input)
        {
            var validationResult = _updateTaskInputValidator.Validate(input);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());

            var task = new Domain.Entities.Task(input.Id, input.Nome, input.Descricao, input.Status, System.DateTime.Now);

            var valueId = _taskUseCase.UpdateTask(task);

            if (valueId == 0) return BadRequest("Código de Tarefa Inválido");

            input.DataAtualizacao = task.DataAtualizacao;

            return Ok(input);
        }
    }
}
