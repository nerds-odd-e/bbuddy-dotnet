using GOOS_Sample.Models.DataModels;
using GOOS_Sample.Models.Repositories;
using GOOS_Sample.Models.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

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

            var wasCreated = false;
            this._budgetService.Created += (sender, e) => { wasCreated = true; };

            var model = new BudgetAddViewModel { Amount = 2000, Month = "2017-02" };

            this._budgetService.Create(model);

            //assert
            _budgetRepositoryStub.Received()
                .Save(Arg.Is<Budget>(x => x.Amount == 2000 && x.YearMonth == "2017-02"));

            Assert.IsTrue(wasCreated);
        }

        [TestMethod()]
        public void CreateTest_when_exist_record_should_update_budget()
        {
            this._budgetService = new BudgetService(_budgetRepositoryStub);

            var wasUpdated = false;
            this._budgetService.Updated += (sender, e) => { wasUpdated = true; };

            var budgetFromDb = new Budget { Amount = 999, YearMonth = "2017-02" };
            _budgetRepositoryStub.Read(Arg.Any<Func<Budget, bool>>())
                .ReturnsForAnyArgs(budgetFromDb);

            var model = new BudgetAddViewModel { Amount = 2000, Month = "2017-02" };
            this._budgetService.Create(model);

            //assert
            _budgetRepositoryStub.Received()
                .Save(Arg.Is<Budget>(x => x == budgetFromDb && x.Amount == 2000));

            Assert.IsTrue(wasUpdated);
        }
    }
}