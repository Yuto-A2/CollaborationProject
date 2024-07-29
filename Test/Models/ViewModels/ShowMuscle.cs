using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Models;

namespace Test.Models.ViewModels
{
    public class ShowMuscle
    {
        public Muscle Muscle { get; set; }
        public IEnumerable<ExerciseDto> RelatedExercises { get; set; }
    }
}