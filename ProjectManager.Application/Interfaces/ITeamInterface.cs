using ProjectManager.Application.DTOs.TeamDTO;
using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Interfaces
{
    public interface ITeamInterface
    {
        Task<ResponseModel<List<TeamModel>>> GetTeams();
        Task<ResponseModel<TeamModel>> GetTeam(int id);
        Task<ResponseModel<TeamModel>> CreateTeam(CreateTeamDto createTeamDto);
        Task<ResponseModel<TeamModel>> UpdateTeam(UpdateTeamDto updateTeamDto);
        Task<ResponseModel<TeamModel>> DeleteTeam(int id);
    }
}
