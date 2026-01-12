using PgExtension.Query;

namespace PgExtension.Objects;

public class PgForeignTable : IPgObject
{
    [DbColumn("oid")]
    public int Oid { get; private set; }
    [DbColumn("table_schema")]
    public string SchemaName { get; private set; } = string.Empty;
    [DbColumn("table_name")]
    public string Name { get; private set; } = string.Empty;
    [DbColumn("is_insertable_into")]
    public bool CanInsert { get; private set; }
    [DbColumn("server_name")]
    public string? ServerName { get; private set; }
    [DbColumn("options")]
    public string? Options { get; private set; }
}
