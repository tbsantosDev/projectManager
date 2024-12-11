using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.DTOs.ProjectDTO;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Models;

namespace ProjectManager.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectInterface _projectInterface;
        public ProjectController(IProjectInterface projectInterface)
        {
            _projectInterface = projectInterface;
        }
        [HttpGet("GetProjects")]
        public async Task<ActionResult<ResponseModel<List<ProjectModel>>>> GetProjects()
        {
            var projects = await _projectInterface.GetProjects();
            return Ok(projects);
        }
        [HttpGet("GetProjectById/{id}")]
        public async Task<ActionResult<ResponseModel<List<ProjectModel>>>> GetProjectById(int id)
        {
            var project = await _projectInterface.GetProjectById(id);
            return Ok(project);
        }
        [HttpPost("CreateProject")]
        public async Task<ActionResult<ResponseModel<List<ProjectModel>>>> CreateProject(CreateProjectDto createProjectDto)
        {
            var project = await _projectInterface.CreateProject(createProjectDto);
            return Ok(project);
        }
        [HttpPut("UpdateProject")]
        public async Task<ActionResult<ResponseModel<List<ProjectModel>>>> UpdateProject(UpdateProjectDto updateProjectDto)
        {
            var project = await _projectInterface.UpdateProject(updateProjectDto);
            return Ok(project);
        }
        [HttpDelete("DeleteProject/{id}")]
        public async Task<ActionResult<ResponseModel<List<ProjectModel>>>> DeleteProject(int id)
        {
            var project = await _projectInterface.DeleteProject(id);
            return Ok(project);
        }
    }
} 
