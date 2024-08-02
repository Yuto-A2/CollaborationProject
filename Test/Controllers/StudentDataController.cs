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
        public IHttpActionResult ListStudentsForDiary(int id)
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
    }
}