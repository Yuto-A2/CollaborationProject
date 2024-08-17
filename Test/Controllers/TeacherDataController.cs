using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Diagnostics;
using Test.Migrations;
using Test.Models;

namespace Test.Controllers
{
    public class TeacherDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Lists the teachers in the database
        /// </summary>
        /// <returns>
        /// An array of teachers dtos.
        /// </returns>
        //GET: api/TeacherData/ListTeachers->
        //[{"teacherId":1, "first_name": Taylor, "last_name": Swift}],
        //[{"teacherId":2, "first_name": Ed, "last_name": Sheeran}]
        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        [ResponseType(typeof(TeacherDto))]
        public IHttpActionResult ListTeachers()
        {
            List<Models.Teacher> Teachers = db.Teachers.ToList();

            List<TeacherDto> TeacherDtos = new List<TeacherDto>();

            Teachers.ForEach(teacher => TeacherDtos.Add(new TeacherDto()
            {
                teacher_id = teacher.teacher_id,
                first_name = teacher.first_name,
                last_name = teacher.last_name,
                email = teacher.email,
                phone_number = teacher.phone_number
            }));

            return Ok(TeacherDtos);
        }
        [ResponseType(typeof(TeacherDto))]
        [Route("api/TeacherData/ListTeachersForTeacherMealPlan/{id}")]
        [HttpGet]
        public IHttpActionResult ListTeachersForTeacherMealPlan(int id)
        {
            Models.Teacher teacher = db.Teachers.Find(id);
            TeacherDto TeacherDto = new TeacherDto()
            {
                teacher_id = id,
                first_name = teacher.first_name,
                last_name = teacher.last_name,
                email = teacher.email,
                phone_number = teacher.phone_number
            };
            if (teacher == null)
            {
                return NotFound();
            }
            db.Teachers.Remove(teacher);
            db.SaveChanges();

            return Ok(TeacherDto);
        }
        [ResponseType(typeof(TeacherDto))]
        [Route("api/TeacherData/FindTeacher/{id}")]
        [HttpGet]
        public IHttpActionResult FindTeacher(int id)
        {
            Models.Teacher Teacher = db.Teachers.Find(id);
            TeacherDto TeacherDto = new TeacherDto()
            {
                teacher_id = id,
                first_name = Teacher.first_name,
                last_name = Teacher.last_name,
                email = Teacher.email,
                phone_number = Teacher.phone_number
            };
            if (Teacher == null)
            {
                return NotFound();
            }
            return Ok(TeacherDto);
        }

        /// <summary>
        /// Adds a teacher in the database
        /// </summary>
        /// <param name="Teacher">JSON FROM DATA of an Teacher</param>
        /// <returns>
        /// HEADER: 201(Created)
        /// CONTENT: Teacher ID, Teacher Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/TeacherData/AddTeacher
        /// FROM DATA: Teacher JSON Object
        /// </example>

        [ResponseType(typeof(Models.Teacher))]
        [HttpPost]
        public IHttpActionResult AddTeacher(Models.Teacher Teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teachers.Add(Teacher);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Teacher.teacher_id }, Teacher);
        }



        /// <summary>
        /// Adds a particular Teacher in the meal plan.
        /// </summary>
        /// <returns>
        /// HEADER: 201(Created)
        /// CONTENT: A Student data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/TeacherMealPlan/AddTeacherMealPlan/
        ///FORM DATA: Student JSON Object
        ///</example>
        [ResponseType(typeof(TeacherMealPlan))]
        [HttpPost]
        public IHttpActionResult AddTeacherMealPlan(TeacherMealPlanDto teacherMealPlanDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Entity
            var teacherMealPlan = new TeacherMealPlan
            {
                teacher_id = teacherMealPlanDto.teacher_id,
                plan_id = teacherMealPlanDto.plan_id,
            };

            db.TeacherMealPlans.Add(teacherMealPlan);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = teacherMealPlan.Id }, teacherMealPlan);
        }



        /// <summary>
        /// Updates a particular teacher in the database
        /// </summary>
        /// <param name="Teacher">JSON FROM DATA of a Teacher</param>
        /// <returns>
        /// HEADER: 204(Success, No Content Response)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/TeacherData/UpdateTeacher/5
        /// FROM DATA: Teacher JSON Object
        /// </example>

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTeacher(int id, Models.Teacher Teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Teacher.teacher_id)
            {
                return BadRequest();
            }
            db.Entry(Teacher).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        private bool TeacherExists(int id)
        {
            return db.Teachers.Count(e => e.teacher_id == id) > 0;
        }

        /// <summary>
        /// Delete a teacher from the database
        /// </summary>
        /// <param name="id">The primary key of the teacher</param>
        /// <returns>
        /// HEADER: 200(Ok)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/TeacherData/DeleteTeacher/5
        /// FROM DATA: Teacher JSON Object
        /// </example>

        [ResponseType(typeof(Models.Teacher))]
        [HttpPost]
        public IHttpActionResult DeleteTeacher(int id)
        {
            Models.Teacher Teacher = db.Teachers.Find(id);
            if (Teacher == null)
            {
                return NotFound();
            }

            db.Teachers.Remove(Teacher);
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

    }
}
   