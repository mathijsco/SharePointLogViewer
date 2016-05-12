using System;
using SharePointLogViewer.DataSources.Uls.Readers;

namespace SharePointLogViewer.DataSources.Uls.Locations
{
    /// <summary>
    /// Class that is a wrapper for the File reader. This class maps all ILocation members to the original location, but only changes the path.
    /// </summary>
    internal sealed class InternalFileLocation : ILocation
    {
        private readonly ILocation _originalLocation;
        private readonly string _filePath;

        public InternalFileLocation(ILocation originalLocation, string filePath)
        {
            _originalLocation = originalLocation;
            _filePath = filePath;
        }

        public bool IsAvailable()
        {
            return _originalLocation.IsAvailable();
        }

        public bool CanChangeTimeLimit()
        {
            return _originalLocation.CanChangeTimeLimit();
        }

        public string GetPath()
        {
            return _filePath;
        }

        public void DetectFileTime(string path)
        {
            _originalLocation.DetectFileTime(path);
        }

        public DateTime ConvertToLocalTime(DateTime dateTime)
        {
            return _originalLocation.ConvertToLocalTime(dateTime);
        }

        public IUlsReader CreateReader()
        {
            throw new NotSupportedException();
        }
    }
}
