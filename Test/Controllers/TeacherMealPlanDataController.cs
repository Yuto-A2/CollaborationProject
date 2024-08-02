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
                plan_id = plan.plan_id,
                plan_name = plan.MealPlan.plan_name
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
                plan_name = d.MealPlan.plan_name,
                teacher_id = d.Teacher.teacher_id,
                plan_id = d.MealPlan.plan_id
            }));

            return Ok(TeacherMealPlanDtos);
        }

        /// <summary>
        /// Adds a StudentMealPlan to the system
        /// </summary>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: StudentMealPlan ID, StudentMealPlan Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/StudentMealPlanData/AddStudentMealPlan
        /// FORM DATA: Diary JSON Object
        /// </example>

        [ResponseType(typeof(Models.TeacherMealPlan))]
        [Route("api/TeacherMealPlanData/AddTeacherMealPlan/")]
        [HttpPost]
        public IHttpActionResult AddTeacherMealPlan(Models.TeacherMealPlan teacherMealPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeacherMealPlans.Add(teacherMealPlan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teacherMealPlan.teacher_meal_plan_id }, teacherMealPlan);
        }

        /// <summary>
        /// Updates a particular StudentMealPlan to the system
        /// </summary>
        /// <param name="id">Represents the student_meal_plan Id primary key
        /// </param>
        /// <param name="diary">JSON FORM DATA of a student meal plan</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or 
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/StudentMealPlanData/UpdateStudentMealPlan/5 
        /// FORM DATA: Diary JSON Object
        /// </example>

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTeacherMealPlan(int id, Models.TeacherMealPlan teachermealplan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teachermealplan.teacher_meal_plan_id)
            {
                return BadRequest();
            }

            db.Entry(teachermealplan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherMealPlanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;

                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete a particular teacher meal plan from the system
        /// </summary>
        /// <param name="id">The primary key of the teacher meal plan
        /// </param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/TeacherMealPlanData/DeleteTeacherMealPlan/5 
        /// FORM DATA: (empty)
        /// </example>

        [ResponseType(typeof(Models.TeacherMealPlan))]
        [HttpPost]
        public IHttpActionResult DeleteTeacherMealPlan(int id)
        {
            Models.TeacherMealPlan teachermealplan = db.TeacherMealPlans.Find(id);
            if (teachermealplan == null)
            {
                return NotFound();
            }
            db.TeacherMealPlans.Remove(teachermealplan);
            db.SaveChanges();
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherMealPlanExists(int id)
        {
            return db.TeacherMealPlans.Count(e => e.teacher_meal_plan_id == id) > 0;
        }
    }
}
