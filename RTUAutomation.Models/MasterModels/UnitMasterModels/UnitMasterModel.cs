namespace RTUAutomation.Models.MasterModels.UnitMasterModels;

public class UnitMasterModel
{
    [JsonProperty("SL03_Unit_ID")]
    [JsonPropertyName("SL03_Unit_ID")]
    public int? Sl03UnitId { get; set; }

    [JsonProperty("SL03_Unit_Name")]
    [JsonPropertyName("SL03_Unit_Name")]
    public string Sl03UnitName { get; set; }

    [JsonProperty("SL03_Unit_Active_Flag")]
    [JsonPropertyName("SL03_Unit_Active_Flag")]
    public string Sl03UnitActiveFlag { get; set; }

    [JsonProperty("SL03_Unit_Order")]
    [JsonPropertyName("SL03_Unit_Order")]
    public int? Sl03UnitOrder { get; set; }

    [JsonProperty(CommonConstants.SessionId)]
    [JsonPropertyName(CommonConstants.SessionId)]
    public int? SessionId { get; set; }
}