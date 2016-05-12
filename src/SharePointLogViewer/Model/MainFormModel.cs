using System;
using System.Collections.Generic;
using System.Linq;
using SharePointLogViewer.Common.Enums;
using SharePointLogViewer.Common.Filters;
using SharePointLogViewer.DataSources.Uls.Locations;
using SharePointLogViewer.Mvp;
using SharePointLogViewer.EventArguments;
using SharePointLogViewer.DataSources.Uls;

namespace SharePointLogViewer.Model
{
    public class MainFormModel : MvpModel, IMainFormModel
    {
        public event EventHandler<LoadingUlsEntriesCompletedEventArgs> LoadingUlsEntriesCompleted;

        private ILocation _currentLocation;
        private string _currentHostName;
        private string _currentPath;
        private UlsCache _currentCache;
        private SimpleFilter _filter;
        private TimeSpan _timeLimit;
        private Stack<SimpleFilter> _filterHistory;

        public MainFormModel()
        {
            _filterHistory = new Stack<SimpleFilter>();
        }

        public SimpleFilter Filter
        {
            get
            {
                _filter.TimeLimit = _timeLimit;
                return _filter;
            }
            set
            {
                if (!_filter.Equals(value))
                    _filterHistory.Push(_filter);

                _filter = value;
            }
        }

        public bool HasFiltersInHistory
        {
            get { return _filterHistory.Count > 0; }
        }

        public UlsLogEntry GetEntry(int index)
        {
            return _currentCache.Entries[index];
        }

        public void ExportEntries(string path)
        {
            var exportManager = new UlsLogEntryExporter(path);
            exportManager.Export(_currentCache.Entries);
        }

        public TimeSpan[] GetTimeLimits()
        {
            return new TimeSpan[]
            {
                new TimeSpan(0, 2, 0),
                new TimeSpan(0, 5, 0),
                new TimeSpan(0, 10, 0),
                new TimeSpan(0, 30, 0),
                new TimeSpan(1, 0, 0)
            };
        }

        public void ChangeTimeLimit(TimeSpan timeSpan)
        {
            _timeLimit = timeSpan;
        }

        public int FindFirstException(int startIndex)
        {
            startIndex++;

            for (int i = startIndex; i < _currentCache.Entries.Count; i++)
            {
                var entry = _currentCache.Entries[i];
                if (entry.Level >= ErrorLevel.Critical && entry.Message.IndexOf("exception", StringComparison.OrdinalIgnoreCase) >= 0)
                    return i;
            }
            return -1;
        }

        public void ChangeSource(SourceChangedEventArgs e)
        {
            // Get the location provider from the event args provided values.
            if (e.HostName != null)
            {
                if (e.Location != null)
                    _currentLocation = new RemoteShareLocation(e.HostName, e.Location);
                else
                    _currentLocation = new RemoteAdminLocation(e.HostName);
            }
            else
            {
                if (e.Location != null)
                {
                    _currentLocation = new StaticLocation(e.Location);
                }
                else
                    _currentLocation = new LocalLocation();
            }

            _currentHostName = e.HostName;
            _currentPath = e.Location;

            e.CanChangeTimeLimitResult = _currentLocation.CanChangeTimeLimit();
            
            var reader = _currentLocation.CreateReader();
            _currentCache = reader.GetCache();
        }

        public void PreviousFilter()
        {
            if (_filterHistory.Count <= 0)
                throw new InvalidOperationException("There is no history available. Cannot load the previous filter.");

            _filter = _filterHistory.Pop();
            RefreshEntriesAsync(false);
        }

        public void RefreshEntriesAsync(bool clearCache)
        {
            if (_currentCache == null)
                return;

            if (clearCache)
                _currentCache.ClearCache();

            var filter = this.Filter;
            if (!_currentLocation.CanChangeTimeLimit())
                filter.TimeLimit = TimeSpan.Zero;

            _currentCache.ReadAsync(filter).ContinueWith(t =>
                this.LoadingUlsEntriesCompleted(this, new LoadingUlsEntriesCompletedEventArgs(
                    !t.IsFaulted ? t.Result : null,
                    t.IsFaulted ? t.Exception.InnerException : null,
                    _currentHostName, _currentPath
                ))
            );
        }

        public int FindIndexFor(UlsLogEntry entry)
        {
            var index = -1;
            if (_currentCache.Entries.Any(e => { index++; return e == entry; }))
                return index;
            return -1;
        }
    }

    public interface IMainFormModel
    {
        event EventHandler<LoadingUlsEntriesCompletedEventArgs> LoadingUlsEntriesCompleted;

        SimpleFilter Filter { get; set; }

        bool HasFiltersInHistory { get; }

        UlsLogEntry GetEntry(int index);

        void ExportEntries(string path);

        TimeSpan[] GetTimeLimits();

        void ChangeTimeLimit(TimeSpan timeSpan);

        int FindFirstException(int startIndex);

        void ChangeSource(SourceChangedEventArgs e);

        void PreviousFilter();

        void RefreshEntriesAsync(bool clearCache);

        int FindIndexFor(UlsLogEntry entry);
    }
}
