using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models.ViewModels
{
    public class DetailsTeacherMealPlan
    {
        public TeacherMealPlanDto SelectedTeacherMealPlan { get; set; }
        public IEnumerable<TeacherMealPlanDto> ResponsibleTeachers { get; set; }
        //public IEnumerable<TeacherDto> ResponsibleTeachers { get; set; }
    }
}