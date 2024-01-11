using Domain.Contracts.UseCases.AddReport;
using Domain.Contracts.UseCases.AddUser;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportUseCase _reportUseCase;
        private readonly IUserUseCase _userUseCase;

        public ReportsController(IReportUseCase reportUseCase, IUserUseCase userUseCase)
        {
            _reportUseCase = reportUseCase;
            _userUseCase = userUseCase;
        }

        [HttpGet("api/ListReportMedio/{usuarioId}")]
        public IActionResult ListReportMedio(int usuarioId)
        {
            var user = _userUseCase.ListUser(usuarioId);

            if (user == null)
                return NotFound("usuário encontrado.");

            if(user.Perfil != Domain.Contracts.Enumerator.User.UserPerfil.Manager)
                return Forbid("usuário não autorizado.");

            DateTime data30DiasAtras = DateTime.UtcNow.AddDays(-30);

            var report = _reportUseCase.ListReport(data30DiasAtras);

            if (report.Count == 0)
                return Ok("Nenhuma tarefa encontrado.");

            return Ok(report);
        }
    }
}
