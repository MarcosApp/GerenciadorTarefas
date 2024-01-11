using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Contracts.UseCases.AddProject
{
    public interface IProjectUseCase
    {
        int AddProject(Project project);
        List<Project> ListProject();
    }
}
