namespace PgExtension.Objects;

internal interface IPgObject
{
    public string SchemaName { get; }
    public string Name { get; }
}
