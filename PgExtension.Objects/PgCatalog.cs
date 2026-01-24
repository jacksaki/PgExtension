using PgExtension.Objects.Query;
using PgExtension.Query;
using System.Runtime.CompilerServices;

namespace PgExtension.Objects;

public sealed class PgCatalog
{
    private readonly string _connectionString;

    public PgCatalog(string connectionString)
    {
        _connectionString = connectionString;
    }

    internal PgQuery CreateQuery()
    {
        return new PgQuery(_connectionString);
    }
    public async IAsyncEnumerable<PgColumn> ListColumnsAsync(uint oid, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var col in PgColumnQuery.ListColumnsAsync(this, oid, ct))
        {
            yield return col;
        }
    }
    public async IAsyncEnumerable<PgConstraint> ListConstraintsAsync(uint tableOid, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var con in PgConstraintQuery.ListAsync(this, tableOid, ct))
        {
            yield return con;
        }
    }

    public async Task<PgConstraint?> GetConstraintAsync(string schemaName, string name, CancellationToken ct = default)
    {
        return await PgConstraintQuery.GetAsync(this, schemaName, name, ct);
    }
    public async Task<IPgObject?> GetAsync(Type t, string schemaName, string name, CancellationToken ct = default)
    {
        if(t == typeof(PgConstraint))
        {
            return await this.GetConstraintAsync(schemaName, name, ct);
        }
        else if (t == typeof(PgForeignTable))
        {
            return await this.GetForeignTableAsync(schemaName, name, ct);
        }
        else if (t == typeof(PgFunction))
        {
            return await this.GetFunctionAsync(schemaName, name, ct);
        }
        else if (t == typeof(PgIndex))
        {
            return await this.GetIndexAsync(schemaName, name, ct);
        }
        else if (t == typeof(PgMaterializedView))
        {
            return await this.GetMaterializedViewAsync(schemaName, name, ct);
        }
        else if (t == typeof(PgPartitionTable))
        {
            return await this.GetPartitionTableAsync(schemaName, name, ct);
        }
        else if (t == typeof(PgProcedure))
        {
            return await this.GetProcedureAsync(schemaName, name, ct);
        }
        else if (t == typeof(PgSequence))
        {
            return await this.GetSequenceAsync(schemaName, name, ct);
        }
        else if (t == typeof(PgTable))
        {
            return await this.GetTableAsync(schemaName, name, ct);
        }
        else if (t == typeof(PgTrigger))
        {
            return await this.GetTriggerAsync(schemaName, name, ct);
        }
        else if (t == typeof(PgView))
        {
            return await this.GetViewAsync(schemaName, name, ct);
        }
        else
        {
            throw new NotSupportedException($"{t.Name} is not supported.");
        }
    }

    public async IAsyncEnumerable<PgConstraint> ListConstraintsAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var con in PgConstraintQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return con;
        }
    }

    public async Task<PgForeignTable?> GetForeignTableAsync(string schemaName, string name, CancellationToken ct = default)
    {
        return await PgForeignTableQuery.GetAsync(this, schemaName, name, ct);
    }

    public async IAsyncEnumerable<PgForeignTable> ListForeignTablesAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var ftable in PgForeignTableQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return ftable;
        }
    }

    public async Task<PgFunction?> GetFunctionAsync(string schemaName, string name, CancellationToken ct = default)
    {
        return await PgFunctionQuery.GetAsync(this, schemaName, name, ct);
    }

    public async IAsyncEnumerable<PgFunction> ListFunctionsAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var function in PgFunctionQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return function;
        }
    }

    public async IAsyncEnumerable<PgIndex> ListIndexesAsync(uint tableOid, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var ind in PgIndexQuery.ListAsync(this, tableOid, ct))
        {
            yield return ind;
        }
    }
    public async Task<PgIndex?> GetIndexAsync(string schemaName, string name, CancellationToken ct = default)
    {
        return await PgIndexQuery.GetAsync(this, schemaName, name, ct);
    }

    public async IAsyncEnumerable<PgIndex> ListIndexesAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var ind in PgIndexQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return ind;
        }
    }
    public async Task<PgMaterializedView?> GetMaterializedViewAsync(string schemaName, string name, CancellationToken ct = default)
    {
        return await PgMaterializedViewQuery.GetAsync(this, schemaName, name, ct);
    }

    public async IAsyncEnumerable<PgMaterializedView> ListMaterializedViewsAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var mview in PgMaterializedViewQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return mview;
        }
    }
    public async Task<PgPartitionTable?> GetPartitionTableAsync(string schemaName, string name, CancellationToken ct = default)
    {
        return await PgPartitionTableQuery.GetAsync(this, schemaName, name, ct);
    }

    public async IAsyncEnumerable<PgPartitionTable> ListPartitionTablesAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var ptable in PgPartitionTableQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return ptable;
        }
    }

    public async Task<PgProcedure?> GetProcedureAsync(string schemaName, string name, CancellationToken ct = default)
    {
        return await PgProcedureQuery.GetAsync(this, schemaName, name, ct);
    }

    public async IAsyncEnumerable<PgProcedure> ListProceduresAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var proc in PgProcedureQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return proc;
        }
    }

    public async IAsyncEnumerable<PgSchema> ListSchemasAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var schema in PgSchemaQuery.ListAsync(this, ct))
        {
            yield return schema;
        }
    }

    public async Task<PgSequence?> GetSequenceAsync(string schemaName, string name, CancellationToken ct = default)
    {
        return await PgSequenceQuery.GetAsync(this, schemaName, name, ct);
    }

    public async IAsyncEnumerable<PgSequence> ListSequencesAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var seq in PgSequenceQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return seq;
        }
    }
    public async IAsyncEnumerable<PgSequence> ListSequencesAsync(uint tableOid, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var seq in PgSequenceQuery.ListAsync(this, tableOid, ct))
        {
            yield return seq;
        }
    }

    public async Task<PgTable?> GetTableAsync(string schemaName, string name, CancellationToken ct = default)
    {
        return await PgTableQuery.GetAsync(this, schemaName, name, ct);
    }

    public async IAsyncEnumerable<PgTable> ListTablesAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var table in PgTableQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return table;
        }
    }

    public async Task<PgTrigger?> GetTriggerAsync(string schemaName, string name, CancellationToken ct = default)
    {
        return await PgTriggerQuery.GetAsync(this, schemaName, name, ct);
    }

    public async IAsyncEnumerable<PgTrigger> ListTriggersAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var trigger in PgTriggerQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return trigger;
        }
    }
    public async IAsyncEnumerable<PgTrigger> ListTriggersAsync(uint tableOid, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var trigger in PgTriggerQuery.ListAsync(this, tableOid, ct))
        {
            yield return trigger;
        }
    }
    public async Task<PgView?> GetViewAsync(string schemaName, string name, CancellationToken ct = default)
    {
        return await PgViewQuery.GetAsync(this, schemaName, name, ct);
    }
    public async IAsyncEnumerable<PgView> ListViewsAsync(string schemaName, string? nameLike, [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var view in PgViewQuery.ListAsync(this, schemaName, nameLike, ct))
        {
            yield return view;
        }
    }
}