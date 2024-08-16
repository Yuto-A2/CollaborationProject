using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Test.Models;

namespace Test.Controllers
{
    public class StudentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Return all students in the database
        /// </summary>
        /// <returns>
        /// CONTENT: all Students in the database, including their associated teachers.
        /// </returns>
        //GET: api/StudentData/ListStudents->
        //[{"studentId":1, "student_fname": John, "student_lname": Lennon}],
        //[{"studentId":2, "student_fname": Paul, "student_lname": McCartney}]
        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        [ResponseType(typeof(StudentDto))]
        public IHttpActionResult ListStudents()
        {
            List<Student> Students = db.Students.ToList();

            List<StudentDto> StudentDtos = new List<StudentDto>();

            Students.ForEach(s => StudentDtos.Add(new StudentDto()
            {
                student_id = s.student_id,
                first_name = s.first_name,
                last_name = s.last_name,
                email = s.email,
                phone_number = s.phone_number
            }));

            return Ok(StudentDtos);
        }

        /// <summary>
        /// Returns a student in the system
        /// </summary>
        /// <param name="id">The primary key of the student</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A student in the systme matching up to the student id primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// GET: api/StudentData/FindStudent/2
        /// </example>
        [ResponseType(typeof(StudentDto))]
        [Route("api/StudentData/FindStudent/{id}")]
        [HttpGet]
        public IHttpActionResult FindStudent(int id)
        {
            Student Student = db.Students.Find(id);
            StudentDto StudentDto = new StudentDto()
            {
                student_id = Student.student_id,
                first_name = Student.first_name,
                last_name = Student.last_name,
                email = Student.email,
                phone_number = Student.phone_number
            };
            if (Student == null)
            {
                return NotFound();
            }

            return Ok(StudentDto);
        }

        ///<summary>
        ///Returns Studnents in the system not caring for a particular student.
        /// </summary>
        /// <returns>
        /// CONTENT: all students in the database about meal plan
        /// </returns>
        /// <param name="id">Content Primary Key</param>
        /// <example>
        /// GET: api/StudentData/ListStudentForStudentMealPlan/1
        /// </example>
        [HttpGet]
        [Route("api/StudentData/ListStudentForStudentMealPlan/{id}")]
        [ResponseType(typeof(StudentDto))]
        public IHttpActionResult ListStudentsForStudentMealPlan(int id)
        {
            List<Student> Students = db.Students.ToList();

            List<StudentDto> StudentDtos = new List<StudentDto>();
            Students.ForEach(s => StudentDtos.Add(new StudentDto()
            {
                student_id = s.student_id,
                first_name = s.first_name,
                last_name = s.last_name,
                email = s.email,
                phone_number = s.phone_number
            }));
            return Ok(StudentDtos);
        }

        /// <summary>
        /// Adds a particular Student in the system with POST Data input.
        /// </summary>
        /// <returns>
        /// HEADER: 201(Created)
        /// CONTENT: A Student data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/StudentData/AddStudent/
        ///FORM DATA: Student JSON Object
        ///</example>

        [ResponseType(typeof(Student))]
        [HttpPost]
        public IHttpActionResult AddStudent(Student Student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Students.Add(Student);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = Student.student_id }, Student);
        }

        /// <summary>
        /// Updates a particular Student in the system with POST Data input.
        /// </summary>
        /// <returns>
        /// HEADER: 204(Success, No Content Response)
        /// CONTENT: A Student in the system matching up to the Student ID primary Key
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <param name="id">Represents the Student ID primary key</param>
        /// <param name="Student">JSON FROM DATA of a student</param>
        /// <example>
        /// POST: api/StudentData/UpdateStudent/1
        ///FORM DATA: Student JSON Object

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateStudent(int id, Student Student)
        {
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != Student.student_id)
            {
                Debug.WriteLine("id mismatch");
                Debug.WriteLine("GET parameter: " + id);
                Debug.WriteLine("POST parameter: " + Student.student_id);
                Debug.WriteLine("POST parameter: " + Student.first_name);
                Debug.WriteLine("POST parameter: " + Student.last_name);
                Debug.WriteLine("POST parameter: " + Student.email);

                return BadRequest();
            }

            db.Entry(Student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.student_id == id) > 0;
        }

        /// <summary>
        /// Delete a Student from the database by its ID.
        /// </summary>
        /// <returns>
        /// HEADER: 200(Ok)
        /// CONTENT: A Student in the system matching up to the Student ID primary Key
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <param name="id">The primary key of the Student</param>
        /// <example>
        /// POST: api/StudentData/DeleteStudent/6
        ///FORM DATA: (empty)

        [ResponseType(typeof(Student))]
        [HttpPost]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            db.Students.Remove(student);
            db.SaveChanges();

            return Ok();
        }
    }
}
