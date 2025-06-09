namespace RTUAutomation.Models.Core;

public class PasLocationsModel
{
    [JsonProperty("RTUName")]
    [JsonPropertyName("RTUName")]
    public string RtuName { get; set; }

    [JsonProperty(nameof(ProtocolName))]
    [JsonPropertyName(nameof(ProtocolName))]
    public string ProtocolName { get; set; }

    [JsonProperty(nameof(StnName))]
    [JsonPropertyName(nameof(StnName))]
    public string StnName { get; set; }
}