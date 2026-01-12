using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PgExtension.Objects;

public abstract class PgRelationBase 
{
    protected uint _oid;
    internal PgRelationBase(PgCatalog catalog)
    {
        _catalog = catalog;
    }
    protected PgCatalog _catalog;

    public async IAsyncEnumerable<PgColumn> ListColumnsAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var col in _catalog.ListColumnsAsync(_oid, ct))
        {
            yield return col;
        }
    }
    public async IAsyncEnumerable<PgIndex> ListIndexesAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var index in _catalog.ListIndexesAsync(_oid, ct))
        {
            yield return index;
        }
    }
    public async IAsyncEnumerable<PgConstraint> ListConstraintsAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var con in _catalog.ListConstraintsAsync(_oid, ct))
        {
            yield return con;
        }
    }
    public async IAsyncEnumerable<PgTrigger> ListTriggersAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var trigger in _catalog.ListTriggersAsync(_oid, ct))
        {
            yield return trigger;
        }
    }
    public async IAsyncEnumerable<PgSequence> ListSequencesAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var seq in _catalog.ListSequencesAsync(_oid, ct))
        {
            yield return seq;
        }
    }
}
