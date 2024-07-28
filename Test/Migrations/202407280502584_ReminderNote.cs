namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReminderNote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReminderNotes",
                c => new
                    {
                        note_id = c.Int(nullable: false, identity: true),
                        note_text = c.String(),
                        teacher_id = c.Int(nullable: false),
                        student_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.note_id)
                .ForeignKey("dbo.Students", t => t.student_id, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.teacher_id, cascadeDelete: true)
                .Index(t => t.teacher_id)
                .Index(t => t.student_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReminderNotes", "teacher_id", "dbo.Teachers");
            DropForeignKey("dbo.ReminderNotes", "student_id", "dbo.Students");
            DropIndex("dbo.ReminderNotes", new[] { "student_id" });
            DropIndex("dbo.ReminderNotes", new[] { "teacher_id" });
            DropTable("dbo.ReminderNotes");
        }
    }
}
