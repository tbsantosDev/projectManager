using ProjectManager.Domain.Models.Enums;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public TaskStatusEnums Status { get; set; }
        [Required]
        public TaskPriorityEnums Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletionAt { get; set; }
        //Chave estrangeira para a tabela projeto
        public int ProjectId { get; set; }
        [JsonIgnore]
        public ProjectModel Project { get; set; }
        //Chave estrangeira para a tabela usuario
        public int TeamId { get; set; }
        [JsonIgnore]
        public TeamModel Team { get; set; }
    }
}
