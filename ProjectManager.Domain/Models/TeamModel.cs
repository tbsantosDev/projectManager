using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public ICollection<TeamMemberModel> TeamMembers { get; set; }
        [JsonIgnore]
        public ICollection<CostModel> Costs { get; set; }
        public ICollection<ProjectModel> Projects { get; set; }
    }
}
