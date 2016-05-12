using System.Collections.Generic;

namespace SharePointLogViewer.DataSources.Uls.Readers
{
    internal interface IUlsLazyReader
    {
        IEnumerable<UlsLogEntry> ReadItems();
    }
}
