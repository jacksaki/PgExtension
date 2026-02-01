using Cysharp.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SQLFormatter;

public class SQLFluffFormatter
{
    public async Task ExecuteAsync(string sql)
    {
        var confPath = System.IO.Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".conf");
        var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var conf = JsonSerializer.Deserialize<Config>(File.ReadAllText(confPath))!;

        var pythonDir = Path.Combine(baseDir, conf.PythonDir);
        var sqlfluffExe = Path.Combine(pythonDir, "Scripts", "sqlfluff.exe");

        await RunCommandAsync(sqlfluffExe, sql);

    }
    public delegate void LoggedEventHandler(object sender, LoggItem e);
    public event LoggedEventHandler Logged = delegate { };
    private async Task RunCommandAsync(string filePath, string sql)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "sqlfluff",
            // "fix" または "lint" を指定。 "-" が標準入力を意味する
            // --dialect postgres を忘れずに（設定ファイルがない場合）
            Arguments = "fix - --dialect postgres",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (var process = new Process { StartInfo = startInfo })
        {
            process.Start();

            // 標準入力へSQLを書き込む
            using (var writer = process.StandardInput)
            {
                writer.Write(sql);
            }

            // 結果（修正済みSQLなど）を読み取る
            string result = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            Console.WriteLine("--- Result ---");
            Logged(this, new LoggItem(InstallLogItemType.Output, result));

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("--- Error/Log ---");
                Logged(this, new LoggItem(InstallLogItemType.Error, error));
            }
        }
    }
}
