using PgExtension.Query;

namespace PgExtension.Objects;


public sealed class PgSchema
{
    internal PgSchema(PgCatalog catalog)
    {
        _catalog = catalog;
    }

    private readonly PgCatalog _catalog;

    [DbColumn("schema_name")]
    public string Name { get; private set; } = string.Empty;
    [DbColumn("schema_owner")]
    public string Owner { get; private set; } = string.Empty;

    public IAsyncEnumerable<PgTable> ListTablesAsync(string nameLike, TableTypes t, CancellationToken ct = default)
    {
        return _catalog.ListTablesAsync(this.Name, nameLike, t, ct);
    }
}