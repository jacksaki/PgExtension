using Npgsql;
using PgExtension.Query;
using System.Runtime.CompilerServices;

namespace PgExtension.Objects.Query;

internal class PgPartitionTableQuery
{
    internal static SQLSet GenerateSQLSet()
        => new SQLSet(SQL, new NpgsqlParameter[]
        {
            new NpgsqlParameter("table_schema", NpgsqlTypes.NpgsqlDbType.Text),
            new NpgsqlParameter("table_name", NpgsqlTypes.NpgsqlDbType.Text),
        });

    private static readonly string SQL = @"SELECT
 c.oid
,nc.nspname::information_schema.sql_identifier AS table_schema
,c.relname::information_schema.sql_identifier AS table_name
,c.relkind IN ('r', 'p') OR c.relkind IN ('v', 'f') AND (pg_relation_is_updatable(c.oid::regclass, false) & 8) = 8  AS is_insertable_into
,pg_get_partkeydef(c.oid) AS partition_key
,json_agg(
    json_build_object(
         'oid',c_child.oid
        ,'child_table_name',c_child.relname
    )
) AS children
FROM
 pg_namespace nc
INNER JOIN pg_class c ON (nc.oid = c.relnamespace)
LEFT OUTER JOIN (pg_type t INNER JOIN pg_namespace nt ON t.typnamespace = nt.oid) ON (c.reloftype = t.oid)
LEFT OUTER JOIN pg_inherits i ON(c.oid = i.inhparent)
LEFT OUTER JOIN pg_class c_child ON (c_child.oid = i.inhrelid)
WHERE
c.relkind = 'p'
--AND NOT pg_is_other_temp_schema(nc.oid)
AND nc.nspname = @table_schema
AND (@table_name IS NULL OR c.relname LIKE @table_name::text)
GROUP BY
 c.oid
,table_schema
,table_name
,is_insertable_into
,partition_key
ORDER BY
 table_schema
,table_name";
    internal static async IAsyncEnumerable<PgPartitionTable> ListAsync(PgCatalog catalog, string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        var sqlSet = GenerateSQLSet();
        sqlSet["table_schema"]!.Value = schemaName;
        sqlSet["table_name"]!.Value = nameLike.Like(DBNull.Value);

        using var q = catalog.CreateQuery();
        await foreach (var table in q.SelectAsync<PgPartitionTable, PgCatalog>(catalog, sqlSet, ct))
        {
            yield return table;
        }
    }
}
