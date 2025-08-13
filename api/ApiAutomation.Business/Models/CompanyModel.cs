using System.Text.Json.Serialization;

namespace ApiAutomation.Business.Models;

public class CompanyModel
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("catchPhrase")]
    public string? CatchPhrase { get; set; }

    [JsonPropertyName("bs")]
    public string? Bs { get; set; }
}