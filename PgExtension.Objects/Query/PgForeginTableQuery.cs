using System.Runtime.CompilerServices;

namespace PgExtension.Objects.Query;

internal class PgForeginTableQuery
{
    private static readonly string SQL = @"SELECT
 c.oid
,nc.nspname::information_schema.sql_identifier AS table_schema
,c.relname::information_schema.sql_identifier AS table_name
,(pg_relation_is_updatable(c.oid::regclass, false) & 8) = 8  AS is_insertable_into
,s.srvname AS server_name
,ft.ftoptions
FROM
 pg_namespace nc
INNER JOIN pg_class c ON (nc.oid = c.relnamespace)
INNER JOIN pg_foreign_table ft ON(c.oid = ft.ftrelid)
INNER JOIN pg_foreign_server s ON (s.oid = ft.ftserver)
LEFT OUTER JOIN (pg_type t INNER JOIN pg_namespace nt ON t.typnamespace = nt.oid) ON (c.reloftype = t.oid)
WHERE
c.relkind = 'f'
--AND NOT pg_is_other_temp_schema(nc.oid)
AND nc.nspname = @table_schema
AND (@table_name IS NULL OR c.relname ILIKE @table_name)
ORDER BY
 nc.nspname
,c.relname";

    internal static async IAsyncEnumerable<PgForeginTable> ListAsync(PgCatalog catalog, string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        var p = new Dictionary<string, object?>()
        {
            { "table_schema", schemaName },
            { "table_name", nameLike.Like() },
        };
        using var q = catalog.CreateQuery();
        await foreach (var table in q.SelectAsync<PgForeginTable, PgCatalog>(catalog, SQL, p, ct))
        {
            yield return table;
        }
    }
}
