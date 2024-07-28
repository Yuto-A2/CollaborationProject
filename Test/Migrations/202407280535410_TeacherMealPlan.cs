namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherMealPlan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeacherMealPlans",
                c => new
                    {
                        teacher_meal_plan_id = c.Int(nullable: false, identity: true),
                        teacher_id = c.Int(nullable: false),
                        plan_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.teacher_meal_plan_id)
                .ForeignKey("dbo.MealPlans", t => t.plan_id, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.teacher_id, cascadeDelete: true)
                .Index(t => t.teacher_id)
                .Index(t => t.plan_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherMealPlans", "teacher_id", "dbo.Teachers");
            DropForeignKey("dbo.TeacherMealPlans", "plan_id", "dbo.MealPlans");
            DropIndex("dbo.TeacherMealPlans", new[] { "plan_id" });
            DropIndex("dbo.TeacherMealPlans", new[] { "teacher_id" });
            DropTable("dbo.TeacherMealPlans");
        }
    }
}
