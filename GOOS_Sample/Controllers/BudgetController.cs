using GOOS_Sample.Models;
using GOOS_Sample.Models.ViewModels;
using System.Web.Mvc;

namespace GOOS_Sample.Controllers
{
    public class BudgetController : Controller
    {
        private IBudgetService budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            this.budgetService = budgetService;
        }

        // GET: Budget
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(BudgetAddViewModel model)
        {
            this.budgetService.Create(model);
            ViewBag.Message = "added successfully";

            return View(model);
        }
    }
}