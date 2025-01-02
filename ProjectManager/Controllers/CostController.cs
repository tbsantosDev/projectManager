using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.DTOs.CostDTO;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Models;

namespace ProjectManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private readonly ICostInterface _costInterface;
        public CostController(ICostInterface costInterface)
        {
            _costInterface = costInterface;
        }
        [HttpGet("GetCostByProject/{projectId}")]
        public async Task<ActionResult<ResponseModel<List<CostModel>>>> GetCostByProject(int projectId)
        {
            var getCostByProjectId = await _costInterface.GetCostByProject(projectId);
            return Ok(getCostByProjectId);
        }
        [HttpGet("GetCostByTask/{taskId}")]
        public async Task<ActionResult<ResponseModel<List<CostModel>>>> GetCostByTask(int taskId)
        {
            var GetCostByTaskId = await _costInterface.GetCostByTask(taskId);
            return Ok(GetCostByTaskId);
        }
        [HttpGet("GetCostByTeam/{teamId}")]
        public async Task<ActionResult<ResponseModel<List<CostModel>>>> GetCostByTeam(int teamId)
        {
            var GetCostByTeamId = await _costInterface.GetCostByTeam(teamId);
            return Ok(GetCostByTeamId);
        }
        [HttpPost("CreateCost")]
        public async Task<ActionResult<ResponseModel<CostModel>>> CreateCost(CreateCostDto createCostDto)
        {
            var createCost = await _costInterface.CreateCost(createCostDto);
            return Ok(createCost);
        }
        [HttpPut("UpdateCost")]
        public async Task<ActionResult<ResponseModel<CostModel>>> UpdateCost(UpdateCostDto updateCostDto)
        {
            var updateCost = await _costInterface.UpdateCost(updateCostDto);
            return Ok(updateCost);
        }
    }
}
