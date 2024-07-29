using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class StudentMealPlan
    {
        [Key]
        public int student_meal_plan_id { get; set; }
        [ForeignKey("Student")]
        public int student_id { get; set; }
        public virtual Student Student { get; set; }
        [ForeignKey("MealPlan")]
        public int plan_id { get; set; }
        public virtual MealPlan MealPlan { get; set; }
    }
}