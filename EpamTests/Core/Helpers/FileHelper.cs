using EpamTests.Core.Logging;

namespace EpamTests.Core.Helpers;

public static class FileHelper
{
    public static bool WaitForFileDownload(string directory, string fileName, int timeoutSeconds = 20)
    {
        string fullPath = Path.Combine(directory, fileName);
        int waitTime = 0;

        Logger.Info($"Waiting for file to download: {fileName}");

        while (!File.Exists(fullPath) && waitTime < timeoutSeconds)
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

        return fileDownloaded;
    }
}