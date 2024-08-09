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
        // change primary key to recipeingredient_id
        public int recipe_ingredient_id { get; set; }
        public string quantity {  get; set; }

        [ForeignKey("Ingredient")]
        public int ingredient_id { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        // add foreign key for recipe_id
        [ForeignKey("Recipe")]
        public int recipe_id { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}