using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models.ViewModels
{
    public class UpdateTeacherMealPlan
    {
        public TeacherMealPlanDto SelectedTeacherMealPlan { get; set; }
        public IEnumerable<TeacherMealPlanDto> TeacherMealPlanOptions { get; set; }
    }
}