namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        tag_id = c.Int(nullable: false, identity: true),
                        tag_name = c.String(),
                    })
                .PrimaryKey(t => t.tag_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tags");
        }
    }
}
