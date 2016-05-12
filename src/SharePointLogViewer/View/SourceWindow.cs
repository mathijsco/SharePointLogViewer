using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharePointLogViewer.EventArguments;

namespace SharePointLogViewer.View
{
    public partial class SourceWindow : Form
    {
        public event EventHandler<SourceChangedEventArgs> SourceChanged;

        public SourceWindow(string hostName, string path)
        {
            InitializeComponent();

            txtRemoteHostName.Text = hostName ?? Settings.LastRemoteHostName;
            txtRemoteShare.Text = path ?? Settings.LastRemoteShareName;
            cmbConnectOption.SelectedIndex = (path != null ? 1 : 0) + (hostName != null ? 2 : 0);
        }

        private void SourceWindow_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SourceWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SourceChangedEventArgs sourceChangedArgs;
            switch (cmbConnectOption.SelectedIndex)
            {
                case 0: // My computer
                    sourceChangedArgs = new SourceChangedEventArgs();
                    break;
                case 2: // Remote using admin
                    sourceChangedArgs = new SourceChangedEventArgs(hostName: txtRemoteHostName.Text.Trim());
                    Settings.LastRemoteHostName = txtRemoteHostName.Text;
                    break;
                case 3: // Remote share
                    sourceChangedArgs = new SourceChangedEventArgs(hostName: txtRemoteHostName.Text.Trim(), location: txtRemoteShare.Text.Trim());
                    Settings.LastRemoteHostName = txtRemoteHostName.Text;
                    Settings.LastRemoteShareName = txtRemoteShare.Text;
                    break;
                default:
                    throw new NotSupportedException();
            }
            
            SourceChanged(this, sourceChangedArgs);
            this.Close();
        }

        private void cmbConnectOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbConnectOption.SelectedIndex)
            {
                case 0: // My computer
                    panelChooseFile.Visible = false;
                    panelRemote.Visible = false;
                    panelRemoteShare.Visible = false;
                    btnOk.Enabled = true;
                    break;
                case 1: // Choose file
                    panelChooseFile.Visible = true;
                    panelRemote.Visible = false;
                    panelRemoteShare.Visible = false;
                    btnOk.Enabled = false;
                    break;
                case 2: // Remote using admin
                    panelChooseFile.Visible = false;
                    panelRemote.Visible = true;
                    panelRemoteShare.Visible = false;
                    btnOk.Enabled = true;
                    txtRemoteHostName.Focus();
                    break;
                case 3: // Remote share
                    panelChooseFile.Visible = false;
                    panelRemote.Visible = true;
                    panelRemoteShare.Visible = true;
                    btnOk.Enabled = true;
                    txtRemoteHostName.Focus();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
