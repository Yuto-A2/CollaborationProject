namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkoutDate_string_to_DateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Workouts", "WorkoutDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workouts", "WorkoutDate", c => c.String());
        }
    }
}
