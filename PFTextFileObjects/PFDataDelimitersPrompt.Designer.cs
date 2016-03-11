namespace PFTextFiles
{
    /// <summary>
    /// Designer class for delimiters winforms prompt.
    /// </summary>
    partial class PFDataDelimitersPrompt
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optOtherDelimiter = new System.Windows.Forms.RadioButton();
            this.optTabDelimited = new System.Windows.Forms.RadioButton();
            this.optCommaDelimited = new System.Windows.Forms.RadioButton();
            this.txtOtherSeparator = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.optOtherLineTerminator = new System.Windows.Forms.RadioButton();
            this.optCrLf = new System.Windows.Forms.RadioButton();
            this.txtOtherLineTerminator = new System.Windows.Forms.TextBox();
            this.chkIncludeColumnHeadingsInOutput = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Column Delimiter:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Line Terminator:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optOtherDelimiter);
            this.panel1.Controls.Add(this.optTabDelimited);
            this.panel1.Controls.Add(this.optCommaDelimited);
            this.panel1.Controls.Add(this.txtOtherSeparator);
            this.panel1.Location = new System.Drawing.Point(130, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 114);
            this.panel1.TabIndex = 4;
            // 
            // optOtherDelimiter
            // 
            this.optOtherDelimiter.AutoSize = true;
            this.optOtherDelimiter.Location = new System.Drawing.Point(16, 54);
            this.optOtherDelimiter.Name = "optOtherDelimiter";
            this.optOtherDelimiter.Size = new System.Drawing.Size(100, 17);
            this.optOtherDelimiter.TabIndex = 6;
            this.optOtherDelimiter.TabStop = true;
            this.optOtherDelimiter.Text = "Other Separator";
            this.optOtherDelimiter.UseVisualStyleBackColor = true;
            // 
            // optTabDelimited
            // 
            this.optTabDelimited.AutoSize = true;
            this.optTabDelimited.Location = new System.Drawing.Point(16, 30);
            this.optTabDelimited.Name = "optTabDelimited";
            this.optTabDelimited.Size = new System.Drawing.Size(96, 17);
            this.optTabDelimited.TabIndex = 5;
            this.optTabDelimited.TabStop = true;
            this.optTabDelimited.Text = "Tab Separated";
            this.optTabDelimited.UseVisualStyleBackColor = true;
            // 
            // optCommaDelimited
            // 
            this.optCommaDelimited.AutoSize = true;
            this.optCommaDelimited.Location = new System.Drawing.Point(16, 6);
            this.optCommaDelimited.Name = "optCommaDelimited";
            this.optCommaDelimited.Size = new System.Drawing.Size(112, 17);
            this.optCommaDelimited.TabIndex = 4;
            this.optCommaDelimited.TabStop = true;
            this.optCommaDelimited.Text = "Comma Separated";
            this.optCommaDelimited.UseVisualStyleBackColor = true;
            // 
            // txtOtherSeparator
            // 
            this.txtOtherSeparator.Location = new System.Drawing.Point(35, 82);
            this.txtOtherSeparator.Name = "txtOtherSeparator";
            this.txtOtherSeparator.Size = new System.Drawing.Size(94, 20);
            this.txtOtherSeparator.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.optOtherLineTerminator);
            this.panel2.Controls.Add(this.optCrLf);
            this.panel2.Controls.Add(this.txtOtherLineTerminator);
            this.panel2.Location = new System.Drawing.Point(130, 143);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(162, 93);
            this.panel2.TabIndex = 5;
            // 
            // optOtherLineTerminator
            // 
            this.optOtherLineTerminator.AutoSize = true;
            this.optOtherLineTerminator.Location = new System.Drawing.Point(16, 40);
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
            this.optCrLf.Location = new System.Drawing.Point(16, 16);
            this.optCrLf.Name = "optCrLf";
            this.optCrLf.Size = new System.Drawing.Size(57, 17);
            this.optCrLf.TabIndex = 3;
            this.optCrLf.TabStop = true;
            this.optCrLf.Text = "CR/LF";
            this.optCrLf.UseVisualStyleBackColor = true;
            // 
            // txtOtherLineTerminator
            // 
            this.txtOtherLineTerminator.Location = new System.Drawing.Point(35, 63);
            this.txtOtherLineTerminator.Name = "txtOtherLineTerminator";
            this.txtOtherLineTerminator.Size = new System.Drawing.Size(100, 20);
            this.txtOtherLineTerminator.TabIndex = 2;
            // 
            // chkIncludeColumnHeadingsInOutput
            // 
            this.chkIncludeColumnHeadingsInOutput.Location = new System.Drawing.Point(25, 249);
            this.chkIncludeColumnHeadingsInOutput.Name = "chkIncludeColumnHeadingsInOutput";
            this.chkIncludeColumnHeadingsInOutput.Size = new System.Drawing.Size(193, 17);
            this.chkIncludeColumnHeadingsInOutput.TabIndex = 0;
            this.chkIncludeColumnHeadingsInOutput.Text = "Include Column Headings in Output";
            this.chkIncludeColumnHeadingsInOutput.UseVisualStyleBackColor = true;
            // 
            // PFDataDelimitersPrompt
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(474, 305);
            this.Controls.Add(this.chkIncludeColumnHeadingsInOutput);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PFDataDelimitersPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Specify Data Delimiters for Text File";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtOtherSeparator;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtOtherLineTerminator;
        private System.Windows.Forms.RadioButton optOtherDelimiter;
        private System.Windows.Forms.RadioButton optTabDelimited;
        private System.Windows.Forms.RadioButton optCommaDelimited;
        private System.Windows.Forms.RadioButton optOtherLineTerminator;
        private System.Windows.Forms.RadioButton optCrLf;
        private System.Windows.Forms.CheckBox chkIncludeColumnHeadingsInOutput;
    }
}