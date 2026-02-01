using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SQLFormatter
{
    internal class Config
    {
        [JsonPropertyName("python_url")]
        [JsonInclude]
        public string PythonUrl { get; private set; }
        [JsonPropertyName("python_dir")]
        [JsonInclude]
        public string PythonDir { get; private set; }
        [JsonPropertyName("sqlfluff_version")]
        [JsonInclude]
        public string SqlfluffVersion { get; private set; }
        [JsonPropertyName("arguments")]
        [JsonInclude]
        public string Arguments { get; private set; }
    }
}
