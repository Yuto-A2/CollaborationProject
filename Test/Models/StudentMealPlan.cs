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
    public class StudentMealPlanDto
    {
        public int student_meal_plan_id { get; set; }

        public int student_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

        public int plan_id { get; set; }
     
}