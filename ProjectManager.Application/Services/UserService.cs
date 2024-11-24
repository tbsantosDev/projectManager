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
using System.Net.Mail;
using System.Net;
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

        public async Task<ResponseModel<UserModel>> CreateAdminUser(CreateUserDto createUserAdminDto)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == createUserAdminDto.Email);
                if (existingUser != null) {
                    response.Message = "Este e-mail já está em uso!";
                    response.Status = false;
                    return response;
                }

                var hashPassword = BCrypt.Net.BCrypt.HashPassword(createUserAdminDto.Password);

                var user = new UserModel
                {
                    Name = createUserAdminDto.Name,
                    Email = createUserAdminDto.Email,
                    Password = hashPassword,
                    CreatedAt = DateTime.UtcNow,
                    Role = Models.Enums.UserEnums.Admin,
                    EmailConfirmed = true,                  
                };

                _context.Add(user);
                await _context.SaveChangesAsync();

                response.Message = "Usuário cadastrado com sucesso!";
                response.Status = true;
                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UserModel>> CreateMemberUser(CreateUserDto createUserMemberDto)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == createUserMemberDto.Email);
                if (existingUser != null)
                {
                    response.Message = "Este e-mail já está em uso!";
                    response.Status = false;
                    return response;
                }

                var hashPassword = BCrypt.Net.BCrypt.HashPassword(createUserMemberDto.Password);

                var user = new UserModel
                {
                    Name = createUserMemberDto.Name,
                    Email = createUserMemberDto.Email,
                    Password = hashPassword,
                    CreatedAt = DateTime.UtcNow,
                    Role = Models.Enums.UserEnums.Member,
                    EmailConfirmationToken = Guid.NewGuid().ToString()
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                await SendConfirmationEmail(user);

                response.Message = "Usuário cadastrado com sucesso! Verifique seu e-mail para confirmar o registro.";
                response.Status = true;
                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
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
                if (user == null) {
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

                response.Dados = usersAdmin;
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

                response.Dados = usersMembers;
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

        public async Task<ResponseModel<string>> RequestPasswordReset(string email)
        {
            var response = new ResponseModel<string>();

            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    response.Message = "E-mail não encontrado.";
                    response.Status = false;
                    return response;
                }

                user.PasswordResetToken = Guid.NewGuid().ToString();
                user.PasswordResetTokenExpires = DateTime.UtcNow.AddHours(1);

                _context.Update(user);
                await _context.SaveChangesAsync();

                var resetLink = $"http://localhost:3000/forgetPassword?token={user.PasswordResetToken}&email={user.Email}";

                var appPassword = Environment.GetEnvironmentVariable("APP_PASSWORD_GOOGLE");
                if (string.IsNullOrEmpty(appPassword))
                {
                    response.Message = "Erro interno: Senha do aplicativo não configurada.";
                    response.Status = false;
                    return response;
                }

                // Configurar o cliente SMTP
                using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587; // Porta para envio SMTP, geralmente 587 ou 465
                    smtpClient.Credentials = new NetworkCredential("seuemail@gmail.com", appPassword);
                    smtpClient.EnableSsl = true;

                    // Configurar o e-mail a ser enviado
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("seuemail@gmail.com", "Nome do seu negocio"),
                        Subject = "Redefinição de senha",
                        Body = $"Olá {user.Name},\n\nPor favor, redefina sua senha clicando no link abaixo:\n{resetLink}",
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add(user.Email);

                    // Enviar o e-mail
                    await smtpClient.SendMailAsync(mailMessage);
                }

                response.Message = "E-mail de redefinição de senha enviado.";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = $"Erro interno: {ex.Message}";
                response.Status = false;
            }

            return response;
        }

        public async Task<ResponseModel<string>> ResetPassword(string email, string token, string newPassword)
        {
            var response = new ResponseModel<string>();

            try
            {
                var user = await _context.Users
                    .SingleOrDefaultAsync(u => u.Email == email && u.PasswordResetToken == token);

                if (user == null || user.PasswordResetTokenExpires < DateTime.UtcNow)
                {
                    response.Message = "Token inválido ou expirado.";
                    response.Status = false;
                    return response;
                }

                // Atualizar a senha
                user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                user.PasswordResetToken = null;
                user.PasswordResetTokenExpires = null;

                _context.Update(user);
                await _context.SaveChangesAsync();

                response.Message = "Senha redefinida com sucesso.";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = $"Erro interno: {ex.Message}";
                response.Status = false;
            }

            return response;
        }

        public async Task SendConfirmationEmail(UserModel user)
        {
            try
            {
                var confirmationLink = $"http://localhost:3000/confirmEmail?token={user.EmailConfirmationToken}";

                var appPassword = Environment.GetEnvironmentVariable("APP_PASSWORD_GOOGLE");
                if (string.IsNullOrEmpty(appPassword))
                {
                    Console.WriteLine("Senha APP não configurada");
                }

                // Configurar o cliente SMTP
                using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587; // Porta para envio SMTP, geralmente 587 ou 465
                    smtpClient.Credentials = new NetworkCredential("seuemail@gmail.com", appPassword);
                    smtpClient.EnableSsl = true;

                    // Configurar o e-mail a ser enviado
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("seuemail@gmail.com", "Nome do seu negocio"),
                        Subject = "Confirmação de E-mail",
                        Body = $"Olá {user.Name},\n\nPor favor, confirme seu e-mail clicando no link abaixo:\n{confirmationLink}",
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add(user.Email);

                    // Enviar o e-mail
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                // Tratamento de erro no envio de e-mail (registrar erro, etc)
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
            }
        }

        public async Task<ResponseModel<UserModel>> UpdateCurrentPasswordUser(UpdateUserDto updateUserDto)
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

                var hashPassword = BCrypt.Net.BCrypt.HashPassword(updateUserDto.NewPassword);

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    response.Message = "Nenhum usuário localizado!";
                    return response;
                }
                if (!BCrypt.Net.BCrypt.Verify(updateUserDto.CurrentPassword, user.Password)) {
                    response.Message = $"Senha atual não confere. user.password{user.Password} --- currentPassword{updateUserDto.CurrentPassword}";
                    return response;
                }
                if (updateUserDto.NewPassword != updateUserDto.ConfirmPassword)
                {
                    response.Message = "Valores dos campos nova senha e confirmação devem ser iguais.";
                    return response;
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserDto.NewPassword);

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
