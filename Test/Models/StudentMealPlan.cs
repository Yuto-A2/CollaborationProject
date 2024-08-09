using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    // a table linking students to their individual meal plans, and the meal plan table
    public class StudentMealPlan
    {
        [Key]
        public int student_meal_plan_id { get; set; }
        public bool StudentMealPlanHasPic { get; set; }
        public string PicExtension { get; set; }

        // each individual student meal plan applies to one student
        // this table has the foreign key because studentmealplan depends on the student
        [ForeignKey("Student")]
        public int student_id { get; set; }
        public virtual Student Student { get; set; }

        // an individual student meal plan can only belong to one meal plan tier
        // each meal plan tier can apply to many individual student meal plans
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
        public string plan_name { get; set; }
        public bool StudentMealPlanHasPic { get; set; }
        public string PicExtension { get; set; }

    }
}