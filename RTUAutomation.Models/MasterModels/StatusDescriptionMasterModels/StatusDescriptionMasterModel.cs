namespace RTUAutomation.Models.MasterModels.StatusDescriptionMasterModels;

public class StatusDescriptionMasterModel
{
    [JsonProperty("SL07_StatusDescription_ID")]
    [JsonPropertyName("SL07_StatusDescription_ID")]
    public int? Sl07StatusDescriptionId { get; set; }

    [JsonProperty("SL07_StatusDescription_Name")]
    [JsonPropertyName("SL07_StatusDescription_Name")]
    public string Sl07StatusDescriptionName { get; set; }

    [JsonProperty("SL07_StatusDescription_Active_Flag")]
    [JsonPropertyName("SL07_StatusDescription_Active_Flag")]
    public string Sl07StatusDescriptionActiveFlag { get; set; }

    [JsonProperty("SL07_StatusDescription_Order")]
    [JsonPropertyName("SL07_StatusDescription_Order")]
    public int? Sl07StatusDescriptionOrder { get; set; }

    [JsonProperty(CommonConstants.SessionId)]
    [JsonPropertyName(CommonConstants.SessionId)]
    public int? SessionId { get; set; }
}