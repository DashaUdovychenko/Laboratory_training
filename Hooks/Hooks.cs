using BDD.Drivers;
using OpenQA.Selenium;
using Reqnroll;
using BDD.Helpers;

namespace BDD.Hooks
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
            var driver = DriverFactory.CreateDriver();
            scenarioContext["WebDriver"] = driver;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (scenarioContext.TryGetValue("WebDriver", out IWebDriver? driver))
            {
                driver.Quit();
            }
        }
    }
}