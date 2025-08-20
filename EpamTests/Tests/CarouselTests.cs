using NUnit.Framework;
using EpamTests.Core.Base;
using EpamTests.Bussines.Pages;
using EpamTests.Core.Logging;
using EpamTests.Core.Driver;
using OpenQA.Selenium;

namespace EpamTests.Tests;

[TestFixture]
public class CarouselTests : BaseTest
{
    [Test]
    public void ValidateArticleTitleMatchesCarousel()
    {
        Logger.Info("Test started: Validate that the article title matches the carousel title after clicking Read More.");

        HomePage home = new HomePage(Driver);
        InsightsPage insights = new InsightsPage(Driver);

        Logger.Debug("Creating page objects for HomePage and InsightsPage.");

        Logger.Info("Navigating to Home page.");
        home.GoTo();

        Logger.Info("Navigating to Insights section.");
        insights.GoToInsights();

        Logger.Info("Clicking carousel 'Next' three times.");
        insights.ClickCarouselNext();
        insights.ClickCarouselNext();

        string expectedTitle = insights.GetActiveSlideTitle();
        Logger.Debug($"Expected title from carousel retrieved: {expectedTitle}");

        insights.ClickReadMore();
        string actualTitle = insights.GetArticleTitle();
        Logger.Info($"Actual article title: {actualTitle}");

        try
        {
            Assert.That(actualTitle, Does.Contain(expectedTitle),
            "The article page title does not contain the carousel slide title.");

            Logger.Info("Test passed.");
        }
        catch (AssertionException ex)
        {
            Logger.Error($"Assertion failed: {ex.Message}");
            throw;
        }
    }
}