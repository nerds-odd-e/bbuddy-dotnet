using GOOS_Sample.Models;
using GOOS_Sample.Models.ViewModels;
using NSubstitute;
using System;
using System.Web.Mvc;
using FluentAssertions;
using Xunit;

namespace GOOS_Sample.Controllers.Tests
{
    public class BudgetControllerTests
    {
        private BudgetController _budgetController;
        private IBudgetService budgetServiceStub = Substitute.For<IBudgetService>();

        [Fact]
        public void AddTest_add_budget_successfully_should_invoke_budgetService_Create_one_time()
        {
            this._budgetController = new BudgetController(budgetServiceStub);

            var model = new BudgetAddViewModel()
                {Amount = 2000, Month = "2017-02"};

            var result = this._budgetController.Add(model);

            budgetServiceStub.Received()
                .Create(Arg.Is<BudgetAddViewModel>(x => x.Amount == 2000 && x.Month == "2017-02"));
        }

        [Fact]
        public void QueryTest()
        {
            this._budgetController = new BudgetController(budgetServiceStub);
            budgetServiceStub.TotalBudget(new Period(new DateTime(2017, 4, 5), new DateTime(2017, 4, 14)))
                .ReturnsForAnyArgs(888);

            var condition = new BudgetQueryViewModel() {StartDate = "2017-04-05", EndDate = "2017-04-14"};

            var result = this._budgetController.Query(condition) as ViewResult;
            var actual = result.ViewData.Model as BudgetQueryViewModel;

            var expected = new BudgetQueryViewModel {StartDate = "2017-04-05", EndDate = "2017-04-14", Amount = 888};

            actual.ShouldBeEquivalentTo(expected);
        }
    }
}