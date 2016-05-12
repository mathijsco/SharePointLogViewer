using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using SharePointLogViewer.DataSources.Uls.Readers;

namespace SharePointLogViewer.DataSources.Uls.Locations
{
    public class RemoteAdminLocation : ILocation
    {
        private readonly string _machineName;
        private string _ulsDirectoryLocation;
        private string _timeZone;

        public RemoteAdminLocation(string machineName)
        {
            _machineName = machineName;
        }

        public bool IsAvailable()
        {
            try
            {
                using (RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, _machineName))
                { }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CanChangeTimeLimit()
        {
            return true;
        }

        public string GetPath()
        {
            if (_ulsDirectoryLocation != null)
                return _ulsDirectoryLocation;

            bool is64bit;

            // Check if remote system is x64 bit
            using (var reg = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, _machineName))
            using (var key = reg.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\"))
            {
                is64bit = key.GetValue("ProgramFilesDir (x86)") != null;
            }

            RegistryKey regKey;
            if (is64bit)
                regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, _machineName, RegistryView.Registry64);
            else
                regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, _machineName, RegistryView.Registry32);
            regKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Shared Tools\Web Server Extensions\14.0\WSS");

            if (regKey == null)
            {
                throw new InvalidOperationException("Cannot access the registry of '" + _machineName + "'. Please check the connection.");
            }

            var path = (string)regKey.GetValue("LogDir");

            // Replace the path to link this 32 bit application to the 64 bit program files directory.
            if (is64bit)
                path = Regex.Replace(path, "%(.*Program)Files%", "%$1W6432%");

            path = Environment.ExpandEnvironmentVariables(path);

            // Replace C: to \\machineName\C$
            return _ulsDirectoryLocation = Regex.Replace(path, @"^([a-zA-Z]):", @"\\" + _machineName + @"\$1$");
        }

        public void DetectFileTime(string path)
        {
            if (_timeZone != null) return;

            _timeZone = GetTimeZone();
            Trace.WriteLine("Time offset for '" + Path.GetFileName(path) + "': " + _timeZone);
        }

        public DateTime ConvertToLocalTime(DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Unspecified)
                return dateTime.ToLocalTime();
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(_timeZone), TimeZoneInfo.Local);
        }

        public IUlsReader CreateReader()
        {
            return new UlsDirectoryReader(this);
        }

        private string GetTimeZone()
        {
            bool is64Bit = false;

            // Check if remote system is x64 bit
            using (var reg = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, _machineName))
            using (var key = reg.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\"))
            {
                if (key != null)
                    is64Bit = key.GetValue("ProgramFilesDir (x86)") != null;
            }

            RegistryKey regKey;
            if (is64Bit)
                regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, _machineName, RegistryView.Registry64);
            else
                regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, _machineName, RegistryView.Registry32);
            regKey = regKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\TimeZoneInformation");

            var timeZone = (string)regKey.GetValue("TimeZoneKeyName");
            return timeZone.TrimEnd('\0');
        }
    }
}
