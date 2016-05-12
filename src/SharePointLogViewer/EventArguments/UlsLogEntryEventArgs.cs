using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharePointLogViewer.DataSources.Uls;

namespace SharePointLogViewer.EventArguments
{
    public class UlsLogEntryEventArgs : EventArgs
    {
        public UlsLogEntryEventArgs(int index)
        {
            this.Index = index;
        }

        public UlsLogEntryEventArgs(UlsLogEntry entry)
        {
            this.Entry = entry;
        }

        public int Index { get; set; }

        public UlsLogEntry Entry { get; set; }
    }
}
