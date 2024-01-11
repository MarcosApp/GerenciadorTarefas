using Dapper;
using Domain.Contracts.Repositories.AddTask;
using Domain.Entities;
using Infra.Repository.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repository.Repository.AddTask
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbContext _dbContext;
        public TaskRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddTask(Task project)
        {
            var query = @"INSERT INTO task (nome, descricao, status, prioridade, datacriacao, projetoid) 
                          VALUES (@nome, @descricao, @status, @prioridade, @datacriacao, @projetoid)
                        SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("nome", project.Nome, System.Data.DbType.String);
            parameters.Add("descricao", project.Descricao, System.Data.DbType.String);
            parameters.Add("status", (int)project.Status, System.Data.DbType.Int32);
            parameters.Add("prioridade", (int)project.Prioridade, System.Data.DbType.Int32);
            parameters.Add("datacriacao", DateTime.Now, System.Data.DbType.DateTime);
            parameters.Add("projetoid", project.ProjetoId, System.Data.DbType.Int32);

            using var connection = _dbContext.CreateConnection();
            int insertedId = connection.QuerySingle<int>(query, parameters);
            return insertedId;
        }

        public int DeleteTask(int id)
        {
            var query = @"DELETE FROM task where id = @id";
            
            var parameters = new DynamicParameters();
            parameters.Add("id", id, System.Data.DbType.Int32);

            using var connection = _dbContext.CreateConnection();
            int affectedRows = connection.Execute(query, parameters);

            return affectedRows;
        }

        public List<Task> ListTask()
        {
            var query = @"SELECT id, nome, descricao, status, 
                                 status, prioridade, datacriacao, 
                                 dataatualizacao, projetoid 
                          FROM task
                        ";
            using var connection = _dbContext.CreateConnection();
            var task = connection.Query<Task>(query).ToList();
            return task;
        }

        public Task ListTask(int id)
        {
            var query = @"SELECT id, nome, descricao, status, 
                                 status, prioridade, datacriacao, 
                                 dataatualizacao, projetoid 
                          FROM task where id = @id
                        ";

            var parameters = new DynamicParameters();
            parameters.Add("id", id, System.Data.DbType.Int32);

            using var connection = _dbContext.CreateConnection();
            var task = connection.QueryFirstOrDefault<Task>(query, new { id });
            return task;
        }

        public int UpdateTask(Task task)
        {
            var query = @"UPDATE task 
                  SET nome = @nome, 
                      descricao = @descricao, 
                      status = @status,
                      dataatualizacao = @dataatualizacao
                  WHERE id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", task.Id, System.Data.DbType.Int32);
            parameters.Add("nome", task.Nome, System.Data.DbType.String);
            parameters.Add("descricao", task.Descricao, System.Data.DbType.String);
            parameters.Add("status", task.Status, System.Data.DbType.Int32);
            parameters.Add("dataatualizacao", task.DataAtualizacao, System.Data.DbType.DateTime);

            using var connection = _dbContext.CreateConnection();
            var execucao = connection.Execute(query, parameters);
            return execucao;
        }
    }
}
