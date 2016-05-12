using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using SharePointLogViewer.DataSources.Uls;
using SharePointLogViewer.Elements;
using SharePointLogViewer.EventArguments;
using SharePointLogViewer.Presenter;
using SharePointLogViewer.Properties;
using System.IO;
using SharePointLogViewer.Mvp;

namespace SharePointLogViewer.View
{
    public partial class MainForm : Form, IMainForm
    {
        #region Buttons

        public event EventHandler<DefaultLoadingEventArgs> DefaultLoading;
        public event EventHandler RefreshRequest;
        public event EventHandler<TimeLimitChangedEventArgs> TimeLimitChanged;
        public event EventHandler<SimpleFilterOpeningEventArgs> FilterOpening;
        public event EventHandler<SimpleFilterChangedEventArgs> FilterChanged;
        public event EventHandler<SimpleFilterHistoryEventArgs> FilterPreviousRequest;
        public event EventHandler<SourceChangedEventArgs> SourceChanged;
        public event EventHandler<UlsLogEntryEventArgs> UlsEntryRequest;
        public event EventHandler<UlsLogEntryEventArgs> FindUlsEntryRequest;
        public event EventHandler<WhatsWrongEventArgs> WhatsWrongRequest;
        public event EventHandler<ExportEntriesEventArgs> ExportEntriesRequest;

        private void OnSourceChanged(SourceChangedEventArgs e)
        {
            StartLoadingList();
            SourceChanged(this, e);

            _canChangeTimeLimit = e.CanChangeTimeLimitResult;
            tcmbTimeLimits.Enabled = e.CanChangeTimeLimitResult;
        }

        #endregion

        private UlsLogEntryElement[] _entryElements;
        private UlsLogEntry _lastSelectedEntry;
        private bool _canChangeTimeLimit;
        private string _lastOpenedHostName;
        private string _lastOpenedLocation;
        private bool _isLoading;


        public MainForm()
        {
            InitializeComponent();
            new MainFormPresenter(this);

            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            txtMessage.Font = new Font(FontFamily.GenericMonospace, SystemFonts.DefaultFont.Size);

            this.Load += LoadDefaults;

        }

        protected virtual void OnFilterChanged(SimpleFilterChangedEventArgs args)
        {
            StartLoadingList();
            FilterChanged(this, args);
            tbtnFilterBack.Enabled = args.HasFiltersInHistory;
        }

        private int SelectedIndex
        {
            get
            {
                return lstUlsItems.SelectedIndices.Count > 0 ? lstUlsItems.SelectedIndices[0] : -1;
            }
        }

        private void LoadDefaults(object sender, EventArgs e)
        {
            // Load the default icons in the image list.
            imageList.Images.Add("Unexpected", Resources.Unexpected);
            imageList.Images.Add("Error", Resources.Error);
            imageList.Images.Add("Medium", Resources.Warning);


            // Load the values from the model
            var defaults = new DefaultLoadingEventArgs();
            if (this.DefaultLoading == null)
                throw new InvalidOperationException("The Default Loading event handler has no subscribers.");
            this.DefaultLoading(this, defaults);

            tcmbTimeLimits.Items.Clear();
            tcmbTimeLimits.Items.AddRange(defaults.TimeLimitation.Select(t => new TimeSpanElement(t)).Cast<object>().ToArray());
            tcmbTimeLimits.SelectedIndex = Settings.DefaultTimeLimitIndex < tcmbTimeLimits.Items.Count ? Settings.DefaultTimeLimitIndex : 0;

            // Set the default location
            //OnSourceChanged(new SourceChangedEventArgs(location: @"C:\Users\mathijs\Desktop\SharePointLogViewer\UlsLogs"));
            OnSourceChanged(new SourceChangedEventArgs());
        }

