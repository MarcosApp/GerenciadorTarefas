using Domain.Contracts.UseCases.AddUser;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Error;
using WebAPI.Models.User;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserUseCase _userUseCase;
        private readonly IValidator<AddUserInput> _addUserInputValidator;

        public UserController(IUserUseCase userUseCase, IValidator<AddUserInput> addUserInputValidator)
        {
            _userUseCase = userUseCase;
            _addUserInputValidator = addUserInputValidator;
        }

        [HttpGet("api/User")]
        public IActionResult ListUser()
        {
            var users = _userUseCase.ListUser();

            if (users.Count == 0)
                return Ok("Nenhuma usuário encontrado.");

            return Ok(users);
        }

        [HttpPost("api/User")]
        public IActionResult AddUser(AddUserInput input)
        {
            var validationResult = _addUserInputValidator.Validate(input);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());           

            var user = new Domain.Entities.User(0, input.Nome, input.Email, input.Senha, input.Perfil);

            var valueId = _userUseCase.AddUser(user);

            user.Id = valueId;

            return Created("", user);
        }
    }
}
