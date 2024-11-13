using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
