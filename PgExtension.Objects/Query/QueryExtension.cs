namespace PgExtension.Objects.Query;

internal static class QueryExtension
{
    public static string? Like(this string? s)
    {
        if (s == null)
        {
            return null;
        }
        return $"%{s}%";
    }
}