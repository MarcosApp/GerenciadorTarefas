using Domain.Contracts.Enumerator.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public User()
        {

        }
        public User(int id, string nome, string email, string senha, UserPerfil userPerfil)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            Perfil = userPerfil;
        }
        public int Id { get; set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public UserPerfil Perfil { get; private set; }
    }
}
