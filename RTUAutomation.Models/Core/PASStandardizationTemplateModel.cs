namespace RTUAutomation.Models.Core;

public class PasStandardizationTemplateAnalogSheetModel
{
    [JsonProperty("RTU")]
    [JsonPropertyName("RTU")]
    public int? Rtu { get; set; }

    [JsonProperty("TMS")]
    [JsonPropertyName("TMS")]
    public int? Tms { get; set; }

    [JsonProperty("Description")]
    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonProperty("OSI Name")]
    [JsonPropertyName("OSI Name")]
    public string OsiName { get; set; }

    [JsonProperty("SCADA Key")]
    [JsonPropertyName("SCADA Key")]
    public string ScadaKey { get; set; }

    [JsonProperty("Ph")]
    [JsonPropertyName("Ph")]
    public string Ph { get; set; }

    [JsonProperty("Unit")]
    [JsonPropertyName("Unit")]
    public string Unit { get; set; }

    [JsonProperty("Full Scale Value")]
    [JsonPropertyName("Full Scale Value")]
    public string FullScaleValue { get; set; }

    [JsonProperty("Full Scale Count")]
    [JsonPropertyName("Full Scale Count")]
    public string FullScaleCount { get; set; }

    [JsonProperty("Termination Point/Type (Utilize for Hardware and software)")]
    [JsonPropertyName("Termination Point/Type (Utilize for Hardware and software)")]
    public string TerminationPointTypeUtilizeForHardwareAndSoftware { get; set; }

    [JsonProperty("Termination Point/Type (Utilize for Hardware and software) 1")]
    [JsonPropertyName("Termination Point/Type (Utilize for Hardware and software) 1")]
    public string TerminationPointTypeUtilizeForHardwareAndSoftware1 { get; set; }

    [JsonProperty("CTR")]
    [JsonPropertyName("CTR")]
    public string Ctr { get; set; }

    [JsonProperty("PTR")]
    [JsonPropertyName("PTR")]
    public string Ptr { get; set; }

    [JsonProperty("Resistor Size")]
    [JsonPropertyName("Resistor Size")]
    public object ResistorSize { get; set; }

    [JsonProperty("Xdcr. Type")]
    [JsonPropertyName("Xdcr. Type")]
    public string XdcrType { get; set; }

    [JsonProperty("Comments")]
    [JsonPropertyName("Comments")]
    public string Comments { get; set; }
}

public class PasStandardizationTemplateStatusSheetModel
{
    [JsonProperty("0")]
    [JsonPropertyName("0")]
    public string _0 { get; set; }

    [JsonProperty("1")]
    [JsonPropertyName("1")]
    public string _1 { get; set; }

    [JsonProperty("RTU")]
    [JsonPropertyName("RTU")]
    public int? Rtu { get; set; }

    [JsonProperty("TMS")]
    [JsonPropertyName("TMS")]
    public int? Tms { get; set; }

    [JsonProperty("Description")]
    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonProperty("OSI Name")]
    [JsonPropertyName("OSI Name")]
    public string OsiName { get; set; }

    [JsonProperty("SCADA Key")]
    [JsonPropertyName("SCADA Key")]
    public string ScadaKey { get; set; }

    [JsonProperty("Status Description")]
    [JsonPropertyName("Status Description")]
    public string StatusDescription { get; set; }

    [JsonProperty("TMS Invert (0/1  1/0)")]
    [JsonPropertyName("TMS Invert (0/1  1/0)")]
    public string TmsInvert0110 { get; set; }

    [JsonProperty("Normal State")]
    [JsonPropertyName("Normal State")]
    public string NormalState { get; set; }

    [JsonProperty("Termination Point/Type (Utilize for Hardware and software)")]
    [JsonPropertyName("Termination Point/Type (Utilize for Hardware and software)")]
    public string TerminationPointTypeUtilizeForHardwareAndSoftware { get; set; }

    [JsonProperty("Termination Point/Type (Utilize for Hardware and software) 1")]
    [JsonPropertyName("Termination Point/Type (Utilize for Hardware and software) 1")]
    public string TerminationPointTypeUtilizeForHardwareAndSoftware1 { get; set; }

    [JsonProperty("Comments")]
    [JsonPropertyName("Comments")]
    public string Comments { get; set; }
}

public class PasStandardizationTemplateControlsSheetModel
{
    [JsonProperty("RTU")]
    [JsonPropertyName("RTU")]
    public int? Rtu { get; set; }

    [JsonProperty("TMS")]
    [JsonPropertyName("TMS")]
    public int? Tms { get; set; }

    [JsonProperty("Description")]
    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonProperty("OSI Name")]
    [JsonPropertyName("OSI Name")]
    public string OsiName { get; set; }

    [JsonProperty("SCADA Key")]
    [JsonPropertyName("SCADA Key")]
    public string ScadaKey { get; set; }

    [JsonProperty("Termination Point/Type (Utilize for Hardware and software)")]
    [JsonPropertyName("Termination Point/Type (Utilize for Hardware and software)")]
    public string TerminationPointTypeUtilizeForHardwareAndSoftware { get; set; }

    [JsonProperty("Termination Point/Type (Utilize for Hardware and software) 1")]
    [JsonPropertyName("Termination Point/Type (Utilize for Hardware and software) 1")]
    public string TerminationPointTypeUtilizeForHardwareAndSoftware1 { get; set; }

    [JsonProperty("Comments")]
    [JsonPropertyName("Comments")]
    public string Comments { get; set; }
}