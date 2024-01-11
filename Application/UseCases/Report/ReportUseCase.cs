using Domain.Contracts.Repositories.AddReport;
using Domain.Contracts.UseCases.AddReport;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.UseCases.AddReport
{
    public class ReportUseCase : IReportUseCase
    {
        private readonly IReportRepository _reportRepository;

        public ReportUseCase(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public List<Report> ListReport(DateTime date)
        {
            return _reportRepository.ListReport(date);
        }
    }
}
