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

        [HttpGet("GetAdminUsers")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> GetAdminUsers()
        {
            var users = await _userInterface.GetAdminUsers();
            return Ok(users);
        }
        [HttpGet("GetMembersUsers")]
        public async Task<ActionResult<ResponseModel<List<UserModel>>>> GetMembersUsers()
        {
            var users = await _userInterface.GetMembersUsers();
            return Ok(users);
        }
        [HttpPost("CreateAdminUser")]
        public async Task<ActionResult<ResponseModel<UserModel>>> CreateAdminUser(CreateUserDto createUserAdminDto)
        {
            var users = await _userInterface.CreateAdminUser(createUserAdminDto);
            return Ok(users);
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> DeleteUser(int id)
        {
            var users = await _userInterface.DeleteUser(id);
            return Ok(users);
        }
        [HttpDelete("UpdateCurrentPasswordUser")]
        public async Task<ActionResult<ResponseModel<UserModel>>> UpdateCurrentPasswordUser(UpdateUserDto updateUserDto)
        {
            var users = await _userInterface.UpdateCurrentPasswordUser(updateUserDto);
            return Ok(users);
        }
    }
}
