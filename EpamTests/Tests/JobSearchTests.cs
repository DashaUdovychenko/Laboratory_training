using NUnit.Framework;
using OpenQA.Selenium;
using EpamTests.Pages;
using EpamTests.Drivers;
using EpamTests.Helpers;

namespace EpamTests.Tests;

[TestFixture]
public class JobSearchTests
{
    private IWebDriver driver;

    [SetUp]
    public void SetUp()
    {
        driver = WebDriverFactory.CreateDriver();
    }

    [Test]
    [TestCase("Java")]
    [TestCase(".NET")]
    public void ValidateJobSearch(string keyword)
    {
        var home = new HomePage(driver);
        var careers = new CareersPage(driver);
        var jobSearch = new JobSearchPage(driver);
        var results = new SearchResultsPage(driver);

        home.GoTo();
        home.ClickCareers();
        careers.ClickVisibleDreamJobLink();
        CookieConsentHelper.AcceptCookies(driver);

        jobSearch.EnterKeyword(keyword);
        jobSearch.SelectAllLocations();
        jobSearch.SelectRemote();
        jobSearch.ClickFind();
        Thread.Sleep(2000);

        string latestTitle = results.GetLatestResultText();

        Assert.That(latestTitle.Contains(keyword, StringComparison.OrdinalIgnoreCase),
            $"Expected job title to contain '{keyword}', but got: {latestTitle}");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
    
}
