using System.ComponentModel;
using SharePointLogViewer.Common.IO;

namespace SharePointLogViewer
{
    public class Settings : ApplicationSettingsBase<Settings>
    {
        private Settings() { }

        [DefaultValue(true)]
        public static bool ShortTimeStamp
        {
            get { return Instance.Get<bool>("ShortTimeStamp"); }
            set { Instance.Set(value, "ShortTimeStamp"); }
        }

        [DefaultValue(0)]
        public static int DefaultTimeLimitIndex
        {
            get { return Instance.Get<int>("DefaultTimeLimitIndex"); }
            set { Instance.Set(value, "DefaultTimeLimitIndex"); }
        }

        [DefaultValue(false)]
        public static bool EnableDiagnostics
        {
            get { return Instance.Get<bool>("EnableDiagnostics"); }
            set { Instance.Set(value, "EnableDiagnostics"); }
        }

        [DefaultValue(true)]
        public static bool LoseFilterForRefresh
        {
            get { return Instance.Get<bool>("LoseFilterForRefresh"); }
            set { Instance.Set(value, "LoseFilterForRefresh"); }
        }

        public static string LastRemoteHostName
        {
            get { return Instance.Get<string>("LastRemoteHostName"); }
            set { Instance.Set(value, "LastRemoteHostName"); }
        }

        [DefaultValue("UlsLogs")]
        public static string LastRemoteShareName
        {
            get { return Instance.Get<string>("LastRemoteShareName"); }
            set { Instance.Set(value, "LastRemoteShareName"); }
        }
    }
}
