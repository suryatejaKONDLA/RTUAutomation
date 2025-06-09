namespace RTUAutomation.Models.Core;

public class PasAnalogTableModel
{
    [JsonProperty("scan_order")]
    [JsonPropertyName("scan_order")]
    public int? ScanOrder { get; set; }

    [JsonProperty(nameof(Name))]
    [JsonPropertyName(nameof(Name))]
    public string Name { get; set; }

    [JsonProperty(nameof(Description))]
    [JsonPropertyName(nameof(Description))]
    public string Description { get; set; }

    [JsonProperty(nameof(Key))]
    [JsonPropertyName(nameof(Key))]
    public string Key { get; set; }

    [JsonProperty("curve_description")]
    [JsonPropertyName("curve_description")]
    public string CurveDescription { get; set; }

    [JsonProperty("protocol_name")]
    [JsonPropertyName("protocol_name")]
    public string ProtocolName { get; set; }
}