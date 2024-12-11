using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.DTOs.TeamMemberDTO;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Models;

namespace ProjectManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberInterface _teamMemberInterface;
        public TeamMemberController(ITeamMemberInterface teamMemberInterface)
        {
            _teamMemberInterface = teamMemberInterface;
        }

        [HttpGet("GetAllTeamMembers")]
        public async Task<ActionResult<ResponseModel<TeamMemberModel>>> GetAllTeamMembers()
        {
            var getTeamMember = await _teamMemberInterface.GetAllTeamMembers();
            return Ok(getTeamMember);
        }
        [HttpGet("GetMembersByTeam/{teamId}")]
        public async Task<ActionResult<ResponseModel<TeamMemberModel>>> GetMembersByTeamId(int teamId)
        {
            var getMemberByTeamId = await _teamMemberInterface.GetMembersByTeamId(teamId);
            return Ok(getMemberByTeamId);
        }
        [HttpPost("CreateTeamMember")]
        public async Task<ActionResult<ResponseModel<TeamMemberModel>>> CreateTeamMember(CreateTeamMemberDto createTeamMemberDto)
        {
            var createTeamMember = await _teamMemberInterface.CreateTeamMember(createTeamMemberDto);
            return Ok(createTeamMember);
        }
        [HttpPut("UpdateTeamMember")]
        public async Task<ActionResult<ResponseModel<TeamMemberModel>>> UpdateTeamMember(UpdateTeamMemberDto updateTeamMemberDto)
        {
            var updateTeamMember = await _teamMemberInterface.UpdateTeamMember(updateTeamMemberDto);
            return Ok(updateTeamMember);
        }
        [HttpDelete("DeleteTeamMember")]
        public async Task<ActionResult<ResponseModel<TeamMemberModel>>> DeleteTeamMember(int id)
        {
            var deleteTeamMember = await _teamMemberInterface.DeleteTeamMember(id);
            return Ok(deleteTeamMember);
        }
    }
}
