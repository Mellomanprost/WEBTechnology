using System.ComponentModel.DataAnnotations;

namespace WEBTechnology.BLL.ViewModels.Role
{
    /// <summary>
    /// Модель роли
    /// </summary>
    public class RoleViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "Name")]
        public string? Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
