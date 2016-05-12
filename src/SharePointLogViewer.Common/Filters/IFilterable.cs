using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.Common.Filters
{
    public interface IFilterable
    {
        bool IsMatch(object entity);
    }
}
