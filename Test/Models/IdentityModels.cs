using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Test.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        // gateway between our C# and our database

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ReminderNote> ReminderNotes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Meal>Meals { get; set; }
        public DbSet<StudentMealPlan> StudentMealPlans { get; set; }
        public DbSet<TeacherMealPlan> TeacherMealPlans { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<NutritionalNeeds> Nutrition {  get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


    }
}