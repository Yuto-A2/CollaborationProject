using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Meal
    {
        [Key]
        public int meal_id { get; set; }

        public string meal_date { get; set; }

        // each meal belongs to one meal plan tier
        // each meal plan tier has many meals
        [ForeignKey("MealPlan")]
        public int plan_id { get; set; }
        public virtual MealPlan MealPlan { get; set; }
    }
}
