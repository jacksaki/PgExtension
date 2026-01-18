namespace CommandProcess;

public sealed class ProcessOutput(ProcessOutputKind kind, string line)
{
    public ProcessOutputKind Kind => kind;
    public string Line => line;
}