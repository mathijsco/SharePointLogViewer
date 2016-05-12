using System.Globalization;
using SharePointLogViewer.Common.Enums;
using SharePointLogViewer.DataSources.Uls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SharePointLogViewer.Elements
{
    public class UlsLogEntryElement : ListViewItem
    {
        public UlsLogEntryElement(UlsLogEntry entry)
        {
            this.Entry = entry;

            InitializeElement();
            SetDisplay();
        }

        public UlsLogEntry Entry { get; private set; }

        private void InitializeElement()
        {
            //var score = (int)Math.Log((int)this.Entry.Level, 2);

            if (Settings.ShortTimeStamp && this.Entry.Timestamp.Date == DateTime.Now.Date)
                this.Text = string.Format(CultureInfo.CurrentCulture, "^ {0:T}", this.Entry.Timestamp);
            else
                this.Text = string.Format(CultureInfo.CurrentCulture, "{0}", this.Entry.Timestamp);

            
            this.SubItems.Add(string.Format(CultureInfo.CurrentCulture, "({0}) {1}", this.Entry.ProcessId, this.Entry.Process.ToLowerInvariant()));
            this.SubItems.Add(string.Format(CultureInfo.CurrentCulture, "{0}", this.Entry.Area));
            //this.SubItems.Add(
            //    string.Format(CultureInfo.CurrentCulture, "{0}", new string('\x2590', score > 0 ? score : 0)),
            //    SystemColors.WindowText,
            //    SystemColors.Window,
            //    new Font("Arial", SystemFonts.DefaultFont.Size, FontStyle.Regular)
            //);
            this.SubItems.Add(string.Format(CultureInfo.CurrentCulture, "{0}", this.Entry.Message));
        }

        private void SetDisplay()
        {
            switch (this.Entry.Level)
            {
                case ErrorLevel.Unexpected:
                    //this.ImageKey = @"Unexpected";
                    this.ImageIndex = 0;
                    this.BackColor = Color.Black;
                    this.ForeColor = Color.Salmon;
                    break;
                case ErrorLevel.Critical:
                    //this.ImageKey = @"Error";
                    this.ImageIndex = 1;
                    break;
                case ErrorLevel.Warning:
                case ErrorLevel.High:
                    //this.ImageKey = @"Medium";
                    this.ImageIndex = 2;
                    break;
                case ErrorLevel.Medium:
                case ErrorLevel.Verbose:
                case ErrorLevel.Monitorable:
                case ErrorLevel.Information:
                case ErrorLevel.Unknown:
                    //this.ImageKey = @"Info";
                    break;
            }
        }
    }
}
