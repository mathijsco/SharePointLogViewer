using SharePointLogViewer.DataSources.Uls.Readers;
using System;

namespace SharePointLogViewer.DataSources.Uls.Locations
{
    public interface ILocation
    {
        bool IsAvailable();

        bool CanChangeTimeLimit();

        string GetPath();

        void DetectFileTime(string path);

        DateTime ConvertToLocalTime(DateTime dateTime);

        IUlsReader CreateReader();
    }
}
