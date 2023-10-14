using System.ComponentModel.DataAnnotations;
using WEBTechnology.BLL.ViewModels.Role;

namespace WEBTechnology.BLL.ViewModels.User
{
    /// <summary>
    /// Модель изменения данных пользователя
    /// </summary>
    public class UserEditViewModel
    {
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Имя")]
        public string? Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Возраст", Prompt = "Возраст")]
        public int Age { get; set; }

        [EmailAddress]
        [Display(Name = "Почта")]
        public string? Email { get; set; }

        [Display(Name = "Роли")]
        public List<RoleViewModel>? Roles { get; set; }
    }
}
