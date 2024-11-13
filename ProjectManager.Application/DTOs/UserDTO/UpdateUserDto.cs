using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.DTOs.UserDTO
{
    public class UpdateUserDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string CurrentPassword { get; set; }
        [Required]
        [MaxLength(50)]
        public string NewPassword { get; set; }
        [Required]
        [MaxLength(50)]
        public string ConfirmPassword { get; set; }
    }
}
