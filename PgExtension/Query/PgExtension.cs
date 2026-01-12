using Npgsql;

namespace PgExtension.Query;

public static class PgExtension
{
    public static async Task<List<T>> ToListAsync<T>(
        this IAsyncEnumerable<T> source,
        CancellationToken ct = default)
    {
        var list = new List<T>();
        await foreach (var item in source.WithCancellation(ct))
        {
            list.Add(item);
        }
        return list;
    }

    public static T Create<T>(this NpgsqlDataReader row)
    {
        var obj = ConstructorCache<T>.CreateInstance();
        var ordinals = OrdinalCache.BuildOrdinalMap(row);
        PropertyFieldSetterCache<T>.Set(obj, row, ordinals);
        PostProcessCache<T>.Invoke?.Invoke(obj);
        return obj;
    }

    public static T Create<T, T0>(this NpgsqlDataReader row, T0 param0)
    {
        var obj = ConstructorCache<T, T0>.CreateInstance(param0);
        var ordinals = OrdinalCache.BuildOrdinalMap(row);
        PropertyFieldSetterCache<T>.Set(obj, row, ordinals);
        PostProcessCache<T>.Invoke?.Invoke(obj);
        return obj;
    }
    public static T Create<T, T0, T1>(this NpgsqlDataReader row, T0 param0, T1 param1)
    {
        var obj = ConstructorCache<T, T0, T1>.CreateInstance(param0, param1);
        var ordinals = OrdinalCache.BuildOrdinalMap(row);
        PropertyFieldSetterCache<T>.Set(obj, row, ordinals);
        PostProcessCache<T>.Invoke?.Invoke(obj);
        return obj;
    }
    public static T Create<T, T0, T1, T2>(this NpgsqlDataReader row, T0 param0, T1 param1, T2 param2)
    {
        var obj = ConstructorCache<T, T0, T1, T2>.CreateInstance(param0, param1, param2);
        var ordinals = OrdinalCache.BuildOrdinalMap(row);
        PropertyFieldSetterCache<T>.Set(obj, row, ordinals);
        PostProcessCache<T>.Invoke?.Invoke(obj);
        return obj;
    }
    public static T Create<T, T0, T1, T2, T3>(this NpgsqlDataReader row, T0 param0, T1 param1, T2 param2, T3 param3)
    {
        var obj = ConstructorCache<T, T0, T1, T2, T3>.CreateInstance(param0, param1, param2, param3);
        var ordinals = OrdinalCache.BuildOrdinalMap(row);
        PropertyFieldSetterCache<T>.Set(obj, row, ordinals);
        PostProcessCache<T>.Invoke?.Invoke(obj);
        return obj;
    }

    public static T Create<T, T0, T1, T2, T3, T4>(this NpgsqlDataReader row, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4)
    {
        var obj = ConstructorCache<T, T0, T1, T2, T3, T4>.CreateInstance(param0, param1, param2, param3, param4);
        var ordinals = OrdinalCache.BuildOrdinalMap(row);
        PropertyFieldSetterCache<T>.Set(obj, row, ordinals);
        PostProcessCache<T>.Invoke?.Invoke(obj);
        return obj;
    }
    public static T Create<T, T0, T1, T2, T3, T4, T5>(this NpgsqlDataReader row, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
    {
        var obj = ConstructorCache<T, T0, T1, T2, T3, T4, T5>.CreateInstance(param0, param1, param2, param3, param4, param5);
        var ordinals = OrdinalCache.BuildOrdinalMap(row);
        PropertyFieldSetterCache<T>.Set(obj, row, ordinals);
        PostProcessCache<T>.Invoke?.Invoke(obj);
        return obj;
    }
    public static T Create<T, T0, T1, T2, T3, T4, T5, T6>(this NpgsqlDataReader row, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
    {
        var obj = ConstructorCache<T, T0, T1, T2, T3, T4, T5, T6>.CreateInstance(param0, param1, param2, param3, param4, param5, param6);
        var ordinals = OrdinalCache.BuildOrdinalMap(row);
        PropertyFieldSetterCache<T>.Set(obj, row, ordinals);
        PostProcessCache<T>.Invoke?.Invoke(obj);
        return obj;
    }

    public static T Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(this NpgsqlDataReader row, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
    {
        var obj = ConstructorCache<T, T0, T1, T2, T3, T4, T5, T6, T7>.CreateInstance(param0, param1, param2, param3, param4, param5, param6, param7);
        var ordinals = OrdinalCache.BuildOrdinalMap(row);
        PropertyFieldSetterCache<T>.Set(obj, row, ordinals);
        PostProcessCache<T>.Invoke?.Invoke(obj);
        return obj;
    }
}
