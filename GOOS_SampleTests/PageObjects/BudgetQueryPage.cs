using FluentAutomation;

namespace GOOS_SampleTests.PageObjects
{
    public class BudgetQueryPage : PageObject<BudgetQueryPage>
    {
        public BudgetQueryPage(FluentTest test) : base(test)
        {
            this.Url = $"{PageContext.Domain}/budget/query";
        }

        public void Query(string startDate, string endDate)
        {
            throw new System.NotImplementedException();
        }

        public void ShouldDisplayAmount(decimal amount)
        {
            throw new System.NotImplementedException();
        }
    }
}