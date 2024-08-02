using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Test.Migrations;
using Test.Models;

namespace Test.Controllers
{
    public class TeacherMealPlanDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Lists the teacher meal plan in the database
        /// </summary>
        /// <returns>
        /// An array of teachers meal plan dtos.
        /// </returns>
        /// curl localhost:44326/api/TeacherMealPlanData/ListTeacherMealPlans
        [HttpGet]
        [Route("api/TeacherMealPlanData/ListTeacherMealPlans")]
        [ResponseType(typeof(TeacherMealPlanDto))]
        public IHttpActionResult TeacherMealPlans()
        {
            List<Models.TeacherMealPlan> TeacherMealPlans = db.TeacherMealPlans.Include(smp => smp.Teacher).ToList();
            List<TeacherMealPlanDto> TeacherMealPlanDtos = new List<TeacherMealPlanDto>();

            TeacherMealPlans.ForEach(plan => TeacherMealPlanDtos.Add(new TeacherMealPlanDto()
            {
                teacher_meal_plan_id = plan.teacher_meal_plan_id,
                teacher_id = plan.teacher_id,
                first_name = plan.Teacher.first_name,
                last_name = plan.Teacher.last_name,
                plan_id = plan.plan_id
            }));

            return Ok(TeacherMealPlanDtos);
        }

        /// <summary>
        /// Gathers information about all meal plans related to a particular teacher's Id
        /// </summary>
        /// <returns>
        /// Content: all meal plans in the database, including their associated teachers matched with a particular teachers ID. 
        /// </returns>
        /// <param name="id">teachers ID.</param>
        /// The id of the teacher.
        /// </param>
        //GET: api/TeacherMealPlanData/ListTeacherMealPlansForTeachers/1->
        //[{"teacher_meal_plan_id":1, "first_name": Taylor, "last_name": Swift, "meal_plan": Vegan, }],
        [HttpGet]
        [Route("api/TeacherMealPlanData/ListTeacherMealPlansForTeacher/{id}")]
        [ResponseType(typeof(TeacherMealPlanDto))]
        public IHttpActionResult ListTeacherMealPlans(int id)
        {

            //SQL equivalent:
            //SELECT first_name, last_name, meal_plan FROM TeacherMealPlans JOIN Teachers on Teacher.teacher_id = TeacherMealPlan.teacher_id JOIN TeacherMealPlan.plan_id = MealPlan.plan_id;
            List<Models.TeacherMealPlan> TeacherMealPlan = db.TeacherMealPlans.ToList();
            List<TeacherMealPlanDto> TeacherMealPlanDtos = new List<TeacherMealPlanDto>();

            TeacherMealPlan.ForEach(d => TeacherMealPlanDtos.Add(new TeacherMealPlanDto()
            {
                teacher_meal_plan_id = id,
                first_name = d.Teacher.first_name,
                last_name = d.Teacher.last_name,
                plan_name = d.MealPlan.plan_name
            }));

            return Ok(TeacherMealPlanDtos);
        }
    }
}
