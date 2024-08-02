using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    // table for notes for cooks regarding user food restrictions
    public class ReminderNote
    {
        [Key]
        public int note_id {  get; set; }
        public string note_text {  get; set; }

        // one user can have only one note
        // each note can only belong to one user
        [ForeignKey("Teacher")]
        public int teacher_id { get; set; }
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("Student")]
        public int student_id { get; set; }
        public virtual Student Student { get; set; }
    }
}