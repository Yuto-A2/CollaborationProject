using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class ReminderNote
    {
        [Key]
        public int note_id {  get; set; }
        public string note_text {  get; set; }

        [ForeignKey("Teacher")]
        public int teacher_id { get; set; }
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("Student")]
        public int student_id { get; set; }
        public virtual Student Student { get; set; }
    }
}