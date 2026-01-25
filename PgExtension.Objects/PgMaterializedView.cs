using PgExtension.Objects.Query;
using PgExtension.Query;

namespace PgExtension.Objects;

public class PgMaterializedView : PgRelationBase, IPgObject
{
    public static SQLSet GetSQLSet() => PgMaterializedViewQuery.GenerateSQLSet();

    public override async Task<string> GenerateDDLAsync(DDLOptions options)
    {
        var columns = await this.ListColumnsAsync().ToTask();
        var sb = new System.Text.StringBuilder();
        sb.Append("CREATE OR REPLACE MATERIALIZED VIEW ");
        if (options.AddSchema)
        {
            sb.Append($"{this.SchemaName}.");
        }
        sb.AppendLine($"{this.Name} (");
        sb.AppendLine(string.Join(",\n", columns.OrderBy(x => x.OrdinalPosition).Select(x => x.GenerateColumnDDL())).Trim());
        sb.AppendLine(") AS");
        sb.AppendLine($"{this.ViewDefinition};");

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
        return sb.ToString();
    }

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
