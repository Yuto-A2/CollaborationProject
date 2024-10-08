﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
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
        [ResponseType(typeof(IEnumerable<StudentMealPlanDto>))]
        
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
                plan_id = plan.plan_id,
                StudentMealPlanHasPic = plan.StudentMealPlanHasPic,
                PicExtension = plan.PicExtension,
                ImagePath = "/Content/Images/StudentMealPlan/" + (plan.StudentMealPlanHasPic ? plan.StudentMealPlanHasPic.ToString() + "." + plan.PicExtension :"defalut.jpg")
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
        //GET: api/StudentMealPlanData/FindStudentMealPlan/1->
        //[{"contentId":1, "content": Fui a Mexio., "Date": May 30, "comment": Goode job, }],
        [HttpGet]
        [Route("api/StudentMealPlanData/FindStudentMealPlan/{id}")]
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
                student_id=d.student_id,
                first_name = d.Student.first_name,
                last_name = d.Student.last_name,
                plan_name = d.MealPlan.plan_name,
                plan_id = d.MealPlan.plan_id,
                StudentMealPlanHasPic = plan.StudentMealPlanHasPic,
                PicExtension = plan.PicExtension,
                ImagePath = "/Content/Images/StudentMealPlan/" + (plan.StudentMealPlanHasPic ? StudentMealPlan.student_meal_plan_id.ToString() + "." + plan.PicExtension : "defalut.jpg")
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
        [Authorize]
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
        /// Removes an association between a particular keeper and a particular animal
        /// </summary>
        /// <param name="animalid">The animal ID primary key</param>
        /// <param name="keeperid">The keeper ID primary key</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST api/AnimalData/AssociateAnimalWithKeeper/9/1
        /// </example>
        [HttpPost]
        [Route("api/StudentMealPlanData/UnAssociateStudetMealPlanWithStudent/{studentmealplanid}/{studentid}")]
        [Authorize]
        public IHttpActionResult UnAssociateStudetMealPlanWithStudent(int student_meal_plan_id, int student_id)
        {

            Models.StudentMealPlan SelectedStudentMealPlan = db.StudentMealPlans.Include(a => a.student_id).Where(a => a.student_meal_plan_id == student_id).FirstOrDefault();
            Models.Student SelectedStudent = db.Students.Find(student_id);

            if (SelectedStudentMealPlan == null || SelectedStudent == null)
            {
                return NotFound();
            }

            Debug.WriteLine("input animal id is: " + student_id);
            Debug.WriteLine("selected animal name is: " + SelectedStudentMealPlan.student_meal_plan_id);
            Debug.WriteLine("input keeper id is: " + student_id);
            Debug.WriteLine("selected keeper name is: " + SelectedStudent.first_name);


            //SelectedStudent.first_name.Remove(SelectedStudentMealPlan);
            db.SaveChanges();

            return Ok();
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

        /// <summary>
        /// Receives student meal picture data, uploads it to the webserver and updates the student meal plan's haPic option
        /// </summary>
        /// <param name="id">the student meal plan's id</param>
        /// <returns>
        ///status code 200 if successful.
        /// </returns>
        /// <example>
        /// curl -F studentmealplanpic=@file.jpg "http://localhost:xx/api/studentmealplandata/uploadstudentmealplanpic/2"
        /// POST: api/StudentMealPlanData/UpdateStudentMealPlanPic/2
        /// FORM DATA: image
        /// </example>

        [HttpPost]
        public IHttpActionResult UploadStudentMealPlanPic(int id)
        {
            bool haspic = false;
            string picextension = null; // `picextenstion` を `picextension` に修正
            if (Request.Content.IsMimeMultipartContent())
            {
                Debug.WriteLine("Received multipart form data.");
                int numfiles = HttpContext.Current.Request.Files.Count;
                Debug.WriteLine("Files Received: " + numfiles);

                // Check if a file is posted
                if (numfiles == 1 && HttpContext.Current.Request.Files[0] != null)
                {
                    var studentMealPlanPic = HttpContext.Current.Request.Files[0];

                    // Check if the file is empty
                    if (studentMealPlanPic.ContentLength > 0)
                    {
                        // Establish valid file types (can be changed to other file extensions if desired)
                        var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
                        var extension = Path.GetExtension(studentMealPlanPic.FileName).Substring(1);

                        // Check the extension of the file
                        if (valtypes.Contains(extension))
                        {
                            try
                            {
                                // File name is the id of the image
                                string fn = id + "." + extension;

                                // Get a direct file path to ~/Content/Images/StudentMealPlans/{id}.{extension}
                                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images/StudentMealPlans/"), fn);

                                // Save the file
                                studentMealPlanPic.SaveAs(path);

                                // If these are all successful then we can set these fields
                                haspic = true;
                                picextension = extension;

                                // Update the student meal plan haspic and picextension fields in the database
                                StudentMealPlan SelectedStudentMealPlan = db.StudentMealPlans.Find(id);
                                if (SelectedStudentMealPlan != null)
                                {
                                    SelectedStudentMealPlan.StudentMealPlanHasPic = haspic;
                                    SelectedStudentMealPlan.PicExtension = picextension;
                                    db.Entry(SelectedStudentMealPlan).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Student meal plan image was not saved successfully.");
                                Debug.WriteLine("Exception: " + ex);
                                return BadRequest();
                            }
                        }
                    }
                }
                return Ok(); // Proper placement inside the method
            }
            return BadRequest(); // Return a bad request if the content is not multipart
        }
    }
}
