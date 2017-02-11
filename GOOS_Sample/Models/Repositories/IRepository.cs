using System;
using GOOS_Sample.Models.DataModels;

namespace GOOS_Sample.Models.Repositories
{
    public interface IRepository<T>
    {
        void Save(T budget);
        T Read(Func<T, bool> predicate);
    }
}