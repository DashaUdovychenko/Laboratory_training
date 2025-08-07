using System.Text.Json.Serialization;

namespace ApiAutomation.Business.Models;

public class AddressModel
{
    [JsonPropertyName("street")]
    public string? Street { get; set; }

    [JsonPropertyName("suite")]
    public string? Suite { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("zipcode")]
    public string? Zipcode { get; set; }

    [JsonPropertyName("geo")]
    public GeoModel? Geo { get; set; }
}