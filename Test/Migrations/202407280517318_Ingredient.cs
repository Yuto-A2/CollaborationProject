namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ingredient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        ingredient_id = c.Int(nullable: false, identity: true),
                        ingredient_name = c.String(),
                        is_allergen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ingredient_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ingredients");
        }
    }
}
