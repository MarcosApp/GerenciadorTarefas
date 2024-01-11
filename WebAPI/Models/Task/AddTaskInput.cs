using Domain.Contracts.Enumerator.Task;
using System;

namespace WebAPI.Models.Task
{
    public class AddTaskInput
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public TaskStatus Status { get; set; }
        public TaskPriority Prioridade { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public virtual int ProjectId { get; set; }
        public virtual int UsuarioId { get; set; }
    }
}
