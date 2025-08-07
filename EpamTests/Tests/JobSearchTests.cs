using NUnit.Framework;
using EpamTests.Core.Base;
using EpamTests.Bussines.Pages;
using EpamTests.Helpers;
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
            var home = new HomePage(Driver);
            var careers = new CareersPage(Driver);
            var jobSearch = new JobSearchPage(Driver);
            var results = new SearchResultsPage(Driver);

            Logger.Debug("Navigating to Home page.");
            home.GoTo();

            Logger.Info("Clicking Careers link.");
            home.ClickCareers();

            Logger.Info("Clicking Dream Job link.");
            careers.ClickDreamJobLink();

            Logger.Info("Accepting cookies.");
            CookieConsentHelper.AcceptCookies(Driver);

            Logger.Info($"Entering job search keyword: {keyword}");
            jobSearch.EnterKeyword(keyword);

            Logger.Info("Selecting all locations and remote options.");
            jobSearch.SelectAllLocations();
            jobSearch.SelectRemote();

            Logger.Info("Clicking Find button.");
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