using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EpamTests.Pages;

public class JobSearchPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public JobSearchPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private IWebElement keywordField => driver.FindElement(By.Id("new_form_job_search-keyword"));
    private IWebElement locationDropdown => driver.FindElement(By.ClassName("select2-selection__rendered"));
    private IWebElement allLocationsOption => driver.FindElement(By.XPath("//li[contains(@class, 'select2-results__option') and @title='All Locations']"));
    private IWebElement remoteCheckbox => driver.FindElement(By.XPath("//label[contains(text(),'Remote')]"));
    private IWebElement findButton => driver.FindElement(By.XPath("//button[@type='submit']"));

    public void EnterKeyword(string keyword)
    {
        keywordField.Click();
        keywordField.SendKeys(keyword);
    }

    public void SelectAllLocations()
        {
            locationDropdown.Click();
            allLocationsOption.Click();
        }

    public void SelectRemote()
    {
        remoteCheckbox.Click();
    }

    public void ClickFind()
    {
        findButton.Click();
    }
}