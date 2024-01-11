using Domain.Contracts.Enumerator.Task;
using Newtonsoft.Json;
using System;

namespace Domain.Entities
{
    public class Task
    {
        public Task()
        {

        }

        public Task(int id, string nome, string descricao, TaskStatus status, DateTime dataAtualizacao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Status = status;
            DataAtualizacao = dataAtualizacao;
        }

        public Task(int id, string nome, string descricao, TaskStatus status, TaskPriority priority, DateTime dataCriacao, DateTime dataAtualizacao, int projetoId)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Status = status;
            Prioridade = priority;
            DataCriacao = dataCriacao;
            DataAtualizacao = dataAtualizacao;
            ProjetoId = projetoId;
        }

        public int Id { get; set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        public TaskStatus Status { get; private set; }

        public TaskPriority Prioridade { get; private set; }

        public DateTime DataCriacao { get; private set; }

        public DateTime DataAtualizacao { get; private set; }

        public virtual int ProjetoId { get; private set; }
    }
}
