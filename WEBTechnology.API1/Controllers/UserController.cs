using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEBTechnology.Project.Models;
using WEBTechnology.Project.Services.IServices;
using WEBTechnology.Project.ViewModels.User;

namespace WEBTechnology.API1.Controllers
{
    [ApiController]
    [Route("controller")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <remarks>
        /// 
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "Name": "Tester",
        ///        "Age": "18",
        ///        "Email": "test@gmail.com", 
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(UserCreateViewModel model)
        {
            var result = await _userService.CreateUser(model);

            return StatusCode(result.Succeeded ? 201 : 204);
        }

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        [HttpPatch]
        [Route("EditUser")]
        public async Task<IActionResult> EditUser(UserEditViewModel model)
        {
            var result = await _userService.EditUser(model.Id);

            return StatusCode(201);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        [HttpDelete]
        [Route("RemoveUser")]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            await _userService.RemoveUser(id);

            return StatusCode(201);
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        [HttpGet]
        [Route("GetUsers")]
        public Task<List<User>> GetUsers()
        {
            var users = _userService.GetUsers().Result;
            var userResponse = users.Select(u => new User()
            {
                Id = u.Id,
                Name = u.Name,
                Age = u.Age,
                Email = u.Email,
            }).ToList();

            return Task.FromResult(userResponse);
        }

    }
}
