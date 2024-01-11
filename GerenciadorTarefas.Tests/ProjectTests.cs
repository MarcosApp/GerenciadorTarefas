using Domain.Contracts.UseCases.AddProject;
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
using WebAPI.Models.Project;
using Xunit;

namespace GerenciadorTarefas.Tests
{
    public class ProjectTests
    {
        [Fact]
        public void ListProject_ReturnsOk_WhenProjectsExist()
        {
            // Arrange
            var projectUseCaseMock = new Mock<IProjectUseCase>();
            projectUseCaseMock.Setup(pc => pc.ListProject()).Returns(new List<Project>());

            var controller = new ProjectsController(
                projectUseCaseMock.Object,
                Mock.Of<IValidator<AddProjectInput>>()
            );

            // Act
            var result = controller.ListProject();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var projects = okResult.Value as List<Project>;
            Assert.NotNull(projects);
        }

        [Fact]
        public void ListProject_ReturnsOkWithMessage_WhenNoProjectsExist()
        {
            // Arrange
            var projectUseCaseMock = new Mock<IProjectUseCase>();
            projectUseCaseMock.Setup(pc => pc.ListProject()).Returns(new List<Project>());

            var controller = new ProjectsController(
                projectUseCaseMock.Object,
                Mock.Of<IValidator<AddProjectInput>>()
            );

            // Act
            var result = controller.ListProject();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            Assert.Equal("Nenhum projeto criado.", okResult.Value);
        }
    }
}
