using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Contracts.Repositories.AddTask
{
    public interface ITaskRepository
    {
        int AddTask(Task task);
        List<Task> ListTask();
        int UpdateTask(Task task);
        int DeleteTask(int input);
        Task ListTask(int id);
    }
}
