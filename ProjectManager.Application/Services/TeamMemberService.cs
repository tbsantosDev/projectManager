using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.DTOs.TeamMemberDTO;
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
    public class TeamMemberService : ITeamMemberInterface
    {
        private readonly AppDbContext _context;
        public TeamMemberService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<TeamMemberModel>> CreateTeamMember(CreateTeamMemberDto createTeamMemberDto)
        {
            ResponseModel<TeamMemberModel> response = new();

            try
            {
                var team = await _context.Teams.FindAsync(createTeamMemberDto.TeamId);
                if (team == null)
                {
                    response.Status = false;
                    response.Message = "Time não encontrado.";
                    return response;
                }

                var user = await _context.Users.FindAsync(createTeamMemberDto.UserId);
                if (user == null)
                {
                    response.Status = false;
                    response.Message = "Usuário não encontrado.";
                    return response;
                }

                var teamMember = new TeamMemberModel()
                {
                    EntryDate = DateTime.UtcNow,
                    Position = createTeamMemberDto.Position,
                    TeamId = createTeamMemberDto.TeamId,
                    UserId = createTeamMemberDto.UserId,
                };

                _context.Add(teamMember);
                await _context.SaveChangesAsync();

                response.Dados = teamMember;
                response.Message = "Membro do time criado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<TeamMemberModel>> DeleteTeamMember(int id)
        {
            ResponseModel<TeamMemberModel> response = new();

            try
            {
                var teamMember = await _context.TeamMembers.FirstOrDefaultAsync(t => t.Id == id);
                if(teamMember == null)
                {
                    response.Message = "Nenhum membro de time localizado!";
                    response.Status = false;
                    return response;
                }

                _context.Remove(teamMember);
                await _context.SaveChangesAsync();

                response.Dados = teamMember;
                response.Message = "Membro de time excluido com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<TeamMemberModel>>> GetAllTeamMembers()
        {
            ResponseModel<List<TeamMemberModel>> response = new ResponseModel<List<TeamMemberModel>>();
            try
            {
                var teamMember = await _context.TeamMembers.ToListAsync();
                response.Dados = teamMember;
                response.Message = "Todos os times coletados com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<TeamMemberModel>>> GetMembersByTeamId(int teamId)
        {
            ResponseModel<List<TeamMemberModel>> response = new ResponseModel<List<TeamMemberModel>>();
            try
            {
                var teamIdExist = await _context.TeamMembers.FirstOrDefaultAsync(t => t.TeamId == teamId);
                if (teamIdExist == null)
                {
                    response.Message = "Time não encontrado.";
                    response.Status = false;
                    return response;
                }
                var membersByTeam = await _context.TeamMembers.Where(m => m.TeamId == teamId).ToListAsync();
                if (membersByTeam.Count == 0 || membersByTeam == null)
                {
                    response.Message = "Não existe nenhum membro neste associado a este time";
                    response.Status = false;
                    return response;
                }

                response.Dados = membersByTeam;
                response.Message = "Membros do time localizado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<TeamMemberModel>> UpdateTeamMember(UpdateTeamMemberDto updateTeamMemberDto)
        {
            ResponseModel<TeamMemberModel> response = new();
            try
            {
                var teamMember = await _context.TeamMembers.FirstOrDefaultAsync(t => t.Id == updateTeamMemberDto.Id);
                if (teamMember == null)
                {
                    response.Message = "Nenhum membro de time localizado!";
                    response.Status = false;
                    return response;
                }

                teamMember.Position = updateTeamMemberDto.Position;

                _context.Update(teamMember);
                await _context.SaveChangesAsync();

                response.Dados = teamMember;
                response.Message = "Membro de time alterado com sucesso!";

                return response;
            }
            catch (Exception ex) { 
            response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
