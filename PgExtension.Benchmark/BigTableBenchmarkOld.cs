using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Tracing.StackSources;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PgExtension;
using DatabaseExtension;

namespace PgExtensionBenchmark;
[MemoryDiagnoser]//←これ
[ShortRunJob]
[IterationCount(5)]
[WarmupCount(3)]
public class BigTableBenchmarkOld
{
    [Benchmark]
    public void ReadClass()
    {
        DatabaseExtension.PgQuery.SetConnectionString(System.Environment.GetEnvironmentVariable("connection_string"));
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        using (var q = new DatabaseExtension.PgQuery())
        {
            foreach (var row in q.GetSqlResult(sql,null).Rows.Select(x=>x.Create<BigTable2>()))
            {
                //  Console.WriteLine(row.Id);
            }
        }
    }
    [Benchmark]
    public void ReadRecord()
    {
        DatabaseExtension.PgQuery.SetConnectionString(System.Environment.GetEnvironmentVariable("connection_string"));
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        using (var q = new DatabaseExtension.PgQuery())
        {
            foreach (var row in q.GetSqlResult(sql, null).Rows.Select(x => x.Create<BigTable5>()))
            {
                //  Console.WriteLine(row.Id);
            }
        }
    }
    [Benchmark]
    public void ReadStruct()
    {
        DatabaseExtension.PgQuery.SetConnectionString(System.Environment.GetEnvironmentVariable("connection_string"));
        var sql = "SELECT name,age,score,created_at,id,payload,note FROM big_table";
        using (var q = new DatabaseExtension.PgQuery())
        {
            foreach (var row in q.GetSqlResult(sql, null).Rows.Select(x => x.Create<BigTable6>()))
            {
                //  Console.WriteLine(row.Id);
            }
        }
    }
}
