using System.Diagnostics;
using System.Text.RegularExpressions;
using SharePointLogViewer.DataSources.Uls.Locations;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharePointLogViewer.DataSources.Uls.Readers
{
    public class UlsDirectoryReader : IUlsLazyReader, IUlsReader
    {
        private readonly ILocation _location;
        private string _locationString;

        public UlsDirectoryReader(ILocation location)
        {
            _location = location;
        }

        private string LocationString
        {
            get { return _locationString = (_locationString ?? _location.GetPath()); }
        }

        private IEnumerable<string> GetFiles()
        {
            Trace.WriteLine("Get all the *.log files from '" + this.LocationString + "'... ");
            var stopwatch = Stopwatch.StartNew();
            var files = Directory.GetFiles(this.LocationString, "*.log")
                .Where(s => Regex.Match(s, "20[0-9]{6}-[0-9]{4}\\.log$", RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
                .OrderByDescending(Path.GetFileName).ToList();
            Trace.WriteLine("done (" + stopwatch.ElapsedMilliseconds + " ms)");

            return files;
        }

        public IEnumerable<UlsLogEntry> ReadItems()
        {
            Trace.WriteLine("Creating ULS file readers for the directory...");
            foreach (var fileInfo in GetFiles())
            {
                Trace.WriteLine("Creating ULS file readers for file '" + Path.GetFileName(fileInfo) + "' and start reading... ");
                var stopwatch = Stopwatch.StartNew();

                IUlsLazyReader reader = new UlsFileReader(new InternalFileLocation(_location, fileInfo));
                foreach (var entry in reader.ReadItems())
                {
                    yield return entry;
                }
                Trace.WriteLine("done (" + stopwatch.ElapsedMilliseconds + " ms)");
            }
        }

        public UlsCache GetCache()
        {
            return new UlsCache(this);
        }
    }
}
