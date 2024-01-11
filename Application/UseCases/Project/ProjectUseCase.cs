using Domain.Contracts.Repositories.AddProject;
using Domain.Contracts.UseCases.AddProject;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.UseCases.AddProject
{
    public class ProjectUseCase : IProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectUseCase(IProjectRepository ProjectRepository)
        {
            _projectRepository = ProjectRepository;
        }
        public int AddProject(Project project)
        {
            return _projectRepository.AddProject(project);
        }

        public int CountProject(int projectId)
        {
            return _projectRepository.CountProject(projectId);
        }

        public List<Project> ListProject()
        {
            return _projectRepository.ListProject();
        }
    }
}
