using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductsAndCategories.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Category> categories, int category, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { Name = "Все", Id = 0 });
            Categories = new SelectList(categories, "Id", "Name", category);
            SelectedCategory = category;
            SelectedName = name;
        }
        public SelectList Categories { get; } 
        public int SelectedCategory { get; } 
        public string SelectedName { get; } 
    }
}
