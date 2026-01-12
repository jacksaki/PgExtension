using PgExtension.Query;
using System.Text.Json;

namespace PgExtension.Objects;

[DbClass(nameof(RefreshItems))]
public class PgPartitionTable
{
    private void RefreshItems()
    {
        if (!string.IsNullOrEmpty(_children))
        {
            var children = JsonSerializer.Deserialize<PgPartitionTableChild[]>(_children);
            if (children != null)
            {
                this.Children = children.ToList().AsReadOnly();
            }
            else
            {
                this.Children = Array.Empty<PgPartitionTableChild>().AsReadOnly();
            }
        }
        else
        {
            this.Children = Array.Empty<PgPartitionTableChild>().AsReadOnly();
        }
    }

    [DbColumn("oid")]
    public int Oid { get; private set; }
    [DbColumn("table_schema")]
    public string SchemaName { get; private set; } = string.Empty;
    [DbColumn("table_name")]
    public string Name { get; private set; } = string.Empty;
    [DbColumn("is_insertable_into")]
    public bool CanInsert { get; private set; }
    [DbColumn("partition_key")]
    public string? PartitionKey { get; private set; }
    [DbColumn("children")]
    private string? _children = null;
    public IReadOnlyList<PgPartitionTableChild> Children { get; private set; } = Array.Empty<PgPartitionTableChild>().AsReadOnly();
}
