using EpamTests.Core.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EpamTests.Bussines.Pages;

public class SearchResultsPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public SearchResultsPage(IWebDriver driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    private By latestResult = By.XPath("//h5/a");

    public string GetLatestResultText()
    {
        Logger.Info("Retrieving the text of the latest search result.");
        wait.Until(d => driver.FindElement(latestResult).Displayed && driver.FindElement(latestResult).Enabled);
        return driver.FindElement(latestResult).Text.Trim();
    }

    private readonly By globalSearchResultLinks = By.CssSelector("div.search-results__items a.search-results__title-link");

    public bool AnyGlobalResultsExist()
    {
        Logger.Info("Checking if any global search results exist.");
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        try
        {
            return wait.Until(d =>
            {
                IReadOnlyCollection<IWebElement> results = d.FindElements(globalSearchResultLinks);
                return results.Any();
            });
        }
        catch (WebDriverTimeoutException)
        { 
            return false;
        }
    }

    public bool AllGlobalResultsContain(string keyword)
    {
        Logger.Info($"Validating that all global search results contain the keyword '{keyword}'.");
        var globalSearchResultLinks = driver.FindElements(this.globalSearchResultLinks);
        
        if (globalSearchResultLinks.Count == 0)
        {
            return false;
        }

        return globalSearchResultLinks
            .Select(link => link.Text)
            .All(text => text.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }
}