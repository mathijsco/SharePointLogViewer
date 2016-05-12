using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SharePointLogViewer.Common.Enums;
using SharePointLogViewer.Common.Filters;

namespace SharePointLogViewer.DataSources.Uls.Utilities
{
    internal static class FilterHelper
    {
        public static bool EntryIsMatch(UlsLogEntry entry, SimpleFilter filter)
        {
            if (filter.ErrorLevel > ErrorLevel.Unknown
                && (entry.Level == ErrorLevel.Unknown
                || !filter.ErrorLevel.HasFlag(entry.Level)
                )) return false;

            if (filter.IncludeText != null)
            {
                var split = SplitCriteria(filter.IncludeText);
                foreach (var row in split)
                {
                    if (entry.Area.IndexOf(row, StringComparison.InvariantCultureIgnoreCase) < 0
                        && entry.Category.IndexOf(row, StringComparison.InvariantCultureIgnoreCase) < 0
                        && entry.Message.IndexOf(row, StringComparison.InvariantCultureIgnoreCase) < 0
                        && entry.Process.IndexOf(row, StringComparison.InvariantCultureIgnoreCase) < 0
                        && (entry.CorrelationId == null
                            || (entry.CorrelationId != null
                                && entry.CorrelationId.ToString()
                                     .IndexOf(row, StringComparison.InvariantCultureIgnoreCase) < 0
                               ))
                        ) return false;
                }
            }

            if (filter.ExcludeText != null)
            {
                var split = SplitCriteria(filter.ExcludeText);
                foreach (var row in split)
                {
                    if ((entry.Area.IndexOf(row, StringComparison.InvariantCultureIgnoreCase) >= 0
                         || entry.Category.IndexOf(row, StringComparison.InvariantCultureIgnoreCase) >= 0
                         || entry.Message.IndexOf(row, StringComparison.InvariantCultureIgnoreCase) >= 0
                         || entry.Process.IndexOf(row, StringComparison.InvariantCultureIgnoreCase) >= 0
                        )) return false;
                }
            }

            return true;
        }

        private static IList<string> SplitCriteria(string input)
        {
            var split = input.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length <= 1)
                return split;

            bool inGroup = false;
            var builder = new StringBuilder();
            var result = new Collection<string>();
            for (int i = 0; i < split.Length; i++)
            {
                var value = split[i];
                // Start the group
                if (!inGroup && value.Length > 0 && value[0] == '"')
                {
                    inGroup = true;
                    value = value.Remove(0, 1);
                }
                // End the group
                if (inGroup && value.Length > 0 && value[value.Length - 1] == '"')
                {
                    inGroup = false;
                    value = value.Remove(value.Length - 1);
                }

                // If there is an value, append it to the builder
                if (value.Length > 0)
                {
                    if (builder.Length > 0)
                        builder.Append(" ");
                    builder.Append(value);
                }
                // If the value is no longer in a group
                if (!inGroup && builder.Length > 0)
                {
                    result.Add(builder.ToString());
                    builder.Clear();
                }
            }

            // Clear the final buffer if it's filled
            if (builder.Length > 0)
                result.Add(builder.ToString());

            return result;
        }
    }
}
