using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Contracts.Repositories.AddTask
{
    public interface ITaskRepository
    {
        int AddTask(Task task);
        List<Task> ListTask();
    }
}
