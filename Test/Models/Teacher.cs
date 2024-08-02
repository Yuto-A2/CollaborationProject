using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Test.Models
{
    public class Teacher
    {
        [Key]
        public int teacher_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set;}
        public string email { get; set; }
        public string phone_number { get; set; }

    }
    public class TeacherDto
    {
        public int teacher_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
    }
}