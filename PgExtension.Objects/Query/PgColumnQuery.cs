using System.Runtime.CompilerServices;

namespace PgExtension.Objects.Query;

internal class PgColumnQuery
{
    private static readonly string SQL = @"SELECT
 c.oid AS table_oid
,nc.nspname AS table_schema
,c.relname AS table_name
,a.attname AS column_name
,a.attnum AS ordinal_position
,CASE WHEN a.attgenerated = '' THEN pg_get_expr(ad.adbin, ad.adrelid) ELSE NULL END AS column_default
,a.attnotnull OR (t.typtype = 'd' AND t.typnotnull) AS is_not_null
,COALESCE(type_map.dst, dt.raw_data_type) AS data_type
,information_schema._pg_char_max_length(information_schema._pg_truetypid(a.*, t.*), information_schema._pg_truetypmod(a.*, t.*)) AS character_maximum_length
,information_schema._pg_numeric_precision(information_schema._pg_truetypid(a.*, t.*), information_schema._pg_truetypmod(a.*, t.*)) AS numeric_precision
,information_schema._pg_numeric_scale(information_schema._pg_truetypid(a.*, t.*), information_schema._pg_truetypmod(a.*, t.*)) AS numeric_scale
,information_schema._pg_datetime_precision(information_schema._pg_truetypid(a.*, t.*), information_schema._pg_truetypmod(a.*, t.*)) AS datetime_precision
,(c.relkind IN ('r', 'p') OR (c.relkind IN ('v', 'f') AND pg_column_is_updatable(c.oid::regclass, a.attnum, false))) AS is_updatable
FROM
 pg_attribute a
LEFT OUTER JOIN pg_attrdef ad ON (a.attrelid = ad.adrelid AND a.attnum = ad.adnum)
INNER JOIN pg_class c ON (a.attrelid = c.oid)
INNER JOIN pg_namespace nc ON (c.relnamespace = nc.oid)
INNER JOIN pg_type t ON (a.atttypid = t.oid)
INNER JOIN pg_namespace nt ON (t.typnamespace = nt.oid)
LEFT OUTER JOIN pg_type bt ON (t.typtype = 'd' AND t.typbasetype = bt.oid)
LEFT OUTER JOIN pg_namespace nbt ON (bt.typnamespace = nbt.oid)
LEFT OUTER JOIN pg_collation co ON (a.attcollation = co.oid)
LEFT OUTER JOIN pg_namespace nco ON (co.collnamespace = nco.oid AND (nco.nspname <> 'pg_catalog' OR co.collname <> 'default'))
LEFT OUTER JOIN pg_depend dep ON (dep.refclassid = 'pg_class'::regclass::oid AND dep.refobjid = c.oid AND dep.refobjsubid = a.attnum AND dep.classid      = 'pg_class'::regclass::oid AND dep.deptype = 'i')
LEFT OUTER JOIN pg_sequence seq ON (dep.objid = seq.seqrelid)
CROSS JOIN LATERAL (
SELECT
CASE WHEN t.typtype = 'd' THEN
    CASE
        WHEN bt.typelem <> 0 AND bt.typlen = -1 THEN 'ARRAY'
        WHEN nbt.nspname = 'pg_catalog' THEN format_type(t.typbasetype, NULL)
        ELSE 'USER-DEFINED'
    END
ELSE
    CASE
        WHEN t.typelem <> 0 AND t.typlen = -1 THEN 'ARRAY'
        WHEN nt.nspname = 'pg_catalog' THEN format_type(a.atttypid, NULL)
        ELSE 'USER-DEFINED'
    END
END AS raw_data_type
) dt
LEFT OUTER JOIN (
    VALUES
     ('character varying', 'varchar')
    ,('character', 'char')
    ,('double precision', 'float8')
--    ,('real', 'float4')
--    ,('timestamp without time zone', 'timestamp')
--    ,('timestamp with time zone', 'timestamptz')
--    ,('time without time zone', 'time')
--    ,('time with time zone', 'timetz')
) AS type_map(src, dst)
ON (type_map.src = dt.raw_data_type)
WHERE
a.attnum > 0
AND NOT a.attisdropped
AND c.relkind IN ('r', 'v', 'f', 'p', 't', 'm')
AND c.oid = @table_oid
ORDER BY
 a.attnum";
    internal static async IAsyncEnumerable<PgColumn> ListColumnsAsync(PgCatalog catalog, int tableOid, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var p = new Dictionary<string, object?>()
        {
            {
               "table_oid", tableOid
            }
        };
        using var q = catalog.CreateQuery();
        await foreach (var result in q.SelectAsync<PgColumn>(SQL, p, ct))
        {
            yield return result;
        }
    }
}
