using PgExtension.Objects.Query;
using PgExtension.Query;

namespace PgExtension.Objects;

public class PgForeignTable : PgRelationBase, IPgObject
{
    public static SQLSet GetSQLSet() => PgForeignTableQuery.GenerateSQLSet();

    internal PgForeignTable(PgCatalog catalog) : base(catalog)
    {
    }

    public override async Task<string> GenerateDDLAsync(DDLOptions options)
    {
        var columns = await this.ListColumnsAsync().ToTask();
        var sb = new System.Text.StringBuilder();
        sb.Append("CREATE TABLE ");
        if (options.AddSchema)
        {
            sb.Append($"{this.SchemaName}.");
        }
        sb.AppendLine($"{this.Name} (");
        sb.AppendLine(string.Join(",\n", columns.OrderBy(x => x.OrdinalPosition).Select(x => x.GenerateColumnDDL())).Trim());
        sb.AppendLine(")");
        sb.AppendLine($"SERVER {this.ServerName};");
        if (options.AddConstraints)
        {
            await foreach (var constraint in this.ListConstraintsAsync())
            {
                sb.AppendLine(constraint.GenerateDDL(options.AddSchema));
            }
        }
        if (options.AddIndexes)
        {
            await foreach (var index in this.ListIndexesAsync())
            {
                if (!options.AddConstraints || (!index.IsPrimaryKey && !index.IsUnique))
                {
                    sb.AppendLine(index.GenerateDDL(options.AddSchema));
                }
            }
        }
        return sb.ToString();
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
    [DbColumn("server_name")]
    public string? ServerName { get; private set; }
    [DbColumn("options")]
    public string? Options { get; private set; }
}
