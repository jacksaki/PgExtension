using ConsoleAppFramework;
using Microsoft.Extensions.Logging;
using ZLogger;

namespace SQLFormatterCmd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = ConsoleApp.Create();
            //.ConfigureLogging(x=>
            //{
            //    x.ClearProviders();
            //    x.SetMinimumLevel(LogLevel.Trace);
            //    x.AddZLoggerConsole();
            //});
            app.Add<InstallCommand>();
            app.Add<FormatCommand>();
            app.Run(args);
        }
    }
}
