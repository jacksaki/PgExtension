using PgExtension.Query;
using System.Text.Json;

namespace PgExtension.Objects;

[DbClass(nameof(RefreshColumns))]
public class PgIndex
{
    internal void RefreshColumns()
    {
        if (!string.IsNullOrEmpty(_columns))
        {
            var columns = JsonSerializer.Deserialize<PgIndexColumn[]>(_columns);
            if (columns != null)
            {
                this.Columns = columns.ToList().AsReadOnly(); ;
            }
            else
            {
                this.Columns = Array.Empty<PgIndexColumn>().AsReadOnly();
            }
        }
        else
        {
            this.Columns = Array.Empty<PgIndexColumn>().AsReadOnly();
        }
    }
    [DbColumn("index_oid")]
    public int Oid { get; private set; }
    [DbColumn("table_oid")]
    public int TableOid { get; private set; }
    [DbColumn("table_schema")]
    public string TableSchema { get; private set; } = string.Empty;
    [DbColumn("table_name")]
    public string TableName { get; private set; } = string.Empty;
    [DbColumn("index_schema")]
    public string SchemaName { get; private set; } = string.Empty;
    [DbColumn("index_name")]
    public string Name { get; private set; } = string.Empty;
    [DbColumn("is_primary_key")]
    public bool IsPrimaryKey { get; private set; }
    [DbColumn("is_unique")]
    public bool IsUnique { get; private set; }
    [DbColumn("columns")]
    private string? _columns = null;
    public IReadOnlyList<PgIndexColumn> Columns { get; private set; } = Array.Empty<PgIndexColumn>().AsReadOnly();
}
