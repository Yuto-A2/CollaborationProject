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
    public class TeacherMealPlanController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static TeacherMealPlanController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44326/api/");
        }

        // GET: TeacherMealPlan/List
        public ActionResult List()
        {
            //curl localhost:44326/api/TeacherMealPlanData/ListTeacherMealPlans 
            //objective: communicate with our TeacherMealPlan data api to retrieve a list of TeacherMealPlans

            string url = "TeacherMealPlanData/ListTeacherMealPlans";

            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine(response.StatusCode);

            IEnumerable<TeacherMealPlanDto> TeacherMealPlans = response.Content.ReadAsAsync<IEnumerable<TeacherMealPlanDto>>().Result;
            //Views/TeacherMealPlan/List.cshtml

            return View(TeacherMealPlans);
        }

        //GET: TeacherMealPlan/Details/3
        public ActionResult Details(int id)
        {
            DetailsTeacherMealPlan viewModel = new DetailsTeacherMealPlan();
            //curl https://localhost:44326//api/TeacherMealPlanData/FindTeacherMealPlan/{id}

            string url = "TeacherMealPlanData/FindTeacherMealPlan/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            TeacherMealPlanDto SelectedContent = response.Content.ReadAsAsync<TeacherMealPlanDto>().Result;
            Debug.WriteLine("Teacher Meal Plan received ");
            Debug.WriteLine(SelectedContent.first_name);
            //Views/TeacherMealPlan/Show.cshtml


            viewModel.SelectedTeacherMealPlan = SelectedContent;

            url = "TeacherMealPlanData/TeacherMealPlansForTeacher/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<TeacherMealPlanDto> ResponsibleTeachers = response.Content.ReadAsAsync<IEnumerable<TeacherMealPlanDto>>().Result;

            viewModel.ResponsibleTeachers = ResponsibleTeachers;

            return View(viewModel);
        }

        // GET: TeacherMealPlan/New
        public ActionResult New()
        {
            string url = "TeacherMealPlanData/ListTeacherMealPlans";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<TeacherMealPlanDto> teacherMealPlanOptions = response.Content.ReadAsAsync<IEnumerable<TeacherMealPlanDto>>().Result;
            return View(teacherMealPlanOptions);
        }

        // POST: TeacherMealPlan/Create
        [HttpPost]
        public ActionResult Create(Models.TeacherMealPlan teacherMealPlan)
        {
            Debug.WriteLine("The JSON payload is:");
            Debug.WriteLine(teacherMealPlan.teacher_meal_plan_id);

            // Add TeacherMealPlan to api
            string url = "TeacherMealPlanData/AddTeacherMealPlan";

            string jsonPayload = jss.Serialize(teacherMealPlan);
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

        //GET: TeacherMealPlan/Edit/1
        public ActionResult Edit(int id)
        {
            string url = "TeacherMealPlanData/FindTeacherMealPlan/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TeacherMealPlanDto SelectedTeacherMealPlan = response.Content.ReadAsAsync<TeacherMealPlanDto>().Result;
            return View(SelectedTeacherMealPlan);
        }
        //POST: TeacherMealPlan/Update/6
        [HttpPost]
        public ActionResult Update(int id, Models.TeacherMealPlan TeacherMealPlan)
        {
            string url = "teachermealplandata/updateteachermealplan/" + id;
            string jsonpayload = jss.Serialize(TeacherMealPlan);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
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