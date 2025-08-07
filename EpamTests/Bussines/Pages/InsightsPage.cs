using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EpamTests.Bussines.Pages;

public class InsightsPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public InsightsPage(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private By insightsMenu = By.CssSelector("a.top-navigation__item-link.js-op[href='/insights']");
    private By carouselNextBtn = By.XPath("//button[contains(@class, 'slider__right-arrow')]");
    private By activeSlideTitle = By.XPath("(//p[@class='scaling-of-text-wrapper' and .//span[contains(text(), 'Security Strategy')]])[2]");
    private By readMoreBtn = By.XPath("(//div[contains(@class, 'single-slide__content') and .//span[contains(@class, 'gradient-text') and contains(normalize-space(), 'Security Strategy')]]//a[normalize-space()='Read More'])[2]");
    private By articleTitle = By.XPath("//div[@class='scaling-of-text-wrapper']//p");

    public void GoToInsights()
    {
        wait.Until(d => driver.FindElement(insightsMenu).Displayed && driver.FindElement(insightsMenu).Enabled);
        driver.FindElement(insightsMenu).Click();
    }

    public void ClickCarouselNext()
    {
        wait.Until(d => driver.FindElement(carouselNextBtn).Displayed && driver.FindElement(carouselNextBtn).Enabled);
        driver.FindElement(carouselNextBtn).Click();
    }

    public string GetActiveSlideTitle()
    {
        wait.Until(d => driver.FindElement(activeSlideTitle).Displayed && driver.FindElement(activeSlideTitle).Enabled);
        return driver.FindElement(activeSlideTitle).Text.Trim();

    }

    public void ClickReadMore()
    {
        wait.Until(d => driver.FindElement(readMoreBtn).Displayed && driver.FindElement(readMoreBtn).Enabled);
        driver.FindElement(readMoreBtn).Click();
    }

    public string GetArticleTitle()
    {
        wait.Until(d => driver.FindElement(articleTitle).Displayed && driver.FindElement(articleTitle).Enabled);
        return driver.FindElement(articleTitle).Text.Trim();
    }
}