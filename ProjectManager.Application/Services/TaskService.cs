using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.DTOs.TaskDTO;
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
    public class TaskService : ITaskInterface
    {
        private readonly AppDbContext _context;
        public TaskService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<TaskModel>> CreateTask(CreateTaskDto createTaskDto)
        {
            ResponseModel<TaskModel> response = new();
            try
            {
                var task = new TaskModel()
                {
                    Name = createTaskDto.Name,
                    Description = createTaskDto?.Description,
                    Status = createTaskDto.Status,
                    Priority = createTaskDto.Priority,
                    CreatedAt = DateTime.UtcNow,
                    ProjectId = createTaskDto.ProjectId,
                    UserId = createTaskDto.UserId,
                };
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();

                response.Dados = task;
                response.Message = "Tarefa criada com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<TaskModel>> DeleteTask(int id)
        {
            ResponseModel<TaskModel> response = new();

            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
                if (task == null)
                {
                    response.Message = "Essa tarefa não existe.";
                    return response;
                }
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                response.Dados = task;
                response.Message = "Tarefa excluida com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<TaskModel>>> GetTasks()
        {
            ResponseModel<List<TaskModel>> response = new ResponseModel<List<TaskModel>>();

            try
            {
                var tasks = await _context.Tasks.ToListAsync();
                if (tasks.Count == 0)
                {
                    response.Message = "Nenhuma tarefa encontrada.";
                    return response;
                }

                response.Dados = tasks;
                response.Message = "Tarefas coletadas com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<TaskModel>> GetTasksById(int taskId)
        {
            ResponseModel<TaskModel> response = new ResponseModel<TaskModel>();

            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
                if (task == null)
                {
                    response.Message = "Nenhuma tarefa encontrada.";
                    return response;
                }
                response.Dados = task;
                response.Message = "Tarefa encontrada com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<TaskModel>> UpdateTask(UpdateTaskDto updateTaskDto)
        {
            ResponseModel<TaskModel> response = new();
            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == updateTaskDto.Id);
                if (task == null)
                {
                    response.Message = "Tarefa não localizada.";
                    response.Status = false;
                    return response;
                }

                task.Name = updateTaskDto.Name;
                task.Description = updateTaskDto?.Description;
                task.Status = updateTaskDto.Status;
                task.Priority = updateTaskDto.Priority;
                task.CompletionAt = updateTaskDto.CompletionAt;
                task.ProjectId = updateTaskDto.ProjectId;
                task.UserId = updateTaskDto.UserId;

                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();

                response.Dados = task;
                response.Message = "Tarefa atualizada com sucesso!";
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
