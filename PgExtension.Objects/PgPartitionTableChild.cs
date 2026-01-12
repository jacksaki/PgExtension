using System.Text.Json.Serialization;

namespace PgExtension.Objects;

public class PgPartitionTableChild
{
    [JsonPropertyName("oid")]
    [JsonInclude]
    public uint Oid { get; private set; }

    [JsonPropertyName("child_table_name")]
    [JsonInclude]
    public string TableName { get; private set; } = string.Empty;
}
