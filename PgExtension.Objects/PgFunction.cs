using PgExtension.Query;

namespace PgExtension.Objects;

public class PgFunction : IPgObject
{
    [DbColumn("routine_schema")]
    public string SchemaName { get; private set; } = string.Empty;
    [DbColumn("routine_name")]
    public string Name { get; private set; } = string.Empty;
    [DbColumn("data_type")]
    public string? DataType { get; private set; }
    [DbColumn("routine_body")]
    public string? Body { get; private set; }
    [DbColumn("routine_definition")]
    public string? Definition { get; private set; }
    [DbColumn("external_name")]
    public string? ExternalName { get; private set; }
    [DbColumn("external_language")]
    public string? ExternalLanguage { get; private set; }
    [DbColumn("is_deterministic")]
    public bool IsDeterministic { get; private set; }
}
