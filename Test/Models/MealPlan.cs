using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class MealPlan
    {
        [Key]
        public int plan_id { get; set; }
        public string plan_name { get; set; }
    }
}