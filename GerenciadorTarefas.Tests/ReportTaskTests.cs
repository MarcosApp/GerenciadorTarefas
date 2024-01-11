using Domain.Contracts.Enumerator.User;
using Domain.Contracts.UseCases.AddReport;
using Domain.Contracts.UseCases.AddUser;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using WebAPI.Controllers;
using Xunit;

namespace GerenciadorTarefas.Tests
{
    public class ReportTaskTests
    {
        [Fact]
        public void ListReportMedio_ReturnsOk_WhenUserIsManager()
        {
            // Arrange
            var userUseCaseMock = new Mock<IUserUseCase>();
            userUseCaseMock.Setup(uc => uc.ListUser(It.IsAny<int>())).Returns(new User { Perfil = UserPerfil.Manager });

            var reportUseCaseMock = new Mock<IReportUseCase>();
            var controller = new ReportsController(reportUseCaseMock.Object, userUseCaseMock.Object);

            // Act
            var result = controller.ListReportMedio(usuarioId: 1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var report = okResult.Value as List<Report>; 
            Assert.Null(report);
        }

        [Fact]
        public void ListReportMedio_ReturnsNotFound_WhenUserNotFound()
        {
            // Arrange
            var userUseCaseMock = new Mock<IUserUseCase>();
            userUseCaseMock.Setup(uc => uc.ListUser(It.IsAny<int>())).Returns((User)null);

            var reportUseCaseMock = new Mock<IReportUseCase>();
            var controller = new ReportsController(reportUseCaseMock.Object, userUseCaseMock.Object);

            // Act
            var result = controller.ListReportMedio(usuarioId: 1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);

            Assert.Equal("usuário encontrado.", notFoundResult.Value);
        }

        [Fact]
        public void ListReportMedio_ReturnsForbid_WhenUserIsNotManager()
        {
            // Arrange
            var userUseCaseMock = new Mock<IUserUseCase>();
            userUseCaseMock.Setup(uc => uc.ListUser(It.IsAny<int>())).Returns(new User { Perfil = UserPerfil.People });

            var reportUseCaseMock = new Mock<IReportUseCase>();
            var controller = new ReportsController(reportUseCaseMock.Object, userUseCaseMock.Object);

            // Act
            var result = controller.ListReportMedio(usuarioId: 1);

            // Assert
            Assert.IsType<ForbidResult>(result);
            var forbidResult = result as ForbidResult;
            Assert.NotNull(forbidResult);

            //Assert.Equal(403, forbidResult.Properties.);
            Assert.Equal("usuário não autorizado.", forbidResult.AuthenticationSchemes[0]);
        }

        [Fact]
        public void ListReportMedio_ReturnsOk_WhenNoReportsFound()
        {
            // Arrange
            var userUseCaseMock = new Mock<IUserUseCase>();
            userUseCaseMock.Setup(uc => uc.ListUser(It.IsAny<int>())).Returns(new User { Perfil = UserPerfil.Manager });

            var reportUseCaseMock = new Mock<IReportUseCase>();
            reportUseCaseMock.Setup(rc => rc.ListReport(It.IsAny<DateTime>())).Returns(new List<Report>());

            var controller = new ReportsController(reportUseCaseMock.Object, userUseCaseMock.Object);

            // Act
            var result = controller.ListReportMedio(usuarioId: 1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var report = okResult.Value as List<Report>;
            Assert.Null(report);
            Assert.Equal("Nenhuma tarefa encontrado.", okResult.Value);
        }
    }
}
