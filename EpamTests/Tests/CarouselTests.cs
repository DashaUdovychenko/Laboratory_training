using NUnit.Framework;
using OpenQA.Selenium;
using EpamTests.Drivers;
using EpamTests.Pages;

namespace EpamTests.Tests;

[TestFixture]
public class CarouselTests
{
    private IWebDriver driver;

    [SetUp]
    public void SetUp()
    {
        driver = WebDriverFactory.CreateDriver();
    }

    [Test]
    public void ValidateArticleTitleMatchesCarousel()
    {
        var home = new HomePage(driver!);
        var insights = new InsightsPage(driver!);

        home.GoTo();
        insights.GoToInsights();
        insights.ClickCarouselNext();
        insights.ClickCarouselNext();

        string expectedTitle = insights.GetActiveSlideTitle();

        insights.ClickReadMore();

        string actualTitle = insights.GetArticleTitle();

        Assert.That(actualTitle, Does.Contain(expectedTitle), "The article page title does not contain the carousel slide title.");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }

}