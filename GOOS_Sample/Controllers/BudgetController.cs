using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GOOS_Sample.Models;
using GOOS_Sample.Models.DataModels;
using GOOS_Sample.Models.ViewModels;

namespace GOOS_Sample.Controllers
{
    public class BudgetController : Controller
    {
        private IBudgetService budgetService;

        public BudgetController()
        {
            this.budgetService = new BudgetService();
        }

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