namespace SharePointLogViewer.View.Controls
{
    partial class SimpleFilterUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtInclude = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstErrorLevels = new System.Windows.Forms.ListBox();
            this.txtExclude = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Include:";
            // 
            // txtInclude
            // 
            this.txtInclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInclude.Location = new System.Drawing.Point(82, 3);
            this.txtInclude.Name = "txtInclude";
            this.txtInclude.Size = new System.Drawing.Size(199, 20);
            this.txtInclude.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Error levels:";
            // 
            // lstErrorLevels
            // 
            this.lstErrorLevels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstErrorLevels.FormattingEnabled = true;
            this.lstErrorLevels.IntegralHeight = false;
            this.lstErrorLevels.Location = new System.Drawing.Point(82, 55);
            this.lstErrorLevels.Name = "lstErrorLevels";
            this.lstErrorLevels.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstErrorLevels.Size = new System.Drawing.Size(199, 86);
            this.lstErrorLevels.TabIndex = 5;
            // 
            // txtExclude
            // 
            this.txtExclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExclude.Location = new System.Drawing.Point(82, 29);
            this.txtExclude.Name = "txtExclude";
            this.txtExclude.Size = new System.Drawing.Size(199, 20);
            this.txtExclude.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Exclude:";
            // 
            // SimpleFilterUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtExclude);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstErrorLevels);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInclude);
            this.Controls.Add(this.label1);
            this.Name = "SimpleFilterUserControl";
            this.Size = new System.Drawing.Size(284, 146);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInclude;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstErrorLevels;
        private System.Windows.Forms.TextBox txtExclude;
        private System.Windows.Forms.Label label3;
    }
}
