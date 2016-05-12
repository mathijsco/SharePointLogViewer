using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.Common.Filters
{
    public class ExcludeFilterCondition<TValue> : IFilterCondition<TValue>
    {
        public ExcludeFilterCondition(TValue value)
        {
            this.Value = value;
        }

        public TValue Value { get; private set; }

        public bool IsMatch(TValue compareTo)
        {
            var valueText = this.Value.ToString();
            var compareToText = compareTo.ToString();

            return valueText.IndexOf(compareToText, StringComparison.OrdinalIgnoreCase) < 0;
        }
    }
}
