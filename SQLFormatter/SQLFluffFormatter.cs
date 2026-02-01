using Cysharp.Diagnostics;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace SQLFormatter;

public class SQLFluffFormatter
{
    public delegate void LoggedEventHandler(object sender, LogItem e);
    public event LoggedEventHandler Logged = delegate { };
    public async Task ExecuteAsync(string sql)
    {
        var confPath = System.IO.Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".conf");
        var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var conf = JsonSerializer.Deserialize<Config>(File.ReadAllText(confPath))!;
        var pythonDir = Path.Combine(baseDir, conf.PythonDir);
        var sqlfluffDir = Path.Combine(pythonDir, "Scripts");
        var sqlfluffExe = Path.Combine(sqlfluffDir, "sqlfluff.exe");
        var psi = new ProcessStartInfo
        {
            FileName = sqlfluffExe,
            WorkingDirectory = sqlfluffDir,
            Arguments = conf.Arguments,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        var (_, stdOut, stdError) = ExtendedProcessX.GetDualAsyncEnumerable(psi,sql);

        var consumeStdOut = Task.Run(async () =>
        {
            await foreach (var item in stdOut)
            {
                Logged(this, new LogItem(InstallLogItemType.Output, item));
            }
        });

        var errorBuffered = new List<string>();
        var consumeStdError = Task.Run(async () =>
        {
            await foreach (var item in stdError)
            {
                Logged(this, new LogItem(InstallLogItemType.Error, item));
                errorBuffered.Add(item);
            }
        });
        try
        {
            await Task.WhenAll(consumeStdOut, consumeStdError);
        }
        catch (ProcessErrorException ex)
        {
            throw new Exception(ex.ExitCode.ToString());
        }

    }
}
