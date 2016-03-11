namespace PFTextFiles
{
    /// <summary>
    /// Designer class for delimiters winforms prompt.
    /// </summary>
    partial class PFFixedLengthDataPrompt
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
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.optOtherLineTerminator = new System.Windows.Forms.RadioButton();
            this.optCrLf = new System.Windows.Forms.RadioButton();
            this.txtOtherLineTerminator = new System.Windows.Forms.TextBox();
            this.chkIncludeColumnHeadingsInOutput = new System.Windows.Forms.CheckBox();
            this.chkAllowDataTruncation = new System.Windows.Forms.CheckBox();
            this.chkUseLineTerminator = new System.Windows.Forms.CheckBox();
            this.lblLineTerminatorChars = new System.Windows.Forms.Label();
            this.panelLineTerminatorChars = new System.Windows.Forms.Panel();
            this.lblColumnWidthForStringData = new System.Windows.Forms.Label();
            this.lblMaximumAllowedColumnWidth = new System.Windows.Forms.Label();
            this.txtColumnWidthForStringData = new System.Windows.Forms.TextBox();
            this.txtMaximumAllowedColumnWidth = new System.Windows.Forms.TextBox();
            this.panelLineTerminatorChars.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(340, 88);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(98, 34);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(340, 11);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(98, 34);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // optOtherLineTerminator
            // 
            this.optOtherLineTerminator.AutoSize = true;
            this.optOtherLineTerminator.Location = new System.Drawing.Point(26, 30);
            this.optOtherLineTerminator.Name = "optOtherLineTerminator";
            this.optOtherLineTerminator.Size = new System.Drawing.Size(127, 17);
            this.optOtherLineTerminator.TabIndex = 4;
            this.optOtherLineTerminator.TabStop = true;
            this.optOtherLineTerminator.Text = "Other Line Terminator";
            this.optOtherLineTerminator.UseVisualStyleBackColor = true;
            // 
            // optCrLf
            // 
            this.optCrLf.AutoSize = true;
            this.optCrLf.Location = new System.Drawing.Point(26, 6);
            this.optCrLf.Name = "optCrLf";
            this.optCrLf.Size = new System.Drawing.Size(57, 17);
            this.optCrLf.TabIndex = 3;
            this.optCrLf.TabStop = true;
            this.optCrLf.Text = "CR/LF";
            this.optCrLf.UseVisualStyleBackColor = true;
            // 
            // txtOtherLineTerminator
            // 
            this.txtOtherLineTerminator.Location = new System.Drawing.Point(45, 53);
            this.txtOtherLineTerminator.Name = "txtOtherLineTerminator";
            this.txtOtherLineTerminator.Size = new System.Drawing.Size(100, 20);
            this.txtOtherLineTerminator.TabIndex = 2;
            // 
            // chkIncludeColumnHeadingsInOutput
            // 
            this.chkIncludeColumnHeadingsInOutput.Location = new System.Drawing.Point(25, 12);
            this.chkIncludeColumnHeadingsInOutput.Name = "chkIncludeColumnHeadingsInOutput";
            this.chkIncludeColumnHeadingsInOutput.Size = new System.Drawing.Size(193, 17);
            this.chkIncludeColumnHeadingsInOutput.TabIndex = 0;
            this.chkIncludeColumnHeadingsInOutput.Text = "Include Column Headings in Output";
            this.chkIncludeColumnHeadingsInOutput.UseVisualStyleBackColor = true;
            // 
            // chkAllowDataTruncation
            // 
            this.chkAllowDataTruncation.AutoSize = true;
            this.chkAllowDataTruncation.Location = new System.Drawing.Point(25, 44);
            this.chkAllowDataTruncation.Name = "chkAllowDataTruncation";
            this.chkAllowDataTruncation.Size = new System.Drawing.Size(134, 17);
            this.chkAllowDataTruncation.TabIndex = 6;
            this.chkAllowDataTruncation.Text = "Allow Data Truncation ";
            this.chkAllowDataTruncation.UseVisualStyleBackColor = true;
            // 
            // chkUseLineTerminator
            // 
            this.chkUseLineTerminator.AutoSize = true;
            this.chkUseLineTerminator.Location = new System.Drawing.Point(25, 77);
            this.chkUseLineTerminator.Name = "chkUseLineTerminator";
            this.chkUseLineTerminator.Size = new System.Drawing.Size(202, 17);
            this.chkUseLineTerminator.TabIndex = 11;
            this.chkUseLineTerminator.Text = "Append Line Terminator to Each Line";
            this.chkUseLineTerminator.UseVisualStyleBackColor = true;
            // 
            // lblLineTerminatorChars
            // 
            this.lblLineTerminatorChars.AutoSize = true;
            this.lblLineTerminatorChars.Location = new System.Drawing.Point(22, 103);
            this.lblLineTerminatorChars.Name = "lblLineTerminatorChars";
            this.lblLineTerminatorChars.Size = new System.Drawing.Size(113, 13);
            this.lblLineTerminatorChars.TabIndex = 12;
            this.lblLineTerminatorChars.Text = "Line Terminator Chars:";
            // 
            // panelLineTerminatorChars
            // 
            this.panelLineTerminatorChars.Controls.Add(this.optCrLf);
            this.panelLineTerminatorChars.Controls.Add(this.txtOtherLineTerminator);
            this.panelLineTerminatorChars.Controls.Add(this.optOtherLineTerminator);
            this.panelLineTerminatorChars.Location = new System.Drawing.Point(25, 121);
            this.panelLineTerminatorChars.Name = "panelLineTerminatorChars";
            this.panelLineTerminatorChars.Size = new System.Drawing.Size(221, 80);
            this.panelLineTerminatorChars.TabIndex = 13;
            // 
            // lblColumnWidthForStringData
            // 
            this.lblColumnWidthForStringData.AutoSize = true;
            this.lblColumnWidthForStringData.Location = new System.Drawing.Point(22, 204);
            this.lblColumnWidthForStringData.Name = "lblColumnWidthForStringData";
            this.lblColumnWidthForStringData.Size = new System.Drawing.Size(147, 13);
            this.lblColumnWidthForStringData.TabIndex = 14;
            this.lblColumnWidthForStringData.Text = "Column Width for String Data:";
            // 
            // lblMaximumAllowedColumnWidth
            // 
            this.lblMaximumAllowedColumnWidth.AutoSize = true;
            this.lblMaximumAllowedColumnWidth.Location = new System.Drawing.Point(22, 232);
            this.lblMaximumAllowedColumnWidth.Name = "lblMaximumAllowedColumnWidth";
            this.lblMaximumAllowedColumnWidth.Size = new System.Drawing.Size(166, 13);
            this.lblMaximumAllowedColumnWidth.TabIndex = 15;
            this.lblMaximumAllowedColumnWidth.Text = "Maximum Allowed Column Width: ";
            // 
            // txtColumnWidthForStringData
            // 
            this.txtColumnWidthForStringData.Location = new System.Drawing.Point(195, 201);
            this.txtColumnWidthForStringData.Name = "txtColumnWidthForStringData";
            this.txtColumnWidthForStringData.Size = new System.Drawing.Size(51, 20);
            this.txtColumnWidthForStringData.TabIndex = 16;
            this.txtColumnWidthForStringData.Text = "255";
            // 
            // txtMaximumAllowedColumnWidth
            // 
            this.txtMaximumAllowedColumnWidth.Location = new System.Drawing.Point(195, 231);
            this.txtMaximumAllowedColumnWidth.Name = "txtMaximumAllowedColumnWidth";
            this.txtMaximumAllowedColumnWidth.Size = new System.Drawing.Size(51, 20);
            this.txtMaximumAllowedColumnWidth.TabIndex = 17;
            this.txtMaximumAllowedColumnWidth.Text = "1024";
            // 
            // PFFixedLengthDataPrompt
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(474, 286);
            this.Controls.Add(this.txtMaximumAllowedColumnWidth);
            this.Controls.Add(this.txtColumnWidthForStringData);
            this.Controls.Add(this.lblMaximumAllowedColumnWidth);
            this.Controls.Add(this.lblColumnWidthForStringData);
            this.Controls.Add(this.panelLineTerminatorChars);
            this.Controls.Add(this.lblLineTerminatorChars);
            this.Controls.Add(this.chkUseLineTerminator);
            this.Controls.Add(this.chkAllowDataTruncation);
            this.Controls.Add(this.chkIncludeColumnHeadingsInOutput);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PFFixedLengthDataPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Specify Fixed Length Data for Text File";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panelLineTerminatorChars.ResumeLayout(false);
            this.panelLineTerminatorChars.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.TextBox txtOtherLineTerminator;
        private System.Windows.Forms.RadioButton optOtherLineTerminator;
        private System.Windows.Forms.RadioButton optCrLf;
        private System.Windows.Forms.CheckBox chkIncludeColumnHeadingsInOutput;
        private System.Windows.Forms.CheckBox chkAllowDataTruncation;
        private System.Windows.Forms.CheckBox chkUseLineTerminator;
        private System.Windows.Forms.Label lblLineTerminatorChars;
        private System.Windows.Forms.Panel panelLineTerminatorChars;
        private System.Windows.Forms.Label lblColumnWidthForStringData;
        private System.Windows.Forms.Label lblMaximumAllowedColumnWidth;
        private System.Windows.Forms.TextBox txtColumnWidthForStringData;
        private System.Windows.Forms.TextBox txtMaximumAllowedColumnWidth;
    }
}