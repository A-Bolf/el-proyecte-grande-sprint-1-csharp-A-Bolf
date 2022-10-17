using System.Text.Json.Serialization;

 [JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatusType
{
    OPEN,
    IN_PROGRESS,
    RESOLVED,
    CLOSED,
}