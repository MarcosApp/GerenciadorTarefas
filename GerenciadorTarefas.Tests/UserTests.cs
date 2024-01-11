using Domain.Contracts.UseCases.AddUser;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;
using WebAPI.Models.User;
using Xunit;

namespace GerenciadorTarefas.Tests
{
    public class UserTests
    {
        [Fact]
        public void ListUser_ReturnsOk_WhenUsersExist()
        {
            // Arrange
            var userUseCaseMock = new Mock<IUserUseCase>();
            userUseCaseMock.Setup(uc => uc.ListUser()).Returns(new List<User> { new User() });

            var controller = new UserController(
                userUseCaseMock.Object,
                Mock.Of<IValidator<AddUserInput>>()
            );

            // Act
            var result = controller.ListUser();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var users = okResult.Value as List<User>;
            Assert.NotNull(users);
        }

        [Fact]
        public void ListUser_ReturnsNotFound_WhenNoUsersExist()
        {
            // Arrange
            var userUseCaseMock = new Mock<IUserUseCase>();
            userUseCaseMock.Setup(uc => uc.ListUser()).Returns(new List<User>());

            var controller = new UserController(
                userUseCaseMock.Object,
                Mock.Of<IValidator<AddUserInput>>()
            );

            // Act
            var result = controller.ListUser();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);

            Assert.Equal("Nenhuma usuário encontrado.", notFoundResult.Value);
        }

        [Fact]
        public void AddUser_ReturnsCreated_WhenUserIsValid()
        {
            // Arrange
            var userUseCaseMock = new Mock<IUserUseCase>();
            userUseCaseMock.Setup(uc => uc.AddUser(It.IsAny<User>())).Returns(1);

            var validatorMock = new Mock<IValidator<AddUserInput>>();
            validatorMock.Setup(v => v.Validate(It.IsAny<AddUserInput>())).Returns(new FluentValidation.Results.ValidationResult());

            var controller = new UserController(
                userUseCaseMock.Object,
                validatorMock.Object
            );

            // Act
            var result = controller.AddUser(new AddUserInput());

            // Assert
            Assert.IsType<CreatedResult>(result);
            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);

            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public void AddUser_ReturnsBadRequest_WhenUserIsInvalid()
        {
            // Arrange
            var validatorMock = new Mock<IValidator<AddUserInput>>();
            validatorMock.Setup(v => v.Validate(It.IsAny<AddUserInput>())).Returns(new FluentValidation.Results.ValidationResult { Errors = { new FluentValidation.Results.ValidationFailure("Nome", "Nome é obrigatório.") } });

            var controller = new UserController(
                Mock.Of<IUserUseCase>(),
                validatorMock.Object
            );

            // Act
            var result = controller.AddUser(new AddUserInput());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);

            Assert.Equal("Nome é obrigatório.", badRequestResult.Value);
        }
    }
}
