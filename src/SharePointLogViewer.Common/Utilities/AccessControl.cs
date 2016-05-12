using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace SharePointLogViewer.Common.Utilities
{
    public static class AccessControl
    {
        public static bool HasReadAccess(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    var accessControl = Directory.GetAccessControl(path);
                    var access = accessControl.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));

                    return access.Cast<FileSystemAccessRule>().Any(a => a.FileSystemRights.HasFlag(FileSystemRights.Read) && a.AccessControlType == AccessControlType.Allow);
                }

                if (File.Exists(path))
                {
                    using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        return stream.CanRead;
                    }
                }
            }
            catch { }
            return false;
        }
    }
}
