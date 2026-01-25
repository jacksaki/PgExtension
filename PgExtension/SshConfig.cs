using System.Text.Json.Serialization;

namespace PgExtension;

public class SshConfig
{
    [JsonIgnore]
    public int? LocalPort { get; set; }

    [JsonPropertyName("ssh_port")]
    public int SshPort { get; set; }
    [JsonPropertyName("ssh_host_name")]
    public string SshHostName { get; set; } = string.Empty;
    [JsonPropertyName("ssh_user_name")]
    public string SshUserName { get; set; } = string.Empty;
    [JsonPropertyName("ssh_password")]
    public string? SshPassword { get; set; }
    [JsonPropertyName("ssh_private_key")]
    public string? SshPrivateKey { get; set; }
}
