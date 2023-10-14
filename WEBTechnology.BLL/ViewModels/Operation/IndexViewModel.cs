namespace WEBTechnology.BLL.ViewModels.Operation
{
    public class IndexViewModel
    {
        public IEnumerable<DAL.Models.User>? Users { get; set; }
        public PageViewModel? PageViewModel { get; set; }
        public FilterViewModel? FilterViewModel { get; set; }
        public SortViewModel? SortViewModel { get; set; }
    }
}
