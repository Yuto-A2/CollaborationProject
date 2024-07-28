namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Student : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        student_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(),
                        last_name = c.String(),
                        email = c.String(),
                        phone_number = c.String(),
                    })
                .PrimaryKey(t => t.student_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
        }
    }
}
