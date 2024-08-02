using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models.ViewModels
{
    public class UpdateStudentMealPlan
    {
        public StudentMealPlanDto SelectedStudentMealPlan { get; set; }
        public IEnumerable<StudentMealPlanDto> StudentMealPlanOptions { get; set; }
    }
}