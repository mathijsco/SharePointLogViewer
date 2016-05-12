using SharePointLogViewer.DataSources.Uls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.EventArguments
{
    public class LoadingUlsEntriesCompletedEventArgs : EventArgs
    {
        public IList<UlsLogEntry> Entries { get; private set; }

        public Exception Exception { get; private set; }

        public string OpenedHostName { get; private set; }

        public string OpenedLocation { get; private set; }

        public LoadingUlsEntriesCompletedEventArgs(IList<UlsLogEntry> entries, Exception exception, string openedHostName, string openedLocation)
        {
            Entries = entries;
            Exception = exception;
            OpenedHostName = openedHostName;
            OpenedLocation = openedLocation;
        }
    }
}
