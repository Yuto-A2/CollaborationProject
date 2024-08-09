namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class collab : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        category_id = c.Int(nullable: false, identity: true),
                        category_name = c.String(),
                    })
                .PrimaryKey(t => t.category_id);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        ExerciseId = c.Int(nullable: false, identity: true),
                        ExerciseName = c.String(),
                        ExerciseDescription = c.String(),
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
                        WorkoutDate = c.DateTime(nullable: false),
                        teacher_id = c.Int(),
                        student_id = c.Int(),
                    })
                .PrimaryKey(t => t.WorkoutId)
                .ForeignKey("dbo.Students", t => t.student_id)
                .ForeignKey("dbo.Teachers", t => t.teacher_id)
                .Index(t => t.teacher_id)
                .Index(t => t.student_id);
            
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
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        ingredient_id = c.Int(nullable: false, identity: true),
                        ingredient_name = c.String(),
                        is_allergen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ingredient_id);
            
            CreateTable(
                "dbo.MealPlans",
                c => new
                    {
                        plan_id = c.Int(nullable: false, identity: true),
                        plan_name = c.String(),
                    })
                .PrimaryKey(t => t.plan_id);
            
            CreateTable(
                "dbo.NutritionalNeeds",
                c => new
                    {
                        need_id = c.Int(nullable: false, identity: true),
                        need_profile = c.String(),
                        protein = c.Int(nullable: false),
                        calories = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.need_id);
            
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
                        recipe_ingredient_id = c.Int(nullable: false, identity: true),
                        quantity = c.String(),
                        ingredient_id = c.Int(nullable: false),
                        recipe_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.recipe_ingredient_id)
                .ForeignKey("dbo.Ingredients", t => t.ingredient_id, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.recipe_id, cascadeDelete: true)
                .Index(t => t.ingredient_id)
                .Index(t => t.recipe_id);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        recipe_id = c.Int(nullable: false, identity: true),
                        recipe_name = c.String(),
                        category_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.recipe_id)
                .ForeignKey("dbo.Categories", t => t.category_id, cascadeDelete: true)
                .Index(t => t.category_id);
            
            CreateTable(
                "dbo.RecipeTags",
                c => new
                    {
                        recipe_tag_id = c.Int(nullable: false, identity: true),
                        recipe_id = c.Int(nullable: false),
                        tag_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.recipe_tag_id)
                .ForeignKey("dbo.Tags", t => t.recipe_id, cascadeDelete: true)
                .Index(t => t.recipe_id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        tag_id = c.Int(nullable: false, identity: true),
                        tag_name = c.String(),
                    })
                .PrimaryKey(t => t.tag_id);
            
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
                "dbo.StudentMealPlans",
                c => new
                    {
                        student_meal_plan_id = c.Int(nullable: false, identity: true),
                        student_id = c.Int(nullable: false),
                        plan_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.student_meal_plan_id)
                .ForeignKey("dbo.MealPlans", t => t.plan_id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.student_id, cascadeDelete: true)
                .Index(t => t.student_id)
                .Index(t => t.plan_id);
            
            CreateTable(
                "dbo.TeacherMealPlans",
                c => new
                    {
                        teacher_meal_plan_id = c.Int(nullable: false, identity: true),
                        teacher_id = c.Int(nullable: false),
                        plan_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.teacher_meal_plan_id)
                .ForeignKey("dbo.MealPlans", t => t.plan_id, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.teacher_id, cascadeDelete: true)
                .Index(t => t.teacher_id)
                .Index(t => t.plan_id);
            
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
            
            CreateTable(
                "dbo.NutritionalNeedsMealPlans",
                c => new
                    {
                        NutritionalNeeds_need_id = c.Int(nullable: false),
                        MealPlan_plan_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NutritionalNeeds_need_id, t.MealPlan_plan_id })
                .ForeignKey("dbo.NutritionalNeeds", t => t.NutritionalNeeds_need_id, cascadeDelete: true)
                .ForeignKey("dbo.MealPlans", t => t.MealPlan_plan_id, cascadeDelete: true)
                .Index(t => t.NutritionalNeeds_need_id)
                .Index(t => t.MealPlan_plan_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeacherMealPlans", "teacher_id", "dbo.Teachers");
            DropForeignKey("dbo.TeacherMealPlans", "plan_id", "dbo.MealPlans");
            DropForeignKey("dbo.StudentMealPlans", "student_id", "dbo.Students");
            DropForeignKey("dbo.StudentMealPlans", "plan_id", "dbo.MealPlans");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ReminderNotes", "teacher_id", "dbo.Teachers");
            DropForeignKey("dbo.ReminderNotes", "student_id", "dbo.Students");
            DropForeignKey("dbo.RecipeTags", "recipe_id", "dbo.Tags");
            DropForeignKey("dbo.RecipeIngredients", "recipe_id", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "category_id", "dbo.Categories");
            DropForeignKey("dbo.RecipeIngredients", "ingredient_id", "dbo.Ingredients");
            DropForeignKey("dbo.Meals", "plan_id", "dbo.MealPlans");
            DropForeignKey("dbo.NutritionalNeedsMealPlans", "MealPlan_plan_id", "dbo.MealPlans");
            DropForeignKey("dbo.NutritionalNeedsMealPlans", "NutritionalNeeds_need_id", "dbo.NutritionalNeeds");
            DropForeignKey("dbo.Workouts", "teacher_id", "dbo.Teachers");
            DropForeignKey("dbo.Workouts", "student_id", "dbo.Students");
            DropForeignKey("dbo.WorkoutExercises", "Exercise_ExerciseId", "dbo.Exercises");
            DropForeignKey("dbo.WorkoutExercises", "Workout_WorkoutId", "dbo.Workouts");
            DropForeignKey("dbo.Exercises", "MuscleId", "dbo.Muscles");
            DropIndex("dbo.NutritionalNeedsMealPlans", new[] { "MealPlan_plan_id" });
            DropIndex("dbo.NutritionalNeedsMealPlans", new[] { "NutritionalNeeds_need_id" });
            DropIndex("dbo.WorkoutExercises", new[] { "Exercise_ExerciseId" });
            DropIndex("dbo.WorkoutExercises", new[] { "Workout_WorkoutId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TeacherMealPlans", new[] { "plan_id" });
            DropIndex("dbo.TeacherMealPlans", new[] { "teacher_id" });
            DropIndex("dbo.StudentMealPlans", new[] { "plan_id" });
            DropIndex("dbo.StudentMealPlans", new[] { "student_id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ReminderNotes", new[] { "student_id" });
            DropIndex("dbo.ReminderNotes", new[] { "teacher_id" });
            DropIndex("dbo.RecipeTags", new[] { "recipe_id" });
            DropIndex("dbo.Recipes", new[] { "category_id" });
            DropIndex("dbo.RecipeIngredients", new[] { "recipe_id" });
            DropIndex("dbo.RecipeIngredients", new[] { "ingredient_id" });
            DropIndex("dbo.Meals", new[] { "plan_id" });
            DropIndex("dbo.Workouts", new[] { "student_id" });
            DropIndex("dbo.Workouts", new[] { "teacher_id" });
            DropIndex("dbo.Exercises", new[] { "MuscleId" });
            DropTable("dbo.NutritionalNeedsMealPlans");
            DropTable("dbo.WorkoutExercises");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TeacherMealPlans");
            DropTable("dbo.StudentMealPlans");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ReminderNotes");
            DropTable("dbo.Tags");
            DropTable("dbo.RecipeTags");
            DropTable("dbo.Recipes");
            DropTable("dbo.RecipeIngredients");
            DropTable("dbo.Meals");
            DropTable("dbo.NutritionalNeeds");
            DropTable("dbo.MealPlans");
            DropTable("dbo.Ingredients");
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Workouts");
            DropTable("dbo.Muscles");
            DropTable("dbo.Exercises");
            DropTable("dbo.Categories");
        }
    }
}
