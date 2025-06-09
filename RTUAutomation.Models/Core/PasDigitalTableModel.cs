namespace RTUAutomation.Models.Core;

public class PasDigitalTableModel
{
    [JsonProperty("scan_order")]
    [JsonPropertyName("scan_order")]
    public int? ScanOrder { get; set; }

    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonProperty("key")]
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonProperty("inverted")]
    [JsonPropertyName("inverted")]
    public bool? Inverted { get; set; }

    [JsonProperty("protocol_name")]
    [JsonPropertyName("protocol_name")]
    public string ProtocolName { get; set; }

    [JsonProperty("states_table")]
    [JsonPropertyName("states_table")]
    public string StatesTable { get; set; }

    [JsonProperty("normal_state")]
    [JsonPropertyName("normal_state")]
    public string NormalState { get; set; }
}