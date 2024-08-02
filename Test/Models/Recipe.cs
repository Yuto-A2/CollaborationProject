using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Recipe
    {
        [Key]
        public int recipe_id { get; set; }
        public string recipe_name { get; set; }

        // each recipe belongs to one food category
        // each category has many recipes
        [ForeignKey("Category")]
        public int category_id { get; set; }
        public virtual Category Category { get; set; }

    }
}