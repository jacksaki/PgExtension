using PgExtension.Objects.Query;
using PgExtension.Query;
using System.Runtime.CompilerServices;

namespace PgExtension.Objects;

public sealed class PgTable : PgRelationBase, IPgObject
{
    public static SQLSet GetSQLSet() => PgTableQuery.GenerateSQLSet();

    internal PgTable(PgCatalog catalog) : base(catalog)
    {
    }

    [DbColumn("oid")]
    public uint Oid
    {
        get => base._oid;
        private set => base._oid = value;
    }

    [DbColumn("table_schema")]
    public string SchemaName { get; private set; } = string.Empty;

    [DbColumn("table_name")]
    public string Name { get; private set; } = string.Empty;

    [DbColumn("is_insertable_into")]
    public bool CanInsert { get; private set; }
}
