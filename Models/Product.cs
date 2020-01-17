using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsAndCategories.Models
{
    public class Product
    {
       [Key]
       public int ProductId {get;set;}
       [Required]
       public string Name {get;set;}
       [Required]
       public string Description {get;set;}
       [Required]
       public decimal Price {get;set;}
       public DateTime CreatedAt {get;set;} = DateTime.Now;
       public DateTime UpdateAt {get;set;} = DateTime.Now;
       public List<Association> Associations {get;set;}
    }
}