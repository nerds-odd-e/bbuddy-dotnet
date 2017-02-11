using System;
using GOOS_Sample.Models.ViewModels;

namespace GOOS_Sample.Models
{
    public interface IBudgetService
    {
        void Create(BudgetAddViewModel budget);
        event EventHandler Created;
        event EventHandler Updated;
    }
}   