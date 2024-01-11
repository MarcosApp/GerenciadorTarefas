using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Comment
{
    public class AddCommentInput
    {
        public string Texto { get; set; }
        public int UsuarioId { get; set; }
        public int TaskId { get; set; }
    }
}
