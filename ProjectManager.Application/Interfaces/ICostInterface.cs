using ProjectManager.Application.DTOs.CostDTO;
using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Interfaces
{
    public interface ICostInterface
    {
        Task<ResponseModel<List<CostModel>>> GetCostByProject(int projectId);
        Task<ResponseModel<List<CostModel>>> GetCostByTeam(int teamId);
        Task<ResponseModel<List<CostModel>>> GetCostByTask(int taskId);
        Task<ResponseModel<CostModel>> CreateCost(CreateCostDto createCostDto);
        Task<ResponseModel<CostModel>> UpdateCost(UpdateCostDto updateCostDto);

    }
}
