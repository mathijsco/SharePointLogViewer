using System;
using System.Text.RegularExpressions;
using SharePointLogViewer.Common.Enums;

namespace SharePointLogViewer.DataSources.Uls
{
    public class UlsLogEntry
    {
        public DateTime Timestamp { get; set; }

        public string Process { get; set; }

        public int ProcessId { get; set; }

        public short Tid { get; set; }

        public string Area { get; set; }

        public string Category { get; set; }

        public string EventId { get; set; }

        public ErrorLevel Level { get; set; }

        public string Message { get; set; }

        public Guid? CorrelationId { get; set; }

        public string GetStackTrace()
        {
            var message = this.Message;
            message = Regex.Replace(message, "(?=-{3}\\>)", Environment.NewLine);
            message = Regex.Replace(message, "(?=-{3} End)", Environment.NewLine);
            message = Regex.Replace(message, "(?= {4}at)", Environment.NewLine);
            message = Regex.Replace(message, "(?= {3}Server stack trace)", Environment.NewLine + Environment.NewLine);

            return message;
        }

        public override int GetHashCode()
        {
            return (int) (this.Timestamp.Ticks & 0xFF00 >> 8)
                   ^ (int) (this.Timestamp.Ticks & 0x00FF)
                   ^ this.Message.GetHashCode();
        }
    }
}
