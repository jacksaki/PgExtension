using Npgsql;
using PgExtension.Query;
using System.Data;
using System.Diagnostics.Contracts;
namespace PgExtension;

public partial class PgQuery : IDisposable
{
    public static IEnumerable<NpgsqlDataReader> ExecuteReader(string connectionString, string query,
        IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.ExecuteReader(new NpgsqlConnection(connectionString), query, parameter, commandType, commandBehavior);
    }

    /// <summary>Executes and returns the data records, when done dispose connection.<para>When done dispose connection.</para></summary>
    /// <param name="connection">Database connection.</param>
    /// <param name="query">SQL code.</param>
    /// <param name="parameter">PropertyName parameterized to PropertyName. if null then no use parameter.</param>
    /// <param name="commandType">Command Type.</param>
    /// <param name="commandBehavior">Command Behavior.</param>
    /// <returns>Query results.</returns>
    public static IEnumerable<NpgsqlDataReader> ExecuteReader(NpgsqlConnection connection, string query,
        IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(query));
        Contract.Ensures(Contract.Result<IEnumerable<NpgsqlDataReader>>() != null);

        return ExecuteReaderHelper(connection, query, parameter, commandType, commandBehavior);
    }

    public static int ExecuteNonQuery(string connectionString, string query,
        IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        return PgQuery.ExecuteNonQuery(new NpgsqlConnection(connectionString), query, parameter, commandType);
    }

    /// <summary>Executes and returns the number of rows affected.<para>When done dispose connection.</para></summary>
    /// <param name="connection">Database connection.</param>
    /// <param name="query">SQL code.</param>
    /// <param name="parameter">PropertyName parameterized to PropertyName. if null then no use parameter.</param>
    /// <param name="commandType">Command Type.</param>
    /// <returns>Rows affected.</returns>
    public static int ExecuteNonQuery(NpgsqlConnection connection, string query,
        IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(query));

        using (var exec = new PgQuery(connection))
        {
            return exec.ExecuteNonQuery(query, parameter, commandType);
        }
    }

    public static async Task<int> ExecuteNonQueryAsync(string connectionString, string query,
        IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        return await PgQuery.ExecuteNonQueryAsync(new NpgsqlConnection(connectionString), query, parameter, commandType);
    }

    /// <summary>Executes and returns the number of rows affected.<para>When done dispose connection.</para></summary>
    /// <param name="connection">Database connection.</param>
    /// <param name="query">SQL code.</param>
    /// <param name="parameter">PropertyName parameterized to PropertyName. if null then no use parameter.</param>
    /// <param name="commandType">Command Type.</param>
    /// <returns>Rows affected.</returns>
    public static async Task<int> ExecuteNonQueryAsync(NpgsqlConnection connection, string query,
        IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(query));

        using (var exec = new PgQuery(connection))
        {
            return await exec.ExecuteNonQueryAsync(query, parameter, commandType);
        }
    }

    public static T? ExecuteScalar<T>(string connectionString, string query,
        IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        return PgQuery.ExecuteScalar<T>(new NpgsqlConnection(connectionString), query, parameter, commandType);
    }

    /// <summary>Executes and returns the first column, first row.<para>When done dispose connection.</para></summary>
    /// <param name="connection">Database connection.</param>
    /// <typeparam name="T">Result type.</typeparam>
    /// <param name="query">SQL code.</param>
    /// <param name="parameter">PropertyName parameterized to PropertyName. if null then no use parameter.</param>
    /// <param name="commandType">Command Type.</param>
    /// <returns>Query results of first column, first row.</returns>
    public static T? ExecuteScalar<T>(NpgsqlConnection connection, string query,
        IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(query));

        using (var exec = new PgQuery(connection))
        {
            return exec.ExecuteScalar<T>(query, parameter, commandType);
        }
    }

    public static async Task<T?> ExecuteScalarAsync<T>(string connectionString, string query,
        IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        return await PgQuery.ExecuteScalarAsync<T>(new NpgsqlConnection(connectionString), query, parameter, commandType);
    }

    /// <summary>Executes and returns the first column, first row.<para>When done dispose connection.</para></summary>
    /// <param name="connection">Database connection.</param>
    /// <typeparam name="T">Result type.</typeparam>
    /// <param name="query">SQL code.</param>
    /// <param name="parameter">PropertyName parameterized to PropertyName. if null then no use parameter.</param>
    /// <param name="commandType">Command Type.</param>
    /// <returns>Query results of first column, first row.</returns>
    public static async Task<T?> ExecuteScalarAsync<T>(NpgsqlConnection connection, string query,
        IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(query));

        using (var exec = new PgQuery(connection))
        {
            return await exec.ExecuteScalarAsync<T>(query, parameter, commandType);
        }
    }

    #region Select (ConnectionString)
    public static IEnumerable<T> Select<T>(string connectionString, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T>(new NpgsqlConnection(connectionString), query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0>(string connectionString, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0>(new NpgsqlConnection(connectionString), t0, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1>(string connectionString, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1>(new NpgsqlConnection(connectionString), t0, t1, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2>(string connectionString, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2>(new NpgsqlConnection(connectionString), t0, t1, t2, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3, T4>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3, T4, T5>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, t5, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5, T6>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3, T4, T5, T6>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, t5, t6, query, parameter, commandType, commandBehavior);
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5, T6, T7>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        return PgQuery.Select<T, T0, T1, T2, T3, T4, T5, T6, T7>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, t5, t6, t7, query, parameter, commandType, commandBehavior);
    }
    #endregion

    #region SelectAsync (ConnectionString)
    public static async IAsyncEnumerable<T> SelectAsync<T>(string connectionString, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T>(new NpgsqlConnection(connectionString), query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0>(string connectionString, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0>(new NpgsqlConnection(connectionString), t0, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1>(string connectionString, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1>(new NpgsqlConnection(connectionString), t0, t1, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2>(string connectionString, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1, T2>(new NpgsqlConnection(connectionString), t0, t1, t2, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1, T2, T3>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1, T2, T3, T4>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1, T2, T3, T4, T5>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, t5, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5, T6>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1, T2, T3, T4, T5, T6>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, t5, t6, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5, T6, T7>(string connectionString, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var item in PgQuery.SelectAsync<T, T0, T1, T2, T3, T4, T5, T6, T7>(new NpgsqlConnection(connectionString), t0, t1, t2, t3, t4, t5, t6, t7, query, parameter, commandType, commandBehavior))
        {
            yield return item;
        }
    }
    #endregion

    #region SelectFirst (ConnectionString)
    public static T? SelectFirst<T>(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T>();
        }
    }
    public static T? SelectFirst<T, T0>(NpgsqlConnection connection, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0>(t0);
        }
    }
    public static T? SelectFirst<T, T0, T1>(NpgsqlConnection connection, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1>(t0, t1);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2>(t0, t1, t2);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4, T5>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4, T5, T6>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
        }
    }
    public static T? SelectFirst<T, T0, T1, T2, T3, T4, T5, T6, T7>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior).ToList();
            if (records.Count == 0)
            {
                return default(T);
            }
            return records.First().Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
        }
    }
    #endregion

    #region SelectFirstAsync (ConnectionString)
    public static async Task<T?> SelectFirstAsync<T>(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T>();
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0>(NpgsqlConnection connection, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0>(t0);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1>(NpgsqlConnection connection, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1>(t0, t1);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2>(t0, t1, t2);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4, T5>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4, T5, T6>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
            }
            return default(T);
        }
    }
    public static async Task<T?> SelectFirstAsync<T, T0, T1, T2, T3, T4, T5, T6, T7>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var t in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                if (t == null)
                {
                    return default(T);
                }
                return t.Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
            }
            return default(T);
        }
    }
    #endregion

    #region Select (NpgsqlConnection)
    public static IEnumerable<T> Select<T>(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T>();
            }
        }
    }
    public static IEnumerable<T> Select<T, T0>(NpgsqlConnection connection, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0>(t0);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1>(NpgsqlConnection connection, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1>(t0, t1);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1, T2>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1, T2>(t0, t1, t2);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5, T6>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
            }
        }
    }
    public static IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5, T6, T7>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            var records = exec.YieldReaderHelper(query, parameter, commandType, commandBehavior);
            foreach (var record in records)
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
            }
        }
    }
    #endregion

    #region SelectAsync (NpgsqlConnection)
    public static async IAsyncEnumerable<T> SelectAsync<T>(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T>();
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0>(NpgsqlConnection connection, T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0>(t0);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1>(NpgsqlConnection connection, T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1>(t0, t1);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2>(t0, t1, t2);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5, T6>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
            }
        }
    }
    public static async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5, T6, T7>(NpgsqlConnection connection, T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        using (var exec = new PgQuery(connection))
        {
            await foreach (var record in exec.YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
            {
                yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
            }
        }
    }
    #endregion
}