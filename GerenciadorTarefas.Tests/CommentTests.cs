using Domain.Contracts.UseCases.AddTask;
using Domain.Contracts.UseCases.AddUser;
using Domain.Contracts.UseCases.Comentario;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Models.Comment;
using Xunit;

namespace GerenciadorTarefas.Tests
{
    public class CommentTests
    {
        [Fact]
        public void AddComment_ReturnsCreated_WhenCommentIsValid()
        {
            // Arrange
            var commentUseCaseMock = new Mock<ICommentUseCase>();
            commentUseCaseMock.Setup(cc => cc.AddComment(It.IsAny<Comment>())).Returns(1);

            var userUseCaseMock = new Mock<IUserUseCase>();
            userUseCaseMock.Setup(uc => uc.ListUser(It.IsAny<int>())).Returns(new User());

            var taskUseCaseMock = new Mock<ITaskUseCase>();
            taskUseCaseMock.Setup(tc => tc.ListTask(It.IsAny<int>())).Returns(new Task());

            var validatorMock = new Mock<IValidator<AddCommentInput>>();
            validatorMock.Setup(v => v.Validate(It.IsAny<AddCommentInput>())).Returns(new FluentValidation.Results.ValidationResult());

            var controller = new CommentController(
                userUseCaseMock.Object,
                commentUseCaseMock.Object,
                taskUseCaseMock.Object,
                validatorMock.Object
            );

            // Act
            var result = controller.AddComment(new AddCommentInput());

            // Assert
            Assert.IsType<CreatedResult>(result);
            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);

            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public void AddComment_ReturnsBadRequest_WhenUserIsNotFound()
        {
            // Arrange
            var commentUseCaseMock = new Mock<ICommentUseCase>();
            var userUseCaseMock = new Mock<IUserUseCase>();
            userUseCaseMock.Setup(uc => uc.ListUser(It.IsAny<int>())).Returns((User)null);

            var taskUseCaseMock = new Mock<ITaskUseCase>();
            var validatorMock = new Mock<IValidator<AddCommentInput>>();
            validatorMock.Setup(v => v.Validate(It.IsAny<AddCommentInput>())).Returns(new FluentValidation.Results.ValidationResult());

            var controller = new CommentController(
                userUseCaseMock.Object,
                commentUseCaseMock.Object,
                taskUseCaseMock.Object,
                validatorMock.Object
            );

            // Act
            var result = controller.AddComment(new AddCommentInput());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);

            Assert.Equal("Usuário não encontrada.", notFoundResult.Value);
        }
    }
}
