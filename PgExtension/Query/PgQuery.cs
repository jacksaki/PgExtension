using Npgsql;
using PgExtension.Query;
using System.Data;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace PgExtension;
public partial class PgQuery : IDisposable
{
    readonly NpgsqlConnection connection;
    public static string? DefaultConnectionString { get; set; }
    // Transaction
    readonly bool isUseTransaction;
    readonly IsolationLevel isolationLevel;
    NpgsqlTransaction? transaction;
    bool isTransactionCompleted = false;

    /// <summary>Create standard executor.</summary>
    /// <param name="connection">Database connection.</param>
    public PgQuery(NpgsqlConnection connection)
    {
        this.connection = connection;
        this.isUseTransaction = false;
    }
    public PgQuery(string connectionString)
    {
        this.connection = new NpgsqlConnection(connectionString);
        this.isUseTransaction = false;
    }
    /// <summary>Use transaction.</summary>
    /// <param name="connection">Database connection.</param>
    /// <param name="isolationLevel">Transaction IsolationLevel.</param>
    public PgQuery(NpgsqlConnection connection, IsolationLevel isolationLevel)
    {
        this.connection = connection;
        this.isUseTransaction = true;
        this.isolationLevel = isolationLevel;
    }

    /// <summary>If connection is not open then open and create command.</summary>
    /// <param name="query">SQL code.</param>
    /// <param name="commandType">Command Type.</param>
    /// <param name="parameter">PropertyName parameterized to PropertyName. if null then no use parameter.</param>
    /// <returns>Setuped IDbCommand.</returns>
    protected NpgsqlCommand PrepareExecute(string query, CommandType commandType, IDictionary<string,object?>? parameter)
    {
        Contract.Ensures(Contract.Result<NpgsqlCommand>() != null);

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        if (transaction == null && isUseTransaction)
        {
            transaction = connection.BeginTransaction(isolationLevel);
        }

        var command = connection.CreateCommand();
        command.CommandText = query;
        command.CommandType = commandType;

        if (parameter != null)
        {
            foreach (var p in parameter)
            {
                command.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
            }
        }

        if (transaction != null)
        {
            command.Transaction = transaction;
        }

        return command;
    }

    #region Select
    public IEnumerable<T> Select<T>(string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        var records = YieldReaderHelper(query, parameter, commandType, commandBehavior);
        foreach (var record in records)
        {
            yield return record.Create<T>();
        }
    }

    public IEnumerable<T> Select<T, T0>(T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        var records = YieldReaderHelper(query, parameter, commandType, commandBehavior);
        foreach (var record in records)
        {
            yield return record.Create<T, T0>(t0);
        }
    }
    public IEnumerable<T> Select<T, T0, T1>(T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        var records = YieldReaderHelper(query, parameter, commandType, commandBehavior);
        foreach (var record in records)
        {
            yield return record.Create<T, T0, T1>(t0, t1);
        }
    }
    public IEnumerable<T> Select<T, T0, T1, T2>(T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        var records = YieldReaderHelper(query, parameter, commandType, commandBehavior);
        foreach (var record in records)
        {
            yield return record.Create<T, T0, T1, T2>(t0, t1, t2);
        }
    }
    public IEnumerable<T> Select<T, T0, T1, T2, T3>(T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        var records = YieldReaderHelper(query, parameter, commandType, commandBehavior);
        foreach (var record in records)
        {
            yield return record.Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
        }
    }
    public IEnumerable<T> Select<T, T0, T1, T2, T3, T4>(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        var records = YieldReaderHelper(query, parameter, commandType, commandBehavior);
        foreach (var record in records)
        {
            yield return record.Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
        }
    }
    public IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5>(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        var records = YieldReaderHelper(query, parameter, commandType, commandBehavior);
        foreach (var record in records)
        {
            yield return record.Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
        }
    }
    public IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5, T6>(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        var records = YieldReaderHelper(query, parameter, commandType, commandBehavior);
        foreach (var record in records)
        {
            yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
        }
    }
    public IEnumerable<T> Select<T, T0, T1, T2, T3, T4, T5, T6, T7>(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        var records = YieldReaderHelper(query, parameter, commandType, commandBehavior);
        foreach (var record in records)
        {
            yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
        }
    }
    #endregion

