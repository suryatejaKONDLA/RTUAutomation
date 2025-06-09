namespace RTUAutomation.Models.Core;

public class PasControlTableModel
{
    [JsonProperty("control_address_from_1")]
    [JsonPropertyName("control_address_from_1")]
    public string ControlAddressFrom1 { get; set; }

    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonProperty("key")]
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonProperty("setpoint_curve")]
    [JsonPropertyName("setpoint_curve")]
    public string SetpointCurve { get; set; }

    [JsonProperty("control_type")]
    [JsonPropertyName("control_type")]
    public string ControlType { get; set; }
}