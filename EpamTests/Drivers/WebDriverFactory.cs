using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EpamTests.Drivers;

public static class WebDriverFactory
{
    public static IWebDriver CreateDriver()
    {
        var opptions = new ChromeOptions();
        opptions.AddArgument("--start-maximized");

        var driver = new ChromeDriver(opptions);

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

        return driver;
    }
}