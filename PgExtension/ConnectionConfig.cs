using System.Text.Json;
using System.Text.Json.Serialization;

namespace PgExtension;

public class ConnectionConfig
{
    public static ConnectionConfig Load(string path)
    {
        return JsonSerializer.Deserialize<ConnectionConfig>(System.IO.File.ReadAllText(path))!;
    }
    public string GetConnectionString()
    {
        if (this.SshConfig == null)
        {
            return $"Server={this.DbHost};Port={this.DbPort};Database={this.DatabaseName};User Id={this.DbUserName};Password=\"{this.DbPassword}\"";
        }
        else
        {
            return $"Server=127.0.0.1;Port={this.SshConfig.LocalPort};Database={this.DatabaseName};User Id={this.DbUserName};Password=\"{this.DbPassword}\"";
        }
    }

    [JsonPropertyName("db_host")]
    public string DbHost { get; set; } = string.Empty;
    [JsonPropertyName("db_port")]
    public int DbPort { get; set; }
    [JsonPropertyName("db_name")]
    public string DatabaseName { get; set; } = string.Empty;
    [JsonPropertyName("db_user_name")]
    public string DbUserName { get; set; } = string.Empty;
    [JsonPropertyName("db_password")]
    public string DbPassword { get; set; } = string.Empty;
    [JsonPropertyName("ssh")]
    public SshConfig? SshConfig { get; set; }
}
