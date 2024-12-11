using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Services
{
    internal class TaskService : ITaskInterface
    {
        public Task<ResponseModel<TaskModel>> CreateTask()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<TaskModel>> DeleteTask()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<TaskModel>>> GetTasks()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<TaskModel>> GetTasksById(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<TaskModel>> UpdateTask()
        {
            throw new NotImplementedException();
        }
    }
}
