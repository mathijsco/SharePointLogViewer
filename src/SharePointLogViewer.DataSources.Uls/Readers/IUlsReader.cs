using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.DataSources.Uls.Readers
{
    public interface IUlsReader
    {
        UlsCache GetCache();
    }
}
