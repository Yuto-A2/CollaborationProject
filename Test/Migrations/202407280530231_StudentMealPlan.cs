namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentMealPlan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentMealPlans",
                c => new
                    {
                        student_meal_plan_id = c.Int(nullable: false, identity: true),
                        student_id = c.Int(nullable: false),
                        plan_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.student_meal_plan_id)
                .ForeignKey("dbo.MealPlans", t => t.plan_id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.student_id, cascadeDelete: true)
                .Index(t => t.student_id)
                .Index(t => t.plan_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentMealPlans", "student_id", "dbo.Students");
            DropForeignKey("dbo.StudentMealPlans", "plan_id", "dbo.MealPlans");
            DropIndex("dbo.StudentMealPlans", new[] { "plan_id" });
            DropIndex("dbo.StudentMealPlans", new[] { "student_id" });
            DropTable("dbo.StudentMealPlans");
        }
    }
}
