using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharePointLogViewer.Common.Enums;
using SharePointLogViewer.Common.Filters;

namespace SharePointLogViewer.EventArguments
{
    public class SimpleFilterOpeningEventArgs : EventArgs
    {
        public SimpleFilter Result { get; set; }
    }
}
