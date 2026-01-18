using PgExtension.Objects.Query;
using PgExtension.Query;

namespace PgExtension.Objects;

public class PgView : PgRelationBase, IPgObject
{
    public static SQLSet GetSQLSet() => PgViewQuery.GenerateSQLSet();
    internal PgView(PgCatalog catalog) : base(catalog)
    {
    }
    public override async Task<string> GenerateDDLAsync(DDLOptions options)
    {
        var columns = await this.ListColumnsAsync().ToTask();
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"CREATE OR REPLACE VIEW {this.SchemaName}.{this.Name} (");
        sb.AppendLine(string.Join(",\n", columns.OrderBy(x => x.OrdinalPosition).Select(x => x.ColumnName)).Trim());
        sb.AppendLine(") AS");
        sb.AppendLine(this.ViewDefinition);
        return sb.ToString();
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
