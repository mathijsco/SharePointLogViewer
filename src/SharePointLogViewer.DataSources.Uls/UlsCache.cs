using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SharePointLogViewer.Common.Filters;
using SharePointLogViewer.DataSources.Uls.Readers;
using SharePointLogViewer.DataSources.Uls.Utilities;

namespace SharePointLogViewer.DataSources.Uls
{
    /// <summary>
    /// Class that contains the ULS log entries cache.
    /// </summary>
    public class UlsCache
    {
        private readonly IList<UlsLogEntry> _entryContainer;
        private readonly IList<UlsLogEntry> _currentEntryContainer;
        private readonly IUlsLazyReader _reader;
        private IEnumerator<UlsLogEntry> _lazyReaderHandle;
        private DateTime _startDateTime;

        internal UlsCache(IUlsLazyReader reader)
        {
            _reader = reader;
            _entryContainer = new Collection<UlsLogEntry>();
            _currentEntryContainer = new Collection<UlsLogEntry>();
        }

        public IList<UlsLogEntry> Entries
        {
            get { return _currentEntryContainer; }
        }

        public void ClearCache()
        {
            Trace.WriteLine("Clearing the ULS cache.");

            _lazyReaderHandle = null;
            _entryContainer.Clear();
            _currentEntryContainer.Clear();
        }

        public Task<IList<UlsLogEntry>> ReadAsync(SimpleFilter filter)
        {
            return Task.Factory.StartNew(() => (IList<UlsLogEntry>)FindInternal(filter).ToList());
        }

        private IEnumerable<UlsLogEntry> FindInternal(SimpleFilter filter)
        {
            // Get the enumerator to loop through the items. Keep it in a variable to loop it once.
            if (_lazyReaderHandle == null)
            {
                _lazyReaderHandle = _reader.ReadItems().GetEnumerator();
                // Reset the start date time to always return the last # minutes.
                _startDateTime = DateTime.Now;
            }

            var timeLimit = DateTime.MinValue;
            if (filter.TimeLimit != TimeSpan.Zero)
            {
                Trace.WriteLine("Searching for ULS entries that are starting from " + _startDateTime + ".");
                timeLimit = _startDateTime - filter.TimeLimit;
            }


            _currentEntryContainer.Clear();

            // Return the cached items
            Trace.WriteLine("Reading entries from the cache...");
            foreach (var entry in _entryContainer.Where(e => FilterHelper.EntryIsMatch(e, filter)))
            {
                if (entry.Timestamp < timeLimit)
                    yield break;

                _currentEntryContainer.Add(entry);

                yield return entry;
            }

            // Search for new entries in the files
            Trace.WriteLine("Reading entries from the ULS files...");
            while (_lazyReaderHandle.MoveNext())
            {
                var entry = _lazyReaderHandle.Current;

                if (entry.Timestamp < timeLimit)
                    yield break;

                _entryContainer.Add(entry);

                if (!FilterHelper.EntryIsMatch(entry, filter))
                    continue;

                _currentEntryContainer.Add(entry);

                yield return entry;
            }
            Trace.WriteLine("Done reading from the ULS files.");
        }

        
    }
}
