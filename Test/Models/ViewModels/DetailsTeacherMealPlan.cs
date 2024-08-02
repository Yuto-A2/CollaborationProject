using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models.ViewModels
{
    public class DetailsTeacherMealPlan
    {
        public TeacherMealPlanDto SelectedTeachertMealPlan { get; set; }
        public IEnumerable<TeacherDto> ResponsibleTeachers { get; set; }
    }
}