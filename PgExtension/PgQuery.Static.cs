using Npgsql;
using System.Data;
using System.Diagnostics.Contracts;
namespace PgExtension;

public partial class PgQuery : IDisposable
{
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

}