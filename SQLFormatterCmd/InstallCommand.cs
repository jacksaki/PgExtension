using ConsoleAppFramework;
using Kokuban;
using Microsoft.Extensions.Logging;
using SQLFormatter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormatterCmd
{
    public class InstallCommand
    {
        [Command("install")]
        public async Task InstallAsync()
        {
            var installer = new LibraryInstaller();
            installer.Logged += (sender, e) =>
            {
                Console.WriteLine($"{e.Date:HH:mm:ss}\t{e.Type}\t{e.Message}");
            };
            await installer.InstallAsync();
        }
    }
}
