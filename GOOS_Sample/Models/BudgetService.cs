using GOOS_Sample.Models.DataModels;
using GOOS_Sample.Models.Repositories;
using GOOS_Sample.Models.ViewModels;
using System;

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
            var budget = new Budget() { Amount = model.Amount, YearMonth = model.Month };
            this._budgetRepository.Save(budget);

            var handler = this.Created;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}