using Npgsql;
using PgExtension.Query;
using System.Runtime.CompilerServices;

namespace PgExtension.Objects.Query;

internal class PgSchemaQuery
{
    internal static SQLSet GenerateSQLSet()
        => new SQLSet(SQL, null);
    internal static async IAsyncEnumerable<PgSchema> ListAsync(PgCatalog catalog, [EnumeratorCancellation] CancellationToken ct)
    {
        using var q = catalog.CreateQuery();
        await foreach (var schema in q.SelectAsync<PgSchema, PgCatalog>(catalog, SQL, (NpgsqlParameter[]?)null, ct))
        {
            yield return schema;
        }
    }

    private static readonly string SQL = @"SELECT
 n.nspname AS schema_name
FROM
 pg_namespace n
--INNER JOIN pg_authid u ON(n.nspowner = u.oid)
--WHERE
--(pg_has_role(n.nspowner, 'USAGE'::text)
--OR has_schema_privilege(n.oid, 'CREATE, USAGE'::text))
ORDER BY
 n.nspname";
}
