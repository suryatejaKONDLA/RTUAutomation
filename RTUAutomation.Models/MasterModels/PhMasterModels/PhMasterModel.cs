namespace RTUAutomation.Models.MasterModels.PhMasterModels;

public class PhMasterModel
{
    [JsonProperty("SL01_PH_ID")]
    [JsonPropertyName("SL01_PH_ID")]
    public int? Sl01PhId { get; set; }

    [JsonProperty("SL01_PH_Name")]
    [JsonPropertyName("SL01_PH_Name")]
    public string Sl01PhName { get; set; }

    [JsonProperty("SL01_PH_Active_Flag")]
    [JsonPropertyName("SL01_PH_Active_Flag")]
    public string Sl01PhActiveFlag { get; set; }

    [JsonProperty("SL01_PH_Order")]
    [JsonPropertyName("SL01_PH_Order")]
    public string Sl01PhOrder { get; set; }

    [JsonProperty(CommonConstants.SessionId)]
    [JsonPropertyName(CommonConstants.SessionId)]
    public int? SessionId { get; set; }
}