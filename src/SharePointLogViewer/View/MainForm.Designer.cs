using System.Windows.Forms;

namespace SharePointLogViewer.View
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstUlsItems = new System.Windows.Forms.ListView();
            this.lstcTimestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstcProcess = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstcArea = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstcMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmenuUlsOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmenuCorrelation = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ulsLogEntryDetails = new SharePointLogViewer.View.Controls.UlsLogEntryDetails();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtnConnectTo = new System.Windows.Forms.ToolStripButton();
            this.tbtnExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tcmbTimeLimits = new System.Windows.Forms.ToolStripComboBox();
            this.tbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnFilterBack = new System.Windows.Forms.ToolStripButton();
            this.tbtnFilter = new System.Windows.Forms.ToolStripButton();
            this.tbtnStaticFilter = new System.Windows.Forms.ToolStripButton();
            this.tbtnWhatsWrong = new System.Windows.Forms.ToolStripButton();
            this.tbtnSettings = new System.Windows.Forms.ToolStripButton();
            this.statusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmenuUlsOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstUlsItems);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(792, 548);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // lstUlsItems
            // 
            this.lstUlsItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lstcTimestamp,
            this.lstcProcess,
            this.lstcArea,
            this.lstcMessage});
            this.lstUlsItems.ContextMenuStrip = this.cmenuUlsOptions;
            this.lstUlsItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUlsItems.FullRowSelect = true;
            this.lstUlsItems.GridLines = true;
            this.lstUlsItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstUlsItems.HideSelection = false;
            this.lstUlsItems.Location = new System.Drawing.Point(0, 0);
            this.lstUlsItems.Margin = new System.Windows.Forms.Padding(2);
            this.lstUlsItems.Name = "lstUlsItems";
            this.lstUlsItems.Size = new System.Drawing.Size(792, 257);
            this.lstUlsItems.SmallImageList = this.imageList;
            this.lstUlsItems.TabIndex = 0;
            this.lstUlsItems.UseCompatibleStateImageBehavior = false;
            this.lstUlsItems.View = System.Windows.Forms.View.Details;
            this.lstUlsItems.VirtualMode = true;
            this.lstUlsItems.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstUlsItems_ItemSelectionChanged);
            this.lstUlsItems.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lstUlsItems_RetrieveVirtualItem);
            // 
            // lstcTimestamp
            // 
            this.lstcTimestamp.Text = "Time";
            this.lstcTimestamp.Width = 150;
            // 
            // lstcProcess
            // 
            this.lstcProcess.Text = "Process";
            this.lstcProcess.Width = 140;
            // 
            // lstcArea
            // 
            this.lstcArea.Text = "Area";
            this.lstcArea.Width = 140;
            // 
            // lstcMessage
            // 
            this.lstcMessage.Text = "Message";
            this.lstcMessage.Width = 320;
            // 
            // cmenuUlsOptions
            // 
            this.cmenuUlsOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmenuCorrelation});
            this.cmenuUlsOptions.Name = "cmenuUlsOptions";
            this.cmenuUlsOptions.Size = new System.Drawing.Size(212, 26);
            this.cmenuUlsOptions.Opening += new System.ComponentModel.CancelEventHandler(this.cmenuUlsOptions_Opening);
            // 
            // cmenuCorrelation
            // 
            this.cmenuCorrelation.Name = "cmenuCorrelation";
            this.cmenuCorrelation.Size = new System.Drawing.Size(211, 22);
            this.cmenuCorrelation.Text = "Show this correlation only";
            this.cmenuCorrelation.Click += new System.EventHandler(this.cmenuCorrelation_Click);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ulsLogEntryDetails);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtMessage);
            this.splitContainer2.Size = new System.Drawing.Size(792, 288);
            this.splitContainer2.SplitterDistance = 326;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 0;
            // 
            // ulsLogEntryDetails
            // 
            this.ulsLogEntryDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ulsLogEntryDetails.Location = new System.Drawing.Point(0, 0);
            this.ulsLogEntryDetails.Name = "ulsLogEntryDetails";
            this.ulsLogEntryDetails.Size = new System.Drawing.Size(326, 288);
            this.ulsLogEntryDetails.TabIndex = 0;
            // 
            // txtMessage
            // 
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(0, 0);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(2);
            this.txtMessage.MaxLength = 1048576;
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(463, 288);
            this.txtMessage.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnConnectTo,
            this.tbtnExport,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tcmbTimeLimits,
            this.tbtnRefresh,
            this.toolStripSeparator2,
            this.tbtnFilterBack,
            this.tbtnFilter,
            this.tbtnStaticFilter,
            this.tbtnWhatsWrong,
            this.tbtnSettings,
            this.statusProgress,
            this.statusLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbtnConnectTo
            // 
            this.tbtnConnectTo.Image = global::SharePointLogViewer.Properties.Resources.ConnectTo;
            this.tbtnConnectTo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnConnectTo.Name = "tbtnConnectTo";
            this.tbtnConnectTo.Size = new System.Drawing.Size(115, 22);
            this.tbtnConnectTo.Text = "Change source...";
            this.tbtnConnectTo.Click += new System.EventHandler(this.tbtnConnectTo_Click);
            // 
            // tbtnExport
            // 
            this.tbtnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnExport.Enabled = false;
            this.tbtnExport.Image = global::SharePointLogViewer.Properties.Resources.Export;
            this.tbtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnExport.Name = "tbtnExport";
            this.tbtnExport.Size = new System.Drawing.Size(23, 22);
            this.tbtnExport.Text = "Export current entries";
            this.tbtnExport.Click += new System.EventHandler(this.tbtnExport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(70, 22);
            this.toolStripLabel1.Text = "Time range:";
            // 
            // tcmbTimeLimits
            // 
            this.tcmbTimeLimits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tcmbTimeLimits.Name = "tcmbTimeLimits";
            this.tcmbTimeLimits.Size = new System.Drawing.Size(92, 25);
            this.tcmbTimeLimits.SelectedIndexChanged += new System.EventHandler(this.tcmbTimeLimits_SelectedIndexChanged);
            // 
            // tbtnRefresh
            // 
            this.tbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnRefresh.Image = global::SharePointLogViewer.Properties.Resources.Refresh;
            this.tbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnRefresh.Name = "tbtnRefresh";
            this.tbtnRefresh.Size = new System.Drawing.Size(23, 22);
            this.tbtnRefresh.Text = "Refresh";
            this.tbtnRefresh.Click += new System.EventHandler(this.tbtnRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnFilterBack
            // 
            this.tbtnFilterBack.Enabled = false;
            this.tbtnFilterBack.Image = global::SharePointLogViewer.Properties.Resources.Back;
            this.tbtnFilterBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnFilterBack.Name = "tbtnFilterBack";
            this.tbtnFilterBack.Size = new System.Drawing.Size(99, 22);
            this.tbtnFilterBack.Text = "Previous filter";
            this.tbtnFilterBack.Click += new System.EventHandler(this.tbtnFilterBack_Click);
            // 
            // tbtnFilter
            // 
            this.tbtnFilter.Image = global::SharePointLogViewer.Properties.Resources.Search;
            this.tbtnFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnFilter.Name = "tbtnFilter";
            this.tbtnFilter.Size = new System.Drawing.Size(53, 22);
            this.tbtnFilter.Text = "Filter";
            this.tbtnFilter.Click += new System.EventHandler(this.tbtnFilter_Click);
            // 
            // tbtnStaticFilter
            // 
            this.tbtnStaticFilter.Enabled = false;
            this.tbtnStaticFilter.Image = global::SharePointLogViewer.Properties.Resources.Search;
            this.tbtnStaticFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnStaticFilter.Name = "tbtnStaticFilter";
            this.tbtnStaticFilter.Size = new System.Drawing.Size(83, 22);
            this.tbtnStaticFilter.Text = "Static filter";
            this.tbtnStaticFilter.Visible = false;
            // 
            // tbtnWhatsWrong
            // 
            this.tbtnWhatsWrong.Image = global::SharePointLogViewer.Properties.Resources.WhatsWrong;
            this.tbtnWhatsWrong.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnWhatsWrong.Name = "tbtnWhatsWrong";
            this.tbtnWhatsWrong.Size = new System.Drawing.Size(108, 22);
            this.tbtnWhatsWrong.Text = "What\'s wrong?!";
            this.tbtnWhatsWrong.Click += new System.EventHandler(this.tbtnWhatsWrong_Click);
            // 
            // tbtnSettings
            // 
            this.tbtnSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbtnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnSettings.Image = global::SharePointLogViewer.Properties.Resources.Settings;
            this.tbtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSettings.Name = "tbtnSettings";
            this.tbtnSettings.Size = new System.Drawing.Size(23, 22);
            this.tbtnSettings.Text = "Open settings...";
            this.tbtnSettings.Click += new System.EventHandler(this.tbtnSettings_Click);
            // 
            // statusProgress
            // 
            this.statusProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.statusProgress.Name = "statusProgress";
            this.statusProgress.Size = new System.Drawing.Size(56, 22);
            // 
            // statusLabel
            // 
            this.statusLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(35, 15);
            this.statusLabel.Text = "Done";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 23);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "SharePoint Log Viewer";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cmenuUlsOptions.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbtnConnectTo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tcmbTimeLimits;
        private System.Windows.Forms.ToolStripButton tbtnRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tbtnFilter;
        private System.Windows.Forms.ToolStripButton tbtnWhatsWrong;
        private System.Windows.Forms.ToolStripProgressBar statusProgress;
        private System.Windows.Forms.ListView lstUlsItems;
        private System.Windows.Forms.ColumnHeader lstcTimestamp;
        private System.Windows.Forms.ColumnHeader lstcProcess;
        private System.Windows.Forms.ColumnHeader lstcArea;
        private System.Windows.Forms.ColumnHeader lstcMessage;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private SplitContainer splitContainer2;
        private TextBox txtMessage;
        private ToolStripButton tbtnStaticFilter;
        private ContextMenuStrip cmenuUlsOptions;
        private ToolStripMenuItem cmenuCorrelation;
        private ToolStripLabel statusLabel;
        private ToolStripButton tbtnSettings;
        private ToolStripButton tbtnFilterBack;
        private Controls.UlsLogEntryDetails ulsLogEntryDetails;
        private ToolStripButton tbtnExport;

    }
}

