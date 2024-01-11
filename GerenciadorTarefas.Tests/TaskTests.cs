using Domain.Contracts.UseCases.AddProject;
using Domain.Contracts.UseCases.AddTask;
using Domain.Contracts.UseCases.AddUser;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using WebAPI.Controllers;
using WebAPI.Models.Task;
using Xunit;

namespace GerenciadorTarefas.Tests
{
    public class TaskTests
    {
        [Fact]
        public void ListTask_ReturnsOk_WhenTasksExist()
        {
            // Arrange
            var taskUseCaseMock = new Mock<ITaskUseCase>();
            taskUseCaseMock.Setup(tc => tc.ListTask()).Returns(new List<Task>());

            var controller = new TaskController(
                taskUseCaseMock.Object,
                Mock.Of<IProjectUseCase>(),
                Mock.Of<IUserUseCase>(),
                Mock.Of<IValidator<AddTaskInput>>(),
                Mock.Of<IValidator<UpdateTaskInput>>()
            );

            // Act
            var result = controller.ListTask();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal("Nenhuma task criado.", okResult.Value);

            var tasks = okResult.Value as List<Task>;
            Assert.Null(tasks);
        }

        [Fact]
        public void ListTask_ReturnsOkWithMessage_WhenNoTasksExist()
        {
            // Arrange
            var taskUseCaseMock = new Mock<ITaskUseCase>();
            taskUseCaseMock.Setup(tc => tc.ListTask()).Returns(new List<Task>());

            var controller = new TaskController(
                taskUseCaseMock.Object,
                Mock.Of<IProjectUseCase>(),
                Mock.Of<IUserUseCase>(),
                Mock.Of<IValidator<AddTaskInput>>(),
                Mock.Of<IValidator<UpdateTaskInput>>()
            );

            // Act
            var result = controller.ListTask();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            Assert.Equal("Nenhuma task criado.", okResult.Value);
        }
    }
}
