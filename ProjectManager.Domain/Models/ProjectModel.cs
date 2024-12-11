using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ProjectManager.Domain.Models.Enums;

namespace ProjectManager.Domain.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public StatusEnums Status { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public ICollection<TaskModel> Tasks { get; set; }
        [JsonIgnore]
        public ICollection<CostModel> Costs { get; set; }
    }
}
