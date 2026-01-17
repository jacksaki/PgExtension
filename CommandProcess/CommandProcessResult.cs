using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandProcess;

public class CommandProcessResult(int exitCode, IEnumerable<string>standardOutput,IEnumerable<string>standardError)
{
    public IEnumerable<string> StandardOutput => standardOutput;
    public IEnumerable<string> StandardError => standardError;
    public int ExitCode => exitCode;
}
