namespace TestprogLogManager
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
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdRunTests = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.grpTestsToRun = new System.Windows.Forms.GroupBox();
            this.chkWriteEveryOtherFromRetryQueueTest = new System.Windows.Forms.CheckBox();
            this.chkWriteMessageToRetryQueue = new System.Windows.Forms.CheckBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.chkWriteMessageToLog = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkShowUsername = new System.Windows.Forms.CheckBox();
            this.chkShowMachineName = new System.Windows.Forms.CheckBox();
            this.chkShowApplicationName = new System.Windows.Forms.CheckBox();
            this.chkShowErrorWarningTypes = new System.Windows.Forms.CheckBox();
            this.chkShowMessageType = new System.Windows.Forms.CheckBox();
            this.chkShowDateTime = new System.Windows.Forms.CheckBox();
            this.chkWriteFromRetryQueueTest = new System.Windows.Forms.CheckBox();
            this.chkInitRetryQueueTest = new System.Windows.Forms.CheckBox();
            this.chkInitLogWriteTest = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.cmdReinit = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(508, 495);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 2;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdRunTests
            // 
            this.cmdRunTests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.appHelpProvider.SetHelpKeyword(this.cmdRunTests, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdRunTests, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdRunTests, "Help for Run Tests: See Help File.");
            this.cmdRunTests.Location = new System.Drawing.Point(508, 60);
            this.cmdRunTests.Name = "cmdRunTests";
            this.appHelpProvider.SetShowHelp(this.cmdRunTests, true);
            this.cmdRunTests.Size = new System.Drawing.Size(93, 37);
            this.cmdRunTests.TabIndex = 1;
            this.cmdRunTests.Text = "&Run Tests";
            this.cmdRunTests.UseVisualStyleBackColor = true;
            this.cmdRunTests.Click += new System.EventHandler(this.cmdRunTest_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(636, 24);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // grpTestsToRun
            // 
            this.grpTestsToRun.Controls.Add(this.chkWriteEveryOtherFromRetryQueueTest);
            this.grpTestsToRun.Controls.Add(this.chkWriteMessageToRetryQueue);
            this.grpTestsToRun.Controls.Add(this.txtMessage);
            this.grpTestsToRun.Controls.Add(this.chkWriteMessageToLog);
            this.grpTestsToRun.Controls.Add(this.groupBox2);
            this.grpTestsToRun.Controls.Add(this.chkWriteFromRetryQueueTest);
            this.grpTestsToRun.Controls.Add(this.chkInitRetryQueueTest);
            this.grpTestsToRun.Controls.Add(this.chkInitLogWriteTest);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 425);
            this.grpTestsToRun.TabIndex = 0;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkWriteEveryOtherFromRetryQueueTest
            // 
            this.chkWriteEveryOtherFromRetryQueueTest.AutoSize = true;
            this.chkWriteEveryOtherFromRetryQueueTest.Location = new System.Drawing.Point(17, 104);
            this.chkWriteEveryOtherFromRetryQueueTest.Name = "chkWriteEveryOtherFromRetryQueueTest";
            this.chkWriteEveryOtherFromRetryQueueTest.Size = new System.Drawing.Size(193, 17);
            this.chkWriteEveryOtherFromRetryQueueTest.TabIndex = 12;
            this.chkWriteEveryOtherFromRetryQueueTest.Text = "&4 Write Every Other in Retry Queue";
            this.chkWriteEveryOtherFromRetryQueueTest.UseVisualStyleBackColor = true;
            // 
            // chkWriteMessageToRetryQueue
            // 
            this.chkWriteMessageToRetryQueue.AutoSize = true;
            this.chkWriteMessageToRetryQueue.Location = new System.Drawing.Point(17, 390);
            this.chkWriteMessageToRetryQueue.Name = "chkWriteMessageToRetryQueue";
            this.chkWriteMessageToRetryQueue.Size = new System.Drawing.Size(181, 17);
            this.chkWriteMessageToRetryQueue.TabIndex = 11;
            this.chkWriteMessageToRetryQueue.Text = "&6 Write Message to Retry Queue";
            this.chkWriteMessageToRetryQueue.UseVisualStyleBackColor = true;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(17, 197);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(255, 173);
            this.txtMessage.TabIndex = 10;
            // 
            // chkWriteMessageToLog
            // 
            this.chkWriteMessageToLog.AutoSize = true;
            this.chkWriteMessageToLog.Location = new System.Drawing.Point(17, 164);
            this.chkWriteMessageToLog.Name = "chkWriteMessageToLog";
            this.chkWriteMessageToLog.Size = new System.Drawing.Size(143, 17);
            this.chkWriteMessageToLog.TabIndex = 9;
            this.chkWriteMessageToLog.Text = "&5 Write Message To Log";
            this.chkWriteMessageToLog.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkShowUsername);
            this.groupBox2.Controls.Add(this.chkShowMachineName);
            this.groupBox2.Controls.Add(this.chkShowApplicationName);
            this.groupBox2.Controls.Add(this.chkShowErrorWarningTypes);
            this.groupBox2.Controls.Add(this.chkShowMessageType);
            this.groupBox2.Controls.Add(this.chkShowDateTime);
            this.groupBox2.Location = new System.Drawing.Point(290, 199);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(118, 171);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message Prefixes";
            // 
            // chkShowUsername
            // 
            this.chkShowUsername.AutoSize = true;
            this.chkShowUsername.Location = new System.Drawing.Point(15, 141);
            this.chkShowUsername.Name = "chkShowUsername";
            this.chkShowUsername.Size = new System.Drawing.Size(74, 17);
            this.chkShowUsername.TabIndex = 11;
            this.chkShowUsername.Text = "Username";
            this.chkShowUsername.UseVisualStyleBackColor = true;
            // 
            // chkShowMachineName
            // 
            this.chkShowMachineName.AutoSize = true;
            this.chkShowMachineName.Location = new System.Drawing.Point(15, 117);
            this.chkShowMachineName.Name = "chkShowMachineName";
            this.chkShowMachineName.Size = new System.Drawing.Size(98, 17);
            this.chkShowMachineName.TabIndex = 10;
            this.chkShowMachineName.Text = "Machine Name";
            this.chkShowMachineName.UseVisualStyleBackColor = true;
            // 
            // chkShowApplicationName
            // 
            this.chkShowApplicationName.AutoSize = true;
            this.chkShowApplicationName.Location = new System.Drawing.Point(15, 93);
            this.chkShowApplicationName.Name = "chkShowApplicationName";
            this.chkShowApplicationName.Size = new System.Drawing.Size(76, 17);
            this.chkShowApplicationName.TabIndex = 9;
            this.chkShowApplicationName.Text = "App Name";
            this.chkShowApplicationName.UseVisualStyleBackColor = true;
            // 
            // chkShowErrorWarningTypes
            // 
            this.chkShowErrorWarningTypes.AutoSize = true;
            this.chkShowErrorWarningTypes.Location = new System.Drawing.Point(15, 69);
            this.chkShowErrorWarningTypes.Name = "chkShowErrorWarningTypes";
            this.chkShowErrorWarningTypes.Size = new System.Drawing.Size(98, 17);
            this.chkShowErrorWarningTypes.TabIndex = 8;
            this.chkShowErrorWarningTypes.Text = "Error/Warnings";
            this.chkShowErrorWarningTypes.UseVisualStyleBackColor = true;
            // 
            // chkShowMessageType
            // 
            this.chkShowMessageType.AutoSize = true;
            this.chkShowMessageType.Location = new System.Drawing.Point(15, 45);
            this.chkShowMessageType.Name = "chkShowMessageType";
            this.chkShowMessageType.Size = new System.Drawing.Size(93, 17);
            this.chkShowMessageType.TabIndex = 7;
            this.chkShowMessageType.Text = "MessaqeType";
            this.chkShowMessageType.UseVisualStyleBackColor = true;
            // 
            // chkShowDateTime
            // 
            this.chkShowDateTime.AutoSize = true;
            this.chkShowDateTime.Location = new System.Drawing.Point(15, 21);
            this.chkShowDateTime.Name = "chkShowDateTime";
            this.chkShowDateTime.Size = new System.Drawing.Size(72, 17);
            this.chkShowDateTime.TabIndex = 6;
            this.chkShowDateTime.Text = "DateTime";
            this.chkShowDateTime.UseVisualStyleBackColor = true;
            // 
            // chkWriteFromRetryQueueTest
            // 
            this.chkWriteFromRetryQueueTest.AutoSize = true;
            this.chkWriteFromRetryQueueTest.Location = new System.Drawing.Point(17, 81);
            this.chkWriteFromRetryQueueTest.Name = "chkWriteFromRetryQueueTest";
            this.chkWriteFromRetryQueueTest.Size = new System.Drawing.Size(186, 17);
            this.chkWriteFromRetryQueueTest.TabIndex = 3;
            this.chkWriteFromRetryQueueTest.Text = "&3 Write Messages In Retry Queue";
            this.chkWriteFromRetryQueueTest.UseVisualStyleBackColor = true;
            // 
            // chkInitRetryQueueTest
            // 
            this.chkInitRetryQueueTest.AutoSize = true;
            this.chkInitRetryQueueTest.Location = new System.Drawing.Point(17, 57);
            this.chkInitRetryQueueTest.Name = "chkInitRetryQueueTest";
            this.chkInitRetryQueueTest.Size = new System.Drawing.Size(136, 17);
            this.chkInitRetryQueueTest.TabIndex = 2;
            this.chkInitRetryQueueTest.Text = "&2 Init Retry Queue Test";
            this.chkInitRetryQueueTest.UseVisualStyleBackColor = true;
            // 
            // chkInitLogWriteTest
            // 
            this.chkInitLogWriteTest.AutoSize = true;
            this.chkInitLogWriteTest.Location = new System.Drawing.Point(17, 33);
            this.chkInitLogWriteTest.Name = "chkInitLogWriteTest";
            this.chkInitLogWriteTest.Size = new System.Drawing.Size(122, 17);
            this.chkInitLogWriteTest.TabIndex = 0;
            this.chkInitLogWriteTest.Text = "&1 Init Log Write Test";
            this.chkInitLogWriteTest.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(39, 515);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 8;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogLogManager\\InitWinFormsAppWithExte" +
    "ndedOptions\\InitWinFormsHelpFile.chm";
            // 
            // cmdReinit
            // 
            this.cmdReinit.Location = new System.Drawing.Point(508, 176);
            this.cmdReinit.Name = "cmdReinit";
            this.cmdReinit.Size = new System.Drawing.Size(93, 37);
            this.cmdReinit.TabIndex = 9;
            this.cmdReinit.Text = "Reinit";
            this.cmdReinit.UseVisualStyleBackColor = true;
            this.cmdReinit.Click += new System.EventHandler(this.cmdReinit_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTests;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(636, 566);
            this.Controls.Add(this.cmdReinit);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.Controls.Add(this.grpTestsToRun);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdRunTests);
            this.Controls.Add(this.cmdExit);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.grpTestsToRun.ResumeLayout(false);
            this.grpTestsToRun.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdRunTests;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.GroupBox grpTestsToRun;
        private System.Windows.Forms.CheckBox chkInitLogWriteTest;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.CheckBox chkInitRetryQueueTest;
        private System.Windows.Forms.CheckBox chkWriteFromRetryQueueTest;
        internal System.Windows.Forms.CheckBox chkShowDateTime;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.CheckBox chkShowMessageType;
        internal System.Windows.Forms.CheckBox chkShowErrorWarningTypes;
        internal System.Windows.Forms.CheckBox chkShowUsername;
        internal System.Windows.Forms.CheckBox chkShowMachineName;
        internal System.Windows.Forms.CheckBox chkShowApplicationName;
        private System.Windows.Forms.Button cmdReinit;
        private System.Windows.Forms.CheckBox chkWriteMessageToRetryQueue;
        internal System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.CheckBox chkWriteMessageToLog;
        private System.Windows.Forms.CheckBox chkWriteEveryOtherFromRetryQueueTest;
    }
}

