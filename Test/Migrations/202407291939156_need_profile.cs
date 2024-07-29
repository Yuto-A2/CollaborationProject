namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class need_profile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NutritionalNeeds", "need_profile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NutritionalNeeds", "need_profile");
        }
    }
}
