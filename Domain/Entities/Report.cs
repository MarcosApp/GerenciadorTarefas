using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Report
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public double Concluidas { get; set; }
    }
}
