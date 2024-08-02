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

        
    }
}