using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Ingredient
    {
        [Key]
        public int ingredient_id { get; set; }
        public string ingredient_name {  get; set; }
        public bool  is_allergen { get; set; }

    }
}