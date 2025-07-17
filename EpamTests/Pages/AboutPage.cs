using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace EpamTests.Pages;

public class AboutPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public AboutPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private IWebElement aboutMenu => driver.FindElement(By.CssSelector("a.top-navigation__item-link.js-op[href='/about']"));
    private IWebElement epamAtGlanceSection => driver.FindElement(By.XPath("//span[contains(@class, 'font-size-80-33') and contains(normalize-space(.), 'EPAM at a Glance')]"));
    private IWebElement downloadBrochureBtn => driver.FindElement(By.XPath("//span[contains(@class, 'button__content--desktop') and normalize-space(text())='DOWNLOAD']"));

    public void GoToAbout()
    {
        aboutMenu.Click();
    }

    public void ScrollToGlanceSection()
    {
        Actions actions = new Actions(driver);
        actions.MoveToElement(epamAtGlanceSection).Perform();
    }

    public void ClickDownload()
    {
        downloadBrochureBtn.Click();
    }
}