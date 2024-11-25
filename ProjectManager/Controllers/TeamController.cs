using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.DTOs.TeamDTO;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Models;

namespace ProjectManager.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamInterface _teamInterface;
        public TeamController(ITeamInterface teamInterface)
        {
            _teamInterface = teamInterface;
        }

        [HttpGet("GetTeams")]
        public async Task<ActionResult<ResponseModel<List<TeamModel>>>> GetTeams()
        {
            var teams = await _teamInterface.GetTeams();
            return Ok(teams);
        }
        [HttpGet("GetTeam/{id}")]
        public async Task<ActionResult<ResponseModel<TeamModel>>> GetTeam(int id)
        {
            var team = await _teamInterface.GetTeam(id);
            return Ok(team);
        }
        [HttpPost("CreateTeam")]
        public async Task<ActionResult<ResponseModel<TeamModel>>> CreateTeam(CreateTeamDto createTeamDto)
        {
            var team = await _teamInterface.CreateTeam(createTeamDto);
            return Ok(team);
        }
        [HttpPut("UpdateTeam")]
        public async Task<ActionResult<ResponseModel<TeamModel>>> UpdateTeam(UpdateTeamDto updateTeamDto)
        {
            var team = await _teamInterface.UpdateTeam(updateTeamDto);
            return Ok(team);
        }
        [HttpDelete("DeleteTeam/{id}")]
        public async Task<ActionResult<ResponseModel<TeamModel>>> DeleteTeam(int id)
        {
            var team = await _teamInterface.DeleteTeam(id);
            return Ok(team);
        }
    }
}
