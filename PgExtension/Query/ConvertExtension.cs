namespace PgExtension.Query;

public static class ConvertExtension
{
    public static async Task<IEnumerable<T>> ToTask<T>(this IAsyncEnumerable<T> source)
    {
        var list = new List<T>();
        await foreach (var item in source)
        {
            list.Add(item);
        }
        return list;
    }

    public static int ToInt32(this object? value)
    {
        return value.ToInt32(default);
    }

    public static int ToInt32(this object? value, int defaultValue)
    {
        return value.ToIntN() ?? defaultValue;
    }

    public static int? ToIntN(this object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        return int.TryParse(value.ToString(), out var ret) ? ret : null;
    }

    public static long ToInt64(this object? value)
    {
        return value.ToInt64(default(int));
    }

    public static long ToInt64(this object? value, long defaultValue)
    {
        return value.ToInt64N() ?? defaultValue;
    }

    public static long? ToInt64N(this object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        return long.TryParse(value.ToString(), out var ret) ? ret : null;
    }

    public static decimal ToDecimal(this object? value)
    {
        return value.ToDecimal(default(int));
    }

    public static decimal ToDecimal(this object? value, long defaultValue)
    {
        return value.ToDecimalN() ?? defaultValue;
    }

    public static decimal? ToDecimalN(this object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        return decimal.TryParse(value.ToString(), out var ret) ? ret : null;
    }

    public static float ToFloat(this object? value)
    {
        return value.ToFloat(default(int));
    }
    public static float ToFloat(this object? value, float defaultValue)
    {
        return value.ToFloatN() ?? defaultValue;
    }

    public static float? ToFloatN(this object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        return float.TryParse(value.ToString(), out var ret) ? ret : null;
    }

    public static double ToDouble(this object? value)
    {
        return value.ToDouble(default(int));
    }

    public static double ToDouble(this object? value, double defaultValue)
    {
        return value.ToDoubleN() ?? defaultValue;
    }

    public static double? ToDoubleN(this object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        return double.TryParse(value.ToString(), out var ret) ? ret : null;
    }

    public static DateTime ToDateTime(this object? value, string? dateFormat)
    {
        return value.ToDateTime(dateFormat, default);
    }

    public static DateTime ToDateTime(this object? value, string? dateFormat, DateTime defaultValue)
    {
        return value.ToDateTimeN(dateFormat) ?? defaultValue;
    }

    public static DateTime? ToDateTimeN(this object? value, string? dateFormat)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }

        if (value is DateTime d)
        {
            return d;
        }

        if (dateFormat == null)
        {
            return DateTime.TryParse(value.ToString(), out var ret) ? ret : null;
        }
        else
        {
            return DateTime.TryParseExact(value.ToString(), dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var ret) ? ret : null;
        }
    }
}
