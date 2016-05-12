namespace SharePointLogViewer.View
{
    partial class SettingsWindow
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.chkEnableTracing = new System.Windows.Forms.CheckBox();
            this.chkRemoveFilter = new System.Windows.Forms.CheckBox();
            this.chkShortTimestamp = new System.Windows.Forms.CheckBox();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.grpGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(182, 124);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Ok";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(263, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // grpGeneral
            // 
            this.grpGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGeneral.Controls.Add(this.chkEnableTracing);
            this.grpGeneral.Controls.Add(this.chkRemoveFilter);
            this.grpGeneral.Controls.Add(this.chkShortTimestamp);
            this.grpGeneral.Location = new System.Drawing.Point(12, 12);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(326, 100);
            this.grpGeneral.TabIndex = 2;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // chkEnableTracing
            // 
            this.chkEnableTracing.AutoSize = true;
            this.chkEnableTracing.Location = new System.Drawing.Point(6, 65);
            this.chkEnableTracing.Name = "chkEnableTracing";
            this.chkEnableTracing.Size = new System.Drawing.Size(201, 17);
            this.chkEnableTracing.TabIndex = 2;
            this.chkEnableTracing.Text = "Enable diagnostics in the application.";
            this.chkEnableTracing.UseVisualStyleBackColor = true;
            // 
            // chkRemoveFilter
            // 
            this.chkRemoveFilter.AutoSize = true;
            this.chkRemoveFilter.Location = new System.Drawing.Point(6, 42);
            this.chkRemoveFilter.Name = "chkRemoveFilter";
            this.chkRemoveFilter.Size = new System.Drawing.Size(220, 17);
            this.chkRemoveFilter.TabIndex = 1;
            this.chkRemoveFilter.Text = "Remove the filter when refreshing the list.";
            this.chkRemoveFilter.UseVisualStyleBackColor = true;
            // 
            // chkShortTimestamp
            // 
            this.chkShortTimestamp.AutoSize = true;
            this.chkShortTimestamp.Location = new System.Drawing.Point(6, 19);
            this.chkShortTimestamp.Name = "chkShortTimestamp";
            this.chkShortTimestamp.Size = new System.Drawing.Size(247, 17);
            this.chkShortTimestamp.TabIndex = 0;
            this.chkShortTimestamp.Text = "Show the short timestamp when log is of today.";
            this.chkShortTimestamp.UseVisualStyleBackColor = true;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblCopyright.Location = new System.Drawing.Point(15, 121);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(119, 26);
            this.lblCopyright.TabIndex = 3;
            this.lblCopyright.Text = "SharePoint log viewer 2\r\nby Mathijs van Gool";
            // 
            // SettingsWindow
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(350, 159);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.grpGeneral);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.CheckBox chkShortTimestamp;
        private System.Windows.Forms.CheckBox chkEnableTracing;
        private System.Windows.Forms.CheckBox chkRemoveFilter;
        private System.Windows.Forms.Label lblCopyright;
    }
}