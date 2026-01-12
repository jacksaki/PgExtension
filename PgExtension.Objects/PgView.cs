using PgExtension.Query;

namespace PgExtension.Objects;

public class PgView : IPgObject
{
    [DbColumn("oid")]
    public int Oid { get; private set; }
    [DbColumn("view_schema")]
    public string SchemaName { get; private set; } = string.Empty;
    [DbColumn("view_name")]
    public string Name { get; private set; } = string.Empty;
    [DbColumn("view_definition")]
    public string ViewDefinition { get; private set; } = string.Empty;
    [DbColumn("is_insertable_into")]
    public bool CanInsert { get; private set; }
}
