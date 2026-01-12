using System.Text.Json.Serialization;

namespace PgExtension.Objects;

internal class PgConstraintColumn
{
    [JsonPropertyName("column_name")]
    [JsonInclude]
    public string ColumnName { get; private set; } = string.Empty;
}
