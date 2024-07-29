using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Test.Models;
using System.Web.Script.Serialization;
using Test.Models.ViewModels;

namespace Test.Controllers
{
    public class ExerciseController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ExerciseController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44326/api/");
        }

        // GET: Exercise/List
        public ActionResult List()
        {
            // objective: communicate with our exercise data api to retrieve a list of exercises
            // curl https://localhost:44326/api/ExerciseData/ListExercises

            string url = "ExerciseData/ListExercises";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<ExerciseDto> Exercises = response.Content.ReadAsAsync<IEnumerable<ExerciseDto>>().Result;
            //Debug.WriteLine("Number of exercises received : ");
            //Debug.WriteLine(Exercises.Count());

            return View(Exercises);
        }

        // GET: Exercise/Show/{id}
        public ActionResult Show(int id)
        {
            ShowExercise ViewModel = new ShowExercise();

            // objective: communicate with our exercise data api to retrieve a list of exercises
            // curl https://localhost:44326/api/ExerciseData/FindExercise/{id}

            string url = "ExerciseData/FindExercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            ExerciseDto SelectedExercise = response.Content.ReadAsAsync<ExerciseDto>().Result;
            //Debug.WriteLine("exercise received: ");
            //Debug.WriteLine(Exercise.ExerciseName);

            ViewModel.SelectedExercise = SelectedExercise;

            // show associated workouts with this exercise
            url = "WorkoutData/ListWorkoutsForExercise/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<WorkoutDto> AssociatedWorkouts = response.Content.ReadAsAsync<IEnumerable<WorkoutDto>>().Result;

            ViewModel.AssociatedWorkouts = AssociatedWorkouts;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Exercise/New
        public ActionResult New()
        {
            // information about the muscle groups in the system
            // GET api/Muscle
            string url = "MuscleData/ListMuscles";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<Muscle> MuscleOptions = response.Content.ReadAsAsync<IEnumerable<Muscle>>().Result;

            return View(MuscleOptions);
        }

        // POST: Exercise/Create
        [HttpPost]
        public ActionResult Create(Exercise exercise)
        {
            Debug.WriteLine("the json payload is: ");

            //objective: add a new exercise into our system using the API
            //curl -H "Content-Type:application/json" -d @exercise.json https://localhost:44326/api/ExerciseData/AddExercise 
            string url = "ExerciseData/AddExercise";

            string jsonpayload = jss.Serialize(exercise);

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

        //objective: communicate with our exercise data api to retrieve one exercise
        //curl -d @exercise.json "https://localhost:44326/api/ExerciseData/FindExercise/{id}"
        // GET: Exercise/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateExercise ViewModel = new UpdateExercise();

            // the existing exercise information
            string url = "ExerciseData/FindExercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ExerciseDto Exercise = response.Content.ReadAsAsync<ExerciseDto>().Result;
            ViewModel.Exercise = Exercise;

            // also include all muscle groups to choose from when updating this exercise
            url = "MuscleData/ListMuscles";
            response = client.GetAsync(url).Result;
            IEnumerable<Muscle> MuscleOptions = response.Content.ReadAsAsync<IEnumerable<Muscle>>().Result;

            ViewModel.MuscleOptions = MuscleOptions;

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Update(int id, Exercise exercise)
        {
            //curl -H "Content-Type:application/json" -d @exercise.json "https://localhost:44326/api/ExerciseData/UpdateExercise/{id}"

            try
            {
                //Debug.WriteLine("The new exercise info is:");
                //Debug.WriteLine(exercise.ExerciseName);
                //Debug.WriteLine(exercise.ExerciseDescription);
                //Debug.WriteLine(exercise.ExerciseWeight);

                //serialize into JSON
                //Send the request to the API

                string url = "ExerciseData/UpdateExercise/" + id;

                string jsonpayload = jss.Serialize(exercise);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                //POST: api/ExerciseData/UpdateExercise/{id}
                //Header : Content-Type: application/json
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                return RedirectToAction("Show/" + id);
            }
            catch
            {
                return View();
            }
        }

        // get: exercise/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "ExerciseData/FindExercise/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            ExerciseDto exercise = response.Content.ReadAsAsync<ExerciseDto>().Result;
            return View(exercise);
        }

        // POST: Animal/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // curl -d "" https://localhost:44326/api/ExerciseData/DeleteExercise/{id}

            string url = "ExerciseData/DeleteExercise/" + id;
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