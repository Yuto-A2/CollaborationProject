using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using Test.Models.ViewModels;
using System.Web.Script.Serialization;
using Test.Models;

namespace Test.Controllers
{
    public class TeacherController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static TeacherController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44326/api/");
        }

        //GET: Teacher/Index
        public ActionResult Index()
        {
            string url = "teacherdata/listteachers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<TeacherDto> Teachers = response.Content.ReadAsAsync<IEnumerable<TeacherDto>>().Result;
            return View(Teachers);
        }

        //GET: Teacher/List
        public ActionResult List()
        {
            string url = "teacherdata/listteachers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<TeacherDto> Teachers = response.Content.ReadAsAsync<IEnumerable<TeacherDto>>().Result;
            Debug.WriteLine("Number of teachers received: ");

            return View(Teachers);
        }

        //GET: Teacher/NEW
        public ActionResult New()
        {
            return View();
        }

        //POST: Teacher/Create
        [HttpPost]
        public ActionResult Create(Teacher Teacher)
        {
            Debug.WriteLine("the json payload is: ");
            Debug.WriteLine(Teacher.first_name);
            //Objective: add a new Teacher into our System using the API
            //curl -H "Content-Type:application/json" -d @Teacher.json https://localhost:44326/api/Teacherdata/addTeacher
            string url = "teacherdata/addteacher";

            string jsonpayload = jss.Serialize(Teacher);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }

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
        /// Add a teacher onto a meal plan.
        /// </summary>
        /// <param name="TeacherMealPlan">The data of the teacher neal plan to be created.</param>
        /// <returns>Redirects to the List view if successful, otherwise to an error page.</returns>
        // POST: Teacher/Create
        [HttpPost]
        public ActionResult AddTeacherMealPlan(TeacherMealPlanDto teacherMealPlan)
        {
            // API URL
            string url = "teachermealplan/addteachermealplan";
            // Serialize the teacher object to JSON format.
            string jsonPayload = jss.Serialize(teacherMealPlan);
            // Create HttpContent for the request body.
            HttpContent content = new StringContent(jsonPayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                // Log the error response content and redirect to the error page.
                Debug.WriteLine("AddTeacherMealPlan Error Response: " + response.Content.ReadAsStringAsync().Result);
                return RedirectToAction("Error");
            }
        }

        //GET: Teacher/Edit/1
        public ActionResult Edit(int id)
        {
            string url = "teacherdata/findteacher/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TeacherDto selectedTeacher = response.Content.ReadAsAsync<TeacherDto>().Result;
            return View(selectedTeacher);
        }


        //POST: Teacher/Update/5
        [HttpPost]
        public ActionResult Update(int id, Teacher Teacher)
        {
            string url = "teacherdata/updateteacher/" + id;
            string jsonpayload = jss.Serialize(Teacher);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        //GET: Teacher/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "teacherdata/findteacher/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TeacherDto selectedTeacher = response.Content.ReadAsAsync<TeacherDto>().Result;
            return View(selectedTeacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "teacherdata/deleteteacher/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
