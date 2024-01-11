using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Contracts.UseCases.AddReport
{
    public interface IReportUseCase
    {
        List<Report> ListReport(DateTime date);
    }
}
