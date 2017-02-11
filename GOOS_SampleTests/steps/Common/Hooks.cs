using FluentAutomation;
using GOOS_Sample.Models;
using GOOS_Sample.Models.DataModels;
using Microsoft.Practices.Unity;
using System.Linq;
using GOOS_Sample.Models.Repositories;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.steps.Common
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeFeature()]
        [Scope(Tag = "web")]
        public static void SetBrowser()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
        }

        [BeforeFeature()]
        public static void RegisterDIContainer()
        {
            UnityContainer = new UnityContainer();

            UnityContainer.RegisterType<IRepository<Budget>, BudgetRepository>();
            UnityContainer.RegisterType<IBudgetService, BudgetService>();
        }

        public static IUnityContainer UnityContainer
        {
            get;
            set;
        }

        [BeforeScenario()]
        public void BeforeScenarioCleanTable()
        {
            CleanTableByTags();
        }

        //[AfterFeature()]
        public static void AfterFeatureCleanTable()
        {
            CleanTableByTags();
        }

        private static void CleanTableByTags()
        {
            var tags = ScenarioContext.Current.ScenarioInfo.Tags
                .Where(x => x.StartsWith("Clean"))
                .Select(x => x.Replace("Clean", ""));

            if (!tags.Any())
            {
                return;
            }

            using (var dbcontext = new NorthwindEntities())
            {
                foreach (var tag in tags)
                {
                    dbcontext.Database.ExecuteSqlCommand($"TRUNCATE TABLE [{tag}]");
                }

                dbcontext.SaveChangesAsync();
            }
        }
    }
}