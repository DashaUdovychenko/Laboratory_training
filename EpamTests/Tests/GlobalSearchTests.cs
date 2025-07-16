using NUnit.Framework;
using OpenQA.Selenium;
using EpamTests.Pages;
using EpamTests.Drivers;

namespace EpamTests.Tests;

[TestFixture]
public class GlobalSearchTests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        driver = WebDriverFactory.CreateDriver();
    }

    [Test]
    [TestCase("BLOCKCHAIN")]
    [TestCase("Cloud")]
    [TestCase("Automation")]
    public void ValidateSearchResults(string keyword)
    {
        var homePage = new HomePage(driver);
        homePage.GoTo();
        homePage.ClickSearchIcon();
        homePage.EnterSearchKeyword(keyword);
        homePage.ClickFind();

        var searchResultsPage = new SearchResultsPage(driver);

        Assert.That(searchResultsPage.AnyGlobalResultsExist(), Is.True, "No search results were found.");
        Assert.That(searchResultsPage.AllGlobalResultsContain(keyword),Is.True, $"Not all search results contain the keyword '{keyword}'.");
    }
    
    [TearDown]
    public void Teardown()
    {
        driver.Quit();
    }
}