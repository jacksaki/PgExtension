using ClosedXML.Excel;
using ConsoleAppFramework;
using Kokuban;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctasgen
{
    public class DDLCommand
    {
        /// <summary>
        /// </summary>
        [Command("")]
        public async Task ExecuteAsync(string path, string sheetName, string connectionString,bool test=true,bool execute=false)
        {
            var conf = await AppConfig.LoadAsync();
            var book = new XLWorkbook(path);
            var sheet = book.Worksheet(sheetName);

        }
    }
}
