using PgExtension.Objects.Query;
using PgExtension.Query;
using System.Runtime.CompilerServices;

namespace PgExtension.Objects;

public class PgMaterializedView: PgRelationBase
{
    public static SQLSet GetSQLSet() => PgMaterializedViewQuery.GenerateSQLSet();
    internal PgMaterializedView(PgCatalog catalog) : base(catalog)
    {
    }

    [DbColumn("oid")]
    public uint Oid
    {
        get => base._oid;
        private set => base._oid = value;
    }

    [DbColumn("mview_schema")]
    public string SchemaName { get; private set; } = string.Empty;
    [DbColumn("mview_name")]
    public string Name { get; private set; } = string.Empty;
    [DbColumn("view_definition")]
    public string? ViewDefinition { get; private set; }
    [DbColumn("is_insertable_into")]
    public bool CanInsert { get; private set; }
}
