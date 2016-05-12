using System;

namespace SharePointLogViewer.EventArguments
{
    public class TimeLimitChangedEventArgs : EventArgs
    {
        public TimeLimitChangedEventArgs(TimeSpan timeSpan)
        {
            this.Time = timeSpan;
        }

        public TimeSpan Time { get; private set; }
    }
}
