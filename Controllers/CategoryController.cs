using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers
{
    public class CategoryController : Controller
    {
        private MyContext dbContext;
        public CategoryController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.AllCategories = dbContext.Categories;
            return View();
        }

        [HttpPost]
        public IActionResult NewCategory(Category newCategory)
        {
            if (ModelState.IsValid)
            {
                dbContext.Categories.Add(newCategory);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllCategories = dbContext.Categories;
            return View("Index");
        }

        [HttpGet("Category/{categoryId}")]
        public IActionResult Show(int categoryId)
        {
            var thisCategory = dbContext.Categories
                .Include(c => c.Associations)
                .ThenInclude(a => a.Product)
                .FirstOrDefault(c => c.CategoryId == categoryId);

            var unrelatedProducts = dbContext.Products
                .Include(p => p.Associations)
                .Where(p => p.Associations.All(a => a.CategoryId != categoryId));

            ViewBag.Unrelated = unrelatedProducts;
            return View(thisCategory);
        }
    }
}