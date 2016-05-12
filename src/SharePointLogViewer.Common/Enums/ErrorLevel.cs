using System;

namespace SharePointLogViewer.Common.Enums
{
    [Flags]
    public enum ErrorLevel
    {
        Unknown = 0x00,
        Verbose = 0x01,
        Monitorable = 0x02,
        Information = 0x04,
        Warning = 0x08,
        Medium = 0x10,
        High = 0x20,
        Critical = 0x40,
        Unexpected = 0x80
    }
}
