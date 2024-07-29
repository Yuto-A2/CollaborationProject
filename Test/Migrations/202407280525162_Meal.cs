namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Meal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        meal_id = c.Int(nullable: false, identity: true),
                        meal_date = c.String(),
                        plan_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.meal_id)
                .ForeignKey("dbo.MealPlans", t => t.plan_id, cascadeDelete: true)
                .Index(t => t.plan_id);
            
            CreateTable(
                "dbo.RecipeIngredients",
                c => new
                    {
                        recipe_id = c.Int(nullable: false, identity: true),
                        quantity = c.String(),
                        ingredient_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.recipe_id)
                .ForeignKey("dbo.Ingredients", t => t.ingredient_id, cascadeDelete: true)
                .Index(t => t.ingredient_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecipeIngredients", "ingredient_id", "dbo.Ingredients");
            DropForeignKey("dbo.Meals", "plan_id", "dbo.MealPlans");
            DropIndex("dbo.RecipeIngredients", new[] { "ingredient_id" });
            DropIndex("dbo.Meals", new[] { "plan_id" });
            DropTable("dbo.RecipeIngredients");
            DropTable("dbo.Meals");
        }
    }
}
