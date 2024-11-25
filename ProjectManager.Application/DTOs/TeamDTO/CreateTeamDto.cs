using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.DTOs.TeamDTO
{
    public class CreateTeamDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
