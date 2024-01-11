
using Dapper;
using Domain.Contracts.Repositories.AddComment;
using Domain.Entities;
using Infra.Repository.DbContext;

namespace Infra.Repository.Repository.AddComment
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDbContext _dbContext;
        public CommentRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddComment(Comment comment)
        {
            var query = @"INSERT INTO comentario (texto, usuarioid, taskid) VALUES (@texto, @usuarioid, @taskid);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("texto", comment.Texto, System.Data.DbType.String);
            parameters.Add("usuarioid", comment.UsuarioId, System.Data.DbType.Int32);
            parameters.Add("taskid", comment.TaskId, System.Data.DbType.DateTime);

            using var connection = _dbContext.CreateConnection();
            int insertedId = connection.QuerySingle<int>(query, parameters);
            return insertedId;
        }
    }
}
