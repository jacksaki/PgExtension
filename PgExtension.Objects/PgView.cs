using PgExtension.Objects.Query;
using PgExtension.Query;
using System.Runtime.CompilerServices;

namespace PgExtension.Objects;

public class PgView : PgRelationBase, IPgObject
{
    public static SQLSet GetSQLSet() => PgViewQuery.GenerateSQLSet();
    internal PgView(PgCatalog catalog):base(catalog)
    {
    }

    [DbColumn("oid")]
    public uint Oid
    {
        get => base._oid; 
        private set => base._oid = value;
    }

    [DbColumn("view_schema")]
    public string SchemaName { get; private set; } = string.Empty;
    [DbColumn("view_name")]
    public string Name { get; private set; } = string.Empty;
    [DbColumn("view_definition")]
    public string? ViewDefinition { get; private set; } = string.Empty;
    [DbColumn("is_insertable_into")]
    public bool CanInsert { get; private set; }
}
