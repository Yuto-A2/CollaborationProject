using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class TeacherMealPlan
    {
        [Key]
        public int teacher_meal_plan_id { get; set; }
        [ForeignKey("Teacher")]
        public int teacher_id { get; set; }
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("MealPlan")]
        public int plan_id { get; set; }
        public virtual MealPlan MealPlan { get; set; }
    }
    public class TeacherMealPlanDto
    {
        public int teacher_meal_plan_id { get; set; }

        public int teacher_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public int plan_id { get; set; }
        public string plan_name { get; set; }

    }
}