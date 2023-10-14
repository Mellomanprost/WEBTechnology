using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WEBTechnology.BLL.Services.IServices;
using WEBTechnology.BLL.ViewModels.Role;
using WEBTechnology.BLL.ViewModels.User;
using WEBTechnology.DAL.Models;

namespace WEBTechnology.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, /*SignInManager<User> signInManager,*/ RoleManager<Role> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task GenerateData()
        {
            var testUser = new UserCreateViewModel { Name = "Admin", Age = 18, Email = "admin@gmail.com" };
            var testUser2 = new UserCreateViewModel { Name = "Support", Age = 16, Email = "support@gmail.com" };
            var testUser3 = new UserCreateViewModel { Name = "User", Age = 22, Email = "user@gmail.com" };
            var user = _mapper.Map<User>(testUser);
            var user2 = _mapper.Map<User>(testUser2);
            var user3 = _mapper.Map<User>(testUser3);

            var userRole = new Role() { Name = "Пользователь", Description = "Стандартная роль приложения" };
            var supportRole = new Role() { Name = "Поддержка", Description = "Данная роль позволяет выполнять редактирование, удаление комментариев и статей в приложении" };
            var adminRole = new Role() { Name = "Администратор", Description = "Роль с максимальными возможностями в приложении" };
            var superAdminRole = new Role() { Name = "Супер Администратор", Description = "Роль со всеми возможностями" };

            await _roleManager.CreateAsync(userRole);
            await _roleManager.CreateAsync(supportRole);
            await _roleManager.CreateAsync(adminRole);
            await _roleManager.CreateAsync(superAdminRole);

            await _userManager.AddToRoleAsync(user, adminRole.Name);
            await _userManager.AddToRoleAsync(user2, supportRole.Name);
            await _userManager.AddToRoleAsync(user3, userRole.Name);
        }


        public async Task<List<User>> GetUsers()
        {
            var users = _userManager.Users.Include(u => u.Roles).ToList();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    var newRole = new Role { Name = role };
                    user.Roles.Add(newRole);
                }
            }
            return users;
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<IdentityResult> CreateUser(UserCreateViewModel model)
        {
            var user = new User();
            if (model.Name != null)
            {
                user.Name = model.Name;
                user.UserName = model.Name;
            }
            if (model.Age > 0)
            {
                user.Age = model.Age;
            }
            if (model.Email != null)
            {
                user.Email = model.Email;
            }
            var roleUser = new Role() { Name = "Пользователь", Description = "Стандартная роль приложения" };
            var result = await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, roleUser.Name);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> AddRoleUser(RoleCreateViewModel model) /// Не тот метод, переделать
        {
            var role = new Role() { Name = model.Name, Description = model.Description };
            await _roleManager.CreateAsync(role);
            return Guid.Parse(role.Id);
        }

        public async Task<UserEditViewModel> EditUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var allRolesName = _roleManager.Roles.ToList();
            var model = new UserEditViewModel
            {
                Name = user.Name,
                Age = user.Age,
                Email = user.Email,
                Id = id,
                Roles = allRolesName.Select(r => new RoleViewModel() { Id = new string(r.Id), Name = r.Name }).ToList(),
            };
            return model;
        }

        public async Task<IdentityResult> EditUser(UserEditViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            if (model.Name != null)
            {
                user.Name = model.Name;
                user.UserName = model.Name;
            }
            if (model.Age > 0)
            {
                user.Age = model.Age;
            }
            if (model.Email != null)
            {
                user.Email = model.Email;
            }

            foreach (var role in model.Roles)
            {
                var roleName = _roleManager.FindByIdAsync(role.Id?.ToString()).Result.Name;
                if (role.IsSelected)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task RemoveUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);
        }
    }
}
