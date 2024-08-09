namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentMealPic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentMealPlans", "StudentMealPlanHasPic", c => c.Boolean(nullable: false));
            AddColumn("dbo.StudentMealPlans", "PicExtension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentMealPlans", "PicExtension");
            DropColumn("dbo.StudentMealPlans", "StudentMealPlanHasPic");
        }
    }
}
