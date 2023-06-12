using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductsAndCategories.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
