namespace WEBTechnology.BLL.ViewModels.Operation
{
    public class SortViewModel
    {
        public enum SortState
        {
            NameAsc,    // по имени по возрастанию
            NameDesc,   // по имени по убыванию
            AgeAsc, // по возрасту по возрастанию
            AgeDesc,    // по возрасту по убыванию
            EmailAsc, // по email по возрастанию
            EmailDesc // по компании по убыванию
        }

        public SortState NameSort { get; private set; } // значение для сортировки по имени
        public SortState AgeSort { get; private set; }    // значение для сортировки по возрасту
        public SortState EmailSort { get; private set; }   // значение для сортировки по email
        public SortState Current { get; private set; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            AgeSort = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            EmailSort = sortOrder == SortState.EmailAsc ? SortState.EmailDesc : SortState.EmailAsc;
            Current = sortOrder;
        }
    }
}
