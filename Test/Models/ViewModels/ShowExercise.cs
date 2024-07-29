using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Models;

namespace Test.Models.ViewModels
{
    public class ShowExercise
    {
        public ExerciseDto SelectedExercise { get; set; }
        public IEnumerable<WorkoutDto> AssociatedWorkouts { get; set; }
    }
}