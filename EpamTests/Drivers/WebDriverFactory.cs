using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EpamTests.Drivers;

public static class WebDriverFactory
{
    public static IWebDriver CreateDriver(bool headless = false)
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");

        if (headless)
        {
            options.AddArgument("--headless=new");
        }

        string downLoadDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

        options.AddUserProfilePreference("download.default_directory", downLoadDir);
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("disable-popup-blocking", true);

        return new ChromeDriver(options);
    }
}