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
    public class ProductController : Controller
    {
        private MyContext dbContext;
        public ProductController(MyContext context)
        {
            dbContext = context;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.AllProducts = dbContext.Products;
            return View();
        }

        [HttpPost]
        public IActionResult NewProduct(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                dbContext.Products.Add(newProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllProducts = dbContext.Products;
            return View("Index");
        }
        [HttpGet("Product/{productId}")]
        public IActionResult Show(int productId)
        {
            var thisProduct = dbContext.Products
                .Include(p => p.Associations)
                .ThenInclude(a => a.Category)
                .FirstOrDefault(p => p.ProductId == productId);

            var unrelatedCategories = dbContext.Categories
                .Include(c => c.Associations)
                .Where(c => c.Associations.All(a => a.ProductId != productId));

            ViewBag.Unrelated = unrelatedCategories;
            return View(thisProduct);
        }
    }
}