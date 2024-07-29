namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nutrition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NutritionalNeeds",
                c => new
                    {
                        need_id = c.Int(nullable: false, identity: true),
                        protein = c.Int(nullable: false),
                        calories = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.need_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NutritionalNeeds");
        }
    }
}
