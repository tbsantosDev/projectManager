using ProjectManager.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.DTOs.ProjectDTO
{
    public class UpdateProjectDto
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
    }
}
