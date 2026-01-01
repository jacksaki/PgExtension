using BenchmarkDotNet.Running;

namespace PgExtensionBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseExtension.PgQuery.SetConnectionString(System.Environment.GetEnvironmentVariable("connection_string"));
            BenchmarkRunner.Run<BigTableBenchmark>();
            BenchmarkRunner.Run<BigTableBenchmarkOld>();
        }
    }
}
