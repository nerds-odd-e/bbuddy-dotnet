using GOOS_Sample.Models.DataModels;
using GOOS_Sample.Models.Repositories;
using GOOS_Sample.Models.ViewModels;

namespace GOOS_Sample.Models
{
    public class BudgetService : IBudgetService
    {
        private IRepository<Budget> _budgetRepository;

        public BudgetService(IRepository<Budget> _budgetRepository)
        {
            this._budgetRepository = _budgetRepository;
        }

        public void Create(BudgetAddViewModel model)
        {
            var budget = new Budget() {Amount = model.Amount, YearMonth = model.Month};
            this._budgetRepository.Save(budget);
        }
    }
}