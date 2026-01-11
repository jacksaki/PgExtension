namespace PgExtension.Objects;

public sealed class PgDatabase
{
    private readonly PgCatalog _catalog;

    public PgDatabase(string connectionString)
    {
        _catalog = new PgCatalog(connectionString);
    }

    public IAsyncEnumerable<PgSchema> ListSchemaAsync(CancellationToken ct = default)
    {
        return _catalog.ListSchemasAsync(ct);
    }
}
