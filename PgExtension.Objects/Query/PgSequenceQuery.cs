using System.Runtime.CompilerServices;

namespace PgExtension.Objects.Query;

internal class PgSequenceQuery
{
    private static readonly string SQL = @"SELECT
 tbl.oid AS table_oid
,ns.nspname AS sequence_schema
,seq.relname AS sequence_name
,typ.typname AS data_type
,s.seqstart AS start_value
,s.seqincrement AS increment_by
,s.seqmin AS min_value
,s.seqmax AS max_value
,s.seqcache AS cache_size
,s.seqcycle AS is_cycled
,tbl_ns.nspname AS owned_table_schema
,tbl.relname AS owned_table_name
,col.attname AS owned_column
FROM
 pg_class seq
INNER JOIN pg_namespace ns ON (ns.oid = seq.relnamespace)
INNER JOIN pg_sequence s ON (s.seqrelid = seq.oid)
INNER JOIN pg_type typ ON (typ.oid = s.seqtypid)
LEFT OUTER JOIN pg_depend dep ON (dep.objid = seq.oid AND dep.deptype = 'a')
LEFT OUTER JOIN pg_class tbl ON (tbl.oid = dep.refobjid)
LEFT OUTER JOIN pg_namespace tbl_ns ON (tbl_ns.oid = tbl.relnamespace)
LEFT OUTER JOIN pg_attribute col ON (col.attrelid = tbl.oid AND col.attnum = dep.refobjsubid)
WHERE
seq.relkind = 'S'
AND (@table_oid IS NULL OR tbl.oid = @table_oid)
AND (@schema_name IS NULL OR ns.nspname = @schema_name)
AND (@sequence_name IS NULL OR seq.relname ILIKE @sequence_name)
ORDER BY
 sequence_schema
,sequence_name";

    internal static async IAsyncEnumerable<PgSequence> ListAsync(PgCatalog catalog, int tableOid, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var p = new Dictionary<string, object?>()
        {
            { "table_oid", tableOid },
            { "schema_name", null },
            { "sequence_name", null },
        };
        using var q = catalog.CreateQuery();
        await foreach (var seq in q.SelectAsync<PgSequence, PgCatalog>(catalog, SQL, p, ct))
        {
            yield return seq;
        }
    }
    internal static async IAsyncEnumerable<PgSequence> ListAsync(PgCatalog catalog, string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var p = new Dictionary<string, object?>()
        {
            { "table_oid", null },
            { "schema_name", schemaName },
            { "sequence_name", nameLike.Like() }
        };
        using var q = catalog.CreateQuery();
        await foreach (var seq in q.SelectAsync<PgSequence, PgCatalog>(catalog, SQL, p, ct))
        {
            yield return seq;
        }
    }
}
