using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment
    {
        public Comment(int id, string texto, int usuarioId, int taskId)
        {
            Id = id;
            Texto = texto;
            UsuarioId = usuarioId;
            TaskId = taskId;
        }
        public int Id { get; set; }
        public string Texto { get; set; }
        public int UsuarioId { get; set; }
        public int TaskId { get; set; }
    }
}
