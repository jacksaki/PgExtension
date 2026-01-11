using System.Runtime.CompilerServices;

namespace PgExtension.Objects.Query;

internal class PgConstraintQuery
{
    private static readonly string SQL = @"SELECT
 cls.oid AS table_oid
,ns.nspname AS table_schema
,cls.relname AS table_name
,cns.nspname AS constraint_schema
,con.conname AS constraint_name
,CASE con.contype
    WHEN 'p' THEN 'PRIMARY KEY'
    WHEN 'u' THEN 'UNIQUE'
    WHEN 'f' THEN 'FOREIGN KEY'
    WHEN 'c' THEN 'CHECK'
    WHEN 'x' THEN 'EXCLUDE' END AS constraint_type
,json_agg(
    json_build_object(
      'column_name',att.attname
    )
    ORDER BY u.ordinality) AS columns
,pg_get_constraintdef(con.oid, true) AS constraint_definition
,fns.nspname AS foreign_table_schema
,fcls.relname AS foreign_table_name
FROM
 pg_constraint con
INNER JOIN pg_namespace cns ON (cns.oid = con.connamespace)
INNER JOIN pg_class cls ON (cls.oid = con.conrelid)
INNER JOIN pg_namespace ns ON (ns.oid = cls.relnamespace)
LEFT OUTER JOIN LATERAL unnest(con.conkey) WITH ORDINALITY u(attnum, ordinality) ON (con.conkey IS NOT NULL)
LEFT OUTER JOIN pg_attribute att ON (att.attrelid = cls.oid AND att.attnum = u.attnum)
LEFT OUTER JOIN pg_class fcls ON (fcls.oid = con.confrelid)
LEFT OUTER JOIN pg_namespace fns ON (fns.oid = fcls.relnamespace)
WHERE
cls.relkind IN ('r', 'p') -- table / partition
AND (@table_oid IS NULL OR cls.oid = @table_oid)
AND (@schema_name IS NULL OR cns.nspname = @schema_name)
AND (@constraint_name IS NULL OR con.conname = @constraint_name)
GROUP BY
 cls.oid
,ns.nspname
,cls.relname
,cns.nspname
,con.conname
,con.contype
,con.oid
,fns.nspname
,fcls.relname
ORDER BY
 ns.nspname
,cls.relname
,con.conname";
    internal static async IAsyncEnumerable<PgConstraint> ListAsync(PgCatalog catalog, int tableOid, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var p = new Dictionary<string, object?>()
        {
            { "table_oid", tableOid },
            { "schema_name", null },
            { "constraint_name", null }
        };
        using var q = catalog.CreateQuery();
        await foreach (var con in q.SelectAsync<PgConstraint, PgCatalog>(catalog, SQL, p, ct))
        {
            yield return con;
        }
    }
    internal static async IAsyncEnumerable<PgConstraint> ListAsync(PgCatalog catalog, string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var p = new Dictionary<string, object?>()
        {
            { "table_oid", null },
            { "schema_name", schemaName },
            { "constraint_name", nameLike.Like() }
        };
        using var q = catalog.CreateQuery();
        await foreach (var con in q.SelectAsync<PgConstraint, PgCatalog>(catalog, SQL, p, ct))
        {
            yield return con;
        }
    }
}
