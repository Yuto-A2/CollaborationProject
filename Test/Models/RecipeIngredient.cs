using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class RecipeIngredient
    {
        [Key]
        public int recipe_id { get; set; }
        public string quantity {  get; set; }
        // made incorrectly
        [ForeignKey("Ingredient")]
        public int ingredient_id { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}