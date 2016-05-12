using System;
using SharePointLogViewer.Common.Enums;

namespace SharePointLogViewer.Common.Filters
{
    public struct SimpleFilter
    {
        public TimeSpan TimeLimit { get; set; }

        public string IncludeText { get; set; }

        public string ExcludeText { get; set; }

        public ErrorLevel ErrorLevel { get; set; }

        public bool IsEmpty
        {
            get { return this.Equals(new SimpleFilter()); }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SimpleFilter))
                return false;

            var other = (SimpleFilter) obj;
            return this.IncludeText == other.IncludeText
                   && this.ExcludeText == other.ExcludeText
                   && this.ErrorLevel == other.ErrorLevel;
        }
    }
}
