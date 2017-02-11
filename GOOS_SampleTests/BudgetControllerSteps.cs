using System.Linq;
using System.Security.AccessControl;
using System.Web.Mvc;
using FluentAssertions;
using GOOS_Sample.Controllers;
using GOOS_Sample.Models.ViewModels;
using GOOS_SampleTests.DataModelsForIntegrationTest;
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
            this._budgetController = new BudgetController();
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

        private static string GetAddingSuccessfullyMessage()
        {
            return "added successfully";
        }
    }
}