using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Project
    {
        public Project()
        {

        }
        public Project(int? id, string nome, string descricao, DateTime dataInicio, DateTime dataFim)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
        }

        public int? Id { get; set; }
        public string Nome { get; private set; } 
        public string Descricao { get; private set; } 
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
    }
}
