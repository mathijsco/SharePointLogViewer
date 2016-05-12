namespace SharePointLogViewer.View
{
    partial class SourceWindow
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelRemote = new System.Windows.Forms.Panel();
            this.panelRemoteShare = new System.Windows.Forms.Panel();
            this.txtRemoteShare = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRemoteHostName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelChooseFile = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbConnectOption = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panelRemote.SuspendLayout();
            this.panelRemoteShare.SuspendLayout();
            this.panelChooseFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panelChooseFile);
            this.panel1.Controls.Add(this.panelRemote);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbConnectOption);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(281, 134);
            this.panel1.TabIndex = 0;
            // 
            // panelRemote
            // 
            this.panelRemote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRemote.Controls.Add(this.panelRemoteShare);
            this.panelRemote.Controls.Add(this.txtRemoteHostName);
            this.panelRemote.Controls.Add(this.label3);
            this.panelRemote.Location = new System.Drawing.Point(-1, 38);
            this.panelRemote.Name = "panelRemote";
            this.panelRemote.Size = new System.Drawing.Size(281, 54);
            this.panelRemote.TabIndex = 5;
            // 
            // panelRemoteShare
            // 
            this.panelRemoteShare.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRemoteShare.Controls.Add(this.txtRemoteShare);
            this.panelRemoteShare.Controls.Add(this.label4);
            this.panelRemoteShare.Location = new System.Drawing.Point(0, 29);
            this.panelRemoteShare.Name = "panelRemoteShare";
            this.panelRemoteShare.Size = new System.Drawing.Size(281, 25);
            this.panelRemoteShare.TabIndex = 6;
            // 
            // txtRemoteShare
            // 
            this.txtRemoteShare.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemoteShare.Location = new System.Drawing.Point(103, 2);
            this.txtRemoteShare.Name = "txtRemoteShare";
            this.txtRemoteShare.Size = new System.Drawing.Size(166, 20);
            this.txtRemoteShare.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Share name:";
            // 
            // txtRemoteHostName
            // 
            this.txtRemoteHostName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemoteHostName.Location = new System.Drawing.Point(103, 2);
            this.txtRemoteHostName.Name = "txtRemoteHostName";
            this.txtRemoteHostName.Size = new System.Drawing.Size(166, 20);
            this.txtRemoteHostName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Host name:";
            // 
            // panelChooseFile
            // 
            this.panelChooseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelChooseFile.Controls.Add(this.label2);
            this.panelChooseFile.Location = new System.Drawing.Point(-1, 38);
            this.panelChooseFile.Name = "panelChooseFile";
            this.panelChooseFile.Size = new System.Drawing.Size(281, 54);
            this.panelChooseFile.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(12, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 42);
            this.label2.TabIndex = 0;
            this.label2.Text = "To open a file or directory, simply drag the file or directory into the SharePoin" +
    "t log viewer application.";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(193, 98);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(111, 98);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connect to:";
            // 
            // cmbConnectOption
            // 
            this.cmbConnectOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbConnectOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnectOption.FormattingEnabled = true;
            this.cmbConnectOption.Items.AddRange(new object[] {
            "My computer",
            "Choose file location",
            "Remote computer as admin",
            "Remote computer using share"});
            this.cmbConnectOption.Location = new System.Drawing.Point(102, 11);
            this.cmbConnectOption.Name = "cmbConnectOption";
            this.cmbConnectOption.Size = new System.Drawing.Size(166, 21);
            this.cmbConnectOption.TabIndex = 0;
            this.cmbConnectOption.SelectedIndexChanged += new System.EventHandler(this.cmbConnectOption_SelectedIndexChanged);
            // 
            // SourceWindow
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 134);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "SourceWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SourceWindow";
            this.Deactivate += new System.EventHandler(this.SourceWindow_Deactivate);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SourceWindow_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelRemote.ResumeLayout(false);
            this.panelRemote.PerformLayout();
            this.panelRemoteShare.ResumeLayout(false);
            this.panelRemoteShare.PerformLayout();
            this.panelChooseFile.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbConnectOption;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panelChooseFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelRemote;
        private System.Windows.Forms.TextBox txtRemoteHostName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelRemoteShare;
        private System.Windows.Forms.TextBox txtRemoteShare;
        private System.Windows.Forms.Label label4;
    }
}