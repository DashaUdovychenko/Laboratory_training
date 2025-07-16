using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EpamTests.Pages;

public class SearchResultsPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public SearchResultsPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private IWebElement LatestResult => driver.FindElement(By.XPath("(//h5/a)[last()]"));

    public string GetLatestResultText()
    {
        var element = wait.Until(driver =>
        {
            try
            {
                var el = driver.FindElement(By.XPath("(//h5/a)[last()]"));
                return el.Displayed ? el : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        });

        return element.Text;
    }

    public void ClickLatestResult()
    {
        LatestResult.Click();
    }

    private IReadOnlyCollection<IWebElement> GlobalSearchResultLinks =>
        driver.FindElements(By.CssSelector("div.search-results__items a.search-results__title-link"));

    public bool AnyGlobalResultsExist()
    {
        return GlobalSearchResultLinks.Any();
    }

    public bool AllGlobalResultsContain(string keyword)
    {
        return GlobalSearchResultLinks
            .Select(link => link.Text)
            .All(text => text.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }
}