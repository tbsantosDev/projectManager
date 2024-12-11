using ProjectManager.Application.DTOs.TeamMemberDTO;
using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Interfaces
{
    public interface ITeamMemberInterface
    {
        Task<ResponseModel<List<TeamMemberModel>>> GetAllTeamMembers();
        Task<ResponseModel<List<TeamMemberModel>>> GetMembersByTeamId(int teamId);
        Task<ResponseModel<TeamMemberModel>> CreateTeamMember(CreateTeamMemberDto createTeamMemberDto);
        Task<ResponseModel<TeamMemberModel>> UpdateTeamMember(UpdateTeamMemberDto updateTeamMemberDto);
        Task<ResponseModel<TeamMemberModel>> DeleteTeamMember(int id);

    }
}
