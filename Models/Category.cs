using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductsAndCategories.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Product> Products { get; set; } = new();
    }
}
