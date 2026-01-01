using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PgExtension;
namespace PgExtensionBenchmark;

public record BigTable3
{
    [DbColumn("id")]
    public int Id { get; private set; }
    [DbColumn("name")]
    public string Name { get; private set; } = string.Empty;
    [DbColumn("age")]
    public int? Age { get; private set; }
    [DbColumn("score")]
    public decimal? Score { get; private set; }
    [DbColumn("created_at")]
    public DateTime? CreatedAt { get; private set; }
    [DbColumn("payload")]
    public string? Payload { get; private set; }
    [DbColumn("note")]
    public string? Note { get; private set; }
}
