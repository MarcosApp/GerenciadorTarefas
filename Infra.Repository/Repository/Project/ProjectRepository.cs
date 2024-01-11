using Dapper;
using Domain.Contracts.Repositories.AddProject;
using Domain.Entities;
using Infra.Repository.DbContext;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repository.Repository.AddProject
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDbContext _dbContext;
        public ProjectRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddProject(Project project)
        {
            var query = @"INSERT INTO project (nome, descricao, datainicio, datafim) 
                          VALUES (@nome, @descricao, @datainicio, @datafim)
                        SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("nome", project.Nome, System.Data.DbType.String);
            parameters.Add("descricao", project.Descricao, System.Data.DbType.String);
            parameters.Add("datainicio", project.DataInicio, System.Data.DbType.DateTime);
            parameters.Add("datafim", project.DataFim, System.Data.DbType.DateTime);

            using var connection = _dbContext.CreateConnection();
            int insertedId = connection.QuerySingle<int>(query, parameters);
            return insertedId;
        }

        public int CountProject(int projectId)
        {
            var query = @"select count(projetoid) from task where projetoid = @projetoid";

            var parameters = new DynamicParameters();
            parameters.Add("projetoid", projectId, System.Data.DbType.Int32);

            using var connection = _dbContext.CreateConnection();
            int countId = connection.QuerySingle<int>(query, parameters);
            return countId;
        }

        public List<Project> ListProject()
        {
            var query = @"SELECT id, nome, descricao, datainicio, datafim  
                          FROM project
                        ";

            using var connection = _dbContext.CreateConnection();
            var projects = connection.Query<Project>(query).ToList();
            return projects;
        }
    }
}
