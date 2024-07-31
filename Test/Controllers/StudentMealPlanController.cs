using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Test.Models;
using Test.Models.ViewModels;
using System.Web.Script.Serialization;
using Test.Migrations;
using System.Web.Http.Dispatcher;
using Microsoft.Owin.Security.Provider;

namespace Test.Controllers
{
    public class StudentMealPlanController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static StudentMealPlanController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44326/api/");
        }

        // GET: Diary/List
        public ActionResult List()
        {
            //curl localhost:44326/api/StudentMealPlanData/ListStudentMealPlans 
            //objective: communicate with our StudentMealPlan data api to retrieve a list of StudentMealPlans

            string url = "StudentMealPlanData/ListStudentMealPlans";

            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine(response.StatusCode);

            IEnumerable<StudentMealPlanDto> StudentMealPlans = response.Content.ReadAsAsync<IEnumerable<StudentMealPlanDto>>().Result;
            //Views/StudentMealPlan/List.cshtml

            return View(StudentMealPlans);
        }

        // GET: StudentMealPlan/New
        public ActionResult New()
        {
            string url = "StudentMealPlanData/ListStudentMealPlans";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<Test.Models.StudentMealPlanDto> studentMealPlanOptions = response.Content.ReadAsAsync<IEnumerable<Test.Models.StudentMealPlanDto>>().Result;
            return View(studentMealPlanOptions);
        }

        // POST: StudentMealPlan/Create
        [HttpPost]
        public ActionResult Create(Test.Models.StudentMealPlan studentMealPlan)
        {
            Debug.WriteLine("The JSON payload is:");
            Debug.WriteLine(studentMealPlan.student_meal_plan_id);

            // Add StudentMealPlan to api
            string url = "StudentMealPlanData/AddStudentMealPlan";

            string jsonPayload = jss.Serialize(studentMealPlan);
            Debug.WriteLine(jsonPayload);

            HttpContent content = new StringContent(jsonPayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}