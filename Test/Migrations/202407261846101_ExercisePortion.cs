namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExercisePortion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        ExerciseId = c.Int(nullable: false, identity: true),
                        ExerciseName = c.String(),
                        ExerciseDescription = c.String(),
                        MuscleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciseId)
                .ForeignKey("dbo.Muscles", t => t.MuscleId, cascadeDelete: true)
                .Index(t => t.MuscleId);
            
            CreateTable(
                "dbo.Muscles",
                c => new
                    {
                        MuscleId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.MuscleId);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        WorkoutId = c.Int(nullable: false, identity: true),
                        WorkoutDate = c.String(),
                    })
                .PrimaryKey(t => t.WorkoutId);
            
            CreateTable(
                "dbo.WorkoutExercises",
                c => new
                    {
                        Workout_WorkoutId = c.Int(nullable: false),
                        Exercise_ExerciseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Workout_WorkoutId, t.Exercise_ExerciseId })
                .ForeignKey("dbo.Workouts", t => t.Workout_WorkoutId, cascadeDelete: true)
                .ForeignKey("dbo.Exercises", t => t.Exercise_ExerciseId, cascadeDelete: true)
                .Index(t => t.Workout_WorkoutId)
                .Index(t => t.Exercise_ExerciseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkoutExercises", "Exercise_ExerciseId", "dbo.Exercises");
            DropForeignKey("dbo.WorkoutExercises", "Workout_WorkoutId", "dbo.Workouts");
            DropForeignKey("dbo.Exercises", "MuscleId", "dbo.Muscles");
            DropIndex("dbo.WorkoutExercises", new[] { "Exercise_ExerciseId" });
            DropIndex("dbo.WorkoutExercises", new[] { "Workout_WorkoutId" });
            DropIndex("dbo.Exercises", new[] { "MuscleId" });
            DropTable("dbo.WorkoutExercises");
            DropTable("dbo.Workouts");
            DropTable("dbo.Muscles");
            DropTable("dbo.Exercises");
        }
    }
}
