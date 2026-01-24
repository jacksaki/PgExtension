namespace PgExtension.Objects;

public interface IPgObject
{
    public string SchemaName { get; }
    public string Name { get; }
    public Task<string> GenerateDDLAsync(DDLOptions options);
}
