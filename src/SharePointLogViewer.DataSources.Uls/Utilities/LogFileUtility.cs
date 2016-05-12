using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace SharePointLogViewer.DataSources.Uls.Utilities
{
    public static class LogFileUtility
    {
        public static TimeSpan GetTimeDifference(string file)
        {
            Trace.WriteLine("Reading time difference from file '" + Path.GetFileName(file) + "'...");

            var fileName = Path.GetFileNameWithoutExtension(file);
            var createdOn = File.GetCreationTime(file);

            var fileNameDateString = Regex.Match(fileName, "20[0-9]{6}-[0-9]{4}$", RegexOptions.Compiled).Value;
            var fileNameDate = DateTime.ParseExact(fileNameDateString, "yyyyMMdd-HHmm", CultureInfo.InvariantCulture);

            var difference = createdOn - fileNameDate;
            Trace.WriteLine("-> File created: " + createdOn);
            Trace.WriteLine("-> File name detection: " + fileNameDate);
            Trace.WriteLine("-> Difference: " + difference);


            return difference;
        }

        //public static TimeZoneInfo GetTimeZoneFromFile(string file)
        //{



        //    //if (dateTime.Kind != DateTimeKind.Unspecified)
        //    //    return dateTime.ToLocalTime();
        //    //return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(GetTimeZone()), TimeZoneInfo.Local);

        //    var fileName = Path.GetFileNameWithoutExtension(file);
        //    var createdOn = File.GetCreationTimeUtc(file);

        //    var fileNameDateString = Regex.Match(fileName, "20[0-9]{6}-[0-9]{4}$", RegexOptions.Compiled).Value;
        //    var fileNameDate = DateTime.ParseExact(fileNameDateString, "yyyyMMdd-HHmm", CultureInfo.InvariantCulture);

        //    var diff = createdOn - fileNameDate;

        //    TimeZoneInfo.ClearCachedData();
        //    return TimeZoneInfo.CreateCustomTimeZone("FileTimeZone", diff, "FileTimeZone", "FileTimeZone");
        //}
    }
}
