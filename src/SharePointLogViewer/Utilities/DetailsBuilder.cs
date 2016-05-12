using SharePointLogViewer.DataSources.Uls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointLogViewer.Utilities
{
    public static class DetailsBuilder
    {
        public static string Build(UlsLogEntry entry)
        {
            return string.Format(@"
                <table>
                <tr><td>Date and Time:</td><td>{0}</td></tr>
                <tr><td>Process:</td><td>{1}</td></tr>
                <tr><td>TID:</td><td>{2}</td></tr>
                <tr><td>Level:</td><td>{3}</td></tr>
                <tr><td>Category:</td><td>{4}</td></tr>
                <tr><td>Area:</td><td>{5}</td></tr>
                <tr><td>Correlation ID:</td><td>{6}</td></tr>
                </table>
            ", entry.Timestamp, entry.Process, entry.Tid, entry.Level, entry.Category, entry.Area, entry.CorrelationId);
        }
    }
}
