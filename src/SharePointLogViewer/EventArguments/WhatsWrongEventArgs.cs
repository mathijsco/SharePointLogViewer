using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.EventArguments
{
    public class WhatsWrongEventArgs : EventArgs
    {
        public WhatsWrongEventArgs(int startIndex)
        {
            this.StartIndex = startIndex;
        }

        public int StartIndex { get; private set; }

        public int ResultIndex { get; set; }
    }
}
