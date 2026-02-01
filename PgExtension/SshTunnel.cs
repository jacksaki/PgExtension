using PgExtension;
using System.ComponentModel;
using System.Runtime.InteropServices;

internal sealed class SshTunnel : IDisposable
{
    #region Win32

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    struct STARTUPINFO
    {
        public int cb;
        public string? lpReserved;
        public string? lpDesktop;
        public string? lpTitle;
        public int dwX;
        public int dwY;
        public int dwXSize;
        public int dwYSize;
        public int dwXCountChars;
        public int dwYCountChars;
        public int dwFillAttribute;
        public int dwFlags;
        public short wShowWindow;
        public short cbReserved2;
        public IntPtr lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct PROCESS_INFORMATION
    {
        public IntPtr hProcess;
        public IntPtr hThread;
        public int dwProcessId;
        public int dwThreadId;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct JOBOBJECT_BASIC_LIMIT_INFORMATION
    {
        public long PerProcessUserTimeLimit;
        public long PerJobUserTimeLimit;
        public JobObjectLimit LimitFlags;
        public UIntPtr MinimumWorkingSetSize;
        public UIntPtr MaximumWorkingSetSize;
        public int ActiveProcessLimit;
        public long Affinity;
        public int PriorityClass;
        public int SchedulingClass;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct IO_COUNTERS
    {
        public ulong ReadOperationCount;
        public ulong WriteOperationCount;
        public ulong OtherOperationCount;
        public ulong ReadTransferCount;
        public ulong WriteTransferCount;
        public ulong OtherTransferCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
    {
        public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
        public IO_COUNTERS IoInfo;
        public UIntPtr ProcessMemoryLimit;
        public UIntPtr JobMemoryLimit;
        public UIntPtr PeakProcessMemoryUsed;
        public UIntPtr PeakJobMemoryUsed;
    }

    enum JobObjectInfoType
    {
        ExtendedLimitInformation = 9
    }

    [Flags]
    enum JobObjectLimit : uint
    {
        KILL_ON_JOB_CLOSE = 0x00002000
    }

    enum CreationFlags : uint
    {
        CREATE_NO_WINDOW = 0x08000000
    }

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern bool CreateProcess(
        string? lpApplicationName,
        string lpCommandLine,
        IntPtr lpProcessAttributes,
        IntPtr lpThreadAttributes,
        bool bInheritHandles,
        CreationFlags dwCreationFlags,
        IntPtr lpEnvironment,
        string? lpCurrentDirectory,
        ref STARTUPINFO lpStartupInfo,
        out PROCESS_INFORMATION lpProcessInformation);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr CreateJobObject(IntPtr lpJobAttributes, string? lpName);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool AssignProcessToJobObject(IntPtr hJob, IntPtr hProcess);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetInformationJobObject(
        IntPtr hJob,
        JobObjectInfoType infoType,
        ref JOBOBJECT_EXTENDED_LIMIT_INFORMATION lpJobObjectInfo,
        int cbJobObjectInfoLength);

    [DllImport("kernel32.dll")]
    static extern bool CloseHandle(IntPtr hObject);

    #endregion

    private IntPtr _job;
    private bool _disposed;

    private SshTunnel(IntPtr job)
    {
        _job = job;
    }

    public static SshTunnel Start(ConnectionConfig conf)
    {
        var ssh = conf.SshConfig!;

        var args =
            $"-N -L {ssh.LocalPort}:{conf.DbHost}:{conf.DbPort} " +
            $"-p {ssh.SshPort} " +
            $"{ssh.SshUserName}@{ssh.SshHostName} " +
            "-o ExitOnForwardFailure=yes";

        if (!string.IsNullOrEmpty(ssh.SshPrivateKey))
            args += $" -i \"{ssh.SshPrivateKey}\"";

        var si = new STARTUPINFO
        {
            cb = Marshal.SizeOf<STARTUPINFO>()
        };

        if (!CreateProcess(
            null,
            $"ssh.exe {args}",
            IntPtr.Zero,
            IntPtr.Zero,
            false,
            CreationFlags.CREATE_NO_WINDOW,
            IntPtr.Zero,
            null,
            ref si,
            out var pi))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        var job = CreateJobObject(IntPtr.Zero, null);
        if (job == IntPtr.Zero)
            throw new Win32Exception(Marshal.GetLastWin32Error());

        var info = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION
        {
            BasicLimitInformation = new JOBOBJECT_BASIC_LIMIT_INFORMATION
            {
                LimitFlags = JobObjectLimit.KILL_ON_JOB_CLOSE
            }
        };

        if (!SetInformationJobObject(
            job,
            JobObjectInfoType.ExtendedLimitInformation,
            ref info,
            Marshal.SizeOf<JOBOBJECT_EXTENDED_LIMIT_INFORMATION>()))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        if (!AssignProcessToJobObject(job, pi.hProcess))
            throw new Win32Exception(Marshal.GetLastWin32Error());

        CloseHandle(pi.hThread);
        CloseHandle(pi.hProcess);

        return new SshTunnel(job);
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;

        if (_job != IntPtr.Zero)
        {
            CloseHandle(_job); // ← これだけで ssh.exe 全殺し
            _job = IntPtr.Zero;
        }
    }
}
