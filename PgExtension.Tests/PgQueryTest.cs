using Npgsql;

namespace PgExtension.Tests
{
    public class PgQueryTest
    {
        private static string GetConnectionString()
        {
            return System.Environment.GetEnvironmentVariable("connection_string")!;
        }
        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(GetConnectionString());
        }

        [Fact]
        public void SelectTest()
        {
            var sql = "SELECT * FROM TEST_TABLE WHERE string_value IN('A','B') ORDER BY string_value";
            var records = PgQuery.Select<TestClass>(GetConnection(), sql);
            foreach (var record in records)
            {
                var json = record;
            }
            using (var q = new PgQuery(GetConnection()))
            {
                var rows = q.Select<TestClass>(sql);
                var rows2 = q.Select<TestClass>(sql);
                q.TransactionComplete();
            }
        }
        [Fact]
        public async Task SelectAsyncTestAsync()
        {
            await Task.CompletedTask;
        }

        [Fact]
        public void ExecuteReaderTest()
        {

        }
        [Fact]
        public async Task ExecuteReaderAsyncTestAsync()
        {
            await Task.CompletedTask;
        }
        [Fact]
        public void ExecuteNonQueryTest()
        {

        }
        [Fact]
        public async Task ExecuteNonQueryAsyncTestAsync()
        {
            await Task.CompletedTask;
        }

        [Fact]
        public void ExecuteScalarTest()
        {

        }

        [Fact]
        public async Task ExecuteScalarAsyncTestAsync()
        {
            await Task.CompletedTask;
        }

        [Fact]
        public void TransactionCompleteTest()
        {

        }
        [Fact]
        public async Task TransactionCompleteAsyncTestAsync()
        {
            await Task.CompletedTask;
        }

        [Fact]
        public void TransactionNotCompleteTest()
        {

        }
        [Fact]
        public async Task TransactionNotCompleteTestAsync()
        {
            await Task.CompletedTask;
        }
    }
}