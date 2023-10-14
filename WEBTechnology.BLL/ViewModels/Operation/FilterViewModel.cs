using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEBTechnology.BLL.ViewModels.Operation
{
    public class FilterViewModel
    {
        public FilterViewModel(List<DAL.Models.User> users, int? user, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            users.Insert(0, new DAL.Models.User { Name = "Все", Id = 0.ToString()});
            Users = new SelectList(users, "Id", "Name", user);
            SelectedUser = user;
            SelectedName = name;
        }
        public SelectList Users { get; private set; } // список пользователей
        public int? SelectedUser { get; private set; }   // выбранный пользователь
        public string SelectedName { get; private set; }    // введенное имя
    }
}
