using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SharePointLogViewer.Common.Enums;
using SharePointLogViewer.DataSources.Uls.Locations;

namespace SharePointLogViewer.DataSources.Uls
{
    /// <summary>
    /// Class to build an ULS Log Entry based on the content of the created logs.
    /// </summary>
    internal class UlsLogEntryBuilder
    {
        private readonly ILocation _location;
        private bool _isExpectingMore;
        private UlsLogEntry _currentEntry;

        public UlsLogEntryBuilder(ILocation location)
        {
            if (location == null) throw new ArgumentNullException("location");
            _location = location;
        }

        /// <summary>
        /// An entry is completed and can be read.
        /// </summary>
        public bool EntryIsReady
        {
            get { return !_isExpectingMore && _currentEntry != null; }
        }

        /// <summary>
        /// The ULS entry that has been build.
        /// </summary>
        public UlsLogEntry Entry
        {
            get
            {
                if (!this.EntryIsReady)
                    return null;
                return _currentEntry;
            }
        }

        /// <summary>
        /// Add a new line and parse it.
        /// </summary>
        /// <param name="line"></param>
        public void AddLine(string line)
        {
            var split = line.Split('\t');
            // Trim all the entries
            for (int i = 0; i < split.Length; i++)
                split[i] = split[i].Trim();

            try
            {
                // Beginning of an entry
                if (!_isExpectingMore)
                {
                    _currentEntry = new UlsLogEntry();

                    _currentEntry.Timestamp = _location.ConvertToLocalTime(DateTime.ParseExact(split[0].TrimEnd('*'), "MM/dd/yyyy HH:mm:ss.ff", CultureInfo.InvariantCulture));
                    _currentEntry.Process = Regex.Match(split[1], @"^(.*)(?= \(0x[0-9a-fA-F]+\))").Value;
                    _currentEntry.ProcessId = int.Parse(Regex.Match(split[1], @"(?<= \(0x)[0-9a-fA-F]+(?=\))").Value,
                                                        NumberStyles.HexNumber);
                    _currentEntry.Tid = Convert.ToInt16(split[2], 16);
                    _currentEntry.Area = split[3];
                    _currentEntry.Category = split[4];
                    _currentEntry.EventId = split[5];

                    var level = ErrorLevel.Unknown;
                    Enum.TryParse(split[6], true, out level);
                    _currentEntry.Level = level;

                    _currentEntry.Message = split[7];

                    Guid g;
                    if (Guid.TryParse(split[8], out g))
                        _currentEntry.CorrelationId = g;

                    if (_currentEntry.Message.EndsWith("..."))
                    {
                        _currentEntry.Message = _currentEntry.Message.Remove(_currentEntry.Message.Length - 3);
                        _isExpectingMore = true;
                    }
                }
                // The rest of an entry.
                else
                {
                    _currentEntry.Message += split[7].Remove(0, 3);
                    // Expect even more lines.
                    if (_currentEntry.Message.EndsWith("..."))
                        _currentEntry.Message = _currentEntry.Message.Remove(_currentEntry.Message.Length - 3);
                    else
                        _isExpectingMore = false;
                }
            }
            catch (Exception)
            {
                _currentEntry = null;
                _isExpectingMore = false;
            }
        }
    }
}
