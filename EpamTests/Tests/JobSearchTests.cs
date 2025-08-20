using NUnit.Framework;
using EpamTests.Core.Base;
using EpamTests.Bussines.Pages;
using EpamTests.Core.Helpers;
using EpamTests.Core.Logging;
using System.Threading;
using EpamTests.Core.Driver;
using OpenQA.Selenium;

namespace EpamTests.Tests;

[TestFixture]
public class JobSearchTests : BaseTest
{
    [Test]
    [TestCase(".NET")]
    [TestCase("Java")]
    public void ValidateJobSearch(string keyword)
    {
        Logger.Info($"Test started: Validate job search results for keyword '{keyword}'.");

        try
        {
            HomePage home = new HomePage(Driver);
            CareersPage careers = new CareersPage(Driver);
            JobSearchPage jobSearch = new JobSearchPage(Driver);
            SearchResultsPage results = new SearchResultsPage(Driver);

            home.GoTo();
            home.ClickCareers();
            careers.ClickDreamJobLink();
            CookieConsentHelper.AcceptCookies(Driver);
            jobSearch.EnterKeyword(keyword);
            jobSearch.SelectAllLocations();
            jobSearch.SelectRemote();
            jobSearch.ClickFind();
            
            Logger.Info("Waiting for search results to load.");
            Thread.Sleep(3000);

            string latestTitle = results.GetLatestResultText();
            Logger.Info($"Latest job result title: {latestTitle}");

            Assert.That(latestTitle.Contains(keyword, StringComparison.OrdinalIgnoreCase),
                $"Expected job title to contain '{keyword}', but got: {latestTitle}");
    
            Logger.Info("Test passed.");
        }
        catch (AssertionException ex)
        {
            Logger.Error ($"Test failed with exception: {ex.Message}");
            throw;
        }
    }
}