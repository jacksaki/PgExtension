using CommandProcess;
using System.IO.Compression;
using System.Reflection;
using System.Text.Json;

namespace SQLFormatter;

public static class LibraryInstaller
{
    public static async Task<CommandProcessResult?> InstallAsync()
    {
        var stdOuts = new List<string>();
        var stdErrors = new List<string>();
        var confPath = System.IO.Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".conf");
        if (!File.Exists(confPath))
        {
            throw new FileNotFoundException("SQLFormatter.conf not found", confPath);
        }

        var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var conf = JsonSerializer.Deserialize<Config>(File.ReadAllText(confPath))!;

        var pythonDir = Path.Combine(baseDir, conf.python_dir);
        var pythonExe = Path.Combine(pythonDir, "python.exe");
        var venvDir = Path.Combine(pythonDir, "venv");
        var sqlfluffExe = Path.Combine(venvDir, "Scripts", "sqlfluff.exe");

        if (File.Exists(sqlfluffExe))
        {
            return null; // すでに全部揃ってる
        }

        Directory.CreateDirectory(pythonDir);

        if (!File.Exists(pythonExe))
        {
            await InstallEmbeddedPythonAsync(conf.python_url, pythonDir);
            EnableSite(pythonDir);
            var result = await InstallPipAsync(pythonExe, pythonDir);
            stdOuts.AddRange(result.StandardOutput);
            stdErrors.AddRange(result.StandardError);
            if (result.ExitCode != 0)
            {
                return result;
            }
        }

        if (!Directory.Exists(venvDir))
        {
            var result = await RunCommandAsync(pythonExe, $"-m venv \"{venvDir}\"");
            stdOuts.AddRange(result.StandardOutput);
            stdErrors.AddRange(result.StandardError);
            if (result.ExitCode != 0)
            {
                return new CommandProcessResult(result.ExitCode, stdOuts, stdErrors);
            }
        }

        var pipResult = await RunCommandAsync(Path.Combine(venvDir, "Scripts", "pip.exe"), "install --upgrade pip");
        stdOuts.AddRange(pipResult.StandardOutput);
        stdErrors.AddRange(pipResult.StandardError);
        if (pipResult.ExitCode != 0)
        {
            return new CommandProcessResult(pipResult.ExitCode, stdOuts, stdErrors);
        }

        var sfResult = await RunCommandAsync(Path.Combine(venvDir, "Scripts", "pip.exe"), "install sqlfluff");
        stdOuts.AddRange(sfResult.StandardOutput);
        stdErrors.AddRange(sfResult.StandardError);
        return new CommandProcessResult(sfResult.ExitCode, stdOuts, stdErrors);
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

    private static async Task<CommandProcessResult> InstallPipAsync(string pythonExe, string pythonDir)
    {
        var getPipPath = Path.Combine(pythonDir, "get-pip.py");

        using var wc = new HttpClient();
        File.WriteAllBytes(
            getPipPath,
            await wc.GetByteArrayAsync("https://bootstrap.pypa.io/get-pip.py"));

        var result = await RunCommandAsync(pythonExe, $"\"{getPipPath}\"");
        File.Delete(getPipPath);
        return result;
    }


    private static async Task<CommandProcessResult> RunCommandAsync(string exe, string args)
    {
        return await CommandProcessor.RunCommandAsync(exe, args);
    }

    private record Config(
        string python_url,
        string python_dir,
        string sqlfluff_version
    );
}
