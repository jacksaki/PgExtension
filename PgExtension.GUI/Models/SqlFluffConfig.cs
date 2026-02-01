using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PgExtension.GUI.Models;

public class SqlFluffConfig
{
    [JsonPropertyName("never_show_again")]
    public bool NeverShowAgain { get; set; }
}
