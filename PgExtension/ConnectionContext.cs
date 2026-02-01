namespace PgExtension;

using Renci.SshNet;
using System.Collections.Concurrent;

public class ConnectionContext:IAsyncDisposable
{
    // 全インスタンスを追跡するための static 管理
    private static readonly ConcurrentBag<ConnectionContext> _instances = new();

    public ConnectionConfig Config { get; }
    private SshClient? _sshClient;
    private ForwardedPortLocal? _forwardedPort;

    public string GetConnectionString()
    {
        return this.Config.GetConnectionString();
    }
    public ConnectionContext(ConnectionConfig config)
    {
        Config = config;
        _instances.Add(this);
    }

    public async Task ConnectAsync()
    {
        if (Config.SshConfig == null) return;
        if (_sshClient?.IsConnected == true) return;

        var ssh = Config.SshConfig;
        ssh.LocalPort = LocalPortAllocator.Allocate();

        var connectionInfo = !string.IsNullOrEmpty(ssh.SshPrivateKey)
            ? new ConnectionInfo(ssh.SshHostName, ssh.SshPort, ssh.SshUserName, new PrivateKeyAuthenticationMethod(ssh.SshUserName, new PrivateKeyFile(ssh.SshPrivateKey)))
            : new ConnectionInfo(ssh.SshHostName, ssh.SshPort, ssh.SshUserName, new PasswordAuthenticationMethod(ssh.SshUserName, ssh.SshPassword));

        _sshClient = new SshClient(connectionInfo);

        await Task.Run(() =>
        {
            _sshClient.Connect();
            _forwardedPort = new ForwardedPortLocal("127.0.0.1", (uint)ssh.LocalPort.Value, Config.DbHost, (uint)Config.DbPort);
            _sshClient.AddForwardedPort(_forwardedPort);
            _forwardedPort.Start();
        });
    }

    // 個別の切断処理
    public async ValueTask DisposeAsync()
    {
        await Task.Run(() =>
        {
            try
            {
                if (_forwardedPort != null && _forwardedPort.IsStarted)
                {
                    _forwardedPort.Stop();
                }
                if (_sshClient != null)
                {
                    if (_sshClient.IsConnected) _sshClient.Disconnect();
                    _sshClient.Dispose();
                }
            }
            catch { /* ignore */ }
        });

        GC.SuppressFinalize(this);
    }

    // 全てを一括で切断する static メソッド
    public static async Task DisconnectAllAsync()
    {
        var tasks = _instances.Select(i => i.DisposeAsync().AsTask());
        await Task.WhenAll(tasks);
        // リストをクリア
        _instances.Clear();
    }
}
