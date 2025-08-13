using BDD.Pages;
using BDD.Helpers;
using OpenQA.Selenium;
using Reqnroll;
using NUnit.Framework;

namespace BDD.Steps
{
    [Binding]
    public class ServicesSteps
    {
        private readonly IWebDriver driver;
        private HomePage homePage;
        private ServicesPage servicesPage;

        public ServicesSteps(ScenarioContext scenarioContext)
        {
            driver = scenarioContext.Get<IWebDriver>("WebDriver");
        }

        [Given(@"I am on the EPAM home page")]
        public void GivenIAmOnTheEPAMHomePage()
        {
            homePage = new HomePage(driver);
            homePage.NavigateToHomePage();
            CookieConsentHelper.AcceptCookies(driver);
        }

        [When(@"I navigate to the ""(.*)"" service")]
        public void WhenINavigateToTheService(string categoryName)
        {
            servicesPage = homePage.NavigateToService(categoryName);
        }

        [Then(@"the page title should contain ""(.*)""")]
        public void ThenThePageTitleShouldContain(string expectedTitle)
        {
            string actualTitle = servicesPage.GetTitle();
            Assert.That(actualTitle, Does.Contain(expectedTitle).IgnoreCase, $"Expected title to contain '{expectedTitle}', but got '{actualTitle}'");
        }

        [Then(@"the 'Our Related Expertise' section should be visible")]
        public void ThenTheOurRelatedExpertiseSectionShouldBeVisible()
        {
            servicesPage.ScrollToRelatedExpertiseSection();
            Assert.That(servicesPage.IsRelatedExpertiseSectionVisible(), Is.True, "The 'Our Related Expertise' section was not visible.");
        }
    }
}