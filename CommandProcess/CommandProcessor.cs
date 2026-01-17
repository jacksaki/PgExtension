using Cysharp.Diagnostics;
using System.Diagnostics;
namespace CommandProcess;

public class CommandProcessor
{
    public static async Task<CommandProcessResult> RunCommandAsync(string filePath, string args)
    {
        var stdOuts = new List<string>();
        var stdErrors = new List<string>();

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
                stdOuts.Add(item);
            }
        });

        var errorBuffered = new List<string>();
        var consumeStdError = Task.Run(async () =>
        {
            await foreach (var item in stdError)
            {
                stdErrors.Add(item);
                errorBuffered.Add(item);
            }
        });

        try
        {
            await Task.WhenAll(consumeStdOut, consumeStdError);
            return new CommandProcessResult(0, stdOuts, stdErrors);
        }
        catch (ProcessErrorException ex)
        {
            return new CommandProcessResult(ex.ExitCode, stdOuts, stdErrors);
        }
    }
}
