namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipeTag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        teacher_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(),
                        last_name = c.String(),
                        email = c.String(),
                        phone_number = c.String(),
                    })
                .PrimaryKey(t => t.teacher_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Teachers");
        }
    }
}
