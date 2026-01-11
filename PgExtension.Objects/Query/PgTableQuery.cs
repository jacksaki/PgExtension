using System.Runtime.CompilerServices;

namespace PgExtension.Objects.Query;

internal class PgTableQuery 
{
    private static readonly string SQL = @"SELECT
 c.oid
,nc.nspname::information_schema.sql_identifier AS table_schema
,c.relname::information_schema.sql_identifier AS table_name
,CASE WHEN nc.oid = pg_my_temp_schema() THEN 'LOCAL TEMPORARY'
      WHEN c.relkind = 'r' THEN 'BASE TABLE'
      WHEN c.relkind = 'p' THEN 'PARTITION TABLE'
      WHEN c.relkind = 'v' THEN 'VIEW'
      WHEN c.relkind = 'f' THEN 'FOREIGN'
      WHEN c.relkind = 't' THEN 'TOAST TABLE'
      WHEN c.relkind = 'm' THEN 'MATERIALIZED VIEW'
      ELSE NULL END::information_schema.character_data AS table_type
,c.relkind IN ('r', 'p') OR c.relkind IN ('v', 'f') AND (pg_relation_is_updatable(c.oid::regclass, false) & 8) = 8  AS is_insertable_into
FROM
 pg_namespace nc
INNER JOIN pg_class c ON (nc.oid = c.relnamespace)
LEFT OUTER JOIN (pg_type t INNER JOIN pg_namespace nt ON t.typnamespace = nt.oid) ON (c.reloftype = t.oid)
WHERE
c.relkind = ANY(@relkinds)
--AND NOT pg_is_other_temp_schema(nc.oid)
AND nc.nspname = @table_schema
AND c.relname = @table_name";
    internal static async IAsyncEnumerable<PgTable> ListAsync(PgCatalog catalog, string schemaName, string nameLike, TableTypes t, [EnumeratorCancellation] CancellationToken ct)
    {
        var p = new Dictionary<string, object?>()
        {
            { "relkinds", t.GetArray() },
            { "table_schema", schemaName },
            { "table_name", nameLike},
        };
        using var q = catalog.CreateQuery();
        await foreach(var table in q.SelectAsync<PgTable, PgCatalog>(catalog, SQL, p, ct))
        {
            yield return table;
        }
    }
}
