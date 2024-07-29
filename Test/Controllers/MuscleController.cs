using Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Test.Models.ViewModels;

namespace Test.Controllers
{
    public class MuscleController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static MuscleController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44326/api/");
        }

        // GET: Muscle/List
        public ActionResult List()
        {
            // objective: communicate with our muscle data api to retrieve a list of muscles
            // curl https://localhost:44326/api/MuscleData/ListMuscles

            string url = "MuscleData/ListMuscles";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<Muscle> Muscles = response.Content.ReadAsAsync<IEnumerable<Muscle>>().Result;
            //Debug.WriteLine("Number of muscles received : ");
            //Debug.WriteLine(Muscles.Count());

            return View(Muscles);
        }

        // GET: Muscle/Show/{id}
        public ActionResult Show(int id)
        {
            // objective: communicate with our muscle data api to retrieve a list of muscles
            // curl https://localhost:44326/api/MuscleData/FindMuscle/{id}

            ShowMuscle ViewModel = new ShowMuscle();

            string url = "MuscleData/FindMuscle/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            Muscle Muscle = response.Content.ReadAsAsync<Muscle>().Result;
            //Debug.WriteLine("muscle received: ");
            //Debug.WriteLine(Muscle.MuscleName);

            ViewModel.Muscle = Muscle;

            // showcase information about exercises related to this muscle group
            // send a request to gather information about exercises related to a specifi muscle ID
            url = "ExerciseData/ListExercisesForMuscles/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<ExerciseDto> RelatedExercises = response.Content.ReadAsAsync<IEnumerable<ExerciseDto>>().Result; ;

            ViewModel.RelatedExercises = RelatedExercises;

            return View(ViewModel);
        }

        // GET: Muscle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Muscle/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Muscle/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Muscle/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Muscle/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Muscle/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
