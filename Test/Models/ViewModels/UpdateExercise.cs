using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Models;

namespace Test.Models.ViewModels
{
    public class UpdateExercise
    {
        // this viewmodel is a class which stores information that needs to be presented to /Exercise/Update

        // existing exercise information

        public ExerciseDto Exercise { get; set; }

        // all muscle groups to choose from when updating this exercise

        public IEnumerable<Muscle> MuscleOptions { get; set; }

    }
}