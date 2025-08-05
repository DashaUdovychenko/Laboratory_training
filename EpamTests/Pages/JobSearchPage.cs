using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EpamTests.Pages;

public class JobSearchPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public JobSearchPage(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private By keywordField = By.Id("new_form_job_search-keyword");
    private By locationDropdown = By.ClassName("select2-selection__rendered");
    private By allLocationsOption = By.XPath("//li[contains(@class, 'select2-results__option') and @title='All Locations']");
    private By remoteCheckbox = By.XPath("//label[contains(text(),'Remote')]");
    private By findButton = By.XPath("//button[@type='submit']");

    public void EnterKeyword(string keyword)
    {
        wait.Until(d => driver.FindElement(keywordField).Displayed && driver.FindElement(keywordField).Enabled);
        driver.FindElement(keywordField).Click();
        driver.FindElement(keywordField).SendKeys(keyword);
    }

    public void SelectAllLocations()
        {
            wait.Until(d => driver.FindElement(locationDropdown).Displayed && driver.FindElement(locationDropdown).Enabled);
            driver.FindElement(locationDropdown).Click();

            wait.Until(d => driver.FindElement(allLocationsOption).Displayed && driver.FindElement(allLocationsOption).Enabled);
            driver.FindElement(allLocationsOption).Click();
        }

    public void SelectRemote()
    {
        wait.Until(d => driver.FindElement(remoteCheckbox).Displayed && driver.FindElement(remoteCheckbox).Enabled);
        driver.FindElement(remoteCheckbox).Click();
    }

    public void ClickFind()
    {
        wait.Until(d => driver.FindElement(findButton).Displayed && driver.FindElement(findButton).Enabled);
        driver.FindElement(findButton).Click();
    }
}