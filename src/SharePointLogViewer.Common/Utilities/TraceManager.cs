using System;
using System.Diagnostics;

namespace SharePointLogViewer.Common.Utilities
{
    /// <summary>
    /// Entry point for writing trace messages
    /// </summary>
    public static class TraceManager
    {
        public static bool Enabled { get; set; }

        public static void WriteNewFile()
        {
            WriteLine(() => "\r\n------\r\nApplication started at " + DateTime.Now + ".\r\n------");
        }

        public static void WriteLine()
        {
            WriteLine(() => string.Empty);
        }

        public static void WriteLine(Func<string> action)
        {
            if (!Enabled)
                return;

            string message;
            try
            {
                message = action();
            }
            catch (Exception ex)
            {
                message = "Cannot create a trace message for the specified action. The exception is: \r\n" + ex;
            }

            Trace.WriteLine(message);
        }

        public static void WriteLineIf<T>(Func<bool> predicate, Func<string> action)
        {
            if (!Enabled)
                return;

            string message;
            try
            {
                if (!predicate())
                    return;
                
                message = action();
            }
            catch (Exception ex)
            {
                message = "Cannot create a trace message for the specified action. The exception is: \r\n" + ex;
            }

            Trace.WriteLine(message);
        }

        public static void TraceError(Func<string> action)
        {
            if (!Enabled)
                return;

            string message;
            try
            {
                message = action();
            }
            catch (Exception ex)
            {
                message = "Cannot create a trace error message for the specified action. The exception is: \r\n" + ex;
            }

            Trace.TraceError(message);
        }
    }
}
