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
        public int? teacher_id { get; set; }
        public virtual Teacher Teacher { get; set; }

        [ForeignKey("Student")]
        public int? student_id { get; set; }
        public virtual Student Student { get; set; }
    }

    public class WorkoutDto
    {
        public int WorkoutId { get; set; }

        // determine whether the workout is associated with a teacher or a student
        public bool IsTeacher { get; set; }

        // id of the associated user (student or teacher)
        public int user_id { get; set; }
        public DateTime WorkoutDate { get; set; }
    }

    // maps a WorkoutDto based on whether a workout is associated with a student or a teacher
    public class WorkoutAssigner
    {    
        public WorkoutDto MaptoDto(Workout workout)
        {
            return new WorkoutDto
            {
                WorkoutId = workout.WorkoutId,
                WorkoutDate = workout.WorkoutDate,
                // check whether the IsTeacher condition is true
                IsTeacher = workout.teacher_id.HasValue,
                // if the IsTeacher condition is true, user_id is a teacher id; if not, user_id is associated with a student_id
                user_id = workout.teacher_id.HasValue ? workout.teacher_id.Value : workout.student_id.Value
            };
        }
    }
}