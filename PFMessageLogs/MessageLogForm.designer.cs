namespace PFMessageLogs
{
    partial class MessageLogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageLogForm));
            this.txtMessageLog = new System.Windows.Forms.RichTextBox();
            this.MainMenu1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileErase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileEraseSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFilePageSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrintSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileHide = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopySeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditSelectAllSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditFindNext = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFormatFont = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.FontDialog1 = new System.Windows.Forms.FontDialog();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.MainMenu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMessageLog
            // 
            this.txtMessageLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtMessageLog.Location = new System.Drawing.Point(40, 45);
            this.txtMessageLog.Name = "txtMessageLog";
            this.txtMessageLog.ReadOnly = true;
            this.txtMessageLog.Size = new System.Drawing.Size(472, 272);
            this.txtMessageLog.TabIndex = 1;
            this.txtMessageLog.Text = "";
            // 
            // MainMenu1
            // 
            this.MainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuFormat,
            this.mnuHelp});
            this.MainMenu1.Location = new System.Drawing.Point(0, 0);
            this.MainMenu1.Name = "MainMenu1";
            this.MainMenu1.Size = new System.Drawing.Size(552, 24);
            this.MainMenu1.TabIndex = 2;
            this.MainMenu1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileSave,
            this.mnuFileSaveSeparator,
            this.mnuFileErase,
            this.mnuFileEraseSeparator,
            this.mnuFilePageSettings,
            this.mnuFilePrintPreview,
            this.mnuFilePrint,
            this.mnuFilePrintSeparator,
            this.mnuFileHide});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(145, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSaveSeparator
            // 
            this.mnuFileSaveSeparator.Name = "mnuFileSaveSeparator";
            this.mnuFileSaveSeparator.Size = new System.Drawing.Size(142, 6);
            // 
            // mnuFileErase
            // 
            this.mnuFileErase.Name = "mnuFileErase";
            this.mnuFileErase.Size = new System.Drawing.Size(145, 22);
            this.mnuFileErase.Text = "&Erase";
            this.mnuFileErase.Click += new System.EventHandler(this.mnuFileErase_Click);
            // 
            // mnuFileEraseSeparator
            // 
            this.mnuFileEraseSeparator.Name = "mnuFileEraseSeparator";
            this.mnuFileEraseSeparator.Size = new System.Drawing.Size(142, 6);
            // 
            // mnuFilePageSettings
            // 
            this.mnuFilePageSettings.Name = "mnuFilePageSettings";
            this.mnuFilePageSettings.Size = new System.Drawing.Size(145, 22);
            this.mnuFilePageSettings.Text = "Page Se&ttings";
            this.mnuFilePageSettings.Click += new System.EventHandler(this.mnuFilePageSettings_Click);
            // 
            // mnuFilePrintPreview
            // 
            this.mnuFilePrintPreview.Name = "mnuFilePrintPreview";
            this.mnuFilePrintPreview.Size = new System.Drawing.Size(145, 22);
            this.mnuFilePrintPreview.Text = "Print Pre&view";
            this.mnuFilePrintPreview.Click += new System.EventHandler(this.mnuFilePrintPreview_Click);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(145, 22);
            this.mnuFilePrint.Text = "&Print";
            this.mnuFilePrint.Click += new System.EventHandler(this.mnuFilePrint_Click);
            // 
            // mnuFilePrintSeparator
            // 
            this.mnuFilePrintSeparator.Name = "mnuFilePrintSeparator";
            this.mnuFilePrintSeparator.Size = new System.Drawing.Size(142, 6);
            // 
            // mnuFileHide
            // 
            this.mnuFileHide.Name = "mnuFileHide";
            this.mnuFileHide.Size = new System.Drawing.Size(145, 22);
            this.mnuFileHide.Text = "&Hide";
            this.mnuFileHide.Click += new System.EventHandler(this.mnuFileHide_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditCopy,
            this.mnuEditCopySeparator,
            this.mnuEditSelectAll,
            this.mnuEditSelectAllSeparator,
            this.mnuEditFind,
            this.mnuEditFindNext});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(143, 22);
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.mnuEditCopy_Click);
            // 
            // mnuEditCopySeparator
            // 
            this.mnuEditCopySeparator.Name = "mnuEditCopySeparator";
            this.mnuEditCopySeparator.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuEditSelectAll
            // 
            this.mnuEditSelectAll.Name = "mnuEditSelectAll";
            this.mnuEditSelectAll.Size = new System.Drawing.Size(143, 22);
            this.mnuEditSelectAll.Text = "Select &All";
            this.mnuEditSelectAll.Click += new System.EventHandler(this.mnuEditSelectAll_Click);
            // 
            // mnuEditSelectAllSeparator
            // 
            this.mnuEditSelectAllSeparator.Name = "mnuEditSelectAllSeparator";
            this.mnuEditSelectAllSeparator.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuEditFind
            // 
            this.mnuEditFind.Name = "mnuEditFind";
            this.mnuEditFind.Size = new System.Drawing.Size(143, 22);
            this.mnuEditFind.Text = "&Find";
            this.mnuEditFind.Click += new System.EventHandler(this.mnuEditFind_Click);
            // 
            // mnuEditFindNext
            // 
            this.mnuEditFindNext.Name = "mnuEditFindNext";
            this.mnuEditFindNext.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuEditFindNext.Size = new System.Drawing.Size(143, 22);
            this.mnuEditFindNext.Text = "Find &Next";
            this.mnuEditFindNext.Click += new System.EventHandler(this.mnuEditFindNext_Click);
            // 
            // mnuFormat
            // 
            this.mnuFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFormatFont});
            this.mnuFormat.Name = "mnuFormat";
            this.mnuFormat.Size = new System.Drawing.Size(57, 20);
            this.mnuFormat.Text = "For&mat";
            // 
            // mnuFormatFont
            // 
            this.mnuFormatFont.Name = "mnuFormatFont";
            this.mnuFormatFont.Size = new System.Drawing.Size(98, 22);
            this.mnuFormatFont.Text = "&Font";
            this.mnuFormatFont.Click += new System.EventHandler(this.mnuFormatFont_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // MessageLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 362);
            this.Controls.Add(this.txtMessageLog);
            this.Controls.Add(this.MainMenu1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MessageLogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CMessageLogForm";
            this.MainMenu1.ResumeLayout(false);
            this.MainMenu1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.RichTextBox txtMessageLog;
        private System.Windows.Forms.MenuStrip MainMenu1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripSeparator mnuFileSaveSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuFileErase;
        private System.Windows.Forms.ToolStripSeparator mnuFileEraseSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePageSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrintPreview;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrint;
        private System.Windows.Forms.ToolStripSeparator mnuFilePrintSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuFileHide;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCopy;
        private System.Windows.Forms.ToolStripSeparator mnuEditCopySeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuEditSelectAll;
        private System.Windows.Forms.ToolStripSeparator mnuEditSelectAllSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuEditFind;
        private System.Windows.Forms.ToolStripMenuItem mnuEditFindNext;
        private System.Windows.Forms.ToolStripMenuItem mnuFormat;
        private System.Windows.Forms.ToolStripMenuItem mnuFormatFont;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.FontDialog FontDialog1;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog1;
    }
}