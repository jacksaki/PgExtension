using System.Text.Json.Serialization;

namespace PgExtension.Objects;

public class PgPartitionTableChild
{
    [JsonPropertyName("oid")]
    [JsonInclude]
    private string _oid = string.Empty;

    [JsonIgnore]
    public uint Oid => _oid.ToUInt32();

    [JsonPropertyName("child_table_name")]
    [JsonInclude]
    public string TableName { get; private set; } = string.Empty;
}
