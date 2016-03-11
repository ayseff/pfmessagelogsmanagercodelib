using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using AppGlobals;
using PFMessageLogs;

namespace TestprogLogManager
{
    static class Program
    {
        public static MainForm _mainForm;
        public static MessageLog _messageLog;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new CMainForm());


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CAppGlobalErrorHandler.WriteToAppLog = true;
            CAppGlobalErrorHandler.WriteToEventLog = false;
            CAppGlobalErrorHandler.CancelApplicationOnGlobalError = false;

            Application.ThreadException += new ThreadExceptionEventHandler(CAppGlobalErrorHandler.GlobalErrorHandler);

            _messageLog = new MessageLog();
            _messageLog.Caption = "Test Progam (TestprogLogManager)";
            _messageLog.ShowDatetime = false;
            _messageLog.Font = "Lucida Console";
            _messageLog.FontSize = (float)10.0;
            _messageLog.ShowWindow();

            //MessageBox.Show(Environment.MachineName);

            _mainForm = new MainForm();

            Application.Run(_mainForm);


        }
    }//end class
}//end namespace
