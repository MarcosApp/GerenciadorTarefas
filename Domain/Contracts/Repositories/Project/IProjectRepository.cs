using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories.AddProject
{
    public interface IProjectRepository
    {
        int AddProject(Project project);
        List<Project> ListProject();
        int CountProject(int projectId);
    }
}
