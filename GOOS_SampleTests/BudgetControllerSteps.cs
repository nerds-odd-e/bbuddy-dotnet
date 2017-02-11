using FluentAssertions;
using GOOS_Sample.Controllers;
using GOOS_Sample.Models.ViewModels;
using GOOS_SampleTests.DataModelsForIntegrationTest;
using GOOS_SampleTests.steps.Common;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Web.Mvc;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace GOOS_SampleTests
{
    [Binding]
    public class BudgetControllerSteps
    {
        private BudgetController _budgetController;

        [BeforeScenario()]
        public void BeforeScenario()
        {
            this._budgetController = Hooks.UnityContainer.Resolve<BudgetController>();
        }

        [When(@"add a budget")]
        public void WhenAddABudget(Table table)
        {
            var model = table.CreateInstance<BudgetAddViewModel>();
            var result = this._budgetController.Add(model);

            ScenarioContext.Current.Set<ActionResult>(result);
        }

        [Then(@"it should exist a budget record in budget table")]
        public void ThenItShouldExistABudgetRecordInBudgetTable(Table table)
        {
            using (var dbcontext = new NorthwindEntitiesForTest())
            {
                var budget = dbcontext.Budgets
                    .FirstOrDefault();

                budget.Should().NotBeNull();

                table.CompareToInstance(budget);
            }
        }

        [Then(@"ViewBag should have a message for adding successfully")]
        public void ThenViewBagShouldHaveAMessageForAddingSuccessfully()
        {
            var result = ScenarioContext.Current.Get<ActionResult>() as ViewResult;
            string message = result.ViewBag.Message;
            message.Should().Be(GetAddingSuccessfullyMessage());
        }

        [Then(@"ViewBag should have a message for updating successfully")]
        public void ThenViewBagShouldHaveAMessageForUpdatingSuccessfully()
        {
            var result = ScenarioContext.Current.Get<ActionResult>() as ViewResult;
            string message = result.ViewBag.Message;
            message.Should().Be(GetUpdatingSuccessfullyMessage());
        }

        private string GetUpdatingSuccessfullyMessage()
        {
            return "updated successfully";
        }

        private static string GetAddingSuccessfullyMessage()
        {
            return "added successfully";
        }

        [When(@"query")]
        public void WhenQuery(Table table)
        {
            var condition = table.CreateInstance<BudgetQueryViewModel>();
            var result = this._budgetController.Query(condition);
            ScenarioContext.Current.Set<ActionResult>(result);
        }

        [Then(@"ViewData\.Model should be")]
        public void ThenViewData_ModelShouldBe(Table table)
        {
            var result = ScenarioContext.Current.Get<ActionResult>() as ViewResult;
            var model = result.ViewData.Model as BudgetQueryViewModel;

            table.CompareToInstance(model);
        }

    }
}