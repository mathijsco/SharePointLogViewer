using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.EventArguments
{
    public class ExportEntriesEventArgs : EventArgs
    {
        public ExportEntriesEventArgs(string path)
        {
            this.Path = path;
        }

        public string Path { get; private set; }
    }
}
