using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.DTOs.CostDTO
{
    public class UpdateCostDto
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Value { get; set; }
        public int? ProjectId { get; set; }
        public int? TaskId { get; set; }
        public int? TeamId { get; set; }
    }
}
