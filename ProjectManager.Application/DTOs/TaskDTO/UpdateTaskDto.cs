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
    public class UpdateTaskDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public TaskStatusEnums Status { get; set; }
        [Required]
        public TaskPriorityEnums Priority { get; set; }
        public DateTime? CompletionAt { get; set; }
        //Chave estrangeira para a tabela projeto
        public int ProjectId { get; set; }

        //Chave estrangeira para a tabela usuario
        public int TeamId { get; set; }

    }
}
