using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ctasgen;

public class AppConfig
{
    public static async Task<AppConfig> LoadAsync()
    {
        var path = System.IO.Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".conf");
       return JsonSerializer.Deserialize<AppConfig>(await System.IO.File.ReadAllTextAsync(path))!;
    }

    [JsonPropertyName("src_mapping")]
    [JsonInclude]
    public MappingConfig SourceMapping { get; private set; } = null!;
    [JsonPropertyName("dest_mapping")]
    [JsonInclude]
    public MappingConfig DestMapping { get; private set; } = null!;

    [JsonPropertyName("add_columns")]
    [JsonInclude]
    public AddColumnConfig[] AddColumns { get; private set; } = Array.Empty<AddColumnConfig>();
}
