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

        // many to many relationship between students and meal plans, since this is a bridging table
        // use of foreign key - can add additional information
        [ForeignKey("Student")]
        public int student_id { get; set; }
        public virtual Student Student { get; set; }
        public bool StudentMealPlanHasPic { get; set; }
        public string PicExtension { get; set; }

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

    }
}