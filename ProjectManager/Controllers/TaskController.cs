using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.DTOs.TaskDTO;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Models;

namespace ProjectManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskInterface _taskInterface;
        public TaskController(ITaskInterface taskInterface)
        {
            _taskInterface = taskInterface;
        }

        [HttpGet("GetTasks")]
        public async Task<ActionResult<ResponseModel<List<TaskModel>>>> GetTasks ()
        {
            var tasks = await _taskInterface.GetTasks();
            return Ok(tasks);
        }
        [HttpGet("GetTasksById/{id}")]
        public async Task<ActionResult<ResponseModel<TaskModel>>> GetTaskById(int id)
        {
            var task = await _taskInterface.GetTasksById(id);
            return Ok(task);
        }
        [HttpPost("CreateTask")]
        public async Task<ActionResult<ResponseModel<TaskModel>>> CreateTask(CreateTaskDto createTaskDto)
        {
            var task = await _taskInterface.CreateTask(createTaskDto);
            return Ok(task);
        }
        [HttpPut("UpdateTask")]
        public async Task<ActionResult<ResponseModel<TaskModel>>> UpdateTask(UpdateTaskDto updateTaskDto)
        {
            var task = await _taskInterface.UpdateTask(updateTaskDto);
            return Ok(task);
        }
        [HttpDelete("DeleteTask/{id}")]
        public async Task<ActionResult<ResponseModel<TaskModel>>> DeleteTask(int id)
        {
            var task = await _taskInterface.DeleteTask(id);
            return Ok(task);
        }
    }
}
