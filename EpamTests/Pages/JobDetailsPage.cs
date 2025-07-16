using OpenQA.Selenium;
namespace EpamTests.Pages;

public class JobDetailsPage
{
    private readonly IWebDriver driver;

    public JobDetailsPage(IWebDriver driver) => this.driver = driver;

    public bool IsKeywordPresent(string keyword)
    {
        return driver.PageSource.Contains(keyword, StringComparison.OrdinalIgnoreCase);
    }
}
