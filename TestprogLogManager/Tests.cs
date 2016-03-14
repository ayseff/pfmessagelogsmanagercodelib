using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Configuration;
using PFLogManagerObjects;

namespace TestprogLogManager
{
    public class Tests
    {
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();
        private static bool _saveErrorMessagesToAppLog = false;

        private static string _textLogFileLocation = @"C:\Temp\TestAppFolders\Logs\Testlog.txt";
        private static string _xmlLogRetryQueueLocation = @"C:\Temp\TestAppFolders\Logs\Testlog_RetryQueue.xml";

        //constructor
        static Tests()
        {
            string configValue = string.Empty;

            configValue = AppGlobals.AppConfig.GetStringValueFromConfigFile("TextLogFileLocation", string.Empty);
            if (configValue != string.Empty)
            {
                _textLogFileLocation = configValue;
            }
            configValue = AppGlobals.AppConfig.GetStringValueFromConfigFile("XmlLogRetryQueueLocation", string.Empty);
            if (configValue != string.Empty)
            {
                _xmlLogRetryQueueLocation = configValue;
            }

        }

        //properties
        public static bool SaveErrorMessagesToAppLog
        {
            get
            {
                return Tests._saveErrorMessagesToAppLog;
            }
            set
            {
                Tests._saveErrorMessagesToAppLog = value;
            }
        }

        //helper methods

        private static PFLogManager GetNewLogManager(MainForm frm)
        {
            PFLogManager logMgr = null;
            enLogFileStorageType logType = enLogFileStorageType.TextFile;
            string logConnectionString = _textLogFileLocation;
            enLogRetryQueueStorageType retryQueueType = enLogRetryQueueStorageType.XmlFile;
            string retryQueueConnectionString = _xmlLogRetryQueueLocation;

            logMgr = new PFLogManager(logType, logConnectionString, retryQueueType, retryQueueConnectionString);

            return logMgr;
        }

        private static PFLogMessage GetNewLogMessage(MainForm frm)
        {
            PFLogMessage logMsg = null;

            logMsg = new PFLogMessage();
            logMsg.ShowDatetime = frm.chkShowDateTime.Checked;
            logMsg.ShowMessageType = frm.chkShowMessageType.Checked;
            logMsg.ShowErrorWarningTypes = frm.chkShowErrorWarningTypes.Checked;
            logMsg.ShowApplicationName = frm.chkShowApplicationName.Checked;
            logMsg.ShowMachineName = frm.chkShowMachineName.Checked;
            logMsg.ShowUsername = frm.chkShowUsername.Checked; 

            return logMsg;
        }

