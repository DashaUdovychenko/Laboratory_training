using NUnit.Framework;
using OpenQA.Selenium;
using EpamTests.Bussines.Pages;
using EpamTests.Core.Base;
using EpamTests.Core.Driver;
using EpamTests.Core.Logging;

namespace EpamTests.Tests;

[TestFixture]
public class GlobalSearchTests : BaseTest
{
    [Test]
    [TestCase("BLOCKCHAIN")]
    [TestCase("Cloud")]
    [TestCase("Automation")]
    public void ValidateSearchResults(string keyword)
    {
        Logger.Info($"Test started: Validate search results for keyword '{keyword}'.");

        try
        {
            HomePage homePage = new HomePage(Driver);
            SearchResultsPage searchResultsPage = new SearchResultsPage(Driver);

            homePage.GoTo();
            homePage.ClickSearchIcon();
            homePage.EnterSearchKeyword(keyword);
            homePage.ClickFind();

            Assert.That(searchResultsPage.AnyGlobalResultsExist(), Is.True, "No search results were found.");
            Assert.That(searchResultsPage.AllGlobalResultsContain(keyword),Is.True, $"Not all search results contain the keyword '{keyword}'.");
    
            Logger.Info("Test passed.");
        }
        catch (AssertionException ex)
        {
            Logger.Error($"Test failed with exception: {ex.Message}");
            throw;
        }
    }
}