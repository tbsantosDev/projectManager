using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.DTOs.CommentDTO;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Models;

namespace ProjectManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentInterface _commentInterface;
        public CommentController(ICommentInterface commentInterface)
        {
            _commentInterface = commentInterface; 
        }

        [HttpGet("GetCommentByTask/{taskId}")]
        public async Task<ActionResult<ResponseModel<List<CommentModel>>>> GetCommentByTask(int taskId)
        {
            var comment = await _commentInterface.GetCommentByTask(taskId);
            return Ok(comment);
        }
        [HttpPost("CreateComment")]
        public async Task<ActionResult<ResponseModel<CommentModel>>> CreateComment(CreateCommentDto createCommentDto)
        {
            var comment = await _commentInterface.CreateComment(createCommentDto);
            return Ok(comment);
        }
        [HttpPatch("UpdateComment")]
        public async Task<ActionResult<ResponseModel<CommentModel>>> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            var comment = await _commentInterface.UpdateComment(updateCommentDto);
            return Ok(comment);
        }
    }
}
