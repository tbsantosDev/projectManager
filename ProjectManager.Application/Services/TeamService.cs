using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.DTOs.TeamDTO;
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
    public class TeamService : ITeamInterface
    {
        private readonly AppDbContext _context;
        public TeamService(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<ResponseModel<TeamModel>> CreateTeam(CreateTeamDto createTeamDto)
        {
            ResponseModel<TeamModel> response = new ResponseModel<TeamModel>();
            try
            {
                var team = new TeamModel()
                {
                    Name = createTeamDto.Name,
                    Description = createTeamDto.Description,
                };

                _context.Teams.Add(team);
                await _context.SaveChangesAsync();

                response.Dados = team;
                response.Message = "Time criado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<TeamModel>> DeleteTeam(int id)
        {
            ResponseModel<TeamModel> response = new ResponseModel<TeamModel>();
            try
            {
                var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
                if (team == null)
                {
                    response.Message = "Nenhum time localizado!";
                    response.Status = false;
                    return response;
                }
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();

                response.Dados = team;
                response.Message = "Time excluido com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<TeamModel>> GetTeam(int id)
        {
            ResponseModel<TeamModel> response = new ResponseModel<TeamModel>();
            try
            {
                var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
                if (team == null)
                {
                    response.Message = "Nenhum time localizado!";
                    response.Status = false;
                    return response;
                }

                response.Dados = team;
                response.Message = "Time coletado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<TeamModel>>> GetTeams()
        {
            ResponseModel<List<TeamModel>> response = new ResponseModel<List<TeamModel>>();
            try
            {
                var teams = await _context.Teams.ToListAsync();

                response.Dados = teams;
                response.Message = "Todos os times coletados!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<TeamModel>> UpdateTeam(UpdateTeamDto updateTeamDto)
        {
            ResponseModel<TeamModel> response = new ResponseModel<TeamModel>();
            try
            {
                var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == updateTeamDto.Id);
                if (team == null)
                {
                    response.Message = "Time não localizado!";
                    response.Status = false;
                    return response;
                }

                team.Name = updateTeamDto.Name;
                team.Description = updateTeamDto.Description;

                _context.Teams.Update(team);
                await _context.SaveChangesAsync();

                response.Dados = team;
                response.Message = "Time atualizado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
