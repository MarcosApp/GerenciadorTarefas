using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Contracts.UseCases.AddTask
{
    public interface ITaskUseCase
    {
        int AddTask(Task task);
        List<Task> ListTask();
        Task ListTask(int id);
        int UpdateTask(Task task);
        int DeleteTask(int input);
    }
}
