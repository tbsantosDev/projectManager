using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.DTOs.UserDTO;
using ProjectManager.Application.Interfaces;
using ProjectManager.Data;
using ProjectManager.Domain.Models;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Services
{
    public class UserService : IUserInterface
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<UserModel>> CreateAdminUser(CreateUserDto createUserDto)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == createUserDto.Email);
                if (existingUser != null) {
                    response.Message = "Este e-mail já está em uso!";
                    response.Status = false;
                    return response;
                }

                var hashPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

                var user = new UserModel
                {
                    Name = createUserDto.Name,
                    Email = createUserDto.Email,
                    Password = hashPassword,
                    Role = Models.Enums.UserEnums.Admin
                };

                _context.Add(user);
                await _context.SaveChangesAsync();

                response.Message = "Usuário cadastrado com sucesso!";
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

        public async Task<ResponseModel<UserModel>> CreateMemberUser(CreateUserDto createUserDto)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == createUserDto.Email);
                if (existingUser != null)
                {
                    response.Message = "Este e-mail já está em uso!";
                    response.Status = false;
                    return response;
                }

                var hashPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

                var user = new UserModel
                {
                    Name = createUserDto.Name,
                    Email = createUserDto.Email,
                    Password = hashPassword,
                    Role = Models.Enums.UserEnums.Member
                };

                _context.Add(user);
                await _context.SaveChangesAsync();

                response.Message = "Usuário cadastrado com sucesso!";
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

        public async Task<ResponseModel<UserModel>> DeleteUser(int id)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user != null) {
                    response.Message = "Usuário não localizado!";
                    response.Status = false;
                    return response;
                }

                _context.Remove(user);
                await _context.SaveChangesAsync();

                response.Message = "Usuário excluido com sucesso!";
                response.Status = true;
                return response;

            }
            catch (Exception ex) { 
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<UserModel>>> GetAdminUsers()
        {
            ResponseModel<List<UserModel>> response = new ResponseModel<List<UserModel>>();

            try
            {
                var usersAdmin = await _context.Users.Where(u => u.Role == Models.Enums.UserEnums.Admin).ToListAsync();

                response.Message = "Usuários Admins coletados com sucesso!";
                response.Status = true;
                return response;
            }
            catch(Exception ex)
            {
                response.Message= ex.Message;
                response.Status = false;
                return response;
            }   
        }

        public async Task<ResponseModel<List<UserModel>>> GetMembersUsers()
        {
            ResponseModel<List<UserModel>> response = new ResponseModel<List<UserModel>>();

            try
            {
                var usersMembers = await _context.Users.Where(u => u.Role == Models.Enums.UserEnums.Member).ToListAsync();

                response.Message = "Usuários membros coletados com sucesso!";
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

        public async Task<ResponseModel<UserModel>> UpdateCurrentUser(UpdateUserDto updateUserDto)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    response.Message = "Usuário não encontrado.";
                    response.Status = false;
                    return response;
                }

                var userId = int.Parse(userIdClaim.Value);

                var hashPassword = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    response.Message = "Nenhum usuário localizado!";
                    return response;
                }

                user.Name = updateUserDto.Name;
                user.Password = hashPassword;

                _context.Update(user);
                await _context.SaveChangesAsync();

                response.Dados = user;
                response.Message = "Dados do Usuário editado com sucesso!";
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
