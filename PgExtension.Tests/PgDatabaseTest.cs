using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PgExtension.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgExtension.Tests
{
    public class PgDatabaseTest
    {
        [Fact]
        public async Task TestAsync()
        {
            var db = new PgDatabase(System.Environment.GetEnvironmentVariable("connection_string") ?? string.Empty);
            await foreach(var schema in db.ListSchemaAsync())
            {
                await foreach(var table in schema.ListTablesAsync(null))
                {
                    await foreach(var col in table.ListColumnsAsync())
                    {

                    }
                    await foreach(var index in table.ListIndexesAsync())
                    {

                    }
                    await foreach(var trigger in table.ListTriggersAsync())
                    {

                    }
                    await foreach(var con in table.ListConstraintsAsync())
                    {

                    }
                    await foreach(var seq in table.ListSequencesAsync())
                    {

                    }
                }
                await foreach (var view in schema.ListViewsAsync(null))
                {
                    await foreach (var col in view.ListColumnsAsync())
                    {

                    }
                    await foreach (var index in view.ListIndexesAsync())
                    {

                    }
                    await foreach (var trigger in view.ListTriggersAsync())
                    {

                    }
                    await foreach (var con in view.ListConstraintsAsync())
                    {

                    }
                    await foreach (var seq in view.ListSequencesAsync())
                    {

                    }
                }
                await foreach (var mview in schema.ListMaterializedViewsAsync(null))
                {
                    await foreach (var col in mview.ListColumnsAsync())
                    {

                    }
                    await foreach (var index in mview.ListIndexesAsync())
                    {

                    }
                    await foreach (var trigger in mview.ListTriggersAsync())
                    {

                    }
                    await foreach (var con in mview.ListConstraintsAsync())
                    {

                    }
                    await foreach (var seq in mview.ListSequencesAsync())
                    {

                    }
                }
                await foreach (var ftable in schema.ListForeignTablesAsync(null))
                {
                    await foreach (var col in ftable.ListColumnsAsync())
                    {

                    }
                    await foreach (var index in ftable.ListIndexesAsync())
                    {

                    }
                    await foreach (var trigger in ftable.ListTriggersAsync())
                    {

                    }
                    await foreach (var con in ftable.ListConstraintsAsync())
                    {

                    }
                    await foreach (var seq in ftable.ListSequencesAsync())
                    {

                    }
                }
                await foreach (var ptable in schema.ListPartitionTablesAsync(null))
                {
                    await foreach (var col in ptable.ListColumnsAsync())
                    {

                    }
                    await foreach (var index in ptable.ListIndexesAsync())
                    {

                    }
                    await foreach (var trigger in ptable.ListTriggersAsync())
                    {

                    }
                    await foreach (var con in ptable.ListConstraintsAsync())
                    {

                    }
                    await foreach (var seq in ptable.ListSequencesAsync())
                    {

                    }
                }
                await foreach (var proc in schema.ListProceduresAsync(null))
                {

                }
                await foreach (var function in schema.ListFunctionsAsync(null))
                {

                }
                await foreach (var index in schema.ListIndexesAsync(null))
                {

                }
                await foreach (var con in schema.ListConstraintsAsync(null))
                {
                }
                await foreach (var seq in schema.ListSequencesAsync(null))
                {

                }
                await foreach (var trigger in schema.ListTriggersAsync(null))
                {

                }
            }
        }
    }
}
