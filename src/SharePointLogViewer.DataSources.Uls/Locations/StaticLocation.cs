using System;
using System.Diagnostics;
using System.IO;
using SharePointLogViewer.Common.Utilities;
using SharePointLogViewer.DataSources.Uls.Readers;
using SharePointLogViewer.DataSources.Uls.Utilities;

namespace SharePointLogViewer.DataSources.Uls.Locations
{
    public class StaticLocation : ILocation
    {
        private readonly string _location;
        private readonly bool _detectTimeOffset;
        private bool _timeSet;
        private TimeSpan _timeDifference;

        public StaticLocation(string location)
            : this(location, false)
        {
        }

        public StaticLocation(string location, bool detectTimeOffset)
        {
            _location = location;
            _detectTimeOffset = detectTimeOffset;
        }

        public bool IsAvailable()
        {
            return AccessControl.HasReadAccess(_location);
        }

        public bool CanChangeTimeLimit()
        {
            if (File.Exists(_location))
                return false;
            return true;
        }

        public string GetPath()
        {
            return _location;
        }

        public void DetectFileTime(string path)
        {
            if (_timeSet) return;

            if (_detectTimeOffset)
            {
                _timeDifference = LogFileUtility.GetTimeDifference(path);
                _timeSet = true;
                Trace.WriteLine("Time offset for '" + Path.GetFileName(path) + "': " + _timeDifference.TotalHours + " hours.");
            }
            else
            {
                Trace.WriteLine("Time offset for '" + Path.GetFileName(path) + "': 0 hours (disabled).");
            }
        }

        public DateTime ConvertToLocalTime(DateTime dateTime)
        {
            if (_detectTimeOffset)
                return new DateTime((dateTime + _timeDifference).Ticks, DateTimeKind.Local);
            return dateTime;
        }

        public IUlsReader CreateReader()
        {
            if (File.Exists(_location))
                return new UlsFileReader(this);
            return new UlsDirectoryReader(this);
        }
    }
}
