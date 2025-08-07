using System.Text.Json;

namespace EpamTests.Core.Configuration;

public class Config
{
    public string Browser { get; set; } = "chrome";
    public bool Headless { get; set; } = false;
    public string BaseUrl { get; set; } = "";
    public string LogLevel { get; set; } = "Info";
    public string DownloadDirectory { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
}

public static class ConfigManager
{
    private static readonly string ConfigPath = Path.Combine(AppContext.BaseDirectory, "Config", "config.json");

    private static readonly Lazy<Config> _config = new(() =>
    {
        if (!File.Exists(ConfigPath))
        {
            throw new FileNotFoundException($"Configuration file not found at path: {ConfigPath}");
        }

        var json = File.ReadAllText(ConfigPath);
        return JsonSerializer.Deserialize<Config>(json)!;
    });

    public static Config Settings => _config.Value;
}