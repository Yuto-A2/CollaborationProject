namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teacher_and_student_ids_nullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Workouts", "student_id", "dbo.Students");
            DropForeignKey("dbo.Workouts", "teacher_id", "dbo.Teachers");
            DropIndex("dbo.Workouts", new[] { "teacher_id" });
            DropIndex("dbo.Workouts", new[] { "student_id" });
            AlterColumn("dbo.Workouts", "teacher_id", c => c.Int());
            AlterColumn("dbo.Workouts", "student_id", c => c.Int());
            CreateIndex("dbo.Workouts", "teacher_id");
            CreateIndex("dbo.Workouts", "student_id");
            AddForeignKey("dbo.Workouts", "student_id", "dbo.Students", "student_id");
            AddForeignKey("dbo.Workouts", "teacher_id", "dbo.Teachers", "teacher_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workouts", "teacher_id", "dbo.Teachers");
            DropForeignKey("dbo.Workouts", "student_id", "dbo.Students");
            DropIndex("dbo.Workouts", new[] { "student_id" });
            DropIndex("dbo.Workouts", new[] { "teacher_id" });
            AlterColumn("dbo.Workouts", "student_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Workouts", "teacher_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Workouts", "student_id");
            CreateIndex("dbo.Workouts", "teacher_id");
            AddForeignKey("dbo.Workouts", "teacher_id", "dbo.Teachers", "teacher_id", cascadeDelete: true);
            AddForeignKey("dbo.Workouts", "student_id", "dbo.Students", "student_id", cascadeDelete: true);
        }
    }
}
