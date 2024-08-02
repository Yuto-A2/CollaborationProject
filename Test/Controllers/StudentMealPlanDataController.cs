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
            List<Models.StudentMealPlan> StudentMealPlans = db.StudentMealPlans.Include(smp => smp.Student).ToList();
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

        /// <summary>
        /// Gathers information about all meal plans related to a particular student's Id
        /// </summary>
        /// <returns>
        /// Content: all meal plans in the database, including their associated students matched with a particular students ID. 
        /// </returns>
        /// <param name="id">students ID.</param>
        /// The id of the student.
        /// </param>
        //GET: api/DiaryData/ListDiariesForStudent/1->
        //[{"contentId":1, "content": Fui a Mexio., "Date": May 30, "comment": Goode job, }],
        [HttpGet]
        [Route("api/StudentMealPlanData/ListStudentMealPlansForStudent/{id}")]
        [ResponseType(typeof(StudentMealPlanDto))]
        public IHttpActionResult ListStudentMealPlans(int id)
        {
            //SQL equivalent:
            //SELECT first_name, last_name, meal_plan FROM StudentMealPlans JOIN Students on Student.student_id = StudentMealPlan.student_id JOIN StudentMealPlan.plan_id = MealPlan.plan_id;
            List<Models.StudentMealPlan> StudentMealPlan = db.StudentMealPlans.ToList();
            List<StudentMealPlanDto> StudentMealPlanDtos = new List<StudentMealPlanDto>();

            StudentMealPlan.ForEach(d => StudentMealPlanDtos.Add(new StudentMealPlanDto()
            {
                student_meal_plan_id = id,
                first_name = d.Student.first_name,
                last_name = d.Student.last_name,
                plan_name = d.MealPlan.plan_name
            }));

            return Ok(StudentMealPlanDtos);
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

        [ResponseType(typeof(Models.StudentMealPlan))]
        [Route("api/StudentMealPlanData/AddStudentMealPlan/")]
        [HttpPost]
        public IHttpActionResult AddStudentMealPlan(Models.StudentMealPlan studentMealPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentMealPlans.Add(studentMealPlan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = studentMealPlan.student_meal_plan_id }, studentMealPlan);
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
        public IHttpActionResult UpdateStudentMealPlan(int id, Models.StudentMealPlan studentmealplan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentmealplan.student_meal_plan_id)
            {
                return BadRequest();
            }

            db.Entry(studentmealplan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentMealPlanExists(id))
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
        /// Delete a particular student meal plan from the system
        /// </summary>
        /// <param name="id">The primary key of the student meal plan
        /// </param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/StudentMealPlanData/DeleteStudentMealPlan/5 
        /// FORM DATA: (empty)
        /// </example>

        [ResponseType(typeof(Models.StudentMealPlan))]
        [HttpPost]
        public IHttpActionResult DeleteStudentMealPlan(int id)
        {
            Models.StudentMealPlan studentmealplan = db.StudentMealPlans.Find(id);
            if (studentmealplan == null)
            {
                return NotFound();
            }
            db.StudentMealPlans.Remove(studentmealplan);
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

        private bool StudentMealPlanExists(int id)
        {
            return db.StudentMealPlans.Count(e => e.student_meal_plan_id == id) > 0;
        }
    }
}


