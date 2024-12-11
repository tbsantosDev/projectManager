using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Interfaces
{
    public interface ITaskInterface
    {
        Task<ResponseModel<List<TaskModel>>> GetTasks();
        Task<ResponseModel<TaskModel>> GetTasksById(int taskId);
        Task<ResponseModel<TaskModel>> CreateTask();
        Task<ResponseModel<TaskModel>> UpdateTask();
        Task<ResponseModel<TaskModel>> DeleteTask();
    }
}
