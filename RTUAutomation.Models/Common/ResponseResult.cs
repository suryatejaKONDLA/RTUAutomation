namespace RTUAutomation.Models.Common;

public record ResponseResult(
    [property: JsonPropertyOrder(0)] [property: JsonProperty(Order = 0)] int ResultCode,
    [property: JsonPropertyOrder(1)] [property: JsonProperty(Order = 1)] string ResultType,
    [property: JsonPropertyOrder(2)] [property: JsonProperty(Order = 2)] string ResultMessage
)
{
    [JsonPropertyOrder(4)]
    [JsonProperty(Order = 4)]
    public DateTime Timestamp { get; init; } = DateTime.Now;
}

public record ResponseResult<T>(
    int ResultCode,
    string ResultType,
    string ResultMessage,
    [property: JsonPropertyOrder(3)] [property: JsonProperty(Order = 3)] T Data
) : ResponseResult(ResultCode, ResultType, ResultMessage)
{
    [JsonPropertyOrder(4)]
    [JsonProperty(Order = 4)]
    public new DateTime Timestamp { get; init; } = DateTime.Now;
}