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
        // Static HttpClient to be used for making requests to the API.
        private static readonly HttpClient client;

        // Serializer to handle JSON conversion.
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        // Static constructor to initialize the HttpClient with the base API URL.
        static StudentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44326/api/");
        }

        /// <summary>
        /// Fetches the list of students from the API and displays it on the List view.
        /// </summary>
        /// <returns>Returns a view with a list of students.</returns>
        // GET: Student/List
        public ActionResult List()
        {
            // URL for the API request.
            // curl https://localhost:44326/api/StudentData/liststudents
            string url = "StudentData/liststudents";

            // Synchronous call to the API to fetch the student data.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Deserialize the response content to a list of StudentDto.
            IEnumerable<StudentDto> Students = response.Content.ReadAsAsync<IEnumerable<StudentDto>>().Result;

            // Pass the list of students to the view.
            return View(Students);
        }

        /// <summary>
        /// Fetches details of a specific student based on their ID.
        /// </summary>
        /// <param name="id">The ID of the student to fetch details for.</param>
        /// <returns>Returns a view with the student's details or redirects to an error page.</returns>
        // GET: Student/Details/1
        public ActionResult Details(int id)
        {
            // URL for the API request, including the student ID.
            // curl https://localhost:44326/api/StudentData/FindStudent/{id}
            string url = "StudentData/findStudent/" + id;

            // Synchronous call to the API to fetch the student data.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Log the response status code.
            Debug.WriteLine("Details Response Status Code: " + response.StatusCode);

            // If the response is successful, deserialize the student data and return the view.
            if (response.IsSuccessStatusCode)
            {
                StudentDto SelectedStudent = response.Content.ReadAsAsync<StudentDto>().Result;
                return View(SelectedStudent);
            }
            else
            {
                // Log the error response content and redirect to the error page.
                Debug.WriteLine("Details Error Response: " + response.Content.ReadAsStringAsync().Result);
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Displays a generic error page.
        /// </summary>
        /// <returns>Returns the error view.</returns>
        public ActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Displays the form for creating a new student.
        /// </summary>
        /// <returns>Returns the New view.</returns>
        // GET: Student/New
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        /// Creates a new student by sending data to the API.
        /// </summary>
        /// <param name="Student">The data of the student to be created.</param>
        /// <returns>Redirects to the List view if successful, otherwise to an error page.</returns>
        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(StudentDto Student)
        {
            // URL for the API request.
            string url = "StudentData/AddStudent";

            // Serialize the student object to JSON format.
            string jsonpayload = jss.Serialize(Student);

            // Create HttpContent for the request body.
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            // Synchronous call to the API to create the student.
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            // If the response is successful, redirect to the List view.
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                // Log the error response content and redirect to the error page.
                Debug.WriteLine("Create Error Response: " + response.Content.ReadAsStringAsync().Result);
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Fetches data of a specific student to edit based on their ID.
        /// </summary>
        /// <param name="id">The ID of the student to edit.</param>
        /// <returns>Returns the Edit view with the student's data or redirects to an error page.</returns>
        // GET: Student/Edit/16
        public ActionResult Edit(int id)
        {
            // URL for the API request, including the student ID.
            string url = "StudentData/FindStudent/" + id;

            // Synchronous call to the API to fetch the student data.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Log the response status code.
            Debug.WriteLine("Edit Response Status Code: " + response.StatusCode);

            // If the response is successful, deserialize the student data and return the view.
            if (response.IsSuccessStatusCode)
            {
                StudentDto selectedStudent = response.Content.ReadAsAsync<StudentDto>().Result;
                return View(selectedStudent);
            }
            else
            {
                // Log the error response content and redirect to the error page.
                Debug.WriteLine("Edit Error Response: " + response.Content.ReadAsStringAsync().Result);
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Updates a student's data by sending the updated data to the API.
        /// </summary>
        /// <param name="id">The ID of the student to update.</param>
        /// <param name="Student">The updated data of the student.</param>
        /// <returns>Redirects to the List view if successful, otherwise to an error page.</returns>
        // POST: Student/Update/6
        [HttpPost]
        public ActionResult Update(int id, StudentDto Student)
        {
            // URL for the API request, including the student ID.
            string url = "StudentData/UpdateStudent/" + id;

            // Serialize the student object to JSON format.
            string jsonpayload = jss.Serialize(Student);

            // Create HttpContent for the request body.
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            // Synchronous call to the API to update the student.
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            // If the response is successful, redirect to the List view.
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                // Log the error response content and redirect to the error page.
                Debug.WriteLine("Update Error Response: " + response.Content.ReadAsStringAsync().Result);
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Fetches data of a specific student to delete based on their ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>Returns the Delete view with the student's data or redirects to an error page.</returns>
        // GET: Student/Delete/16
        public ActionResult Delete(int id)
        {
            // URL for the API request, including the student ID.
            string url = "StudentData/FindStudent/" + id;

            // Synchronous call to the API to fetch the student data.
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Log the response status code.
            Debug.WriteLine("Delete Response Status Code: " + response.StatusCode);

            // If the response is successful, deserialize the student data and return the view.
            if (response.IsSuccessStatusCode)
            {
                StudentDto selectedStudent = response.Content.ReadAsAsync<StudentDto>().Result;
                return View(selectedStudent);
            }
            else
            {
                // Log the error response content and redirect to the error page.
                Debug.WriteLine("Delete Error Response: " + response.Content.ReadAsStringAsync().Result);
                return RedirectToAction("Error");
            }
        }
        //GET: Student/Delete/6
        public ActionResult DeleteConfirm(int id)
        {
            string url = "StudentData/FindStudent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            StudentDto selectedStudent = response.Content.ReadAsAsync<StudentDto>().Result;
            return View(selectedStudent);
        }
  }
   

}

