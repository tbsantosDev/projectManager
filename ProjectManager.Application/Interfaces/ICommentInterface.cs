using ProjectManager.Application.DTOs.CommentDTO;
using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Interfaces
{
    public interface ICommentInterface
    {
        Task<ResponseModel<List<CommentModel>>> GetCommentByTask(int taskId);
        Task<ResponseModel<CommentModel>> CreateComment(CreateCommentDto createCommentDto);
        Task<ResponseModel<CommentModel>> UpdateComment(UpdateCommentDto updateCommentDto);
    }
}
