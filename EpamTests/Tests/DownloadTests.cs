using NUnit.Framework;
using OpenQA.Selenium;
using EpamTests.Drivers;
using EpamTests.Pages;

namespace EpamTests.Tests;

[TestFixture]
public class DownloadTests
{
    private IWebDriver driver;
    private string downloadDir;
    private string expectedFileName = "EPAM_Corporate_Overview_Q4FY-2024.pdf";

    [SetUp]
    public void SetUp()
    {
        downloadDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

        string fullPath = Path.Combine(downloadDir, expectedFileName);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        driver = WebDriverFactory.CreateDriver(headless: false);
    }

    [Test]
    [TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
    public void ValidateFileDownload(string fileName)
    {
        var home = new HomePage(driver);
        var about = new AboutPage(driver);

        home.GoTo();
        about.GoToAbout();
        about.ScrollToGlanceSection();
        about.ClickDownload();

        string fullPath = Path.Combine(downloadDir, fileName);
        int waitTime = 0;
        while (!File.Exists(fullPath) && waitTime < 20)
        {
            Thread.Sleep(1000);
            waitTime++;
        }

        Assert.That(File.Exists(fullPath), Is.True, $"File '{fileName}' was not downloaded successfully.");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}