        public void StartLoading()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(StartLoading));
                return;
            }

            _isLoading = true;

            statusLabel.Visible = false;
            statusProgress.Visible = true;
            statusProgress.Style = ProgressBarStyle.Marquee;

            tbtnRefresh.Enabled = false;
            tcmbTimeLimits.Enabled = false;
            tbtnWhatsWrong.Enabled = false;
            tbtnFilter.Enabled = false;
            tbtnConnectTo.Enabled = false;
            tbtnExport.Enabled = false;
        }

        private void StartLoadingList()
        {
            _lastSelectedEntry = lstUlsItems.SelectedIndices.Count > 0 ? ((UlsLogEntryElement)_entryElements[lstUlsItems.SelectedIndices[0]]).Entry : null;
            lstUlsItems.VirtualListSize = 0;
        }

        public void StopLoading()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(StopLoading));
                return;
            }

            _isLoading = false;

            statusLabel.Visible = true;
            statusProgress.Visible = false;
            statusProgress.Style = ProgressBarStyle.Blocks;

            tbtnRefresh.Enabled = true;
            tcmbTimeLimits.Enabled = _canChangeTimeLimit;
            tbtnWhatsWrong.Enabled = true;
            tbtnFilter.Enabled = true;
            tbtnConnectTo.Enabled = true;
            tbtnExport.Enabled = _entryElements != null && _entryElements.Length > 0;
        }

        private void StopLoadingList()
        {
            lstUlsItems.VirtualListSize = _entryElements != null ? _entryElements.Length : 0;

            statusLabel.Text = string.Format("Found {0:#,##0} entries", lstUlsItems.VirtualListSize);

            // Update the filter applied state
            var openingRequest = new SimpleFilterOpeningEventArgs();
            FilterOpening(this, openingRequest);
            // Make text bold
            if (!openingRequest.Result.IsEmpty && !tbtnFilter.Font.Bold)
                tbtnFilter.Font = new Font(tbtnFilter.Font, FontStyle.Bold);
            // Make the text normal
            else if (openingRequest.Result.IsEmpty && tbtnFilter.Font.Bold)
                tbtnFilter.Font = new Font(tbtnFilter.Font, FontStyle.Regular);
            
            // Restore filter
            if (_lastSelectedEntry != null)
            {
                var args = new UlsLogEntryEventArgs(_lastSelectedEntry);
                FindUlsEntryRequest(this, args);
                lstUlsItems.SelectedIndices.Clear();
                if (args.Index >= 0)
                {
                    lstUlsItems.SelectedIndices.Add(args.Index);
                    lstUlsItems.EnsureVisible(args.Index);
                    lstUlsItems.FocusedItem = _entryElements[args.Index];
                    lstUlsItems.Focus();
                }
            }
        }

        public void LoadUlsEntries(int entryCount, string openedHostName, string openedLocation)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int, string, string>(LoadUlsEntries), entryCount, openedHostName, openedLocation);
                return;
            }

            _entryElements = new UlsLogEntryElement[entryCount];
            _lastOpenedHostName = openedHostName;
            _lastOpenedLocation = openedLocation;
            StopLoadingList();
        }

        public void ExportEntriesCompleted(bool success, Exception exception)
        {
            if (success)
            {
                MessageBox.Show("Export succeeded.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Export failed with the following reason:" + Environment.NewLine + exception.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowException(Exception ex)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<Exception>(ShowException), ex);
                return;
            }

            _entryElements = null;
            StopLoadingList();

            MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private UlsLogEntry GetEntry(int index)
        {
            var entryEventArgs = new UlsLogEntryEventArgs(index);
            UlsEntryRequest(this, entryEventArgs);
            return entryEventArgs.Entry;
        }

        private Point GetPopupLocation(ToolStripItem openingButton)
        {
            var bounds = openingButton.Bounds;
            var pos = PointToScreen(bounds.Location);
            pos.Y += bounds.Height;
            return pos;
        }


        private void tbtnRefresh_Click(object sender, EventArgs e)
        {
            StartLoadingList();
            RefreshRequest(this, EventArgs.Empty);
        }

        private void tcmbTimeLimits_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.DefaultTimeLimitIndex = tcmbTimeLimits.SelectedIndex;

            StartLoadingList();
            TimeLimitChanged(null, new TimeLimitChangedEventArgs(((TimeSpanElement)tcmbTimeLimits.SelectedItem).TimeSpan));
        }

        private void lstUlsItems_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (_entryElements[e.ItemIndex] == null)
                _entryElements[e.ItemIndex] = new UlsLogEntryElement(GetEntry(e.ItemIndex));

            e.Item = _entryElements[e.ItemIndex];
        }

        private void lstUlsItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (_isLoading && e.ItemIndex >= 0) return;

            var entry = ((UlsLogEntryElement)e.Item).Entry;
            txtMessage.Text = entry.GetStackTrace();
            ulsLogEntryDetails.SetUlsLogEntry(entry);
        }

        private void tbtnWhatsWrong_Click(object sender, EventArgs e)
        {
            var eventArgs = new WhatsWrongEventArgs(this.SelectedIndex);
            WhatsWrongRequest(this, eventArgs);
            if (eventArgs.ResultIndex >= 0)
            {
                lstUlsItems.SelectedIndices.Clear();
                lstUlsItems.SelectedIndices.Add(eventArgs.ResultIndex);
                lstUlsItems.EnsureVisible(eventArgs.ResultIndex);
            }
            else
            {
                MessageBox.Show(@"No more exceptions where found within the last " + tcmbTimeLimits.SelectedItem + @".", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //private void lstUlsItems_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    var selected = this.SelectedIndex;
        //    if (selected < 0) return;

        //    var popup = new PopoutWindow(GetEntry(selected));
        //    popup.Show();
        //}

        private void tbtnConnectTo_Click(object sender, EventArgs e)
        {
            var window = new SourceWindow(_lastOpenedHostName, _lastOpenedLocation);
            window.Location = GetPopupLocation(tbtnConnectTo);
            window.SourceChanged += (o, args) => OnSourceChanged(args);
            window.Show();
        }

        private void tbtnExport_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.DefaultExt = ".log";
            dialog.Filter = "ULS Log files (*.log)|*.log";
            dialog.OverwritePrompt = true;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ExportEntriesRequest(this, new ExportEntriesEventArgs(dialog.FileName));
            }
        }

        private void tbtnFilterBack_Click(object sender, EventArgs e)
        {
            var args = new SimpleFilterHistoryEventArgs();
            FilterPreviousRequest(this, args);
            tbtnFilterBack.Enabled = args.HasFiltersInHistory;
        }

        private void tbtnFilter_Click(object sender, EventArgs e)
        {
            var openingRequest = new SimpleFilterOpeningEventArgs();
            FilterOpening(this, openingRequest);

            var popup = new CustomFilterWindow(openingRequest.Result);
            popup.Location = GetPopupLocation(tbtnFilter);
            popup.FilterChanged += (o, args) =>
                {
                    OnFilterChanged(args);
                };
            popup.Show();
        }

        private void tbtnSettings_Click(object sender, EventArgs e)
        {
            var popup = new SettingsWindow();
            if (popup.ShowDialog() == DialogResult.OK)
            {
                _entryElements = new UlsLogEntryElement[lstUlsItems.VirtualListSize];
                lstUlsItems.Invalidate();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && !e.Shift && !e.Alt)
            {
                e.Handled = true;
                switch (e.KeyCode)
                {
                    case Keys.O:
                        tbtnConnectTo.PerformClick();
                        break;
                    case Keys.F:
                        tbtnFilter.PerformClick();
                        break;
                    case Keys.W:
                        tbtnWhatsWrong.PerformClick();
                        break;
                    default:
                        e.Handled = false;
                        break;
                }
            }
            else if (!e.Control && !e.Shift && !e.Alt)
            {
                e.Handled = true;
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        tbtnRefresh.PerformClick();
                        break;
                    case Keys.BrowserBack:
                    case Keys.Back:
                        tbtnFilterBack.PerformClick();
                        break;
                    default:
                        e.Handled = false;
                        break;
                }
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            OnSourceChanged(new SourceChangedEventArgs(location: files.First()));
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var file = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
                if (file.EndsWith(".log", StringComparison.OrdinalIgnoreCase) || Directory.Exists(file))
                    e.Effect = DragDropEffects.Link;
            }
            
        }

        #region Context menu

        private void cmenuUlsOptions_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = _entryElements == null || _entryElements.Length <= 0 || lstUlsItems.SelectedIndices.Count <= 0;
        }

        private void cmenuCorrelation_Click(object sender, EventArgs e)
        {
            var selectedItem = GetEntry(this.SelectedIndex);

            if (selectedItem.CorrelationId == null)
                return;

            var openingRequest = new SimpleFilterOpeningEventArgs();
            FilterOpening(this, openingRequest);
            var filter = openingRequest.Result;
            filter.IncludeText = selectedItem.CorrelationId.ToString();

            OnFilterChanged(new SimpleFilterChangedEventArgs(filter));
        }

        #endregion
    }
}