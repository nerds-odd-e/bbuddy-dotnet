using System.Web.Mvc;
using GOOS_Sample.Models;
using Microsoft.Practices.Unity;
using Microsoft.SqlServer.Server;
using Unity.Mvc5;

namespace GOOS_Sample
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IBudgetService, BudgetService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}