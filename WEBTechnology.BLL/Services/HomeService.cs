using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBTechnology.BLL.Services.IServices;
using WEBTechnology.BLL.ViewModels.Operation;
using WEBTechnology.BLL.ViewModels.User;
using WEBTechnology.DAL.Models;
using WEBTechnology.Project;
using static WEBTechnology.BLL.ViewModels.Operation.SortViewModel;

namespace WEBTechnology.BLL.Services
{
    public class HomeService : IHomeService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public IMapper _mapper;
        WebDbContext db;

        public HomeService(RoleManager<Role> roleManager, UserManager<User> userManager, IMapper mapper, WebDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            this.db = context;
            if (db.Roles.Count() == 0)
            {
                var userRole = new Role() { Name = "Пользователь", Description = "Стандартная роль приложения" };
                var supportRole = new Role() { Name = "Поддержка", Description = "Данная роль позволяет выполнять редактирование, удаление комментариев и статей в приложении" };
                var adminRole = new Role() { Name = "Администратор", Description = "Роль с максимальными возможностями в приложении" };
                var superAdminRole = new Role() { Name = "Супер Администратор", Description = "Роль со всеми возможностями" };

                var testUser = new UserCreateViewModel { Name = "Admin", Age = 18, Email = "admin@gmail.com", Id = Guid.Parse("00000000-0000-0000-0000-000000000000") };
                var testUser2 = new UserCreateViewModel { Name = "Support", Age = 16, Email = "support@gmail.com", Id = Guid.Parse("00000000-0000-0000-0000-000000000001") };
                var testUser3 = new UserCreateViewModel { Name = "User", Age = 22, Email = "user@gmail.com", Id = Guid.Parse("00000000-0000-0000-0000-000000000002") };
                var user = _mapper.Map<User>(testUser);
                var user2 = _mapper.Map<User>(testUser2);
                var user3 = _mapper.Map<User>(testUser3);

                _userManager.AddToRoleAsync(user, adminRole.Name);
                _userManager.AddToRoleAsync(user2, supportRole.Name);
                _userManager.AddToRoleAsync(user3, userRole.Name);

                db.Roles.AddRange(userRole, supportRole, adminRole, superAdminRole);
                db.Users.AddRange(user, user2, user3);

                db.SaveChanges();
            }
        }

        public async Task<IndexViewModel> Index(int role, string name, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 3;

            //фильтрация
            IQueryable<User> users = _userManager.Users.Include(x => x.Roles);

            if (role != null && role != 0)
            {
                users = users.Where(p => Convert.ToInt32(p.Id) == role);
            }
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.AgeAsc:
                    users = users.OrderBy(s => s.Age);
                    break;
                case SortState.AgeDesc:
                    users = users.OrderByDescending(s => s.Age);
                    break;
                case SortState.EmailAsc:
                    users = users.OrderBy(s => s.Email);
                    break;
                case SortState.EmailDesc:
                    users = users.OrderByDescending(s => s.Email);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }

            // пагинация
            var count = await users.CountAsync();
            var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(_userManager.Users.ToList(), role, name),
                Users = items
            };
            return viewModel;
        }





        //public async Task GenerateData()
        //{
        //    var testUser = new UserCreateViewModel { Name = "Admin", Age = 18, Email = "admin@gmail.com"};
        //    var testUser2 = new UserCreateViewModel { Name = "Support", Age = 16, Email = "support@gmail.com"};
        //    var testUser3 = new UserCreateViewModel { Name = "User", Age = 22, Email = "user@gmail.com" };
        //    var user = _mapper.Map<User>(testUser);
        //    var user2 = _mapper.Map<User>(testUser2);
        //    var user3 = _mapper.Map<User>(testUser3);

        //    var userRole = new Role() { Name = "Пользователь", Description = "Стандартная роль приложения" };
        //    var supportRole = new Role() { Name = "Поддержка", Description = "Данная роль позволяет выполнять редактирование, удаление комментариев и статей в приложении" };
        //    var adminRole = new Role() { Name = "Администратор", Description = "Роль с максимальными возможностями в приложении" };
        //    var superAdminRole = new Role() { Name = "Супер Администратор", Description = "Роль со всеми возможностями" };

        //    await _roleManager.CreateAsync(userRole);
        //    await _roleManager.CreateAsync(supportRole);
        //    await _roleManager.CreateAsync(adminRole);
        //    await _roleManager.CreateAsync(superAdminRole);

        //    await _userManager.AddToRoleAsync(user, adminRole.Name);
        //    await _userManager.AddToRoleAsync(user2, supportRole.Name);
        //    await _userManager.AddToRoleAsync(user3, userRole.Name);
        //}
    }
}
