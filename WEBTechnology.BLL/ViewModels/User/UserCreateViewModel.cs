using System.ComponentModel.DataAnnotations;

namespace WEBTechnology.BLL.ViewModels.User
{
    /// <summary>
    /// Модель создания пользователя
    /// </summary>
    public class UserCreateViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Введите имя")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Поле Возраст обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Поле Почта обязательно для заполнения")]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string? Email { get; set; }
    }
}
