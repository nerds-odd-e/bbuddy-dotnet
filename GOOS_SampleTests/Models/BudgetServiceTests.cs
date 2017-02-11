using GOOS_Sample.Models.DataModels;
using GOOS_Sample.Models.Repositories;
using GOOS_Sample.Models.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace GOOS_Sample.Models.Tests
{
    [TestClass()]
    public class BudgetServiceTests
    {
        private BudgetService _budgetService;
        private IRepository<Budget> _budgetRepositoryStub = Substitute.For<IRepository<Budget>>();

        [TestMethod()]
        public void CreateTest_should_invoke_repository_one_time()
        {
            this._budgetService = new BudgetService(_budgetRepositoryStub);

            var model = new BudgetAddViewModel { Amount = 2000, Month = "2017-02" };

            this._budgetService.Create(model);

            _budgetRepositoryStub.Received()
                .Save(Arg.Is<Budget>(x => x.Amount == 2000 && x.YearMonth == "2017-02"));
        }
    }
}