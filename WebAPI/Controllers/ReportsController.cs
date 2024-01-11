using Domain.Contracts.UseCases.AddReport;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportUseCase _userUseCase;

        public ReportsController(IReportUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        [HttpGet("api/ListReportMedio")]
        public IActionResult ListReportMedio()
        {
            DateTime data30DiasAtras = DateTime.UtcNow.AddDays(-30);

            var report = _userUseCase.ListReport(data30DiasAtras);

            if (report.Count == 0)
                return Ok("Nenhuma tarefa encontrado.");

            return Ok(report);
        }
    }
}
