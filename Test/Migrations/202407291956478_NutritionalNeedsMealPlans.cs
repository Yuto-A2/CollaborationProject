namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NutritionalNeedsMealPlans : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NutritionalNeedsMealPlans",
                c => new
                    {
                        NutritionalNeeds_need_id = c.Int(nullable: false),
                        MealPlan_plan_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NutritionalNeeds_need_id, t.MealPlan_plan_id })
                .ForeignKey("dbo.NutritionalNeeds", t => t.NutritionalNeeds_need_id, cascadeDelete: true)
                .ForeignKey("dbo.MealPlans", t => t.MealPlan_plan_id, cascadeDelete: true)
                .Index(t => t.NutritionalNeeds_need_id)
                .Index(t => t.MealPlan_plan_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NutritionalNeedsMealPlans", "MealPlan_plan_id", "dbo.MealPlans");
            DropForeignKey("dbo.NutritionalNeedsMealPlans", "NutritionalNeeds_need_id", "dbo.NutritionalNeeds");
            DropIndex("dbo.NutritionalNeedsMealPlans", new[] { "MealPlan_plan_id" });
            DropIndex("dbo.NutritionalNeedsMealPlans", new[] { "NutritionalNeeds_need_id" });
            DropTable("dbo.NutritionalNeedsMealPlans");
        }
    }
}
