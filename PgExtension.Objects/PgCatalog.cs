using PgExtension.Objects.Query;
using System.Runtime.CompilerServices;

namespace PgExtension.Objects;

internal sealed class PgCatalog
{
    private readonly string _connectionString;

    internal PgCatalog(string connectionString)
    {
        _connectionString = connectionString;
    }
    internal PgQuery CreateQuery()
    {
        return new PgQuery(_connectionString);
    }

    internal async IAsyncEnumerable<PgSchema> ListSchemasAsync(
        [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var schema in PgSchemaQuery.ListAsync(this, ct))
        {
            yield return schema;
        }
    }

    internal async IAsyncEnumerable<PgTable> ListTablesAsync(string schemaName, string nameLike, TableTypes t, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var table in PgTableQuery.ListAsync(this, schemaName, nameLike, t, ct))
        {
            yield return table;
        }
    }
}