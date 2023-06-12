namespace ProductsAndCategories.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; } // значение для сортировки по имени
        public SortState CategorySort { get; }   // значение для сортировки по категории
        public SortState Current { get; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            CategorySort = sortOrder == SortState.CategoryAsc ? SortState.CategoryDesc : SortState.CategoryAsc;
            Current = sortOrder;
        }
    }
}
