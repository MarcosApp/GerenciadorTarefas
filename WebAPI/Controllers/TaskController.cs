using Domain.Contracts.Enumerator.Task;
using Domain.Contracts.UseCases.AddProject;
using Domain.Contracts.UseCases.AddTask;
using Domain.Contracts.UseCases.AddUser;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Error;
using WebAPI.Models.Task;

namespace WebAPI.Controllers
{

    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IUserUseCase _userUseCase;
        private readonly IProjectUseCase _projectUseCase;
        private readonly ITaskUseCase _taskUseCase;
        private readonly IValidator<AddTaskInput> _taskInputValidator;
        private readonly IValidator<UpdateTaskInput> _updateTaskInputValidator;
        public TaskController(ITaskUseCase taskUseCase, IProjectUseCase projectUseCase, IUserUseCase userUseCase, IValidator<AddTaskInput> taskInputValidator, IValidator<UpdateTaskInput> updateTaskInputValidator)
        {
            _taskUseCase = taskUseCase;
            _projectUseCase = projectUseCase;
            _userUseCase = userUseCase;
            _taskInputValidator = taskInputValidator;
            _updateTaskInputValidator = updateTaskInputValidator;
        }

        [HttpGet("api/Task")]
        public IActionResult ListTask()
        {
            var projects = _taskUseCase.ListTask();

            if (projects.Count == 0) 
                return Ok("Nenhuma task criado.");

            return Ok(projects);
        }

        [HttpGet("api/Task/{id}")]
        public IActionResult ListTaskId(int id)
        {
            var validationResult = new ListTaskInputValidator().Validate(id);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());

            var task = _taskUseCase.ListTask(id);

            if (task == null) 
                return NotFound("Task não encontrada.");

            return Ok(task);
        }

        [HttpPost("api/Task")]
        public IActionResult AddTask(AddTaskInput input)
        {
            var validationResult = _taskInputValidator.Validate(input);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());

            var user = _userUseCase.ListUser(input.UsuarioId);
            if (user == null)
                return NotFound("Usuário não encontrada.");

            var countProjectTask = _projectUseCase.CountProject(input.ProjectId);

            if (countProjectTask > 20) 
                return BadRequest("Esse projeto não é mais permitido adicionar tarefas máximo 20");

            var task = new Domain.Entities.Task(0, input.Nome, input.Descricao, input.Status, input.Prioridade, 
                                                    input.DataCriacao, input.DataAtualizacao, input.ProjectId, input.UsuarioId
                                                );

            var valueId = _taskUseCase.AddTask(task);

            task.Id = valueId;

            return Created("", task);
        }

        [HttpPut("api/Update")]
        public IActionResult UpdateTask(UpdateTaskInput input)
        {
            var validationResult = _updateTaskInputValidator.Validate(input);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());

            var task = new Domain.Entities.Task(input.Id, input.Nome, input.Descricao, input.Status, System.DateTime.Now);

            var valueId = _taskUseCase.UpdateTask(task);

            if (valueId == 0) 
                return BadRequest("Código de Tarefa Inválido");

            input.DataAtualizacao = task.DataAtualizacao;

            return Ok(input);
        }

        [HttpDelete("api/Delete/{id}")]
        public IActionResult DeleteTask(int id)
        {
            var validationResult = new DeleteTaskInputValidator().Validate(id);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());

            var task = _taskUseCase.ListTask(id);

            if (task == null)
                return NotFound("Task não encontrada.");

            if (task.Status != TaskStatus.Done)
                return BadRequest("Esta tarefa não pode ser excluída porque ainda não foi concluída.");

            var valueId = _taskUseCase.DeleteTask(id);

            if (valueId == 0) 
                return BadRequest("Código de Tarefa Inválido");

            return Ok("Tarefa exluida com sucesso.");
        }
    }
}
