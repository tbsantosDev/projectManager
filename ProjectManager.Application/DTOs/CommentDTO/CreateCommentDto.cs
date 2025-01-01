using ProjectManager.Domain.Models;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManager.Application.DTOs.CommentDTO
{
    public class CreateCommentDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int TaskId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
