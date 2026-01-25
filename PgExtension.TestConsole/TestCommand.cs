using ConsoleAppFramework;
using Npgsql;
using PgExtension.Objects;
using PgExtension.Query;

namespace PgExtension.TestConsole
{
    public class TestCommand
    {
        [Command("ddl")]
        public async Task ExecuteDDLAsync(string connectionString, string type, string schemaName, string objectName)
        {
            var t = GetType(type);
            var types = typeof(PgTable).Assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && typeof(IPgObject).IsAssignableFrom(x));
            if (!types.Contains(t))
            {
                Console.WriteLine($"{type} is not supported.");
                return;
            }
            var catalog = new PgCatalog(connectionString);
            var obj = await catalog.GetAsync(t, schemaName, objectName);
            if (obj == null)
            {
                Console.WriteLine($"{type} {schemaName}.{objectName} not found.");
                return;
            }

            Console.WriteLine(await obj.GenerateDDLAsync(new DDLOptions() { AddConstraints = true, AddIndexes = true, AddSchema = true }));
        }
        private Type GetType(string type)
        {
            switch (type.ToLower())
            {
                case "const": return typeof(PgConstraint);
                case "ftable": return typeof(PgForeignTable);
                case "func": return typeof(PgFunction);
                case "index": return typeof(PgIndex);
                case "mview": return typeof(PgMaterializedView);
                case "ptable": return typeof(PgPartitionTable);
                case "proc": return typeof(PgProcedure);
                case "schema": return typeof(PgSchema);
                case "seq": return typeof(PgSequence);
                case "table": return typeof(PgTable);
                case "trigger": return typeof(PgTrigger);
                case "view": return typeof(PgView);
                default:
                    throw new ArgumentException($"{type}: Invalid type");
            }
        }

        /// <summary>
        /// sql
        /// </summary>
        /// <param name="connectionString">-c, connection string.</param>
        /// <param name="type">-t, [column,const,ftable,func,index,mview,ptable,proc,schema,seq,table,trigger,view]</param>
        /// <param name="schemaName">-s, schema name</param>
        /// <param name="tableOid">-o, table oid(column,index,seq,trigger)</param>
        [Command("sql")]
        public async Task ExecuteAsync(string connectionString, string type, string schemaName, uint? tableOid = null)
        {
            var sqlSet = GetSQL(type);
            if (tableOid.HasValue && sqlSet.Parameters?.Where(x => x.ParameterName.Contains("oid")).Any() == true)
            {
                var p = sqlSet.Parameters.Where(x => x.ParameterName.Contains("oid")).First();
                p.Value = tableOid;
            }
            else if (sqlSet.Parameters != null)
            {
                var p = sqlSet.Parameters.Where(x => x.ParameterName.Contains("schema")).First();
                p.Value = schemaName;
            }
            using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sqlSet.SQL;
            if (sqlSet.Parameters != null)
            {
                foreach (var p in sqlSet.Parameters)
                {
                    cmd.Parameters.Add(p);
                }
            }
            cmd.CommandType = System.Data.CommandType.Text;
            var dr = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.SchemaOnly);
            var fieldCount = dr.FieldCount;
            for (var i = 0; i < fieldCount; i++)
            {
                var fieldName = dr.GetName(i);
                var t = dr.GetDataTypeName(i);
                Console.WriteLine($"{fieldName}: {t}");
            }

        }
        private static SQLSet GetSQL(string type)
        {
            switch (type.ToLower())
            {
                case "column": return PgColumn.GetSQLSet();
                case "const": return PgConstraint.GetSQLSet();
                case "ftable": return PgForeignTable.GetSQLSet();
                case "func": return PgFunction.GetSQLSet();
                case "index": return PgIndex.GetSQLSet();
                case "mview": return PgMaterializedView.GetSQLSet();
                case "ptable": return PgPartitionTable.GetSQLSet();
                case "proc": return PgProcedure.GetSQLSet();
                case "schema": return PgSchema.GetSQLSet();
                case "seq": return PgSequence.GetSQLSet();
                case "table": return PgTable.GetSQLSet();
                case "trigger": return PgTrigger.GetSQLSet();
                case "view": return PgView.GetSQLSet();
                default:
                    throw new ArgumentException($"{type}: Invalid type");
            }
            ;
        }
    }
}
