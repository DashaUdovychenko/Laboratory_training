using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using EpamTests.Core.Driver;
using EpamTests.Core.Logging;
using NUnit.Framework.Interfaces;

namespace EpamTests.Core.Base;

public abstract class BaseTest
{
    protected IWebDriver Driver => SingletonWebDriver.GetDriver();

    private const string ScreenshotFolderName = "Screenshots";

    [SetUp]
    public virtual void SetUp()
    {
        Logger.Info($"=== START TEST: {TestContext.CurrentContext.Test.Name} ===");
    }

    [TearDown]
    public void TearDown()
    {
        LogTestResult();

        Logger.Info("Quitting WebDriver.");
        SingletonWebDriver.Close();

        Logger.Info($"=== END TEST: {TestContext.CurrentContext.Test.Name} ===");
    }

    private void LogTestResult()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        var testName = TestContext.CurrentContext.Test.Name;

        if (status == TestStatus.Failed)
        {
            Logger.Error($"Test Failed: {testName}");
            TakeScreenshot(testName);
        }
        else
        {
            Logger.Info($"Test Passed: {testName}");
        }
    }

    private void TakeScreenshot(string testName)
    {
        try
        {
            Logger.Info("Attempting to capture screenshot.");
            
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var screenshotsDir = Path.Combine(AppContext.BaseDirectory, ScreenshotFolderName);

            if (!Directory.Exists(screenshotsDir))
            {
                Directory.CreateDirectory(screenshotsDir);
                Logger.Debug($"Created screenshot directory: {screenshotsDir}");
            }

            var filePath = Path.Combine(screenshotsDir, $"{testName}_{timestamp}.png");
            screenshot.SaveAsFile(filePath);

            Logger.Info($"Screenshot saved: {filePath}");
            TestContext.AddTestAttachment(filePath, "Screenshot on Failure");
        }
        catch (Exception ex)
        {
            Logger.Error($"Could not take screenshot: {ex.Message}");
        }
    }
}