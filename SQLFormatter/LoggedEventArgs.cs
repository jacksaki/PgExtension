using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormatter;

public enum InstallLogItemType
{
    Output,
    Error,
}
public class LoggItem
{
    public LoggItem(InstallLogItemType type, string message)
    {
        this.Date = DateTime.Now;
        this.Message = message;
        this.Type = type;
    }
    public InstallLogItemType Type { get; }
    public string Message { get; }
    public DateTime Date { get; }
}
