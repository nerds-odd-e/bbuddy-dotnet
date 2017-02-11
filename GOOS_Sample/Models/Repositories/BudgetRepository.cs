using System;
using GOOS_Sample.Models.DataModels;

namespace GOOS_Sample.Models.Repositories
{
    public class BudgetRepository : IRepository<Budget>
    {
        public void Save(Budget budget)
        {
            using (var dbcontext = new NorthwindEntities())
            {
                dbcontext.Budgets.Add(budget);

                dbcontext.SaveChanges();
            }
        }

        public Budget Read(Func<Budget, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}