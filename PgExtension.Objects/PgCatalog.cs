using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PgExtension.Objects.Query;
using System.Runtime.CompilerServices;

namespace PgExtension.Objects;

internal sealed class PgCatalog
{
    private readonly string _connectionString;

    internal PgCatalog(string connectionString)
    {
        _connectionString = connectionString;
    }
    internal PgQuery CreateQuery()
    {
        return new PgQuery(_connectionString);
    }
    internal async IAsyncEnumerable<PgColumn> ListColumnsAsync(int oid, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var col in PgColumnQuery.ListColumnsAsync(this, oid, ct))
        {
            yield return col;
        }
    }

    internal async IAsyncEnumerable<PgConstraint> ListConstraintsAsync(int tableOid, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var con in PgConstraintQuery.ListAsync(this, tableOid, ct))
        {
            yield return con;
        }
    }
    internal async IAsyncEnumerable<PgConstraint> ListConstraintsAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var con in PgConstraintQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return con;
        }
    }
    internal async IAsyncEnumerable<PgForeginTable> ListForeginTablesAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var ftable in PgForeginTableQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return ftable;
        }
    }

    internal async IAsyncEnumerable<PgFunction> ListFunctionsAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var function in PgFunctionQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return function;
        }
    }

    internal async IAsyncEnumerable<PgIndex> ListIndexesAsync(int tableOid, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var ind in PgIndexQuery.ListAsync(this, tableOid, ct))
        {
            yield return ind;
        }
    }
    internal async IAsyncEnumerable<PgIndex> ListIndexesAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var ind in PgIndexQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return ind;
        }
    }
    internal async IAsyncEnumerable<PgMaterializedView> ListMaterializedViewsAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var mview in PgMaterializedViewQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return mview;
        }
    }
    internal async IAsyncEnumerable<PgPartitionTable> ListPartitionTablesAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var ptable in PgPartitionTableQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return ptable;
        }
    }

    internal async IAsyncEnumerable<PgProcedure> ListProceduresAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var proc in PgProcedureQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return proc;
        }
    }

    internal async IAsyncEnumerable<PgSchema> ListSchemasAsync(
        [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var schema in PgSchemaQuery.ListAsync(this, ct))
        {
            yield return schema;
        }
    }

    internal async IAsyncEnumerable<PgSequence> ListSequencesAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var seq in PgSequenceQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return seq;
        }
    }
    internal async IAsyncEnumerable<PgSequence> ListSequencesAsync(int tableOid, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var seq in PgSequenceQuery.ListAsync(this, tableOid, ct))
        {
            yield return seq;
        }
    }

    internal async IAsyncEnumerable<PgTable> ListTablesAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var table in PgTableQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return table;
        }
    }


    internal async IAsyncEnumerable<PgTrigger> ListTriggersAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var trigger in PgTriggerQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return trigger;
        }
    }
    internal async IAsyncEnumerable<PgTrigger> ListTriggersAsync(int tableOid, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var trigger in PgTriggerQuery.ListAsync(this, tableOid, ct))
        {
            yield return trigger;
        }
    }


    internal async IAsyncEnumerable<PgView> ListViewsAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var view in PgViewQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return view;
        }
    }

}