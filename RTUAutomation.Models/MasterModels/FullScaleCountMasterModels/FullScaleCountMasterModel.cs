namespace RTUAutomation.Models.MasterModels.FullScaleCountMasterModels;

public class FullScaleCountMasterModel
{
    [JsonProperty("SL05_FullScaleCount_ID")]
    [JsonPropertyName("SL05_FullScaleCount_ID")]
    public int? Sl05FullScaleCountId { get; set; }

    [JsonProperty("SL05_FullScaleCount_Name")]
    [JsonPropertyName("SL05_FullScaleCount_Name")]
    public string Sl05FullScaleCountName { get; set; }

    [JsonProperty("SL05_FullScaleCount_Active_Flag")]
    [JsonPropertyName("SL05_FullScaleCount_Active_Flag")]
    public string Sl05FullScaleCountActiveFlag { get; set; }

    [JsonProperty("SL05_FullScaleCount_Order")]
    [JsonPropertyName("SL05_FullScaleCount_Order")]
    public int? Sl05FullScaleCountOrder { get; set; }

    [JsonProperty(CommonConstants.SessionId)]
    [JsonPropertyName(CommonConstants.SessionId)]
    public int? SessionId { get; set; }
}