using System;
using SharePointLogViewer.Common.IO;
using SharePointLogViewer.DataSources.Uls.Locations;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SharePointLogViewer.DataSources.Uls.Readers
{
    public class UlsFileReader : IUlsLazyReader, IUlsReader
    {
        private readonly ContinuesBufferFileReader _fileReader;
        private readonly ILocation _location;
        private readonly string _locationString;
        private bool _firstLineSkipped;

        public UlsFileReader(ILocation location)
        {
            _locationString = location.GetPath();
            if (!File.Exists(_locationString))
                throw new FileNotFoundException("Cannot open the requested file. Be sure that the file exists.", _locationString);

            _location = location;
            _fileReader = new ContinuesBufferFileReader(_locationString);
        }

        private IEnumerable<UlsLogEntry> ReadItemsOldToNew()
        {
            Trace.WriteLine("Processing and build entries from file '" + _locationString + "'...");

            // Detect the time difference from the file and it's entries.
            _location.DetectFileTime(_locationString);

            var builder = new UlsLogEntryBuilder(_location);

            while (!_fileReader.EndOfFile)
            {
                var line = _fileReader.ReadLine();
                if (!_firstLineSkipped)
                {
                    if (line.IndexOf("Timestamp", StringComparison.OrdinalIgnoreCase) < 0
                        || line.IndexOf("Correlation", StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        throw new FileLoadException("Cannot scan the file '" + Path.GetFileName(_locationString) + "' because it is not a valid ULS log.", _locationString);
                    }

                    _firstLineSkipped = true;
                    continue;
                }

                builder.AddLine(line);
                if (builder.EntryIsReady)
                {
                    yield return builder.Entry;
                }
            }
            Trace.WriteLine("Processing done.");
        }

        public IEnumerable<UlsLogEntry> ReadItems()
        {
            return ReadItemsOldToNew().Reverse();
        }

        public UlsCache GetCache()
        {
            return new UlsCache(this);
        }
    }
}
