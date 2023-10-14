using Microsoft.AspNetCore.Mvc;
using WEBTechnology.BLL.Services.IServices;
using WEBTechnology.BLL.ViewModels.Role;
using WEBTechnology.DAL.Models;

namespace WEBTechnology.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Получение всех ролей
        /// </summary>
        [HttpGet]
        [Route("GetRoles")]
        public async Task<IEnumerable<Role>> GetRoles()
        {
            var roles = await _roleService.GetRoles();

            return roles;
        }

        /// <summary>
        /// Добавление роли
        /// </summary>
        /// <remarks>
        /// 
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "name": "SuperUser",
        ///        "description": "VIP User with extended access rights"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole(RoleCreateViewModel model)
        {
            await _roleService.CreateRole(model);

            return StatusCode(201);
        }

        /// <summary>
        /// Редактирование роли
        /// </summary>
        [HttpPatch]
        [Route("EditRole")]
        public async Task<IActionResult> EditRole(RoleEditViewModel model)
        {
            await _roleService.EditRole(model);

            return StatusCode(201);
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        [HttpDelete]
        [Route("RemoveRole")]
        public async Task<IActionResult> RemoveRole(Guid id)
        {
            await _roleService.RemoveRole(id);

            return StatusCode(201);
        }
    }
}
