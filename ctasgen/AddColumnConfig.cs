using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ctasgen;

public class AddColumnConfig
{
    [JsonPropertyName("name")]
    [JsonInclude]
    public string Name { get; private set; } = string.Empty;
    [JsonPropertyName("data_type")]
    [JsonInclude]
    public string DataType { get; private set; } = string.Empty;
    [JsonPropertyName("name")]
    [JsonInclude]
    public string SourceName { get; private set; } = string.Empty;
    [JsonPropertyName("src_name")]
    [JsonInclude]
    public bool NotNull { get; private set; }
}
