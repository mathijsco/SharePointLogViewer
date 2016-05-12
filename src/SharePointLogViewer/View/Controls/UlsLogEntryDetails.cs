using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharePointLogViewer.DataSources.Uls;

namespace SharePointLogViewer.View.Controls
{
    public partial class UlsLogEntryDetails : UserControl
    {
        private UlsLogEntry _ulsLogEntry;

        public UlsLogEntryDetails()
        {
            InitializeComponent();
            SetUlsLogEntry(null);
        }

        public void SetUlsLogEntry(UlsLogEntry ulsLogEntry)
        {
            _ulsLogEntry = ulsLogEntry;

            if (_ulsLogEntry != null)
            {
                txtTimestamp.Text = ulsLogEntry.Timestamp.ToString("g");
                txtProcess.Text = string.Concat(_ulsLogEntry.Process, " (", _ulsLogEntry.ProcessId, ")");
                txtTid.Text = _ulsLogEntry.Tid.ToString();
                txtArea.Text = _ulsLogEntry.Area;
                txtCategory.Text = _ulsLogEntry.Category;
                txtEventId.Text = _ulsLogEntry.EventId;
                txtLevel.Text = _ulsLogEntry.Level.ToString();
                txtCorrelation.Text = string.Format("{0}", _ulsLogEntry.CorrelationId);
            }
            else
            {
                txtTimestamp.Text = "...";
                txtProcess.Text = string.Empty;
                txtTid.Text = string.Empty;
                txtArea.Text = string.Empty;
                txtCategory.Text = string.Empty;
                txtEventId.Text = string.Empty;
                txtLevel.Text = string.Empty;
                txtCorrelation.Text = string.Empty;
            }
        }
    }
}
