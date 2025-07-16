using EpamTests.Helpers;
using OpenQA.Selenium;

namespace EpamTests.Pages;

public class HomePage
{
    private readonly IWebDriver driver;
    public HomePage(IWebDriver driver) => this.driver = driver;

    private IWebElement Careers => driver.FindElement(By.CssSelector("li.top-navigation__item.epam:last-of-type"));
    private IWebElement searchIcon => driver.FindElement(By.CssSelector("span.search-icon.header-search__search-icon"));
    private IWebElement searchField => driver.FindElement(By.Id("new_form_search"));
    private IWebElement btnFind => driver.FindElement(By.ClassName("bth-text-layer"));

    public void GoTo()
    {
        driver.Navigate().GoToUrl("https://www.epam.com/");
        CookieConsentHelper.AcceptCookies(driver);
    }

    public void ClickCareers()
    {
        Careers.Click();
    }

    public void ClickSearchIcon()
    {
        searchIcon.Click();
    }

    public void EnterSearchKeyword(string keyword)
    {
        searchField.Clear();
        searchField.SendKeys(keyword);
    }

    public void ClickFind()
    {
        btnFind.Click();
    }
}