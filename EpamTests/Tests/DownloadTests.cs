using NUnit.Framework;
using EpamTests.Core.Base;
using EpamTests.Core.Helpers;
using EpamTests.Bussines.Pages;
using EpamTests.Core.Logging;
using EpamTests.Core.Driver;
using OpenQA.Selenium;
using Allure.NUnit.Attributes;
using Allure.Commons;

namespace EpamTests.Tests;

[TestFixture]
[AllureSuite("Download Tests")]
[AllureSubSuite("Validate File Download")]
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
    [AllureName("Validate file download from About page")]
    public void ValidateFileDownload(string fileName)
    {
        Logger.Info("Test started: Validate file download from About page.");

        try
        {
            HomePage home = new HomePage(Driver);
            AboutPage about = new AboutPage(Driver);

            home.GoTo();
            about.GoToAbout();
            about.ScrollToGlanceSection();
            about.ClickDownload();

            bool fileDownloaded = FileHelper.WaitForFileDownload(downloadDir, fileName);

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