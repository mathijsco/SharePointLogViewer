using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.Common.Filters
{
    public class EqualsFilterCondition<TValue>
        : IFilterCondition<TValue> where TValue : IEquatable<TValue>
    {
        public EqualsFilterCondition(TValue value)
        {
            this.Value = value;
        }

        public TValue Value { get; private set; }

        public bool IsMatch(TValue compareTo)
        {
            return this.Value.Equals(compareTo);
        }
    }
}
