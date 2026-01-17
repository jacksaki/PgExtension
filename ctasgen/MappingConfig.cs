using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ctasgen;

public class MappingConfig
{
    [JsonPropertyName("table")]
    [JsonInclude]
    public string Table { get; private set; } = string.Empty;
    [JsonPropertyName("column")]
    [JsonInclude]
    public string Column { get; private set; } = string.Empty;
    [JsonPropertyName("data_type")]
    [JsonInclude]
    public string DataType { get; private set; } = string.Empty;
    [JsonPropertyName("not_null")]
    [JsonInclude]
    public string NotNull { get; private set; } = string.Empty;
}
