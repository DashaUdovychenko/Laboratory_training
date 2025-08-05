using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EpamTests.Pages;

public class CareersPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private readonly By dreamJobLinkBy = By.XPath("(//a[normalize-space(text())='Find Your Dream Job'])[6]");

    public CareersPage(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void ClickDreamJobLink()
    {
        wait.Until(d => driver.FindElement(dreamJobLinkBy).Displayed && driver.FindElement(dreamJobLinkBy).Enabled);
        driver.FindElement(dreamJobLinkBy).Click();
    }
}