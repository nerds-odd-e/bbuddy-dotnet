using FluentAssertions;
using GOOS_Sample.Models.DataModels;
using GOOS_Sample.Models.Repositories;
using GOOS_Sample.Models.ViewModels;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace GOOS_Sample.Models.Tests
{
    public class BudgetServiceTests
    {
        private IRepository<Budget> _budgetRepositoryStub = Substitute.For<IRepository<Budget>>();
        private BudgetService _budgetService;

        [Fact]
        public void CreateTest_should_invoke_repository_one_time()
        {
            InjectStubToBudgetService();

            var wasCreated = false;
            this._budgetService.Created += (sender, e) => { wasCreated = true; };

            this._budgetService.Create(new BudgetAddViewModel { Amount = 2000, Month = "2017-02" });

            //assert
            _budgetRepositoryStub.Received()
                .Save(Arg.Is<Budget>(x => x.Amount == 2000 && x.YearMonth == "2017-02"));

            Assert.True(wasCreated);
        }

        [Fact]
        public void CreateTest_when_exist_record_should_update_budget()
        {
            InjectStubToBudgetService();

            var wasUpdated = false;
            this._budgetService.Updated += (sender, e) => { wasUpdated = true; };


            var budgetFromDb = new Budget { Amount = 999, YearMonth = "2017-02" };
            _budgetRepositoryStub.Read(Arg.Any<Func<Budget, bool>>())
                .ReturnsForAnyArgs(budgetFromDb);

            this._budgetService.Create(new BudgetAddViewModel { Amount = 2000, Month = "2017-02" });

            //assert
            _budgetRepositoryStub.Received()
                .Save(Arg.Is<Budget>(x => x == budgetFromDb && x.Amount == 2000));

            Assert.True(wasUpdated);
        }

        [Fact]
        public void TotalBudgetTest_Period_EndDate_over_month_when_two_months_budget()
        {
            InjectStubToBudgetService();

            _budgetRepositoryStub.ReadAll()
                .ReturnsForAnyArgs(new List<Budget>
                {
                    new Budget() { YearMonth = "2017-04", Amount = 9000 },
                    new Budget() { YearMonth = "2017-05", Amount = 3100 },
                });

            AssertTotalAmount(9500, new Period(new DateTime(2017, 4, 1), new DateTime(2017, 5, 5)));
        }

        [Fact]
        public void TotalBudgetTest_Period_EndDate_over_single_month_but_only_one_month_budget()
        {
            InjectStubToBudgetService();

            _budgetRepositoryStub.ReadAll()
                .ReturnsForAnyArgs(new List<Budget> { new Budget() { YearMonth = "2017-04", Amount = 9000 } });

            AssertTotalAmount(3000, new Period(new DateTime(2017, 4, 21), new DateTime(2017, 5, 10)));
        }

        [Fact]
        public void TotalBudgetTest_Period_of_single_month()
        {
            InjectStubToBudgetService();

            _budgetRepositoryStub.ReadAll()
                .ReturnsForAnyArgs(new List<Budget> { new Budget() { YearMonth = "2017-04", Amount = 9000 } });

            AssertTotalAmount(3000, new Period(new DateTime(2017, 4, 5), new DateTime(2017, 4, 14)));
        }

        [Fact]
        public void TotalBudgetTest_Period_over_month_when_3_months_budget()
        {
            InjectStubToBudgetService();

            _budgetRepositoryStub.ReadAll()
                .ReturnsForAnyArgs(new List<Budget>
                {
                    new Budget() { YearMonth = "2017-03", Amount = 6200 },
                    new Budget() { YearMonth = "2017-04", Amount = 9000 },
                    new Budget() { YearMonth = "2017-05", Amount = 3100 },
                });

            AssertTotalAmount(11500, new Period(new DateTime(2017, 3, 22), new DateTime(2017, 5, 5)));
        }

        [Fact]
        public void TotalBudgetTest_Period_over_single_month_but_only_one_month_budget()
        {
            InjectStubToBudgetService();

            _budgetRepositoryStub.ReadAll()
                .ReturnsForAnyArgs(new List<Budget> { new Budget() { YearMonth = "2017-04", Amount = 9000 } });

            AssertTotalAmount(9000, new Period(new DateTime(2017, 3, 21), new DateTime(2017, 5, 10)));
        }

        [Fact]
        public void TotalBudgetTest_Period_StartDate_over_month_when_two_months_budget()
        {
            InjectStubToBudgetService();

            _budgetRepositoryStub.ReadAll()
                .ReturnsForAnyArgs(new List<Budget>
                {
                    new Budget() { YearMonth = "2017-03", Amount = 3100 },
                    new Budget() { YearMonth = "2017-04", Amount = 9000 }
                });

            AssertTotalAmount(10000, new Period(new DateTime(2017, 3, 22), new DateTime(2017, 4, 30)));
        }

        [Fact]
        public void TotalBudgetTest_Period_StartDate_over_single_month_but_only_one_month_budget()
        {
            InjectStubToBudgetService();

            _budgetRepositoryStub.ReadAll()
                .ReturnsForAnyArgs(new List<Budget> { new Budget() { YearMonth = "2017-04", Amount = 9000 } });

            AssertTotalAmount(3000, new Period(new DateTime(2017, 3, 5), new DateTime(2017, 4, 10)));
        }

        private void AssertTotalAmount(int expected, Period period)
        {
            this._budgetService.TotalBudget(period).ShouldBeEquivalentTo(expected);
        }

        private void InjectStubToBudgetService()
        {
            this._budgetService = new BudgetService(_budgetRepositoryStub);
        }
    }
}