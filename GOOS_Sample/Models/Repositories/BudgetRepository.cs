using GOOS_Sample.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GOOS_Sample.Models.Repositories
{
    public class BudgetRepository : IRepository<Budget>
    {
        public void Save(Budget budget)
        {
            using (var dbcontext = new NorthwindEntities())
            {
                var budgetFromDb = dbcontext.Budgets.FirstOrDefault(x => x.YearMonth == budget.YearMonth);

                if (budgetFromDb == null)
                {
                    dbcontext.Budgets.Add(budget);
                }
                else
                {
                    budgetFromDb.Amount = budget.Amount;
                }

                dbcontext.SaveChanges();
            }
        }

        public Budget Read(Func<Budget, bool> predicate)
        {
            using (var dbcontext = new NorthwindEntities())
            {
                var firstBudget = dbcontext.Budgets.FirstOrDefault(predicate);
                return firstBudget;
            }
        }

        public IEnumerable<Budget> ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}