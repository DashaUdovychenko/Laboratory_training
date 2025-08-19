using NUnit.Framework;
using EpamTests.Core.Base;
using EpamTests.Bussines.Pages;
using EpamTests.Core.Logging;
using EpamTests.Core.Driver;
using OpenQA.Selenium;

namespace EpamTests.Tests;

[TestFixture]
public class DownloadTests : BaseTest
{
    private string downloadDir = null!;
    private readonly string expectedFileName = "EPAM_Corporate_Overview_Q4FY-2024.pdf";

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();

        Logger.Info("Setting up test preconditions for file download");

        downloadDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Downloads"
        );

        var fullPath = Path.Combine(downloadDir, expectedFileName);

        if (File.Exists(fullPath))
        {
            Logger.Info($"Existing file found. Deleting: {expectedFileName}");
            File.Delete(fullPath);
        }
        else
        {
            Logger.Debug("No existing file to delete.");
        }
    }

    [Test]
    [TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
    public void ValidateFileDownload(string fileName)
    {
        try
        {
            Logger.Info("Creating page objects");
            HomePage home = new HomePage(Driver);
            AboutPage about = new AboutPage(Driver);

            Logger.Info("Navigating to Home page.");
            home.GoTo();

            Logger.Info("Navigating to About page.");
            about.GoToAbout();

            Logger.Debug("Scrolling to 'EPAM at a Glance' section.");
            about.ScrollToGlanceSection();

            Logger.Info("Clicking 'Download' button.");
            about.ClickDownload();

            string fullPath = Path.Combine(downloadDir, fileName);
            int waitTime = 0;

            Logger.Info($"Waiting for file to download: {fileName}");
            while (!File.Exists(fullPath) && waitTime < 20)
            {
                Thread.Sleep(1000);
                waitTime++;
                Logger.Debug($"Download wait time elapsed: {waitTime}s");
            }

            bool fileDownloaded = File.Exists(fullPath);

            if (fileDownloaded)
            {
                Logger.Info($"File downloaded successfully: {fileName}");
            }
            else
            {
                Logger.Error($"File was not downloaded within timeout: {fileName}");
            }

            Assert.That(fileDownloaded, Is.True, $"File '{fileName}' was not downloaded successfully.");
            Logger.Info("Test passed.");
        }
        catch (AssertionException ex)
        {
            Logger.Error($"Assertion failed in ValidateFileDownload: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Logger.Error($" Unexpected error in ValidateFileDownload: {ex.Message}");
            throw;
        }
    }
}