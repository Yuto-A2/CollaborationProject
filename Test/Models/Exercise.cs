using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class Exercise
    {
        // describe an exercise
        [Key]

        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public string ExerciseDescription { get; set; }
        // weight is in lb
        //public int ExerciseWeight { get; set; }

        // an exercise can only focus on one muscle (group)
        // a muscle (group) can be the focus of many exercises
        [ForeignKey("Muscle")]
        public int MuscleId { get; set; }
        public virtual Muscle Muscle { get; set; }

        // one exercise can be in many workouts
        public ICollection<Workout> Workouts { get; set; }

        // one exercise can apply to many students and teachers
        //public ICollection<teacherEntity> TeacherEntities { get; set; }
        //public ICollection<studentEntity> StudentEntities {  get; set; }   

    }
    public class ExerciseDto
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }

        // muscle group name
        public string GroupName { get; set; }
        //public int ExerciseWeight { get; set; }
        public string ExerciseDescription { get; set; }
    }
}