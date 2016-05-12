using System.Diagnostics;
using System.IO;
using SharePointLogViewer.Common.Utilities;
using System;
using SharePointLogViewer.DataSources.Uls.Readers;
using SharePointLogViewer.DataSources.Uls.Utilities;

namespace SharePointLogViewer.DataSources.Uls.Locations
{
    public class RemoteShareLocation : ILocation
    {
        private readonly string _machineName;
        private readonly string _shareName;
        private bool _timeSet;
        private TimeSpan _timeDifference;

        public RemoteShareLocation(string machineName, string shareName)
        {
            _machineName = machineName;
            _shareName = shareName;
        }

        public bool CanChangeTimeLimit()
        {
            return true;
        }

        public string GetPath()
        {
            return "\\\\" + _machineName + "\\" + _shareName.TrimStart('\\');
        }

        public void DetectFileTime(string path)
        {
            if (_timeSet) return;

            _timeDifference = LogFileUtility.GetTimeDifference(path);
            _timeSet = true;
            Trace.WriteLine("Time offset for '" + Path.GetFileName(path) + "': " + _timeDifference.TotalHours + " hours.");
        }

        public DateTime ConvertToLocalTime(DateTime dateTime)
        {
            return new DateTime((dateTime + _timeDifference).Ticks, DateTimeKind.Local);
        }

        public IUlsReader CreateReader()
        {
            return new UlsDirectoryReader(this);
        }

        public bool IsAvailable()
        {
            return AccessControl.HasReadAccess(GetPath());
        }
    }
}
