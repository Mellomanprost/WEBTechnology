using Microsoft.AspNetCore.Identity;
using WEBTechnology.BLL.ViewModels.Role;
using WEBTechnology.BLL.ViewModels.User;
using WEBTechnology.DAL.Models;

namespace WEBTechnology.BLL.Services.IServices
{
    public interface IUserService
    {
        Task GenerateData();

        Task<List<User>> GetUsers();

        Task<User> GetUser(Guid id);

        Task<Guid> AddRoleUser(RoleCreateViewModel model);

        Task<IdentityResult> CreateUser(UserCreateViewModel model);

        Task<UserEditViewModel> EditUser(Guid id);

        Task<IdentityResult> EditUser(UserEditViewModel model);

        Task RemoveUser(Guid id);
    }
}
