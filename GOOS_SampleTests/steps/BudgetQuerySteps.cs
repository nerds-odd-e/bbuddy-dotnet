using FluentAutomation;
using GOOS_SampleTests.PageObjects;
using System;
using System.Data.Entity.Core.Objects.DataClasses;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.steps
{
    [Binding]
    [Scope(Feature = "BudgetQuery")]
    public class BudgetQuerySteps : FluentTest
    {
        private BudgetQueryPage _budgetQueryPage;

        public BudgetQuerySteps()
        {
            this._budgetQueryPage = new BudgetQueryPage(this);
        }

        [Given(@"go to query budget page")]
        public void GivenGoToQueryBudgetPage()
        {
            this._budgetQueryPage.Go();
        }

        [When(@"query from ""(.*)"" to ""(.*)""")]
        public void WhenQueryFromTo(string startDate, string endDate)
        {
            this._budgetQueryPage.Query(startDate, endDate);
        }

        [Then(@"show budget (.*)")]
        public void ThenShowBudget(Decimal amount)
        {
            this._budgetQueryPage.ShouldDisplayAmount(amount);
        }
    }
}