using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using EpamTests.Core.Configuration;
using EpamTests.Core.Logging;

namespace EpamTests.Core.Driver;

public static class BrowserOptionsFactory
{
    public static ChromeOptions GetChromeOptions(bool headless, string downloadDir)
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        if (headless)
        {
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");
        }

        options.AddUserProfilePreference("download.default_directory", downloadDir);
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("download.directory_upgrade", true);
        options.AddUserProfilePreference("safebrowsing.enabled", true);

        return options;
    }

    public static FirefoxOptions GetFirefoxOptions(bool headless, string downloadDir)
    {
        FirefoxOptions options = new FirefoxOptions();

        if (headless)
            options.AddArgument("--headless");

        options.SetPreference("browser.download.folderList", 2);
        options.SetPreference("browser.download.dir", downloadDir);
        options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf");
        options.SetPreference("pdfjs.disabled", true);

        return options;
    }

    public static EdgeOptions GetEdgeOptions(bool headless, string downloadDir)
    {
        EdgeOptions options = new EdgeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");
        }

        options.AddUserProfilePreference("download.default_directory", downloadDir);
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("download.directory_upgrade", true);
        options.AddUserProfilePreference("safebrowsing.enabled", true);

        return options;
    }
}