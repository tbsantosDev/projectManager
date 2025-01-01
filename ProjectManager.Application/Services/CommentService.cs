using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.DTOs.CommentDTO;
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
    public class CommentService : ICommentInterface
    {
        private readonly AppDbContext _context;
        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<CommentModel>> CreateComment(CreateCommentDto createCommentDto)
        {
            ResponseModel<CommentModel> response = new();
            try
            {
                var newComment = new CommentModel()
                {
                    Content = createCommentDto.Content,
                    CreatedAt = DateTime.UtcNow,
                    TaskId = createCommentDto.TaskId,
                    UserId = createCommentDto.UserId,
                };
                _context.Comments.Add(newComment);
                await _context.SaveChangesAsync();

                response.Dados = newComment;
                response.Message = "Comentario criado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<CommentModel>>> GetCommentByTask(int taskId)
        {
            ResponseModel<List<CommentModel>> response = new();

            try
            {
                var comments = await _context.Comments.Include(c => c.Tasks).Where(t => t.Id == taskId).ToListAsync();
                if (comments.Count == 0)
                {
                    response.Message = "Nenhum comentario encontrado.";
                    return response;
                }
                response.Dados = comments;
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }


        public async Task<ResponseModel<CommentModel>> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            ResponseModel<CommentModel> response = new();

            try
            {
                var updateComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == updateCommentDto.Id);
                if (updateComment == null)
                {
                    response.Message = "Comentário inexistente.";
                    response.Status = false;
                    return response;
                }
                updateComment.Content = updateCommentDto.Content;

                _context.Comments.Update(updateComment);
                await _context.SaveChangesAsync();

                response.Dados = updateComment;
                response.Message = "Comentario atualizado com sucesso!";
                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
