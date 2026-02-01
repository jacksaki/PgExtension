using ConsoleAppFramework;
using SQLFormatter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormatterCmd
{
    public class FormatCommand
    {
        /// <summary>
        /// format
        /// </summary>
        /// <param name="sqlFilePath">-p, sql file path</param>
        /// <returns></returns>
        [Command("format")]
        public async Task FormatAsync(string sqlFilePath)
        {
            var formatter = new SQLFluffFormatter();
            formatter.Logged += async (sender, e) => Console.WriteLine(e.FormattedMessage);
            await formatter.ExecuteAsync(System.IO.File.ReadAllText(sqlFilePath));
        }
    }
}