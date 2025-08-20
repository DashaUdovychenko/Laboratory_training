using BDD.Core.Drivers;
using OpenQA.Selenium;
using Reqnroll;
using BDD.Core.Helpers;

namespace BDD.Tests.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            IWebDriver driver = DriverFactory.CreateDriver();
            scenarioContext["WebDriver"] = driver;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (scenarioContext.TryGetValue("WebDriver", out IWebDriver? driver))
            {
                driver!.Quit();
            }
        }
    }
}