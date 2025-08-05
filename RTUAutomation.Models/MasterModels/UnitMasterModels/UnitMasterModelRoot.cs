namespace RTUAutomation.Models.MasterModels.UnitMasterModels;

public class UnitMasterModelRoot
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

    [JsonProperty("SL03_Created_ID")]
    [JsonPropertyName("SL03_Created_ID")]
    public int? Sl03CreatedId { get; set; }

    [JsonProperty("SL03_Created_Date")]
    [JsonPropertyName("SL03_Created_Date")]
    public DateTime? Sl03CreatedDate { get; set; }

    [JsonProperty("SL03_Modified_ID")]
    [JsonPropertyName("SL03_Modified_ID")]
    public int? Sl03ModifiedId { get; set; }

    [JsonProperty("SL03_Modified_Date")]
    [JsonPropertyName("SL03_Modified_Date")]
    public DateTime? Sl03ModifiedDate { get; set; }

    [JsonProperty("SL03_Approved_ID")]
    [JsonPropertyName("SL03_Approved_ID")]
    public int? Sl03ApprovedId { get; set; }

    [JsonProperty("SL03_Approved_Date")]
    [JsonPropertyName("SL03_Approved_Date")]
    public DateTime? Sl03ApprovedDate { get; set; }

    [JsonProperty("SL03_Created_Name")]
    [JsonPropertyName("SL03_Created_Name")]
    public string Sl03CreatedName { get; set; }

    [JsonProperty("SL03_Modified_Name")]
    [JsonPropertyName("SL03_Modified_Name")]
    public string Sl03ModifiedName { get; set; }

    [JsonProperty("SL03_Approved_Name")]
    [JsonPropertyName("SL03_Approved_Name")]
    public string Sl03ApprovedName { get; set; }
}