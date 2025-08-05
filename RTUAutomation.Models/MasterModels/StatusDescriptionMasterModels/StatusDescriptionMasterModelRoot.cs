namespace RTUAutomation.Models.MasterModels.StatusDescriptionMasterModels;

public class StatusDescriptionMasterModelRoot
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

    [JsonProperty("SL07_Created_ID")]
    [JsonPropertyName("SL07_Created_ID")]
    public int? Sl07CreatedId { get; set; }

    [JsonProperty("SL07_Created_Date")]
    [JsonPropertyName("SL07_Created_Date")]
    public DateTime? Sl07CreatedDate { get; set; }

    [JsonProperty("SL07_Modified_ID")]
    [JsonPropertyName("SL07_Modified_ID")]
    public int? Sl07ModifiedId { get; set; }

    [JsonProperty("SL07_Modified_Date")]
    [JsonPropertyName("SL07_Modified_Date")]
    public DateTime? Sl07ModifiedDate { get; set; }

    [JsonProperty("SL07_Approved_ID")]
    [JsonPropertyName("SL07_Approved_ID")]
    public int? Sl07ApprovedId { get; set; }

    [JsonProperty("SL07_Approved_Date")]
    [JsonPropertyName("SL07_Approved_Date")]
    public DateTime? Sl07ApprovedDate { get; set; }

    [JsonProperty("SL07_Created_Name")]
    [JsonPropertyName("SL07_Created_Name")]
    public string Sl07CreatedName { get; set; }

    [JsonProperty("SL07_Modified_Name")]
    [JsonPropertyName("SL07_Modified_Name")]
    public string Sl07ModifiedName { get; set; }

    [JsonProperty("SL07_Approved_Name")]
    [JsonPropertyName("SL07_Approved_Name")]
    public string Sl07ApprovedName { get; set; }
}