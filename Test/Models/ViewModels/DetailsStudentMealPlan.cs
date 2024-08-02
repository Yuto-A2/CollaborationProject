using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models.ViewModels
{
    public class DetailsStudentMealPlan
    {
        public StudentMealPlanDto SelectedStudentMealPlan { get; set; }
        public IEnumerable<StudentDto> ResponsibleStudents { get; set; }
        //public IEnumerable<TeacherDto> ResponsibleTeachers { get; set; }
    }
}