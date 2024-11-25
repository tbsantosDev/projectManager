using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.DTOs.UserDTO;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Models;
using ProjectManager.Models;

namespace ProjectManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserInterface _userInterface;

        public UsersController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }
        [Authorize]
        [HttpGet("GetAdminUsers")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> GetAdminUsers()
        {
            var users = await _userInterface.GetAdminUsers();
            return Ok(users);
        }
        [Authorize]
        [HttpGet("GetMembersUsers")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> GetMembersUsers()
        {
            var users = await _userInterface.GetMembersUsers();
            return Ok(users);
        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> GetCurrentUser()
        {
            var user = await _userInterface.GetCurrentUser();
            return Ok(user);
        }
        [Authorize]
        [HttpPost("CreateAdminUser")]
        public async Task<ActionResult<ResponseModel<UserModel>>> CreateAdminUser(CreateUserDto createUserAdminDto)
        {
            var users = await _userInterface.CreateAdminUser(createUserAdminDto);
            return Ok(users);
        }
        [HttpPost("requestPasswordReset")]
        public async Task<ActionResult<ResponseModel<string>>> RequestPasswordReset([FromBody] string email)
        {
            Console.WriteLine($"Parametros que chegaram ao backend {email}");
            var user = await _userInterface.RequestPasswordReset(email);
            return Ok(user);
        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult<ResponseModel<string>>> ResetPassword([FromBody] ResetUserPasswordDto resetUserPasswordDto)
        {
            var result = await _userInterface.ResetPassword(
                resetUserPasswordDto.Email,
                resetUserPasswordDto.Token,
                resetUserPasswordDto.NewPassword
                );
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> DeleteUser(int id)
        {
            var users = await _userInterface.DeleteUser(id);
            return Ok(users);
        }
        [Authorize]
        [HttpPut("UpdateCurrentPasswordUser")]
        public async Task<ActionResult<ResponseModel<UserModel>>> UpdateCurrentPasswordUser(UpdateUserDto updateUserDto)
        {
            var users = await _userInterface.UpdateCurrentPasswordUser(updateUserDto);
            return Ok(users);
        }

    }
}
