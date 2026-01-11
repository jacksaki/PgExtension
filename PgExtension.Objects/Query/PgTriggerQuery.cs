using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgExtension.Objects.Query;

internal class PgTriggerQuery
{
    private static readonly string SQL = @"SELECT
 n.nspname AS trigger_schema
,t.tgname AS trigger_name
,em.text AS event_manipulation
,n.nspname AS event_object_schema
,c.relname AS event_object_table
,rank() OVER (PARTITION BY n.nspname, c.relname, em.num, (t.tgtype::integer & 1), (t.tgtype::integer & 66) ORDER BY t.tgname) AS action_order
,CASE WHEN pg_has_role(c.relowner, 'USAGE') THEN (regexp_match(pg_get_triggerdef(t.oid), '.{35,} WHEN \((.+)\) EXECUTE FUNCTION'))[1] ELSE NULL END AS action_condition
,SUBSTRING(pg_get_triggerdef(t.oid) FROM POSITION(('EXECUTE FUNCTION') IN (SUBSTRING(pg_get_triggerdef(t.oid) FROM 48))) + 47) AS action_statement
,CASE t.tgtype & 1 WHEN 1 THEN 'ROW' ELSE 'STATEMENT' END AS action_orientation
,CASE t.tgtype & 66 WHEN 2 THEN 'BEFORE' WHEN 64 THEN 'INSTEAD OF' ELSE 'AFTER' END AS action_timing
,t.tgoldtable AS action_reference_old_table
,t.tgnewtable AS action_reference_new_table
FROM
 pg_namespace n
INNER JOIN pg_class c ON (n.oid = c.relnamespace)
INNER JOIN pg_trigger t ON (c.oid = t.tgrelid)
,( VALUES (4,'INSERT'), (8,'DELETE'), (16,'UPDATE')) em(num, text)
WHERE
(t.tgtype::integer & em.num) <> 0
AND NOT t.tgisinternal
AND NOT pg_is_other_temp_schema(n.oid)
--AND (
--	pg_has_role(c.relowner, 'USAGE')
--	OR has_table_privilege(c.oid, 'INSERT, UPDATE, DELETE, TRUNCATE, REFERENCES, TRIGGER')
--	OR has_any_column_privilege(c.oid, 'INSERT, UPDATE, REFERENCES')
--)";
}
