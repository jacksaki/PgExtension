using PgExtension.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PgExtension.Objects.Query;

internal class PgSchemaQuery
{
    internal static async IAsyncEnumerable<PgSchema> ListAsync(PgCatalog catalog, [EnumeratorCancellation] CancellationToken ct)
    {
        using var q = catalog.CreateQuery();
        await foreach (var schema in q.SelectAsync<PgSchema, PgCatalog>(catalog, SQL, null, ct))
        {
            yield return schema;
        }
    }

    private static readonly string SQL = @"SELECT
 n.nspname AS schema_name
,u.rolname AS schema_owner
FROM
 pg_namespace n
INNER JOIN pg_authid u ON(n.nspowner = u.oid)
--WHERE
--(pg_has_role(n.nspowner, 'USAGE'::text)
--OR has_schema_privilege(n.oid, 'CREATE, USAGE'::text))
ORDER BY
 n.nspname";
}
