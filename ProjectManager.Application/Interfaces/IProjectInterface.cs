using ProjectManager.Application.DTOs.ProjectDTO;
using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Interfaces
{
    public interface IProjectInterface
    {
        Task<ResponseModel<List<ProjectModel>>> GetProjects();
        Task<ResponseModel<ProjectModel>> GetProjectById(int id);
        Task<ResponseModel<ProjectModel>> CreateProject(CreateProjectDto createProjectDto);
        Task<ResponseModel<ProjectModel>> UpdateProject(UpdateProjectDto updateProjectDto);
        Task<ResponseModel<ProjectModel>> DeleteProject(int id);

    }
}
