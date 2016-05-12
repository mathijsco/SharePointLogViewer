using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.EventArguments
{
    public class SimpleFilterHistoryEventArgs : EventArgs
    {
        public bool HasFiltersInHistory { get; set; }
    }
}
