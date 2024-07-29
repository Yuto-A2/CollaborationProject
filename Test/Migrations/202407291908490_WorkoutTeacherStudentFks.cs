namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkoutTeacherStudentFks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workouts", "teacher_id", c => c.Int(nullable: false));
            AddColumn("dbo.Workouts", "student_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Workouts", "teacher_id");
            CreateIndex("dbo.Workouts", "student_id");
            AddForeignKey("dbo.Workouts", "student_id", "dbo.Students", "student_id", cascadeDelete: true);
            AddForeignKey("dbo.Workouts", "teacher_id", "dbo.Teachers", "teacher_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workouts", "teacher_id", "dbo.Teachers");
            DropForeignKey("dbo.Workouts", "student_id", "dbo.Students");
            DropIndex("dbo.Workouts", new[] { "student_id" });
            DropIndex("dbo.Workouts", new[] { "teacher_id" });
            DropColumn("dbo.Workouts", "student_id");
            DropColumn("dbo.Workouts", "teacher_id");
        }
    }
}
