using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories.AddReport
{
    public interface IReportRepository
    {
        List<Report> ListReport(DateTime data);
    }
}
