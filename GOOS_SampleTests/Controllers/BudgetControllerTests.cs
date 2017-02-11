using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOOS_Sample.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using GOOS_Sample.Models;
using GOOS_Sample.Models.ViewModels;

namespace GOOS_Sample.Controllers.Tests
{
    [TestClass()]
    public class BudgetControllerTests
    {
        private BudgetController _budgetController;
        private IBudgetService budgetServiceStub = Substitute.For<IBudgetService>();

        [TestMethod()]
        public void AddTest_add_budget_successfully_should_invoke_budgetService_Create_one_time()
        {
            this._budgetController = new BudgetController(budgetServiceStub);            

            var model = new BudgetAddViewModel()
                {Amount = 2000, Month = "2017-02"};

            var result = this._budgetController.Add(model);

            budgetServiceStub.Received()
                .Create(Arg.Is<BudgetAddViewModel>(x => x.Amount == 2000 && x.Month == "2017-02"));
        }
    }
}