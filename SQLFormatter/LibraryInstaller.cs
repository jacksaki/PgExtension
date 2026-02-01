using Cysharp.Diagnostics;
using ObservableCollections;
using R3;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SQLFormatter;

public class LibraryInstaller
{
    public ReactiveProperty<string> Title { get; }
    public LibraryInstaller()
    {
        this.Title = new ReactiveProperty<string>();
    }

    private async Task RunCommandAsync(string filePath, string args)
    {
        var stdOuts = new List<string>();

        var psi = new ProcessStartInfo
        {
            FileName = filePath,
            Arguments = args,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        var (_, stdOut, stdError) = ProcessX.GetDualAsyncEnumerable(psi);
        var consumeStdOut = Task.Run(async () =>
        {
            await foreach (var item in stdOut)
            {
                Logged(this,new LogItem(InstallLogItemType.Output,item));
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

    public delegate void LoggedEventHandler(object sender, LogItem e);
    public event LoggedEventHandler Logged = delegate { };

    public async Task InstallAsync()
    {
        this.Title.Value = "事前チェック";
        var stdOuts = new List<string>();
        var stdErrors = new List<string>();
        var confPath = System.IO.Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".conf");
        if (!File.Exists(confPath))
        {
            throw new FileNotFoundException("SQLFormatter.conf not found", confPath);
        }

        var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var conf = JsonSerializer.Deserialize<Config>(File.ReadAllText(confPath))!;

        var pythonDir = Path.Combine(baseDir, conf.PythonDir);
        var pythonExe = Path.Combine(pythonDir, "python.exe");
        var sqlfluffExe = Path.Combine(pythonDir, "Scripts", "sqlfluff.exe");

        if (File.Exists(sqlfluffExe))
        {
            this.Title.Value = "完了";
            return;
        }

        Directory.CreateDirectory(pythonDir);

        if (!File.Exists(pythonExe))
        {
            this.Title.Value = "Embedded python インストール";
            await InstallEmbeddedPythonAsync(conf.PythonUrl, pythonDir);
            EnableSite(pythonDir);
            this.Title.Value = "get-pip";
            await InstallPipAsync(pythonExe, pythonDir);
        }


        this.Title.Value = "pip インストール";
        await RunCommandAsync(Path.Combine(pythonDir, "Scripts", "pip.exe"), "install --upgrade pip");

        this.Title.Value = "sqlfluff インストール";
        await RunCommandAsync(Path.Combine(pythonDir, "Scripts", "pip.exe"), "install sqlfluff");
        this.Title.Value = "完了";
    }

    private static async Task InstallEmbeddedPythonAsync(string url, string targetDir)
    {
        var zipPath = Path.Combine(targetDir, "python_embed.zip");

        using var wc = new HttpClient();
        File.WriteAllBytes(zipPath, await wc.GetByteArrayAsync(url));

        ZipFile.ExtractToDirectory(zipPath, targetDir, overwriteFiles: true);
        File.Delete(zipPath);
    }

    private static void EnableSite(string pythonDir)
    {
        var pth = Directory.GetFiles(pythonDir, "python*._pth").Single();

        var lines = File.ReadAllLines(pth)
            .Select(l => l.Trim())
            .ToList();

        if (!lines.Any(l => l == "import site"))
        {
            lines.Add("import site");
            File.WriteAllLines(pth, lines);
        }
    }

    private async Task InstallPipAsync(string pythonExe, string pythonDir)
    {
        var getPipPath = Path.Combine(pythonDir, "get-pip.py");

        using var wc = new HttpClient();
        File.WriteAllBytes(
            getPipPath,
            await wc.GetByteArrayAsync("https://bootstrap.pypa.io/get-pip.py"));

        await RunCommandAsync(pythonExe, $"\"{getPipPath}\"");
    }
}
