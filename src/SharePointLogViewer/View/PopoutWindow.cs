using SharePointLogViewer.DataSources.Uls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharePointLogViewer.View
{
    public partial class PopoutWindow : Form
    {
        public PopoutWindow(UlsLogEntry entry)
        {
            InitializeComponent();

            txtMessage.Font = new Font(FontFamily.GenericMonospace, SystemFonts.DefaultFont.Size);

            txtMessage.Text = entry.GetStackTrace();
            txtMessage.SelectionLength = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
