﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Test.Models;
using Test.Models.ViewModels;
using System.Web.Script.Serialization;

namespace Test.Controllers
{
    public class StudentMealPlanController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static StudentMealPlanController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                UseCookies = false
            };
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44326/api/");
        }

        private void GetApplicationCookie()
        {
            string token = "";
            client.DefaultRequestHeaders.Remove("Cookie");
            if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;
            Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }

        // GET: StudentMealPlan/List
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

        //GET: StudentMealPlan/Details/3
        public ActionResult Details(int id)
        {
            DetailsStudentMealPlan viewModel = new DetailsStudentMealPlan();
            //curl https://localhost:44326//api/StudentMealPlanData/ListStudentMealPlans/{id}

            string url = "StudentMealPlanData/ListStudentMealPlans/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            StudentMealPlanDto SelectedContent = response.Content.ReadAsAsync<StudentMealPlanDto>().Result;
            Debug.WriteLine("Student Meal Plan received ");
            Debug.WriteLine(SelectedContent.first_name);
            //Views/StudentMealPlan/Show.cshtml


            viewModel.SelectedStudentMealPlan = SelectedContent;

            url = "StudentMealPlanData/FindStudentMealPlan/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<StudentMealPlanDto> ResponsibleStudents = response.Content.ReadAsAsync<IEnumerable<StudentMealPlanDto>>().Result;

            viewModel.ResponsibleStudents = ResponsibleStudents;

            return View(viewModel);
        }


        // GET: StudentMealPlan/New
        public ActionResult New()
        {
            string url = "StudentMealPlanData/ListStudentMealPlans";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<Models.StudentMealPlanDto> studentMealPlanOptions = response.Content.ReadAsAsync<IEnumerable<Models.StudentMealPlanDto>>().Result;
            return View(studentMealPlanOptions);
        }

        // POST: StudentMealPlan/Create
        [HttpPost]
        public ActionResult Create(Models.StudentMealPlan studentMealPlan)
        {
            Debug.WriteLine("The JSON payload is:");
            Debug.WriteLine(studentMealPlan.student_meal_plan_id);

            // Add StudentMealPlan to api
            string url = "StudentMealPlanData/AddStudentMealPlan/";

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

        //GET: StudentMealPlan/Edit/1
        public ActionResult Edit(int id)
        {
            string url = "StudentMealPlanData/FindStudentMealPlan/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            StudentMealPlanDto SelectedStudentMealPlan = response.Content.ReadAsAsync<StudentMealPlanDto>().Result;
            return View(SelectedStudentMealPlan);
        }
        //POST: StudentMealPlan/Update/6
        [HttpPost]
        [Authorize]
        public ActionResult Update(int id, Models.StudentMealPlan StudentMealPlan, HttpPostedFileBase StudentMealPlanHasPic)
        {
            GetApplicationCookie();
            string url = "studentmealplandata/updatestudentmealplan/" + id;
            string jsonpayload = jss.Serialize(StudentMealPlan);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode && StudentMealPlanHasPic != null)
            {
                Debug.WriteLine("Calling Update Image method.");
                url = "StudentMealPlan/UploadStudentMealPlanPic" + id;
                MultipartFormDataContent requestcontent = new MultipartFormDataContent();
                HttpContent imagecontent = new StreamContent(StudentMealPlanHasPic.InputStream);
                requestcontent.Add(imagecontent, "StudentMealPlanPic", StudentMealPlanHasPic.FileName);
                response = client.PostAsync(url, requestcontent).Result;
                return RedirectToAction("List");
            }
            else if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            {
                return RedirectToAction("Error");
            }
        }

        // GET: StudentMealPlan/Delete/5
        [Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "StudentMealPlanData/FindStudentMealPlan" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            StudentMealPlanDto selectedstudentmealplan = response.Content.ReadAsAsync<StudentMealPlanDto>().Result;
            return View(selectedstudentmealplan);
        }

        //POST: Diary/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();
            string url = "studentmealplandata/deletestudentmealplan" + id;
            HttpContent content = new StringContent("");
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