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
            homePage.GoTo();
            Logger.Debug("Navigated to Home Page.");

            Logger.Info("Clicking search icon.");
            homePage.ClickSearchIcon();

            Logger.Info($"Entering search keyword: {keyword}");
            homePage.EnterSearchKeyword(keyword);

            Logger.Info("Clicking find button.");
            homePage.ClickFind();

            SearchResultsPage searchResultsPage = new SearchResultsPage(Driver);

            Logger.Info("Validating if any search results exist.");
            Assert.That(searchResultsPage.AnyGlobalResultsExist(), Is.True, "No search results were found.");

            Logger.Info($"Validating that all results contain keyword '{keyword}'.");
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