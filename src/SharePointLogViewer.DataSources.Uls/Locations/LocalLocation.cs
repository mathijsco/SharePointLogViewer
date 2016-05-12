using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using SharePointLogViewer.DataSources.Uls.Readers;
using System.IO;

namespace SharePointLogViewer.DataSources.Uls.Locations
{
    public class LocalLocation : ILocation
    {
        public bool IsAvailable()
        {
            return true;
        }

        public bool CanChangeTimeLimit()
        {
            return true;
        }

        public string GetPath()
        {
            RegistryKey regKey;
            if (Environment.Is64BitOperatingSystem)
                regKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            else
                regKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            regKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Shared Tools\Web Server Extensions\14.0\WSS");

            if (regKey == null)
                throw new InvalidOperationException("The SharePoint installation cannot be found on this computer.\r\nUse a different source to view ULS logs.");

            var path = regKey.GetValue("LogDir") as string;

            // Replace the path to link this 32 bit application to the 64 bit program files directory.
            if (Environment.Is64BitOperatingSystem)
                path = Regex.Replace(path, "%(.*Program)Files%", "%$1W6432%");

            return Environment.ExpandEnvironmentVariables(path);
        }

        public void DetectFileTime(string path)
        {
            Trace.WriteLine("Time offset for '" + Path.GetFileName(path) + "': 0 hours");
        }

        public DateTime ConvertToLocalTime(DateTime dateTime)
        {
            return dateTime;
        }

        public IUlsReader CreateReader()
        {
            return new UlsDirectoryReader(this);
        }
    }
}
