using PgExtension.Objects;
using PgExtension.Query;
using System.Text.Json;
using Xunit.Abstractions;

namespace PgExtension.Tests
{
    public class PgDatabaseTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public PgDatabaseTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public async Task TestAsync()
        {
            var conf = ConnectionConfig.Load(System.Environment.GetEnvironmentVariable("connection_config")!);
            await using var conn = new ConnectionContext(conf);
            await conn.ConnectAsync();
            _testOutputHelper.WriteLine(conn.GetConnectionString());
            var db = new PgDatabase(conn.GetConnectionString());
            var options = new JsonSerializerOptions() { WriteIndented = true };
            await foreach (var schema in db.ListSchemaAsync())
            {
                _testOutputHelper.WriteLine(schema.Name);
                if (!schema.Name.Equals("public"))
                {
                    continue;
                }
                await foreach (var table in schema.ListTablesAsync(null))
                {
                    _testOutputHelper.WriteLine($"Table: {table.SchemaName}.{table.Name}");
                    _testOutputHelper.WriteLine(await table.GenerateDDLAsync(new DDLOptions() { AddConstraints = false, AddIndexes = false, AddSchema = false }));
                    _testOutputHelper.WriteLine($"Columns:");
                    var columns = await table.ListColumnsAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(columns, options));

                    _testOutputHelper.WriteLine($"Indexes:");
                    var ind = await table.ListIndexesAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(ind, options));

                    _testOutputHelper.WriteLine($"Triggers:");
                    var trg = await table.ListTriggersAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(trg, options));

                    _testOutputHelper.WriteLine($"Constraints:");
                    var con = await table.ListConstraintsAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(con, options));

                    _testOutputHelper.WriteLine($"Sequences:");
                    var seq = await table.ListSequencesAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(seq, options));
                }
                await foreach (var view in schema.ListViewsAsync(null))
                {
                    _testOutputHelper.WriteLine($"View: {view.SchemaName}.{view.Name}");
                    _testOutputHelper.WriteLine($"Columns:");
                    var columns = await view.ListColumnsAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(columns, options));

                    _testOutputHelper.WriteLine($"Indexes:");
                    var ind = await view.ListIndexesAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(ind, options));

                    _testOutputHelper.WriteLine($"Triggers:");
                    var trg = await view.ListTriggersAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(trg, options));

                    _testOutputHelper.WriteLine($"Constraints:");
                    var con = await view.ListConstraintsAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(con, options));

                    _testOutputHelper.WriteLine($"Sequences:");
                    var seq = await view.ListSequencesAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(seq, options));
                }
                await foreach (var mview in schema.ListMaterializedViewsAsync(null))
                {
                    _testOutputHelper.WriteLine($"Materialized View: {mview.SchemaName}.{mview.Name}");
                    _testOutputHelper.WriteLine($"Columns:");
                    var columns = await mview.ListColumnsAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(columns, options));

                    _testOutputHelper.WriteLine($"Indexes:");
                    var ind = await mview.ListIndexesAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(ind, options));

                    _testOutputHelper.WriteLine($"Triggers:");
                    var trg = await mview.ListTriggersAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(trg, options));

                    _testOutputHelper.WriteLine($"Constraints:");
                    var con = await mview.ListConstraintsAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(con, options));

                    _testOutputHelper.WriteLine($"Sequences:");
                    var seq = await mview.ListSequencesAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(seq, options));
                }
                await foreach (var ftable in schema.ListForeignTablesAsync(null))
                {
                    _testOutputHelper.WriteLine($"Foreign Table: {ftable.SchemaName}.{ftable.Name}");
                    _testOutputHelper.WriteLine($"Columns:");
                    var columns = await ftable.ListColumnsAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(columns, options));

                    _testOutputHelper.WriteLine($"Indexes:");
                    var ind = await ftable.ListIndexesAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(ind, options));

                    _testOutputHelper.WriteLine($"Triggers:");
                    var trg = await ftable.ListTriggersAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(trg, options));

                    _testOutputHelper.WriteLine($"Constraints:");
                    var con = await ftable.ListConstraintsAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(con, options));

                    _testOutputHelper.WriteLine($"Sequences:");
                    var seq = await ftable.ListSequencesAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(seq, options));
                }
                await foreach (var ptable in schema.ListPartitionTablesAsync(null))
                {
                    _testOutputHelper.WriteLine($"Partition Table: {ptable.SchemaName}.{ptable.Name}");
                    _testOutputHelper.WriteLine($"Columns:");
                    var columns = await ptable.ListColumnsAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(columns, options));

                    _testOutputHelper.WriteLine($"Indexes:");
                    var ind = await ptable.ListIndexesAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(ind, options));

                    _testOutputHelper.WriteLine($"Triggers:");
                    var trg = await ptable.ListTriggersAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(trg, options));

                    _testOutputHelper.WriteLine($"Constraints:");
                    var con = await ptable.ListConstraintsAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(con, options));

                    _testOutputHelper.WriteLine($"Sequences:");
                    var seq = await ptable.ListSequencesAsync().ToListAsync();
                    _testOutputHelper.WriteLine(JsonSerializer.Serialize(seq, options));
                }

                _testOutputHelper.WriteLine($"Procedures");
                var procedures = await schema.ListProceduresAsync(null).ToListAsync();
                _testOutputHelper.WriteLine(JsonSerializer.Serialize(procedures, options));

                _testOutputHelper.WriteLine($"Functions");
                var functions = await schema.ListFunctionsAsync(null).ToListAsync();
                _testOutputHelper.WriteLine(JsonSerializer.Serialize(functions, options));

                _testOutputHelper.WriteLine($"Indexes");
                var indexes = await schema.ListIndexesAsync(null).ToListAsync();
                _testOutputHelper.WriteLine(JsonSerializer.Serialize(indexes, options));

                _testOutputHelper.WriteLine($"Constraints");
                var constraints = await schema.ListFunctionsAsync(null).ToListAsync();
                _testOutputHelper.WriteLine(JsonSerializer.Serialize(constraints, options));

                _testOutputHelper.WriteLine($"Sequences");
                var sequences = await schema.ListSequencesAsync(null).ToListAsync();
                _testOutputHelper.WriteLine(JsonSerializer.Serialize(sequences, options));

                _testOutputHelper.WriteLine($"Triggers");
                var triggers = await schema.ListTriggersAsync(null).ToListAsync();
                _testOutputHelper.WriteLine(JsonSerializer.Serialize(triggers, options));
            }
            _testOutputHelper.WriteLine("-----------");
        }
    }
}
