namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class fullMigrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercises",
                c => new
                {
                    ExerciseId = c.Int(nullable: false, identity: true),
                    ExerciseName = c.String(),
                    ExerciseDescription = c.String(),
                    ExerciseWeight = c.Int(nullable: false),
                    MuscleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ExerciseId)
                .ForeignKey("dbo.Muscles", t => t.MuscleId, cascadeDelete: true)
                .Index(t => t.MuscleId);

            CreateTable(
                "dbo.Muscles",
                c => new
                {
                    MuscleId = c.Int(nullable: false, identity: true),
                    GroupName = c.String(),
                })
                .PrimaryKey(t => t.MuscleId);

            CreateTable(
                "dbo.Workouts",
                c => new
                {
                    WorkoutId = c.Int(nullable: false, identity: true),
                    WorkoutDate = c.String(),
                })
                .PrimaryKey(t => t.WorkoutId);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.WorkoutExercises",
                c => new
                {
                    Workout_WorkoutId = c.Int(nullable: false),
                    Exercise_ExerciseId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Workout_WorkoutId, t.Exercise_ExerciseId })
                .ForeignKey("dbo.Workouts", t => t.Workout_WorkoutId, cascadeDelete: true)
                .ForeignKey("dbo.Exercises", t => t.Exercise_ExerciseId, cascadeDelete: true)
                .Index(t => t.Workout_WorkoutId)
                .Index(t => t.Exercise_ExerciseId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.WorkoutExercises", "Exercise_ExerciseId", "dbo.Exercises");
            DropForeignKey("dbo.WorkoutExercises", "Workout_WorkoutId", "dbo.Workouts");
            DropForeignKey("dbo.Exercises", "MuscleId", "dbo.Muscles");
            DropIndex("dbo.WorkoutExercises", new[] { "Exercise_ExerciseId" });
            DropIndex("dbo.WorkoutExercises", new[] { "Workout_WorkoutId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Exercises", new[] { "MuscleId" });
            DropTable("dbo.WorkoutExercises");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Workouts");
            DropTable("dbo.Muscles");
            DropTable("dbo.Exercises");
        }
    }
}
