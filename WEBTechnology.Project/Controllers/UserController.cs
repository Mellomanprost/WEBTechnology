using Microsoft.AspNetCore.Mvc;
using WEBTechnology.BLL.Services;
using WEBTechnology.BLL.Services.IServices;
using WEBTechnology.BLL.ViewModels.User;

namespace WEBTechnology.Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        //private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// [Get] Метод, получения всех пользователей
        /// </summary>
        [Route("User/Get")]
        //[Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            await _userService.GenerateData();

            return View(users);
        }

        /// <summary>
        /// [Get] Метод, получения одного пользователя по Id
        /// </summary>
        [Route("User/Details")]
        //[Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> DetailsUser(Guid id)
        {
            var model = await _userService.GetUser(id);

            return View(model);
        }

        /// <summary>
        /// [Get] Метод, создания пользователя
        /// </summary>
        [Route("User/Create")]
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        /// <summary>
        /// [Post] Метод, создания пользователя
        /// </summary>
        [Route("User/Create")]
        [HttpPost]
        public async Task<IActionResult> AddUser(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateUser(model);

                if (result.Succeeded)
                {
                    //Logger.Info($"Создан аккаунт, пользователем с правами администратора, с использованием адреса - {model.Email}");
                    return RedirectToAction("GetUsers", "User");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        /// <summary>
        /// [Get] Метод, редактирования пользователя
        /// </summary>
        [Route("User/Edit")]
        //[Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> EditUser(Guid id)
        {
            var model = await _userService.EditUser(id);

            return View(model);
        }

        /// <summary>
        /// [Post] Метод, редактирования пользователя
        /// </summary>
        [Route("User/Edit")]
        //[Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditUser(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.EditUser(model);
                //Logger.Info($"Аккаунт {model.UserName} был изменен");

                return RedirectToAction("GetUsers", "User");
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// [Get] Метод, удаление пользователя
        /// </summary>
        [Route("User/Remove")]
        //[Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveUser(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemoveUser(id);

            return RedirectToAction("GetUsers", "User");
        }

        /// <summary>
        /// [Post] Метод, удаление пользователя
        /// </summary>
        [Route("User/Remove")]
        //[Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            var account = await _userService.GetUser(id);
            await _userService.RemoveUser(id);
            //Logger.Info($"Аккаунт с id - {id} удален");

            return RedirectToAction("GetUsers", "User");
        }
    }
}
