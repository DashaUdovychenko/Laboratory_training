using NUnit.Framework;
using EpamTests.Core.Base;
using EpamTests.Bussines.Pages;
using EpamTests.Core.Logging;
using EpamTests.Core.Driver;
using OpenQA.Selenium;
using Allure.NUnit.Attributes;
using Allure.Commons;

namespace EpamTests.Tests;

[TestFixture]
[AllureEpic("UI Tests")]
[AllureFeature("Carousel Module")]
public class CarouselTests : BaseTest
{
    [Test]
    [AllureName("Validate article title matches carousel slide")]
    public void ValidateArticleTitleMatchesCarousel()
    {
        Logger.Info("Test started: Validate that the article title matches the carousel title after clicking Read More.");

        HomePage home = new HomePage(Driver);
        InsightsPage insights = new InsightsPage(Driver);

        home.GoTo();
        insights.GoToInsights();

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