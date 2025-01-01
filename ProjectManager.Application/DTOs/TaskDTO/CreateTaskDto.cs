using ProjectManager.Domain.Models.Enums;
using ProjectManager.Domain.Models;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManager.Application.DTOs.TaskDTO
{
    public class CreateTaskDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public TaskStatusEnums Status { get; set; }
        [Required]
        public TaskPriorityEnums Priority { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        //Chave estrangeira para a tabela projeto
        public int ProjectId { get; set; }
        //Chave estrangeira para a tabela usuario
        public int TeamId { get; set; } 
    }
}
