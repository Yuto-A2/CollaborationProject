using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Test.Models;

namespace Test.Controllers
{
    public class StudentMealPlanDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Lists the student meal plan in the database
        /// </summary>
        /// <returns>
        /// An array of students meal plan dtos.
        /// </returns>
        /// curl localhost:44326/api/StudentMealPlanData/ListStudentMealPlans
        [HttpGet]
        [Route("api/StudentMealPlanData/ListStudentMealPlans")]
        [ResponseType(typeof(StudentMealPlanDto))]
        public IHttpActionResult StudentMealPlans()
        {
            List<StudentMealPlan> StudentMealPlans = db.StudentMealPlans.Include(smp => smp.Student).ToList();
            List<StudentMealPlanDto> StudentMealPlanDtos = new List<StudentMealPlanDto>();

            StudentMealPlans.ForEach(plan => StudentMealPlanDtos.Add(new StudentMealPlanDto()
            {
                student_meal_plan_id = plan.student_meal_plan_id,
                student_id = plan.student_id,
                first_name = plan.Student.first_name,
                last_name = plan.Student.last_name,
                plan_id = plan.plan_id
            }));

            return Ok(StudentMealPlanDtos);
        }
    }
}


