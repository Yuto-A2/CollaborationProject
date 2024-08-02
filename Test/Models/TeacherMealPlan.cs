using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    // a table linking teachers to their individual meal plans, and the meal plan table
    public class TeacherMealPlan
    {
        [Key]
        public int teacher_meal_plan_id { get; set; }

        // each individual teacher meal plan applies to one teacher
        // this table has the foreign key because teachermealplan depends on the teacher
        [ForeignKey("Teacher")]
        public int teacher_id { get; set; }
        public virtual Teacher Teacher { get; set; }

        // an individual teacher meal plan can only belong to one meal plan tier
        // each meal plan tier can apply to many individual teacher meal plans
        [ForeignKey("MealPlan")]
        public int plan_id { get; set; }
        public virtual MealPlan MealPlan { get; set; }
    }
}