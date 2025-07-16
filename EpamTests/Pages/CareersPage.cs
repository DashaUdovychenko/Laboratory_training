using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EpamTests.Pages;

public class CareersPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public CareersPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

   public void ClickVisibleDreamJobLink()
    {
        wait.Until(driver =>
            driver.FindElements(By.XPath("//a[normalize-space(text())='Find Your Dream Job']"))
                .Any(e => e.Displayed && e.Enabled));

        var elements = driver.FindElements(By.XPath("//a[normalize-space(text())='Find Your Dream Job']"));

        var visibleElement = elements.FirstOrDefault(e => e.Displayed && e.Enabled);

        if (visibleElement == null)
        {
            throw new NoSuchElementException("Couldn't find visible 'Find your dream job' element.");
        }

        // Спроба прокрутки + клік
        try
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", visibleElement);
            visibleElement.Click();
        }
        catch (ElementClickInterceptedException)
        {
         // Альтернативна стратегія з Actions
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(visibleElement).Click().Perform();
        }
    }
}