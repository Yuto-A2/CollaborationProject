using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Test.Models;

namespace Test.Controllers
{
    public class WorkoutDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all workouts in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all workouts in the database, including their associated muscle groups.
        /// </returns>
        /// <example>
        /// GET: api/WorkoutData/ListWorkouts
        /// </example>        
        [HttpGet]
        [ResponseType(typeof(WorkoutDto))]
        public IHttpActionResult ListWorkouts()
        {
            List<Workout> Workouts = db.Workouts.ToList();
            List<WorkoutDto> WorkoutDtos = new List<WorkoutDto>();

            Workouts.ForEach(a => WorkoutDtos.Add(new WorkoutDto()
            {
                WorkoutId = a.WorkoutId,
                WorkoutDate = a.WorkoutDate
            }));

            return Ok(WorkoutDtos);
        }

        /// <summary>
        /// Returns all workouts in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all workouts in the database, including their associated muscle groups.
        /// </returns>
        /// <param name="id">Exercise Primary Key</param>
        /// <example>
        /// GET: api/WorkoutData/ListWorkoutsForExercise/2
        /// </example>        
        [HttpGet]
        [ResponseType(typeof(WorkoutDto))]
        public IHttpActionResult ListWorkoutsForExercise(int id)
        {
            List<Workout> Workouts = db.Workouts.Where(
                w => w.Exercises.Any(
                    e => e.ExerciseId == id)
                ).ToList();
            List<WorkoutDto> WorkoutDtos = new List<WorkoutDto>();

            Workouts.ForEach(a => WorkoutDtos.Add(new WorkoutDto()
            {
                WorkoutId = a.WorkoutId,
                WorkoutDate = a.WorkoutDate
            }));

            return Ok(WorkoutDtos);
        }

        /// <summary>
        /// Associate a particular workout with a particular exercise
        /// </summary>
        /// <param name="ExerciseId">The exercise primary key</param>
        /// <param name="WorkoutId">The workout primary key</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST api/WorkoutData/AssociateWorkoutWithExercise/2/2
        /// </example>
        [HttpPost]
        [Route("api/WorkoutData/AssociateWorkoutWithExercise/{WorkoutId}/{ExerciseId}")]
        public IHttpActionResult AssociateWorkoutWithExercise(int WorkoutId, int ExerciseId)
        {
            Workout SelectedWorkout = db.Workouts.Include(w => w.Exercises).Where(w => w.WorkoutId == WorkoutId).FirstOrDefault();
            Exercise SelectedExercise = db.Exercises.Find(ExerciseId);

            if (SelectedExercise == null || SelectedWorkout == null)
            {
                return NotFound();
            }

            Debug.WriteLine("input workout id is:" + WorkoutId);
            Debug.WriteLine("input exercise id is:" + ExerciseId);

            Debug.WriteLine("The sekected workout is: " + SelectedWorkout);
            Debug.WriteLine("The selected exercise is: " + SelectedExercise);

            SelectedWorkout.Exercises.Add(SelectedExercise);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Associate a particular workout with a particular exercise
        /// </summary>
        /// <param name="ExerciseId">The exercise primary key</param>
        /// <param name="WorkoutId">The workout primary key</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST api/WorkoutData/UnAssociateWorkoutsWithExercise/2/2
        /// </example>
        [HttpPost]
        [Route("api/WorkoutData/UnAssociateWorkoutsWithExercise/{WorkoutId}/{ExerciseId}")]
        public IHttpActionResult UnAssociateWorkoutsWithExercise(int WorkoutId, int ExerciseId)
        {
            Workout SelectedWorkout = db.Workouts.Include(w => w.Exercises).Where(w => w.WorkoutId == WorkoutId).FirstOrDefault();
            Exercise SelectedExercise = db.Exercises.Find(ExerciseId);

            if (SelectedExercise == null || SelectedWorkout == null)
            {
                return NotFound();
            }

            Debug.WriteLine("input workout id is:" + WorkoutId);
            Debug.WriteLine("input exercise id is:" + ExerciseId);

            Debug.WriteLine("The sekected workout is: " + SelectedWorkout);
            Debug.WriteLine("The selected exercise is: " + SelectedExercise);

            SelectedWorkout.Exercises.Remove(SelectedExercise);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Returns all workout in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A workout in the system matching up to the workout ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the workout</param>
        /// <example>
        /// GET: api/WorkoutData/FindWorkout/5
        /// </example>
        [ResponseType(typeof(WorkoutDto))]
        [HttpGet]
        public IHttpActionResult FindWorkout(int id)
        {
            Workout Workout = db.Workouts.Find(id);
            WorkoutDto WorkoutDto = new WorkoutDto()
            {
                WorkoutId = Workout.WorkoutId,
                WorkoutDate = Workout.WorkoutDate
            };
            if (Workout == null)
            {
                return NotFound();
            }

            return Ok(WorkoutDto);
        }


        /// <summary>
        /// Updates a particular workout in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the workout ID primary key</param>
        /// <param name="workout">JSON FORM DATA of a workout</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/WorkoutData/UpdateWorkout/5
        /// FORM DATA: Workout JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateWorkout(int id, Workout workout)
        {
            Debug.WriteLine("Reached update workout method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != workout.WorkoutId)
            {
                Debug.WriteLine("Id mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + workout.WorkoutId);
                Debug.WriteLine("POST parameter" + workout.WorkoutDate);
                //Debug.WriteLine("POST parameter" + workout.MuscleId);

                return BadRequest();
            }

            db.Entry(workout).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
                {
                    Debug.WriteLine("workout not found");

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("none of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// Adds an workout to the system
        /// </summary>
        /// <param name="workout">JSON FORM DATA of an workout</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Workout ID, workout Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/WorkoutData/AddWorkout
        /// FORM DATA: Workout JSON Object
        /// </example>
        [ResponseType(typeof(Workout))]
        [HttpPost]
        public IHttpActionResult AddWorkout(Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Workouts.Add(workout);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workout.WorkoutId }, workout);
        }


        /// <summary>
        /// Deletes an workout from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the workout</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/WorkoutData/DeleteWorkout/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Workout))]
        [HttpPost]
        public IHttpActionResult DeleteWorkout(int id)
        {
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return NotFound();
            }

            db.Workouts.Remove(workout);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkoutExists(int id)
        {
            return db.Workouts.Count(e => e.WorkoutId == id) > 0;
        }
    }
}