using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.Mvp
{
    public class SimpleValueEventArgs<T> : EventArgs
    {
        public SimpleValueEventArgs()
        {
            
        }

        public SimpleValueEventArgs(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }
    }
}
