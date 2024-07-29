using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Test.Migrations;

namespace Test.Models
{
    public class NutritionalNeeds
    {
        [Key]
        public int need_id { get; set; }
        public string need_profile {  get; set; }
        public int protein {  get; set; }
        public int calories { get; set; }

        // one nutritional need profile can be referenced by multiple meal plans
        public ICollection<MealPlan> MealPlans { get; set; }
    }
}