using Dapper;
using Domain.Contracts.Repositories.AddUser;
using Domain.Entities;
using Infra.Repository.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repository.Repository.AddUser
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _dbContext;
        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddUser(User user)
        {
            var query = @"INSERT INTO usuario (nome, email, senha, perfil) 
                          VALUES (@nome, @email, @senha, @perfil)
                        SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("nome", user.Nome, System.Data.DbType.String);
            parameters.Add("email", user.Email, System.Data.DbType.String);
            parameters.Add("senha",  user.Senha, System.Data.DbType.String);
            parameters.Add("perfil", (int)user.Perfil, System.Data.DbType.Int32);

            using var connection = _dbContext.CreateConnection();
            int insertedId = connection.QuerySingle<int>(query, parameters);
            return insertedId;
        }

        public List<User> ListUser()
        {
            var query = @"SELECT id, nome, email, perfil
                          FROM usuario
                        ";
            using var connection = _dbContext.CreateConnection();
            var user = connection.Query<User>(query).ToList();
            return user;
        }
    }
}
