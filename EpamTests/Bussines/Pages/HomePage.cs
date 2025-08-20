using EpamTests.Core.Logging;
using EpamTests.Core.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EpamTests.Bussines.Pages;

public class HomePage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public HomePage(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private By careers = By.CssSelector("li.top-navigation__item.epam:last-of-type");
    private By searchIcon = By.CssSelector("span.search-icon.header-search__search-icon");
    private By searchField = By.Id("new_form_search");
    private By btnFind = By.ClassName("bth-text-layer");

    public void GoTo()
    {
        Logger.Info("Navigating to Home page.");
        driver.Navigate().GoToUrl("https://www.epam.com/?_gl=1*1lcowp0*_ga*ODU2ODQ2MzY0LjE3MjgxOTMxMTk.*_ga_WBGDS7S6W6*MTc0MTkwMjAxOS4xMS4xLjE3NDE5MDQwNTMuNjAuMC4w");
        CookieConsentHelper.AcceptCookies(driver);
    }

    public void ClickCareers()
    {
        Logger.Info("Clicking on the Careers menu item.");
        wait.Until(d => driver.FindElement(careers).Displayed && driver.FindElement(careers).Enabled);
        driver.FindElement(careers).Click();
    }

    public void ClickSearchIcon()
    {
        Logger.Info("Clicking on the search icon.");
        wait.Until(d => driver.FindElement(searchIcon).Displayed && driver.FindElement(searchIcon).Enabled);
        driver.FindElement(searchIcon).Click();
    }

    public void EnterSearchKeyword(string keyword)
    {
        Logger.Info($"Entering search keyword: {keyword}");
        wait.Until(d => driver.FindElement(searchField).Displayed && driver.FindElement(searchField).Enabled);

        driver.FindElement(searchField).Click();
        driver.FindElement(searchField).SendKeys(keyword);
    }

    public void ClickFind()
    {
        Logger.Info("Clicking the 'Find' button.");
        wait.Until(d => driver.FindElement(btnFind).Displayed && driver.FindElement(btnFind).Enabled);
        driver.FindElement(btnFind).Click();
    }
}