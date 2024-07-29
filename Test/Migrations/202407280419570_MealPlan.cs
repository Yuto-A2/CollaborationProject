namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealPlan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MealPlans",
                c => new
                    {
                        plan_id = c.Int(nullable: false, identity: true),
                        plan_name = c.String(),
                    })
                .PrimaryKey(t => t.plan_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MealPlans");
        }
    }
}
