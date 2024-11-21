using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Models
{
    public class CostModel
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        //Chave estrangeira para a tabela projeto
        public int? ProjectId { get; set; }
        [JsonIgnore]
        public ICollection<ProjectModel> Projects { get; set; }
        //Chave estrangeira para a tabela tarefa
        public int? TaskId { get; set; }
        [JsonIgnore]
        public ICollection<TaskModel> Tasks { get; set; }
        //Chave estrangeira para a tabela time
        public int? TeamId { get; set; }
        [JsonIgnore]
        public ICollection<TeamModel> Teams { get; set; }
    }
}
