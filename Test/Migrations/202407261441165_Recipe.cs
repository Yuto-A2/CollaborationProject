namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Recipe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        recipe_id = c.Int(nullable: false, identity: true),
                        recipe_name = c.String(),
                    })
                .PrimaryKey(t => t.recipe_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recipes");
        }
    }
}
