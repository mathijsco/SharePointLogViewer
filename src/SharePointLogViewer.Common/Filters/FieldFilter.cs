using System;
using SharePointLogViewer.Common.Enums;

namespace SharePointLogViewer.Common.Filters
{
    public class FieldFilter
    {
        public IFilterCondition<TimeSpan> TimeLimit { get; set; }

        public IFilterCondition<Guid> CorrelationId { get; set; }

        public IFilterCondition<string> AnyText { get; set; }

        public IFilterCondition<ErrorLevel> ErrorLevel { get; set; }
    }
}
