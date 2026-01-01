namespace PgExtension;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class DbColumnAttribute: Attribute
{
    public string ColumnName { get; }
    public string? DateFormat { get; }
    public DbColumnAttribute(string columnName, string? dateFormat = null)
    {
        this.ColumnName = columnName;
        this.DateFormat = dateFormat;
    }
}
