namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Recipe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "category_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Recipes", "category_id");
            AddForeignKey("dbo.Recipes", "category_id", "dbo.Categories", "category_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "category_id", "dbo.Categories");
            DropIndex("dbo.Recipes", new[] { "category_id" });
            DropColumn("dbo.Recipes", "category_id");
        }
    }
}
