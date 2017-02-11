using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GOOS_SampleTests.DataModelsForIntegrationTest;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace GOOS_SampleTests.steps.Common
{
    [Binding]
    public sealed class InsertTable
    {
        [Given(@"Budget table existed budgets")]
        public void GivenBudgetTableExistedBudgets(Table table)
        {
            var budgets = table.CreateSet<Budget>();
            using (var dbcontext = new NorthwindEntitiesForTest())
            {
                dbcontext.Budgets.AddRange(budgets);
                dbcontext.SaveChanges();
            }
        }
    }
}
