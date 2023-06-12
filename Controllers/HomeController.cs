using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Data;
using ProductsAndCategories.Models;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductsAndCategories.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        ApplicationContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
            //начальные данные 
            if (!db.Categories.Any())
            {
                Category groceries = new Category { Name = "Бакалея" };
                Category meat = new Category { Name = "Мясо" };
                Category bread = new Category { Name = "Хлеб" };
                Category vegetables = new Category { Name = "Овощи" };

                Product user1 = new Product { Name = "Овсянные хлопья", Category = groceries };
                Product user2 = new Product { Name = "Крупа гречневая", Category = groceries };
                Product user3 = new Product { Name = "Хлеб ржаной", Category = bread };
                Product user4 = new Product { Name = "Хлеб тостовый", Category = bread };
                Product user5 = new Product { Name = "Хлеб деревенский", Category = bread };
                Product user6 = new Product { Name = "Филе говядины", Category = meat };
                Product user7 = new Product { Name = "Грудинка свиная", Category = meat };
                Product user8 = new Product { Name = "Перец сладкий", Category = vegetables };

                db.Categories.AddRange(groceries, bread, meat, vegetables);
                db.Products.AddRange(user1, user2, user3, user4, user5, user6, user7, user8);
                db.SaveChanges();
            }
        }

        public async Task<IActionResult> IndexAsync(string name, int category = 0, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 3;

            //фильтрация
            IQueryable<Product> users = db.Products.Include(x => x.Category);

            if (category != 0)
            {
                users = users.Where(p => p.CategoryId == category);
            }
            if (!string.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name!.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.CategoryAsc:
                    users = users.OrderBy(s => s.Category!.Name);
                    break;
                case SortState.CategoryDesc:
                    users = users.OrderByDescending(s => s.Category!.Name);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }

            // пагинация
            var count = await users.CountAsync();
            var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new FilterViewModel(db.Categories.ToList(), category, name),
                new SortViewModel(sortOrder)
            );
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}