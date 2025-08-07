using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace EpamTests.Core.Driver;

public static class WebDriverInitializer
{
    public static IWebDriver CreateDriver(string browser, bool headless, string downloadDir)
    {
        return browser switch
        {
            "chrome" => InitChromeDriver(headless, downloadDir),
            "firefox" => InitFirefoxDriver(headless, downloadDir),
            "edge" => InitEdgeDriver(headless, downloadDir),
            _ => throw new ArgumentException($"Unsupported browser: {browser}")
        };
    }

    private static IWebDriver InitChromeDriver(bool headless, string downloadDir)
    {
        new DriverManager().SetUpDriver(new ChromeConfig());
        return new ChromeDriver(BrowserOptionsFactory.GetChromeOptions(headless, downloadDir));
    }

    private static IWebDriver InitFirefoxDriver(bool headless, string downloadDir)
    {
        new DriverManager().SetUpDriver(new FirefoxConfig());
        return new FirefoxDriver(BrowserOptionsFactory.GetFirefoxOptions(headless, downloadDir));
    }

    private static IWebDriver InitEdgeDriver(bool headless, string downloadDir)
    {
        new DriverManager().SetUpDriver(new EdgeConfig());
        return new EdgeDriver(BrowserOptionsFactory.GetEdgeOptions(headless, downloadDir));
    }
}