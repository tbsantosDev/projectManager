using ProjectManager.Application.DTOs.UserDTO;
using ProjectManager.Domain.Models;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Interfaces
{
    public interface IUserInterface
    {
        Task<ResponseModel<List<UserModel>>> GetAdminUsers();
        Task<ResponseModel<List<UserModel>>> GetMembersUsers();
        Task<ResponseModel<UserModel>> CreateAdminUser(CreateUserDto createUserDto);
        Task<ResponseModel<UserModel>> CreateMemberUser(CreateUserDto createUserDto);
        Task<ResponseModel<UserModel>> UpdateCurrentPasswordUser(UpdateUserDto updateUserDto);
        Task<ResponseModel<UserModel>> DeleteUser(int id);
    }
}
