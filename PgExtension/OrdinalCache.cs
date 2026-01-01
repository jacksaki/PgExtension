using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgExtension;

internal static class OrdinalCache
{
    internal static Dictionary<string, int> BuildOrdinalMap(NpgsqlDataReader reader)
    {
        var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < reader.FieldCount; i++)
        {
            dict[reader.GetName(i)] = i;
        }
        return dict;
    }
}
