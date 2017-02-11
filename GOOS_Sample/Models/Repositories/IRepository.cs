using System;
using System.Collections;
using GOOS_Sample.Models.DataModels;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GOOS_Sample.Models.Repositories
{
    public interface IRepository<T>
    {
        void Save(T budget);
        T Read(Func<T, bool> predicate);
        IEnumerable<T> ReadAll();
    }
}