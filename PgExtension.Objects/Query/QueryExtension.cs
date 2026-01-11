using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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