    #region SelectAsync
    public async IAsyncEnumerable<T> SelectAsync<T>(string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        await foreach (var record in YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
        {
            yield return record.Create<T>();
        }
    }
    public async IAsyncEnumerable<T> SelectAsync<T, T0>(T0 t0, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {

        await foreach (var record in YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
        {
            yield return record.Create<T, T0>(t0);
        }
    }
    public async IAsyncEnumerable<T> SelectAsync<T, T0, T1>(T0 t0, T1 t1, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {

        await foreach (var record in YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
        {
            yield return record.Create<T, T0, T1>(t0, t1);
        }
    }
    public async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2>(T0 t0, T1 t1, T2 t2, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {

        await foreach (var record in YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
        {
            yield return record.Create<T, T0, T1, T2>(t0, t1, t2);
        }
    }
    public async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3>(T0 t0, T1 t1, T2 t2, T3 t3, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {

        await foreach (var record in YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
        {
            yield return record.Create<T, T0, T1, T2, T3>(t0, t1, t2, t3);
        }
    }
    public async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4>(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {

        await foreach (var record in YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
        {
            yield return record.Create<T, T0, T1, T2, T3, T4>(t0, t1, t2, t3, t4);
        }
    }
    public async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5>(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {

        await foreach (var record in YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
        {
            yield return record.Create<T, T0, T1, T2, T3, T4, T5>(t0, t1, t2, t3, t4, t5);
        }
    }
    public async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5, T6>(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {

        await foreach (var record in YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
        {
            yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6>(t0, t1, t2, t3, t4, t5, t6);
        }
    }
    public async IAsyncEnumerable<T> SelectAsync<T, T0, T1, T2, T3, T4, T5, T6, T7>(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text, CommandBehavior commandBehavior = CommandBehavior.Default)
    {

        await foreach (var record in YieldReaderHelperAsync(query, parameter, commandType, commandBehavior))
        {
            yield return record.Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(t0, t1, t2, t3, t4, t5, t6, t7);
        }
    }
    #endregion

    IEnumerable<NpgsqlDataReader> YieldReaderHelper(string query, IDictionary<string,object?>? parameter, CommandType commandType, CommandBehavior commandBehavior)
    {
        using (var command = PrepareExecute(query, commandType, parameter))
        using (var reader = command.ExecuteReader(commandBehavior))
        {
            while (reader.Read())
            {
                yield return reader;
            }
        }
    }

    async IAsyncEnumerable<NpgsqlDataReader> YieldReaderHelperAsync(string query, IDictionary<string, object?>? parameter, CommandType commandType, CommandBehavior commandBehavior, [EnumeratorCancellation] CancellationToken ct = default)
    {
        using (var command = PrepareExecute(query, commandType, parameter))
        using (var reader = await command.ExecuteReaderAsync(commandBehavior, ct))
        {
            while (await reader.ReadAsync(ct))
            {
                yield return reader;
            }
        }
    }

    /// <summary>Executes and returns the data records.</summary>
    /// <param name="query">SQL code.</param>
    /// <param name="parameter">PropertyName parameterized to PropertyName. if null then no use parameter.</param>
    /// <param name="commandType">Command Type.</param>
    /// <param name="commandBehavior">Command Behavior.</param>
    /// <returns>Query results.</returns>
    public IEnumerable<NpgsqlDataReader> ExecuteReader(
        string query, 
        IDictionary<string,object?>? parameter = null, 
        CommandType commandType = CommandType.Text, 
        CommandBehavior commandBehavior = CommandBehavior.Default)
    {
        Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(query));
        Contract.Ensures(Contract.Result<IEnumerable<NpgsqlDataReader>>() != null);

        return YieldReaderHelper(query, parameter, commandType, commandBehavior);
    }

    /// <summary>Executes and returns the data records.</summary>
    /// <param name="query">SQL code.</param>
    /// <param name="parameter">PropertyName parameterized to PropertyName. if null then no use parameter.</param>
    /// <param name="commandType">Command Type.</param>
    /// <param name="commandBehavior">Command Behavior.</param>
    /// <returns>Query results.</returns>
    public async IAsyncEnumerable<NpgsqlDataReader> ExecuteReaderAsync(
        string query,
        IDictionary<string, object?>? parameter = null,
        CommandType commandType = CommandType.Text,
        CommandBehavior commandBehavior = CommandBehavior.Default,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(query));
        Contract.Ensures(Contract.Result<IEnumerable<NpgsqlDataReader>>() != null);

        await foreach(var r in YieldReaderHelperAsync(query, parameter, commandType, commandBehavior, ct))
        {
            yield return r;
        }
    }

    /// <summary>Executes and returns the number of rows affected.</summary>
    /// <param name="query">SQL code.</param>
    /// <param name="parameter">PropertyName parameterized to PropertyName. if null then no use parameter.</param>
    /// <param name="commandType">Command Type.</param>
    /// <returns>Rows affected.</returns>
    public int ExecuteNonQuery(string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(query));

        using (var command = PrepareExecute(query, commandType, parameter))
        {
            return command.ExecuteNonQuery();
        }
    }

    /// <summary>Executes and returns the number of rows affected.</summary>
    /// <param name="query">SQL code.</param>
    /// <param name="parameter">PropertyName parameterized to PropertyName. if null then no use parameter.</param>
    /// <param name="commandType">Command Type.</param>
    /// <returns>Rows affected.</returns>
    public async Task<int> ExecuteNonQueryAsync(string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(query));

        using (var command = PrepareExecute(query, commandType, parameter))
        {
            return await command.ExecuteNonQueryAsync();
        }
    }

    /// <summary>Executes and returns the first column, first row.</summary>
    /// <typeparam name="T">Result type.</typeparam>
    /// <param name="query">SQL code.</param>
    /// <param name="parameter">PropertyName parameterized to PropertyName. if null then no use parameter.</param>
    /// <param name="commandType">Command Type.</param>
    /// <returns>Query results of first column, first row.</returns>
    public T? ExecuteScalar<T>(string query, IDictionary<string,object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(query));

        using (var command = PrepareExecute(query, commandType, parameter))
        {
            return (T?)command.ExecuteScalar() ?? default(T);
        }
    }

    /// <summary>Executes and returns the first column, first row.</summary>
    /// <typeparam name="T">Result type.</typeparam>
    /// <param name="query">SQL code.</param>
    /// <param name="parameter">PropertyName parameterized to PropertyName. if null then no use parameter.</param>
    /// <param name="commandType">Command Type.</param>
    /// <returns>Query results of first column, first row.</returns>
    public async Task<T?> ExecuteScalarAsync<T>(string query, IDictionary<string, object?>? parameter = null, CommandType commandType = CommandType.Text)
    {
        Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(query));

        using (var command = PrepareExecute(query, commandType, parameter))
        {
            return (T?)await command.ExecuteScalarAsync() ?? default(T);
        }
    }

    /// <summary>Commit transaction.</summary>
    public void TransactionComplete()
    {
        if (transaction != null)
        {
            transaction.Commit();
            isTransactionCompleted = true;
        }
    }

    /// <summary>Dispose inner connection.</summary>
    public void Dispose()
    {
        try
        {
            if (transaction != null && !isTransactionCompleted)
            {
                transaction.Rollback();
                isTransactionCompleted = true;
            }
        }
        finally
        {
            connection.Dispose();
        }
    }

    static IEnumerable<NpgsqlDataReader> ExecuteReaderHelper(NpgsqlConnection connection, string query, IDictionary<string, object?>? parameter, CommandType commandType, CommandBehavior commandBehavior)
    {
        using (var exec = new PgQuery(connection))
        {
            foreach (var item in exec.ExecuteReader(query, parameter, commandType, commandBehavior))
            {
                yield return item;
            }
        }
    }
}
