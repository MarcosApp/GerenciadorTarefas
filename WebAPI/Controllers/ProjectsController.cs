using Domain.Contracts.UseCases.AddProject;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Error;
using WebAPI.Models.Project;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectUseCase _projectUseCase;
        private readonly IValidator<AddProjectInput> _projectInputValidator;
        public ProjectsController(IProjectUseCase projectUseCase, IValidator<AddProjectInput> projectInputValidator)
        {
            _projectUseCase = projectUseCase;
            _projectInputValidator = projectInputValidator;
        }

        [HttpPost]
        public IActionResult AddProject(AddProjectInput input)
        {
            var validationResult = _projectInputValidator.Validate(input);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());

            var project = new Domain.Entities.Project(0, input.Nome, input.Descricao, input.DataInicio, input.DataFim);

            var valueId = _projectUseCase.AddProject(project);

            project.Id = valueId;

            return Created("", project);
        }

        [HttpGet]
        public IActionResult ListProject()
        {
            var projects = _projectUseCase.ListProject();

            if (projects.Count == 0) return Ok("Nenhum projeto criado.");

            return Ok(projects);
        }
    }
}
