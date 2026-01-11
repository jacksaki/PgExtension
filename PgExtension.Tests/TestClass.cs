using PgExtension.Query;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PgExtension.Tests;

[DbClass(nameof(RefreshItems))]
public class TestClass
{
    private void RefreshItems()
    {
        if(string.IsNullOrEmpty(_items))
        {
            this.Items = null;
        }
        else
        {
            this.Items = JsonSerializer.Deserialize<List<TestItemClass>>(_items);
        }
    }
    [DbColumn("string_value")]
    public string? StringValue { get; private set; }
    [DbColumn("int_value")]
    public int IntValue { get; private set; }
    [DbColumn("intn_value")]
    public int? IntNValue { get; private set; }
    [DbColumn("long_value")]
    public long LongValue { get; private set; }
    [DbColumn("longn_value")]
    public long? LongNValue { get; private set; }
    [DbColumn("float_value")]
    public float FloatValue { get; private set; }
    [DbColumn("floatn_value")]
    public float? FloatNValue { get; private set; }
    [DbColumn("double_value")]
    public double DoubleValue { get; private set; }
    [DbColumn("doublen_value")]
    public double? DoubleNValue { get; private set; }
    [DbColumn("decimal_value")]
    public decimal DecimalValue { get; private set; }
    [DbColumn("decimaln_value")]
    public decimal? DecimalNValue { get; private set; }
    [DbColumn("datetime_value")]
    public DateTime DateTimeValue { get; private set; }
    [DbColumn("datetimen_value")]
    public DateTime? DateTimeNValue { get; private set; }

    [DbColumn("items")]
    private string? _items = null;
    public List<TestItemClass>? Items { get; private set; }
}

public class TestItemClass
{
    [JsonPropertyName("cd")]
    public int Cd { get; private set; }
    [JsonPropertyName("name")]
    public string Name { get; private set; } = string.Empty;
}