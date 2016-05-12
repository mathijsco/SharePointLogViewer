using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.DataSources.Uls
{
    public class UlsLogEntryExporter
    {
        private readonly string _path;

        public UlsLogEntryExporter(string path)
        {
            _path = path;
        }

        public void Export(IList<UlsLogEntry> entries)
        {
            if (File.Exists(_path))
                File.Delete(_path);

            File.WriteAllText(_path, BuildContent(entries));
        }

        private static string BuildContent(IList<UlsLogEntry> entries)
        {
            var builder = new StringBuilder();

            builder.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "{0,-23}\t{1,-40}\t{2,-6}\t{3,-30}\t{4,-30}\t{5,-7}\t{6,-10}\t{7,-8}\t{8}",
                "Timestamp", "Process", "TID", "Area", "Category", "EventID", "Level", "Message", "Correlation"
            ));

            for (int i = entries.Count - 1; i >= 0; i--)
            {
                var entry = entries[i];

                var currentMessage = entry.Message;
                var firstEntry = true;
                while (currentMessage.Length > 0)
                {
                    var take = firstEntry ? 800 : 797;
                    var me = currentMessage.Length > take ? currentMessage.Substring(0, take) : currentMessage;
                    currentMessage = currentMessage.Substring(Math.Min(take, currentMessage.Length));
                    me = string.Concat(!firstEntry ? "..." : string.Empty, me, currentMessage.Length > 0 ? "..." : string.Empty);

                    // 7: max 800 + 3 ...
                    builder.AppendLine(string.Format(CultureInfo.InvariantCulture,
                        "{0,-23}\t{1,-40}\t{2,-6}\t{3,-30}\t{4,-30}\t{5,-7}\t{6,-10}\t{7}\t{8}",
                        string.Format(CultureInfo.InvariantCulture, "{0:d} {0:T}.{0:ff}{1}", entry.Timestamp, firstEntry ? string.Empty : "*"),
                        string.Format("{0} (0x{1:X4})", entry.Process, entry.ProcessId),
                        string.Format("0x{0:X4}", entry.Tid),
                        entry.Area,
                        entry.Category,
                        entry.EventId,
                        entry.Level,
                        me,
                        entry.CorrelationId
                    ));

                    firstEntry = false;
                }
            }

            return builder.ToString();
        }
    }
}
