using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.DTOs.CostDTO;
using ProjectManager.Application.Interfaces;
using ProjectManager.Data;
using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Services
{
    public class CostService : ICostInterface
    {
        private readonly AppDbContext _context;
        public CostService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<CostModel>> CreateCost(CreateCostDto createCostDto)
        {
            ResponseModel<CostModel> response = new();
            try
            {
                var createCost = new CostModel()
                {
                    Description = createCostDto.Description,
                    Value = createCostDto.Value,
                    CreatedAt = DateTime.UtcNow,
                    ProjectId = createCostDto?.ProjectId,
                    TaskId = createCostDto?.TaskId,
                    TeamId = createCostDto?.TeamId,
                };
                _context.Add(createCost);
                await _context.SaveChangesAsync();

                response.Dados = createCost;
                response.Message = "Custo criado com sucesso!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<CostModel>>> GetCostByProject(int projectId)
        {
            ResponseModel<List<CostModel>> response = new();
            try
            {
                var costByProject = await _context.Costs.Where(c => c.ProjectId == projectId).ToListAsync();

                if (costByProject.Count == 0)
                {
                    response.Message = "Projeto não existe ou não tem nenhum custo associado.";
                    return response;
                }

                response.Dados = costByProject;
                response.Message = "Custos coletados com sucesso!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<List<CostModel>>> GetCostByTask(int taskId)
        {
            ResponseModel<List<CostModel>> response = new();
            try
            {
                var costByTask = await _context.Costs.Where(c => c.TaskId == taskId).ToListAsync();

                if (costByTask.Count == 0)
                {
                    response.Message = "Tarefa inexistente ou não tem nenhum custo associado.";
                    return response;
                }

                response.Dados = costByTask;
                response.Message = "Custos coletados com sucesso!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<CostModel>>> GetCostByTeam(int teamId)
        {
            ResponseModel<List<CostModel>> response = new();
            try
            {
                var costByTeam = await _context.Costs.Where(c => c.ProjectId == teamId).ToListAsync();

                if (costByTeam.Count == 0)
                {
                    response.Message = "Time inexistente ou não tem nenhum custo associado.";
                    return response;
                }

                response.Dados = costByTeam;
                response.Message = "Custos coletados com sucesso!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<CostModel>> UpdateCost(UpdateCostDto updateCostDto)
        {
            ResponseModel<CostModel> response = new();
            try
            {
                var updateCost = await _context.Costs.FirstOrDefaultAsync(c => c.Id == updateCostDto.Id);
                if (updateCost == null) {
                    response.Message = "Custo não encontrado!";
                    return response;
                }

                updateCost.Description = updateCostDto.Description;
                updateCost.Value = updateCostDto.Value;
                updateCost.ProjectId = updateCostDto?.ProjectId;
                updateCost.TaskId = updateCostDto?.TaskId;
                updateCost.TeamId = updateCostDto?.TeamId;

                _context.Costs.Update(updateCost);
                await _context.SaveChangesAsync();

                response.Dados = updateCost;
                response.Message = "Custo atualizado com sucesso!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message += ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
