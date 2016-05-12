using System;
using SharePointLogViewer.Common.Filters;

namespace SharePointLogViewer.EventArguments
{
    public class SimpleFilterChangedEventArgs : SimpleFilterHistoryEventArgs
    {
        public SimpleFilterChangedEventArgs(SimpleFilter filter)
        {
            this.Filter = filter;
        }

        public SimpleFilter Filter { get; private set; }
    }
}
