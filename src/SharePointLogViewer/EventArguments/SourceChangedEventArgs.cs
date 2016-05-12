using System;

namespace SharePointLogViewer.EventArguments
{
    public class SourceChangedEventArgs : EventArgs
    {
        public SourceChangedEventArgs(string location = null, string hostName = null)
        {
            this.Location = location;
            this.HostName = hostName;
        }

        public string Location { get; private set; }

        public string HostName { get; private set; }


        public bool CanChangeTimeLimitResult { get; set; }
    }
}
