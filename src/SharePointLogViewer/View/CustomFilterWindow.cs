using System;
using System.Windows.Forms;
using SharePointLogViewer.Common.Filters;
using SharePointLogViewer.EventArguments;

namespace SharePointLogViewer.View
{
    public partial class CustomFilterWindow : Form
    {
        public event EventHandler<SimpleFilterChangedEventArgs> FilterChanged;

        public CustomFilterWindow(SimpleFilter filter)
        {
            InitializeComponent();

            simpleFilterUserControl1.Filter = filter;
        }

        private void CustomFilterWindow_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (simpleFilterUserControl1.FilterChanged)
            {
                FilterChanged(this, new SimpleFilterChangedEventArgs(simpleFilterUserControl1.Filter));
            }
            
            Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!simpleFilterUserControl1.Filter.Equals(new SimpleFilter()) && !simpleFilterUserControl1.InitialFilterIsEmpty)
            {
                FilterChanged(this, new SimpleFilterChangedEventArgs(new SimpleFilter()));
            }
            Close();
        }

        private void CustomFilterWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
