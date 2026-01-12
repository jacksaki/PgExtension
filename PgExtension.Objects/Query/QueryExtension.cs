namespace PgExtension.Objects.Query;

internal static class QueryExtension
{
    public static object? Like(this string? s, object? nullValue)
    {
        if (s == null)
        {
            return nullValue;
        }
        return $"%{s}%";
    }
}