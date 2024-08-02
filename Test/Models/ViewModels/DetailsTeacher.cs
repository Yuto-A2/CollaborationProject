using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Models;

namespace Test.Models.ViewModels
{
    public class DetailsTeacher
    {
        public TeacherDto SelectedTeacher { get; set; }
        public IEnumerable<TeacherMealPlanDto> KeptTeacherMealPlans { get; set; }

    }
}