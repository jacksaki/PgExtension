using PgExtension.Objects.Query;
using PgExtension.Query;
using System.Text.Json;

namespace PgExtension.Objects;

[DbClass(nameof(RefreshItems))]
public class PgPartitionTable : PgRelationBase,IPgObject
{
    public static SQLSet GetSQLSet() => PgPartitionTableQuery.GenerateSQLSet();
    internal PgPartitionTable(PgCatalog catalog) : base(catalog)
    {
    }

    [DbColumn("oid")]
    public uint Oid
    {
        get => base._oid;
        private set => base._oid = value;
    }

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

    public override async Task<string> GenerateDDLAsync(DDLOptions options)
    {
        var sequences = await this.ListSequencesAsync().ToTask();
        var seqColumns = sequences.Select(x => x.OwnedColumn).Where(x => x != null).ToArray() ?? Array.Empty<string?>();
        var columns = await this.ListColumnsAsync().ToTask();
        var sb = new System.Text.StringBuilder();
        sb.Append("CREATE TABLE ");
        if (options.AddSchema)
        {
            sb.Append($"{this.SchemaName}.");
        }
        sb.AppendLine($"{this.Name} (");
        sb.AppendLine(string.Join(",\n", columns.OrderBy(x => x.OrdinalPosition).Select(x => x.GenerateColumnDDL(seqColumns))).Trim());
        sb.AppendLine(")");
        sb.AppendLine($"PARTITION BY {this.PartitionKey};");


        if (options.AddConstraints)
        {
            await foreach (var constraint in this.ListConstraintsAsync())
            {
                sb.AppendLine(await constraint.GenerateDDLAsync(options));
            }
        }
        if (options.AddIndexes)
        {
            await foreach (var index in this.ListIndexesAsync())
            {
                if (!options.AddConstraints || (!index.IsPrimaryKey && !index.IsUnique))
                {
                    sb.AppendLine(await index.GenerateDDLAsync(options));
                }
            }
        }
        foreach (var child in this.Children)
        {
            sb.Append($"CREATE TABLE ");
            if (options.AddSchema)
            {
                sb.Append($"{child.TableSchema}.");
            }
            sb.Append($"{child.TableName} PARTITION OF ");
            if (options.AddSchema)
            {
                sb.Append($"{this.SchemaName}.");
            }
            sb.AppendLine($"{this.Name}");
            sb.AppendLine($"{child.PartitionBound};");
        }
        return sb.ToString();

    }
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
