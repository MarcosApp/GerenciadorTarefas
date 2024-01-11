using Domain.Contracts.UseCases.Comentario;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Comment;
using WebAPI.Models.Error;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentUseCase _commentUseCase;
        private readonly IValidator<AddCommentInput> _commentInputValidator;

        public CommentController(ICommentUseCase commentUseCase, IValidator<AddCommentInput> commentInputValidator)
        {
            _commentUseCase = commentUseCase;
            _commentInputValidator = commentInputValidator;
        }

        [HttpPost]
        public IActionResult AddComment(AddCommentInput input)
        {
            var validationResult = _commentInputValidator.Validate(input);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());

            var comment = new Domain.Entities.Comment(0, input.Texto, input.UsuarioId, input.TaskId);

            var valueId = _commentUseCase.AddComment(comment);
            comment.Id = valueId;

            return Created("", comment);
        }
    }
}
