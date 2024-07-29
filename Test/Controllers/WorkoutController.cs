using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Security.Policy;
// distinction made between ViewModels and Models bc ViewModels stores information
// while Models create the database
using Test.Models.ViewModels;
using Test.Models;

namespace Test.Controllers
{
    public class WorkoutController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static WorkoutController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44326/api/");
        }

        // GET: Workout/List
        public ActionResult List()
        {
            // objective: communicate with our workout data api to retrieve a list of workouts
            // curl https://localhost:44326/api/WorkoutData/ListWorkouts

            string url = "WorkoutData/ListWorkouts";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<WorkoutDto> workouts = response.Content.ReadAsAsync<IEnumerable<WorkoutDto>>().Result;

            //Debug.WriteLine("Number of workouts received : ");
            //Debug.WriteLine(workouts.Count());

            return View(workouts);
        }

        // GET: Workout/Show/5
        public ActionResult Show(int id)
        {
            ShowWorkout ViewModel = new ShowWorkout();

            // objective: communicate with our workout data api to retrieve a list of workouts
            // curl https://localhost:44326/api/WorkoutData/FindWorkout/{id}

            string url = "WorkoutData/FindWorkout/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            WorkoutDto SelectedWorkout = response.Content.ReadAsAsync<WorkoutDto>().Result;
            Debug.WriteLine("workout received: ");
            Debug.WriteLine(SelectedWorkout.WorkoutDate);

            ViewModel.SelectedWorkout = SelectedWorkout;

            // show all exercises included in this workout
            url = "ExerciseData/ListExercisesForWorkout/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<ExerciseDto> IncludedExercises = response.Content.ReadAsAsync<IEnumerable<ExerciseDto>>().Result;

            ViewModel.IncludedExercises = IncludedExercises;

            url = "ExerciseData/ListExercisesNotIncludedInWorkout/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<ExerciseDto> AvailableExercises = response.Content.ReadAsAsync<IEnumerable<ExerciseDto>>().Result;

            ViewModel.AvailableExercises = AvailableExercises;

            return View(ViewModel);
        }

        //POST: Workout/Associate/{WorkoutId}/{ExerciseId}
        [HttpPost]
        public ActionResult Associate(int id, int ExerciseId)
        {
            // objective: communicate with our workout data api to retrieve a list of exercises not currently included in a particular exercise
            // curl -d "" https://localhost:44326/api/WorkoutData/AssociateWorkoutWithExercise/{WorkoutId}/{ExerciseId}
            Debug.WriteLine("Attempting to associate workout: " + id + "with exercise " + ExerciseId);

            // associate workout with exercise
            string url = "WorkoutData/AssociateWorkoutWithExercise/" + id + "/" + ExerciseId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Show/" + id);
        }

        //GET: Workout/UnAssociate/{id}?ExerciseId={ExerciseId}
        [HttpGet]
        public ActionResult UnAssociate(int id, int ExerciseId)
        {
            // objective: communicate with our workout data api to retrieve a list of exercises not currently included in a particular exercise
            // curl -d "" https://localhost:44326/api/WorkoutData/UnAssociateWorkoutsWithExercise/{WorkoutId}/{ExerciseId}
            Debug.WriteLine("Attempting to UnAssociate workout: " + id + "with exercise " + ExerciseId);

            // UnAssociate workout with exercise
            string url = "WorkoutData/UnAssociateWorkoutsWithExercise/" + id + "/" + ExerciseId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Show/" + id);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Workout/New
        public ActionResult New()
        {
            // information about the exercises in the system
            //GET api/Exercise
            string url = "ExerciseData/ListExercises";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<Exercise> ExerciseOptions = response.Content.ReadAsAsync<IEnumerable<Exercise>>().Result;

            return View(ExerciseOptions);
        }

        // POST: Workout/Create
        [HttpPost]
        public ActionResult Create(Workout workout)
        {
            // objective: add a new workout into our system using the API
            //curl - H "Content-Type:application/json" - d @workout.json https://localhost:44326/api/WorkoutData/AddWorkout
            Debug.WriteLine("the json payload is: ");
            Debug.WriteLine(workout.WorkoutDate);

            string url = "WorkoutData/AddWorkout";

            string jsonpayload = jss.Serialize(workout);

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
        //objective: communicate with our workout data api to retrieve one workout
        //curl -d @workout.json "https://localhost:44326/api/WorkoutData/FindWorkout/{id}"
        // GET: Workout/Edit/5
        public ActionResult Edit(int id)
        {
            // the existing workout information
            string url = "WorkoutData/FindWorkout/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            WorkoutDto Workout = response.Content.ReadAsAsync<WorkoutDto>().Result;

            return View(Workout);
        }

        // POST: Workout/Update/5
        [HttpPost]
        public ActionResult Update(int id, Workout workout)
        {
            //curl -H "Content-Type:application/json" -d @workout.json "https://localhost:44326/api/WorkoutData/UpdateWorkout/{id}"

            try
            {
                //Debug.WriteLine("The new workout info is:");
                //Debug.WriteLine(workout.WorkoutId);
                //Debug.WriteLine(workout.WorkoutDate);

                //serialize into JSON
                //Send the request to the API

                string url = "WorkoutData/UpdateWorkout/" + id;

                string jsonpayload = jss.Serialize(workout);
                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                //POST: api/WorkoutData/UpdateWorkout/{id}
                //Header : Content-Type: application/json
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                return RedirectToAction("Show/" + id);

            }
            catch
            {
                return View();
            }
        }

        // GET: Workout/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "WorkoutData/FindWorkout/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            Workout workout = response.Content.ReadAsAsync<Workout>().Result;
            return View(workout);
        }

        // POST: Workout/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // curl -d "" https://localhost:44326/api/WorkoutData/DeleteWorkout/{id}

            string url = "WorkoutData/DeleteWorkout/" + id;
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
