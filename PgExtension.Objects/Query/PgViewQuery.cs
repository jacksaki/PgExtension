using System.Runtime.CompilerServices;

namespace PgExtension.Objects.Query;

internal class PgViewQuery
{
    private static readonly string SQL = @"SELECT
 c.oid
,nc.nspname::information_schema.sql_identifier AS view_schema
,c.relname::information_schema.sql_identifier AS view_name
,pg_get_viewdef(c.oid, true) AS view_definition
,(pg_relation_is_updatable(c.oid::regclass, false) & 8) = 8  AS is_insertable_into
FROM
 pg_namespace nc
INNER JOIN pg_class c ON (nc.oid = c.relnamespace)
LEFT OUTER JOIN (pg_type t INNER JOIN pg_namespace nt ON t.typnamespace = nt.oid) ON (c.reloftype = t.oid)
WHERE
c.relkind = 'v'
--AND NOT pg_is_other_temp_schema(nc.oid)
AND nc.nspname = @view_schema
AND (@view_name IS NULL OR c.relname ILIKE @view_name)
ORDER BY
 nc.nspname
,c.relname";

    internal static async IAsyncEnumerable<PgView> ListAsync(PgCatalog catalog, string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        var p = new Dictionary<string, object?>()
        {
            { "view_schema", schemaName },
            { "view_name", nameLike.Like() },
        };
        using var q = catalog.CreateQuery();
        await foreach (var view in q.SelectAsync<PgView, PgCatalog>(catalog, SQL, p, ct))
        {
            yield return view;
        }
    }
}
