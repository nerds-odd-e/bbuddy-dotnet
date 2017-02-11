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
            this.budgetService.Created += (sender, e) => { ViewBag.Message = "added successfully"; };
            this.budgetService.Updated += (sender, e) => { ViewBag.Message = "updated successfully"; };
            this.budgetService.Create(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Query()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Query(BudgetQueryViewModel model)
        {
            model.Amount = 10000;
            return View(model);
        }
    }
}