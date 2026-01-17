using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandProcess;

public sealed class ProcessOutput(ProcessOutputKind kind, string line)
{
    public ProcessOutputKind Kind => kind;
    public string Line => line;
}