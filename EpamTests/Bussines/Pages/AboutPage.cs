using EpamTests.Core.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace EpamTests.Bussines.Pages;

public class AboutPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public AboutPage(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private By aboutMenu = By.CssSelector("a.top-navigation__item-link.js-op[href='/about']");
    private IWebElement epamAtGlanceSection => driver.FindElement(By.XPath("//span[contains(@class, 'font-size-80-33') and contains(normalize-space(.), 'EPAM at a Glance')]"));
    private By downloadBrochureBtn = By.XPath("//span[contains(@class, 'button__content--desktop') and normalize-space(text())='DOWNLOAD']");

    public void GoToAbout()
    {
        Logger.Info("Navigating to the About page.");
        wait.Until(d => driver.FindElement(aboutMenu).Displayed && driver.FindElement(aboutMenu).Enabled);
        driver.FindElement(aboutMenu).Click();
    }

    public void ScrollToGlanceSection()
    {
        Logger.Info("Scrolling to the 'EPAM at a Glance' section.");
        Actions actions = new Actions(driver);
        actions.MoveToElement(epamAtGlanceSection).Perform();
    }

    public void ClickDownload()
    {
        Logger.Info("Clicking the 'Download Brochure' button.");
        wait.Until(d => driver.FindElement(downloadBrochureBtn).Displayed && driver.FindElement(downloadBrochureBtn).Enabled);
        driver.FindElement(downloadBrochureBtn).Click();
    }
}