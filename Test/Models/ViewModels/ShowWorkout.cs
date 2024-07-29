using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Models;

namespace Test.Models.ViewModels
{
    public class ShowWorkout
    {
        public WorkoutDto SelectedWorkout { get; set; }
        public IEnumerable<ExerciseDto> IncludedExercises { get; set; }
        public IEnumerable<ExerciseDto> AvailableExercises { get; set; }
    }
}