        //tests
        public static void InitLogWriteTest(MainForm frm)
        {
            PFLogManager logMgr = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("InitLogWriteTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                logMgr = GetNewLogManager(frm);

                PFLogMessage logMsg = GetNewLogMessage(frm);
                logMsg.LogMessageType = enLogMessageType.Alert;
                logMsg.MessageText = "Test message from a test program ... just testing!";
                logMgr.WriteMessageToLog(logMsg);

                logMsg.LogMessageType = enLogMessageType.Information;
                logMsg.MessageText = "Message from a test program ... another one*";
                logMgr.WriteMessageToLog(logMsg);

                logMsg.LogMessageType = enLogMessageType.Warning;
                logMsg.MessageText = "Message from a test program ... this is a warning!*";
                logMgr.WriteMessageToLog(logMsg);

                logMsg.LogMessageType = enLogMessageType.Error;
                logMsg.MessageText = "Message from a test program ... the error is found!!*";
                logMgr.WriteMessageToLog(logMsg);


                if (logMgr != null)
                {
                    if (String.IsNullOrEmpty(logMgr.LogFileConnectionString) == false)
                    {
                        _msg.Length = 0;
                        _msg.Append(Environment.NewLine);
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Log messages written to ");
                        _msg.Append(logMgr.LogFileConnectionString);
                        _msg.Append(Environment.NewLine);
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... InitLogWriteTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void InitRetryQueueTest(MainForm frm)
        {
            PFLogManager logMgr = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("InitRetryQueueTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                logMgr = GetNewLogManager(frm);

                PFLogMessage logMsg = GetNewLogMessage(frm);
                logMsg.LogMessageType = enLogMessageType.Alert;
                logMsg.MessageText = "Test message from a test program ... just testing!";
                logMgr.WriteMessageToLogRetryQueue(logMsg);

                logMsg = GetNewLogMessage(frm);
                logMsg.LogMessageType = enLogMessageType.Warning;
                logMsg.MessageText = "Message from a test program ... another one*";
                logMgr.WriteMessageToLogRetryQueue(logMsg);

                logMsg = GetNewLogMessage(frm);
                logMsg.LogMessageType = enLogMessageType.Warning;
                logMsg.MessageText = "Message from a test program ... this is a warning!*";
                logMgr.WriteMessageToLogRetryQueue(logMsg);

                logMsg = GetNewLogMessage(frm);
                logMsg.LogMessageType = enLogMessageType.Error;
                logMsg.MessageText = "Message from a test program ... the error is found!!*";
                logMgr.WriteMessageToLogRetryQueue(logMsg);

                if (logMgr != null)
                {
                    if (String.IsNullOrEmpty(logMgr.LogFileConnectionString) == false)
                    {
                        _msg.Length = 0;
                        _msg.Append(Environment.NewLine);
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Log messages written to retry queue at ");
                        _msg.Append(logMgr.LogRetryQueueConnectionString);
                        _msg.Append(Environment.NewLine);
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... InitRetryQueueTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void WriteFromRetryQueueTest(MainForm frm)
        {
            PFLogManager logMgr = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("WriteFromRetryQueueTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                logMgr = new PFLogManager(enLogFileStorageType.TextFile, _textLogFileLocation, enLogRetryQueueStorageType.XmlFile, _xmlLogRetryQueueLocation);

                int numMsgsSent = logMgr.WriteLogMessagesOnRetryQueue();
                //int numMsgsSent = logMgr.TestWriteLogMessagesOnRetryQueue();

                _msg.Length = 0;
                _msg.Append("Num messages resent: ");
                _msg.Append(numMsgsSent.ToString("#,##0"));
                Program._messageLog.WriteLine(_msg.ToString());

                if (logMgr != null)
                {
                    if (String.IsNullOrEmpty(logMgr.LogFileConnectionString) == false)
                    {
                        _msg.Length = 0;
                        _msg.Append(Environment.NewLine);
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Log messages read from retry queue at ");
                        _msg.Append(logMgr.LogRetryQueueConnectionString);
                        _msg.Append(" to log file at ");
                        _msg.Append(logMgr.LogFileConnectionString);
                        _msg.Append(Environment.NewLine);
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... WriteFromRetryQueueTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void WriteEveryOtherFromRetryQueueTest(MainForm frm)
        {
            PFLogManager logMgr = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("WriteEveryOtherFromRetryQueueTest started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                logMgr = new PFLogManager(enLogFileStorageType.TextFile, _textLogFileLocation, enLogRetryQueueStorageType.XmlFile, _xmlLogRetryQueueLocation);

                int numMsgsSent = logMgr.TestWriteLogMessagesOnRetryQueue();

                _msg.Length = 0;
                _msg.Append("Num messages resent: ");
                _msg.Append(numMsgsSent.ToString("#,##0"));
                Program._messageLog.WriteLine(_msg.ToString());

                if (logMgr != null)
                {
                    if (String.IsNullOrEmpty(logMgr.LogFileConnectionString) == false)
                    {
                        _msg.Length = 0;
                        _msg.Append(Environment.NewLine);
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Log messages read from retry queue at ");
                        _msg.Append(logMgr.LogRetryQueueConnectionString);
                        _msg.Append(" to log file at ");
                        _msg.Append(logMgr.LogFileConnectionString);
                        _msg.Append(Environment.NewLine);
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... WriteEveryOtherFromRetryQueueTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public static void TestWriteMessageToLog(MainForm frm)
        {
            PFLogManager logMgr = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("TestWriteMessageToLog started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                logMgr = GetNewLogManager(frm);
                PFLogMessage logMsg = GetNewLogMessage(frm);
                logMsg.MessageText = frm.txtMessage.Text;

                logMgr.WriteMessageToLog(logMsg);

                if (logMgr != null)
                {
                    if (String.IsNullOrEmpty(logMgr.LogFileConnectionString) == false)
                    {
                        _msg.Length = 0;
                        _msg.Append(Environment.NewLine);
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Message written to ");
                        _msg.Append(logMgr.LogFileConnectionString);
                        _msg.Append(Environment.NewLine);
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... TestWriteMessageToLog finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        public static void TestWriteMessageToRetryQueue(MainForm frm)
        {
            PFLogManager logMgr = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("TestWriteMessageToRetryQueue started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                logMgr = GetNewLogManager(frm);
                PFLogMessage logMsg = GetNewLogMessage(frm);
                logMsg.MessageText = frm.txtMessage.Text;

                logMgr.WriteMessageToLogRetryQueue(logMsg);

                if (logMgr != null)
                {
                    if (String.IsNullOrEmpty(logMgr.LogFileConnectionString) == false)
                    {
                        _msg.Length = 0;
                        _msg.Append(Environment.NewLine);
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Message written to retry queue at ");
                        _msg.Append(logMgr.LogRetryQueueConnectionString);
                        _msg.Append(Environment.NewLine);
                        Program._messageLog.WriteLine(_msg.ToString());
                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... TestWriteMessageToRetryQueue finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
        


    }//end class
}//end namespace
