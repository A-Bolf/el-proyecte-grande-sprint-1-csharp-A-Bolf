using System.Text.Json.Serialization;

 [JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketType
{
    TASK,
    BUG,
    REQUEST,
    OTHER,
}