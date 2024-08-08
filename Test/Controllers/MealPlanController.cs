using System.Web.Mvc;

namespace Test.Controllers
{
    public class MealPlanController : Controller
    {
        // GET: MealPlan/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: MealPlan/StudentMealPlans
        public ActionResult StudentMealPlans()
        {
            return RedirectToAction("Index", "StudentMealPlan");
        }

        // GET: MealPlan/TeacherMealPlans
        public ActionResult TeacherMealPlans()
        {
            return RedirectToAction("Index", "TeacherMealPlan");
        }
    }
}
