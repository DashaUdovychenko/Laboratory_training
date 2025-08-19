using System.Text.Json;

namespace ApiAutomation.Core.Configuration
{
    public class TestConfig
    {
        private static TestConfig? _instance;

        public static TestConfig Instance =>
            _instance ??= Load("testconfig.json");

        public string BaseUrl { get; set; } = "https://jsonplaceholder.typicode.com";
        public string LogLevel { get; set; } = "Info";

        private static TestConfig Load(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<TestConfig>(json)
                   ?? throw new InvalidOperationException("Cannot parse config");
        }
    }
}