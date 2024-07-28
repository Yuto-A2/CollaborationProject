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
        [ForeignKey("MealPlan")]
        public int plan_id { get; set; }
        public virtual MealPlan MealPlan { get; set; }
    }
}
