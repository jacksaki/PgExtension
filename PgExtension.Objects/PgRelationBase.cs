using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgExtension.Objects;

public abstract class PgRelationBase
{
    public string Schema { get; } = string.Empty;
    public string Name { get; } = string.Empty;
    public int Oid { get; }
}

