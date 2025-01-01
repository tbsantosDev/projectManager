using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.DTOs.ProjectDTO;
using ProjectManager.Application.Interfaces;
using ProjectManager.Data;
using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Services
{
    public class ProjectService : IProjectInterface
    {
        private readonly AppDbContext _context;
        public ProjectService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<ProjectModel>> CreateProject(CreateProjectDto createProjectDto)
        {
            ResponseModel<ProjectModel> response = new ResponseModel<ProjectModel>();
            try
            {
                var project = new ProjectModel()
                {
                    Name = createProjectDto.Name,
                    Description = createProjectDto.Description,
                    Status = createProjectDto.Status,
                    InitialDate = createProjectDto.InitialDate,
                    EndDate = createProjectDto.EndDate,
                    CreatedAt = DateTime.UtcNow,
                };
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();

                response.Dados = project;
                response.Message = "Projeto criado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<ProjectModel>> DeleteProject(int id)
        {
            ResponseModel<ProjectModel> response = new ResponseModel<ProjectModel>();

            try
            {
                var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
                if (project == null)
                {
                    response.Message = "Nenhum projeto localizado!";
                    response.Status = false;
                    return response;
                }
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();

                response.Message = "Projeto excluido com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<ProjectModel>> GetProjectById(int id)
        {
            ResponseModel<ProjectModel> response = new ResponseModel<ProjectModel>();

            try
            {
                var project = await _context.Projects
                    .Include(p => p.Tasks)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (project == null)
                {
                    response.Message = "Nenhum projeto localizado!";
                    response.Status = false;
                    return response;
                }

                response.Dados = project;
                response.Message = "Todos os projetos coletados com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ProjectModel>>> GetProjects()
        {
            ResponseModel<List<ProjectModel>> response = new ResponseModel<List<ProjectModel>>();

            try
            {
                var projects = await _context.Projects.Include(p => p.Tasks).ToListAsync();

                response.Dados = projects;
                response.Message = "Todos os projetos coletados com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<ProjectModel>> UpdateProject(UpdateProjectDto updateProjectDto)
        {
            ResponseModel<ProjectModel> response = new ResponseModel<ProjectModel>();
            try
            {
                var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == updateProjectDto.Id);
                if (project == null)
                {
                    response.Message = "Projeto não localizado!";
                    response.Status = false;
                    return response;
                }

                project.Name = updateProjectDto.Name;
                project.Description = updateProjectDto.Description;
                project.Status = updateProjectDto.Status;
                project.InitialDate = updateProjectDto.InitialDate;
                project.EndDate = updateProjectDto.EndDate;

                _context.Projects.Update(project);
                await _context.SaveChangesAsync();

                response.Dados = project;
                response.Message = "Projeto alterado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
