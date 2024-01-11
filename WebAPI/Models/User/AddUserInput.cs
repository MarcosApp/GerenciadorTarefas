using Domain.Contracts.Enumerator.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.User
{
    public class AddUserInput
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; }
        public UserPerfil Perfil { get; set; }
    }
}
