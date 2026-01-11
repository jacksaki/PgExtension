using BenchmarkDotNet.Attributes;
using Npgsql;
using PgExtension;

namespace PgExtensionBenchmark;
[MemoryDiagnoser]//←これ
[ShortRunJob]
[IterationCount(5)]
[WarmupCount(3)]

public class BigTableBenchmark
{
    [Benchmark]
    public void ReadClassStatic()
    {
        var conn = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("connection_string")!);
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        foreach (var row in PgQuery.Select<BigTable>(conn, sql))
        {
            // Console.WriteLine(row.Id);
        }
    }
    [Benchmark]
    public async Task ReadClassStaticAsync()
    {
        var conn = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("connection_string")!);
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        await foreach (var row in PgQuery.SelectAsync<BigTable>(conn, sql))
        {
            // Console.WriteLine(row.Id);
        }
    }

    [Benchmark]
    public void ReadClassInstance()
    {
        var conn = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("connection_string")!);
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        using (var q = new PgQuery(conn))
        {
            foreach (var row in q.Select<BigTable>(sql))
            {
                //  Console.WriteLine(row.Id);
            }
        }
    }
    [Benchmark]
    public async Task ReadClassInstanceAsync()
    {
        var conn = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("connection_string")!);
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        using (var q = new PgQuery(conn))
        {
            await foreach (var row in q.SelectAsync<BigTable>(sql, null))
            {
                //  Console.WriteLine(row.Id);
            }
        }
    }


    [Benchmark]
    public void ReadRecordStatic()
    {
        var conn = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("connection_string")!);
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        foreach (var row in PgQuery.Select<BigTable3>(conn, sql))
        {
            // Console.WriteLine(row.Id);
        }
    }
    [Benchmark]
    public async Task ReadRecordStaticAsync()
    {
        var conn = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("connection_string")!);
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        await foreach (var row in PgQuery.SelectAsync<BigTable3>(conn, sql))
        {
            // Console.WriteLine(row.Id);
        }
    }

    [Benchmark]
    public void ReadRecordInstance()
    {
        var conn = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("connection_string")!);
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        using (var q = new PgQuery(conn))
        {
            foreach (var row in q.Select<BigTable3>(sql))
            {
                //  Console.WriteLine(row.Id);
            }
        }
    }
    [Benchmark]
    public async Task ReadRecordInstanceAsync()
    {
        var conn = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("connection_string")!);
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        using (var q = new PgQuery(conn))
        {
            await foreach (var row in q.SelectAsync<BigTable3>(sql, null))
            {
                //  Console.WriteLine(row.Id);
            }
        }
    }

    [Benchmark]
    public void ReadStructStatic()
    {
        var conn = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("connection_string")!);
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        foreach (var row in PgQuery.Select<BigTable4>(conn, sql))
        {
            // Console.WriteLine(row.Id);
        }
    }
    [Benchmark]
    public async Task ReadStructStaticAsync()
    {
        var conn = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("connection_string")!);
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        await foreach (var row in PgQuery.SelectAsync<BigTable4>(conn, sql))
        {
            // Console.WriteLine(row.Id);
        }
    }

    [Benchmark]
    public void ReadStructInstance()
    {
        var conn = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("connection_string")!);
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        using (var q = new PgQuery(conn))
        {
            foreach (var row in q.Select<BigTable4>(sql))
            {
                //  Console.WriteLine(row.Id);
            }
        }
    }
    [Benchmark]
    public async Task ReadStructInstanceAsync()
    {
        var conn = new NpgsqlConnection(System.Environment.GetEnvironmentVariable("connection_string")!);
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        using (var q = new PgQuery(conn))
        {
            await foreach (var row in q.SelectAsync<BigTable4>(sql, null))
            {
                //  Console.WriteLine(row.Id);
            }
        }
    }

}
