using System.ComponentModel.DataAnnotations;

namespace WEBTechnology.BLL.ViewModels.Role
{
    /// <summary>
    /// Модель изменения роли пользователя
    /// </summary>
    public class RoleEditViewModel
    {
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string? Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Описание роли", Prompt = "Описание")]
        public string? Description { get; set; } = null;
    }
}
