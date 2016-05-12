using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharePointLogViewer.View
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();

            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            chkShortTimestamp.Checked = Settings.ShortTimeStamp;
            chkRemoveFilter.Checked = Settings.LoseFilterForRefresh;
            chkEnableTracing.Checked = Settings.EnableDiagnostics;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (chkEnableTracing.Checked && !Settings.EnableDiagnostics)
                MessageBox.Show(@"To enable diagnostics, please restart the application.", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (!chkEnableTracing.Checked && Settings.EnableDiagnostics)
                MessageBox.Show(@"To disable diagnostics, please restart the application.", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Settings.ShortTimeStamp = chkShortTimestamp.Checked;
            Settings.LoseFilterForRefresh = chkRemoveFilter.Checked;
            Settings.EnableDiagnostics = chkEnableTracing.Checked;

            this.DialogResult = DialogResult.OK;
        }
    }
}
