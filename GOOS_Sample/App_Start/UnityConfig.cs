using GOOS_Sample.Models;
using GOOS_Sample.Models.DataModels;
using GOOS_Sample.Models.Repositories;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Unity.Mvc5;

namespace GOOS_Sample
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IRepository<Budget>, BudgetRepository>();
            container.RegisterType<IBudgetService, BudgetService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}