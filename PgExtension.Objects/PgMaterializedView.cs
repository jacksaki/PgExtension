using PgExtension.Query;

namespace PgExtension.Objects;

public class PgMaterializedView
{
    [DbColumn("oid")]
    public int Oid { get; private set; }
    [DbColumn("mview_schema")]
    public string SchemaName { get; private set; } = string.Empty;
    [DbColumn("mview_name")]
    public string Name { get; private set; } = string.Empty;
    [DbColumn("view_definition")]
    public string? ViewDefinition { get; private set; }
    [DbColumn("is_insertable_into")]
    public bool CanInsert { get; private set; }
}
