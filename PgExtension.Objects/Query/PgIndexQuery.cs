using System.Runtime.CompilerServices;

namespace PgExtension.Objects.Query;

internal class PgIndexQuery
{
    private static readonly string SQL = @"SELECT
 i.oid AS index_oid
,t.oid AS table_oid
,nt.nspname AS table_schema
,t.relname AS table_name
,ni.nspname AS index_schema
,i.relname AS index_name
,ix.indisprimary AS is_primary_key
,ix.indisunique  AS is_unique
,json_agg(
    json_build_object(
         'column_name',regexp_replace(pg_get_indexdef(i.oid, k + 1, true), '\s+(ASC|DESC)\s*$', '', 'i')
        ,'order', COALESCE((regexp_match(pg_get_indexdef(i.oid, k + 1, true), '\s+(ASC|DESC)\s*$', 'i'))[1], 'ASC')
    )
    ORDER BY k
) AS columns
FROM
 pg_class i
INNER JOIN pg_index ix ON (ix.indexrelid = i.oid)
INNER JOIN pg_class t ON (t.oid = ix.indrelid)
INNER JOIN pg_namespace nt ON (nt.oid = t.relnamespace)
INNER JOIN pg_namespace ni ON (ni.oid = t.relnamespace)
INNER JOIN generate_subscripts(ix.indkey, 1) AS k ON true
WHERE
i.relkind = 'i'
AND (@table_oid IS NULL OR t.oid = @table_oid)
AND (@schema_name IS NULL OR ni.nspname = @schema_name)
AND (@index_name IS NULL OR i.relname ILIKE @index_name)
GROUP BY
 i.oid
,t.oid
,nt.nspname
,ni.nspname
,ix.indisprimary
,ix.indisunique
,i.relname
ORDER BY
 nt.nspname
,ni.nspname
,t.relname
,i.relname";
    internal static async IAsyncEnumerable<PgIndex> ListAsync(PgCatalog catalog, int tableOid, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var p = new Dictionary<string, object?>()
        {
            { "table_oid", tableOid },
            { "schema_name", null },
            { "index_name", null },
        };
        using var q = catalog.CreateQuery();
        await foreach (var ind in q.SelectAsync<PgIndex, PgCatalog>(catalog, SQL, p, ct))
        {
            yield return ind;
        }
    }
    internal static async IAsyncEnumerable<PgIndex> ListAsync(PgCatalog catalog, string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var p = new Dictionary<string, object?>()
        {
            { "table_oid", null },
            { "schema_name", schemaName },
            { "index_name", nameLike.Like() },
        };
        using var q = catalog.CreateQuery();
        await foreach (var ind in q.SelectAsync<PgIndex, PgCatalog>(catalog, SQL, p, ct))
        {
            yield return ind;
        }
    }
}
