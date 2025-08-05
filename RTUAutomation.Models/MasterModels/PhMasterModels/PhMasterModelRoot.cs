namespace RTUAutomation.Models.MasterModels.PhMasterModels;

public class PhMasterModelRoot
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
    public int? Sl01PhOrder { get; set; }

    [JsonProperty("SL01_Created_ID")]
    [JsonPropertyName("SL01_Created_ID")]
    public int? Sl01CreatedId { get; set; }

    [JsonProperty("SL01_Created_Date")]
    [JsonPropertyName("SL01_Created_Date")]
    public DateTime? Sl01CreatedDate { get; set; }

    [JsonProperty("SL01_Modified_ID")]
    [JsonPropertyName("SL01_Modified_ID")]
    public int? Sl01ModifiedId { get; set; }

    [JsonProperty("SL01_Modified_Date")]
    [JsonPropertyName("SL01_Modified_Date")]
    public DateTime? Sl01ModifiedDate { get; set; }

    [JsonProperty("SL01_Approved_ID")]
    [JsonPropertyName("SL01_Approved_ID")]
    public int? Sl01ApprovedId { get; set; }

    [JsonProperty("SL01_Approved_Date")]
    [JsonPropertyName("SL01_Approved_Date")]
    public DateTime? Sl01ApprovedDate { get; set; }

    [JsonProperty("SL01_Created_Name")]
    [JsonPropertyName("SL01_Created_Name")]
    public string Sl01CreatedName { get; set; }

    [JsonProperty("SL01_Modified_Name")]
    [JsonPropertyName("SL01_Modified_Name")]
    public string Sl01ModifiedName { get; set; }

    [JsonProperty("SL01_Approved_Name")]
    [JsonPropertyName("SL01_Approved_Name")]
    public string Sl01ApprovedName { get; set; }
}