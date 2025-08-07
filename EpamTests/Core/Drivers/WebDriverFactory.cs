using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using EpamTests.Core.Configuration;
using EpamTests.Core.Logging;

namespace EpamTests.Core.Driver;

public static class WebDriverFactory
{
    public static IWebDriver CreateDriverFromConfig()
    {
        var settings = ConfigManager.Settings;
        var browser = settings.Browser?.ToLower() ?? "chrome";
        var headless = settings.Headless;
        var downloadDir = GetDownloadDirectory(settings.DownloadDirectory);

        Logger.Info($"Initializing WebDriver: {browser}, Headless: {headless}, DownloadDir: {downloadDir}");

        return WebDriverInitializer.CreateDriver(browser, headless, downloadDir);
    }

    private static string GetDownloadDirectory(string? customPath)
    {
        var path = string.IsNullOrWhiteSpace(customPath)
            ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")
            : customPath;

        Directory.CreateDirectory(path);
        return path;
    }
}