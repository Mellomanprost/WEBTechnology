using Microsoft.AspNetCore.Mvc;
using WEBTechnology.BLL.ViewModels.Operation;

namespace WEBTechnology.BLL.Services.IServices
{
    public interface IHomeService
    {
        //Task GenerateData();

        Task<IndexViewModel> Index(int a, string b, int c, SortViewModel.SortState d);
    }
}
