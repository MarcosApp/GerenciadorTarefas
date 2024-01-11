using Domain.Contracts.Enumerator.Task;
using System;

namespace WebAPI.Models.Task
{
    public class UpdateTaskInput
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public TaskStatus Status { get; set; }
        public DateTime DataAtualizacao { get; set; }

    }
}
