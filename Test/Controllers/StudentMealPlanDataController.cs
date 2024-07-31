using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Test.Models;
using System.Diagnostics;
using System.Web.UI.WebControls;
using System.Web.Mvc;


namespace PassionProject.Controllers
{
    public class StudentMealPlanController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Lists the student meal plan in the database
        /// </summary>
        /// <returns>
        /// An array of students meal plan dtos.
        /// </returns>
        /// curl localhost:44326/api/StudentMealPlanData/ListStudentMealPlans
        //GET: api/StudentMealPlanData/ListStudentMealPlans->
        //[{"student_meal_plan_id":1, "student fname": Taro., "Student lname": Tanaka, "Meal": Taco, }],
        [HttpGet]
        [Route("api/StudentMealPlanData/ListStudentMealPlans")]
        [ResponseType(typeof(StudentMealPlanDto))]
        public IHttpActionResult ListDiaries()
        {
            List<StudentMealPlan> StudentMealPlans = db.StudentMealPlans.ToList();

            //List<StudentMealPlan> StudentMealPlanDto = new List<StudentMealPlanDto>();

            StudentMealPlan.ForEach(diary => StudentMealPlanDtos.Add(new DiaryDto()
            {
                content_Id = diary.content_Id,
                title = diary.title,
                diary_body = diary.diary_body,
                Post_date = diary.Post_date,
                comment = diary.comment,
                student_fname = diary.Student.student_fname,
                student_lname = diary.Student.student_lname,
                teacher_fname = diary.Teacher.teacher_fname,
                teacher_lname = diary.Teacher.teacher_lname
            }));


            return Ok(StudentMealPlanDtos);
        }
    }
}

