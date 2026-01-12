using PgExtension.Objects.Query;
using PgExtension.Query;

namespace PgExtension.Objects;

public class PgSequence : IPgObject
{
    public static SQLSet GetSQLSet() => PgSequenceQuery.GenerateSQLSet();
    internal PgSequence(PgCatalog catalog)
    {
        _catalog = catalog;
    }
    private PgCatalog _catalog;
    [DbColumn("table_oid")]
    public uint TableOid { get; private set; }
    [DbColumn("sequence_schema")]
    public string SchemaName { get; private set; } = string.Empty;
    [DbColumn("sequence_name")]
    public string Name { get; private set; } = string.Empty;
    [DbColumn("data_type")]
    public string DataType { get; private set; } = string.Empty;
    [DbColumn("start_value")]
    public long StartValue { get; private set; }
    [DbColumn("increment_by")]
    public long IncrementBy { get; private set; }
    [DbColumn("min_value")]
    public long MinValue { get; private set; }
    [DbColumn("max_value")]
    public long MaxValue { get; private set; }
    [DbColumn("cache_size")]
    public long CacheSize { get; private set; }
    [DbColumn("is_cycled")]
    public bool IsCycled { get; private set; }

    [DbColumn("owned_table_schema")]
    public string? OwnedTableSchema { get; private set; }
    [DbColumn("owned_table_name")]
    public string? OwnedTableName { get; private set; }

    [DbColumn("owned_column")]
    public string? OwnedColumn { get; private set; }

}
