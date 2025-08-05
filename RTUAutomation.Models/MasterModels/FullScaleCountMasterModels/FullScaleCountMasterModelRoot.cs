namespace RTUAutomation.Models.MasterModels.FullScaleCountMasterModels;

public class FullScaleCountMasterModelRoot
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

    [JsonProperty("SL05_Created_ID")]
    [JsonPropertyName("SL05_Created_ID")]
    public int? Sl05CreatedId { get; set; }

    [JsonProperty("SL05_Created_Date")]
    [JsonPropertyName("SL05_Created_Date")]
    public DateTime? Sl05CreatedDate { get; set; }

    [JsonProperty("SL05_Modified_ID")]
    [JsonPropertyName("SL05_Modified_ID")]
    public int? Sl05ModifiedId { get; set; }

    [JsonProperty("SL05_Modified_Date")]
    [JsonPropertyName("SL05_Modified_Date")]
    public DateTime? Sl05ModifiedDate { get; set; }

    [JsonProperty("SL05_Approved_ID")]
    [JsonPropertyName("SL05_Approved_ID")]
    public int? Sl05ApprovedId { get; set; }

    [JsonProperty("SL05_Approved_Date")]
    [JsonPropertyName("SL05_Approved_Date")]
    public DateTime? Sl05ApprovedDate { get; set; }

    [JsonProperty("SL05_Created_Name")]
    [JsonPropertyName("SL05_Created_Name")]
    public string Sl05CreatedName { get; set; }

    [JsonProperty("SL05_Modified_Name")]
    [JsonPropertyName("SL05_Modified_Name")]
    public string Sl05ModifiedName { get; set; }

    [JsonProperty("SL05_Approved_Name")]
    [JsonPropertyName("SL05_Approved_Name")]
    public string Sl05ApprovedName { get; set; }
}