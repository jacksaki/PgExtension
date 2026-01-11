using DatabaseExtension;
namespace PgExtensionBenchmark;

public struct BigTable6
{
    public BigTable6()
    {
    }

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
