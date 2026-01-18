using ClosedXML.Excel;
using ConsoleAppFramework;

namespace ctasgen
{
    public class DDLCommand
    {
        /// <summary>
        /// </summary>
        [Command("")]
        public async Task ExecuteAsync(string path, string sheetName, string connectionString, bool test = true, bool execute = false)
        {
            var conf = await AppConfig.LoadAsync();
            var book = new XLWorkbook(path);
            var sheet = book.Worksheet(sheetName);

        }
    }
}
