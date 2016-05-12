using System;

namespace SharePointLogViewer.EventArguments
{
    public class DefaultLoadingEventArgs : EventArgs
    {
        public TimeSpan[] TimeLimitation { get; set; }
    }
}
