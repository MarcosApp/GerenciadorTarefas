using Dapper;
using Domain.Contracts.Repositories.AddReport;
using Domain.Entities;
using Infra.Repository.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repository.Repository.Reports
{
    public class ReportRepository : IReportRepository
    {
        private readonly IDbContext _dbContext;
        public ReportRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Report> ListReport(DateTime data)
        {
            var query = @"SELECT usuarioid, AVG(tarefas_concluidas) AS Concluidas, nome
                            FROM (
                                SELECT usuarioid, COUNT(*) AS tarefas_concluidas, u.nome
                                FROM task t 
                                INNER JOIN usuario u ON u.id = t.usuarioid
                                WHERE status = 3
                                GROUP BY usuarioid, u.nome
                            ) AS subquery
                            GROUP BY usuarioid, nome;

                        ";
            var parameters = new DynamicParameters();
            parameters.Add("Data30DiasAtras", data, System.Data.DbType.DateTime);

            using var connection = _dbContext.CreateConnection();
            var task = connection.Query<Report>(query, parameters).ToList();
            return task;
        }
    }
}
