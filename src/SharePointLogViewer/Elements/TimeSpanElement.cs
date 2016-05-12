using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.Elements
{
    public class TimeSpanElement
    {
        public TimeSpanElement(TimeSpan timeSpan)
        {
            this.TimeSpan = timeSpan;
        }

        public TimeSpan TimeSpan { get; private set; }

        public override string ToString()
        {
            var minutes = this.TimeSpan.TotalMinutes;
            if (minutes >= 1)
                return string.Format("{0:0.##} minute{1}", minutes, minutes != 1 ? "s" : string.Empty);
            return string.Format("{0:0.##} second{1}", this.TimeSpan.TotalSeconds, this.TimeSpan.TotalSeconds != 1 ? "s" : string.Empty);
        }
    }
}
