using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PgExtension.Objects;

internal static class TableTypeExtension
{
    public static string[] GetArray(this TableTypes t)
    {
        if (t == 0)
            return Array.Empty<string>();

        var type = typeof(TableTypes);

        return Enum.GetValues<TableTypes>()
            .Where(flag => flag != 0 && t.HasFlag(flag))
            .Select(flag =>
            {
                var member = type.GetMember(flag.ToString())[0];
                var attr = member.GetCustomAttribute<TableTypeAttribute>();
                return attr?.RelKind;
            })
            .Where(x => x != null)
            .Distinct()
            .ToArray()!;
    }
}

