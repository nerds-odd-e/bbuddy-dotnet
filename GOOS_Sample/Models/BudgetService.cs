using GOOS_Sample.Models.DataModels;
using GOOS_Sample.Models.Repositories;
using GOOS_Sample.Models.ViewModels;
using System;
using System.Linq;

namespace GOOS_Sample.Models
{
    public class BudgetService : IBudgetService
    {
        private IRepository<Budget> _budgetRepository;

        public BudgetService(IRepository<Budget> _budgetRepository)
        {
            this._budgetRepository = _budgetRepository;
        }

        public event EventHandler Created;

        public event EventHandler Updated;

        public void Create(BudgetAddViewModel model)
        {
            var budget = this._budgetRepository.Read(x => x.YearMonth == model.Month);
            if (budget == null)
            {
                this._budgetRepository.Save(new Budget() { Amount = model.Amount, YearMonth = model.Month });

                var handler = this.Created;
                handler?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                budget.Amount = model.Amount;
                this._budgetRepository.Save(budget);

                var handler = this.Updated;
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        public decimal TotalBudget(Period period)
        {
            var totalBudget =
                this._budgetRepository
                    .ReadAll()
                    .Where(x => IsBetweenPeriod(period, x))
                    .Sum(x => x.GetOverlappingAmount(period));

            return totalBudget;
        }

        private static bool IsBetweenPeriod(Period period, Budget x)
        {
            return string.Compare(x.YearMonth, period.StartMonthString, StringComparison.Ordinal) >= 0 && String.Compare(x.YearMonth, period.EndMonthString, StringComparison.Ordinal) <= 0;
        }
    }
}