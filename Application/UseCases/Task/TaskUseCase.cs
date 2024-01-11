using Domain.Contracts.Repositories.AddTask;
using Domain.Contracts.UseCases.AddTask;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.UseCases.AddTask
{
    public class TaskUseCase : ITaskUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public int AddTask(Task task)
        {
            return _taskRepository.AddTask(task);
        }

        public int DeleteTask(int input)
        {
            return _taskRepository.DeleteTask(input);
        }

        public List<Task> ListTask()
        {
            return _taskRepository.ListTask();
        }

        public int UpdateTask(Task task)
        {
            return _taskRepository.UpdateTask(task);
        }
    }
}
