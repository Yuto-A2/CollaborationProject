using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Recipe
    {
        [Key]
        public int recipe_id { get; set; }
        public string recipe_name { get; set; }
    }
}