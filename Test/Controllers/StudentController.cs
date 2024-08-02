using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using System.Web.Script.Serialization;
using Test.Models;
using Test.Models.ViewModels;

namespace Test.Controllers
{
    public class StudentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static StudentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44326/api/");
        }
        //GET: Student/List
        public ActionResult List()
        {
            string url = "studentdata/liststudents";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<StudentDto> Students = response.Content.ReadAsAsync<IEnumerable<StudentDto>>().Result;
            return View(Students);
        }

        //GET: Student/Details/1
        public ActionResult Details(int id)
        {
            DetailsStudent ViewModel = new DetailsStudent();
            //objective: communication with our Student data api to retrieve one Student.
            //curl https://localhost:44326/api/Studentdata/findStudent/{id}
            string url = "studentdata/findStudent" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is: ");
            Debug.WriteLine(response.StatusCode);

            StudentDto SelectedStudent = response.Content.ReadAsAsync<StudentDto>().Result;
            Debug.WriteLine("Student received: ");
            Debug.WriteLine(SelectedStudent.first_name);

            ViewModel.SelectedStudent = SelectedStudent;

            url = "StudentData/ListStudentForStudentMealPlan/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<StudentMealPlanDto> KeptStudentMealPlans = response.Content.ReadAsAsync<IEnumerable<StudentMealPlanDto>>().Result;

            ViewModel.KeptStudentMealPlans = KeptStudentMealPlans;

            return View(ViewModel);

        }
        public ActionResult Error()
        {
            return View();
        }

        //GET: Student/New
        public ActionResult New()
        {
            return View();
        }

        //POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student Student)
        {
            Debug.WriteLine("the json payload is :");
            Debug.WriteLine(Student.first_name);
            //Objective: add a new Student into our system using the API
            //curl -H "Content-Type:application/json" -d @Student.json Https://localhost:44326/api/StudentData/AddStudent
            string url = "StudentData/AddStudent";

            string jsonpayload = jss.Serialize(Student);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
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