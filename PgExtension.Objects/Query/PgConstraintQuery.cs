namespace PgExtension.Objects.Query;

internal class PgConstraintQuery
{
    private static readonly string SQL = @"SELECT
 cls.oid AS table_oid
,ns.nspname AS table_schema
,cls.relname AS table_name
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
INNER JOIN pg_class cls ON (cls.oid = con.conrelid)
INNER JOIN pg_namespace ns ON (ns.oid = cls.relnamespace)
LEFT OUTER JOIN LATERAL unnest(con.conkey) WITH ORDINALITY u(attnum, ordinality) ON (con.conkey IS NOT NULL)
LEFT OUTER JOIN pg_attribute att ON (att.attrelid = cls.oid AND att.attnum = u.attnum)
LEFT OUTER JOIN pg_class fcls ON (fcls.oid = con.confrelid)
LEFT OUTER JOIN pg_namespace fns ON (fns.oid = fcls.relnamespace)
WHERE
cls.relkind IN ('r', 'p') -- table / partition
AND (@table_oid IS NULL OR cls.oid = @table_oid)
GROUP BY
 cls.oid
,ns.nspname
,cls.relname
,con.conname
,con.contype
,con.oid
,fns.nspname
,fcls.relname
ORDER BY
 table_schema
,table_name
,constraint_name";
}
