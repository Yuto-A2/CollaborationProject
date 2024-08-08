using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Workout
    {
        [Key]
        public int WorkoutId { get; set; }
        public DateTime WorkoutDate { get; set; }

        // one workout can have many exercises
        public ICollection<Exercise> Exercises { get; set; }

        // each workout is unique to one person
        // but each person can have many workouts
        // ? = nullable foreign key
        [ForeignKey("Teacher")]
        //public int? teacher_id { get; set; }

        public int teacher_id { get; set; }
        public virtual Teacher Teacher { get; set; }

        [ForeignKey("Student")]
        //public int? student_id { get; set; }

        public int student_id { get; set; }
        public virtual Student Student { get; set; }
    }

    public class WorkoutDto
    {
        public int WorkoutId { get; set; }
        public DateTime WorkoutDate { get; set; }
    }
}