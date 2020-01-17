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
    public class AssociationController : Controller
    {
        private MyContext dbContext;
        public AssociationController(MyContext context)
        {
            dbContext = context;
        }
        
        [HttpPost]
        public IActionResult AssociateCategory(Association newAssociation)
        {
            dbContext.Associations.Add(newAssociation);
            dbContext.SaveChanges();
            int catid = newAssociation.CategoryId;
            return RedirectToAction("Show", "Category", new {categoryId = catid});
        }

        [HttpPost]
        public IActionResult AssociateProduct(Association newAssociation)
        {
            dbContext.Associations.Add(newAssociation);
            dbContext.SaveChanges();
            int prodid = newAssociation.ProductId;
            return RedirectToAction("Show", "Product", new {productId = prodid});
        }
    }
}