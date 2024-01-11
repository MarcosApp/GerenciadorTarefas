using Domain.Contracts.UseCases.AddProject;
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
        private readonly IProjectUseCase _projectUseCase;
        private readonly ITaskUseCase _taskUseCase;
        private readonly IValidator<AddTaskInput> _taskInputValidator;
        private readonly IValidator<UpdateTaskInput> _updateTaskInputValidator;
        private readonly IValidator<DeleteTaskInput> _deleteTaskInputValidator;
        public TaskController(ITaskUseCase taskUseCase, IProjectUseCase projectUseCase, IValidator<AddTaskInput> taskInputValidator, IValidator<UpdateTaskInput> updateTaskInputValidator, IValidator<DeleteTaskInput> deleteTaskInputValidator)
        {
            _taskUseCase = taskUseCase;
            _projectUseCase = projectUseCase;
            _taskInputValidator = taskInputValidator;
            _updateTaskInputValidator = updateTaskInputValidator;
            _deleteTaskInputValidator = deleteTaskInputValidator;
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

            var countProjectTask = _projectUseCase.CountProject(input.ProjectId);

            if (countProjectTask > 20) return BadRequest("Esse projeto não é mais permitido adicionar tarefas máximo 20");

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

        [HttpDelete]
        public IActionResult DeleteTask(DeleteTaskInput input)
        {
            var validationResult = _deleteTaskInputValidator.Validate(input);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());

            var valueId = _taskUseCase.DeleteTask(input.Id);

            if (valueId == 0) return NotFound("Código de Tarefa Inválido");

            return Ok(input);
        }
    }
}
