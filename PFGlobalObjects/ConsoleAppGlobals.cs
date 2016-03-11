using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace AppGlobals
{

    /// <summary>
    /// Class that console applications can use for displaying application messages and logging events to text and windows logs.
    /// </summary>
    public class ConsoleMessages
    {
        /// <summary>
        /// Enumeration o valid message types for console messages.
        /// </summary>
        public enum pfConsoleMessageType
        {
#pragma warning disable 1591
            Information = 0,
            Alert = 1,
            Warning = 2,
            Error = 3
#pragma warning restore 1591
        }

        // Fields
        private static string _appLogFilename = "";
        private static bool _copyConsoleOutputToAppLog;
        private static string _prodName = AppInfo.AssemblyProduct.Length > 0 ? AppInfo.AssemblyProduct : AppInfo.AssemblyName.Length>0?AppInfo.AssemblyName:Environment.MachineName;
        private static string _defaultMessageBoxCaption = _prodName;

        // Properties
        /// <summary>
        /// Full path to file that will contain messages.
        /// </summary>
        public static string AppLogFilename
        {
            get
            {
                return _appLogFilename;
            }
            set
            {
                _appLogFilename = value;
            }
        }

        /// <summary>
        /// Set to true to copy all console output to the log file.
        /// </summary>
        public static bool CopyConsoleOutputToAppLog
        {
            get
            {
                return ConsoleMessages._copyConsoleOutputToAppLog;
            }
            set
            {
                ConsoleMessages._copyConsoleOutputToAppLog = value;
            }
        }

        /// <summary>
        /// Caption for any message boxes that can be displayed.
        /// </summary>
        public static string DefaultMessageBoxCaption
        {
            get
            {
                return _defaultMessageBoxCaption;
            }
            set
            {
                _defaultMessageBoxCaption = value;
            }
        }

        // Methods
        /// <summary>
        /// Output alert type message.
        /// </summary>
        /// <param name="message">Text of message to output</param>
        public static void DisplayAlertMessage(string message)
        {
            DisplayAlertMessage(message, false, false);
        }

        /// <summary>
        /// Output alert type message.
        /// </summary>
        /// <param name="message">Text of message to output</param>
        /// <param name="writeToAppLog">If true, then message will be copied to the app log file.</param>
        public static void DisplayAlertMessage(string message, bool writeToAppLog)
        {
            DisplayAlertMessage(message, writeToAppLog, false);
        }

        /// <summary>
        /// Output alert type message.
        /// </summary>
        /// <param name="message">Text of message to output</param>
        /// <param name="writeToAppLog">If true, then message will be copied to the app log file.</param>
        /// <param name="writeToEventLog">If true, then message will be copied to the windows application event log.</param>
        public static void DisplayAlertMessage(string message, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                DisplayMessage(message, pfConsoleMessageType.Alert, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error in DisplayMessage" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Output error type message.
        /// </summary>
        /// <param name="ex">Exception that triggered the error message.</param>
        public static void DisplayErrorMessage(Exception ex)
        {
            DisplayErrorMessage(ex, false, false);
        }

        /// <summary>
        /// Output error type message.
        /// </summary>
        /// <param name="message">Text of message to output</param>
        public static void DisplayErrorMessage(string message)
        {
            DisplayErrorMessage(message, false, false);
        }

        /// <summary>
        /// Output error type message.
        /// </summary>
        /// <param name="ex">Exception that triggered the error message.</param>
        /// <param name="writeToAppLog">If true, then message will be copied to the app log file.</param>
        public static void DisplayErrorMessage(Exception ex, bool writeToAppLog)
        {
            DisplayErrorMessage(ex, writeToAppLog, false);
        }

        /// <summary>
        /// Output error type message.
        /// </summary>
        /// <param name="message">Text of message to output</param>
        /// <param name="writeToAppLog">If true, then message will be copied to the app log file.</param>
        public static void DisplayErrorMessage(string message, bool writeToAppLog)
        {
            DisplayErrorMessage(message, writeToAppLog, false);
        }

        /// <summary>
        /// Output error type message.
        /// </summary>
        /// <param name="pEx">Exception that triggered the error message.</param>
        /// <param name="writeToAppLog">If true, then message will be copied to the app log file.</param>
        /// <param name="writeToEventLog">If true, then message will be copied to the windows application event log.</param>
        public static void DisplayErrorMessage(Exception pEx, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                DisplayMessage(FormatErrorMessage(pEx), pfConsoleMessageType.Error, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error in DisplayMessage" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Output error type message.
        /// </summary>
        /// <param name="message">Text of message to output</param>
        /// <param name="writeToAppLog">If true, then message will be copied to the app log file.</param>
        /// <param name="writeToEventLog">If true, then message will be copied to the windows application event log.</param>
        public static void DisplayErrorMessage(string message, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                DisplayMessage(message, pfConsoleMessageType.Error, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error in DisplayMessage" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Output information type message.
        /// </summary>
        /// <param name="message">Text of message to output.</param>
        public static void DisplayInfoMessage(string message)
        {
            DisplayInfoMessage(message, false, false);
        }

        /// <summary>
        /// Output information type message.
        /// </summary>
        /// <param name="message">Text of message to output.</param>
        /// <param name="writeToAppLog">If true, then message will be copied to the app log file.</param>
        public static void DisplayInfoMessage(string message, bool writeToAppLog)
        {
            DisplayInfoMessage(message, writeToAppLog, false);
        }

        /// <summary>
        /// Output information type message.
        /// </summary>
        /// <param name="message">Text of message to output.</param>
        /// <param name="writeToAppLog">If true, then message will be copied to the app log file.</param>
        /// <param name="writeToEventLog">If true, then message will be copied to the windows application event log.</param>
        public static void DisplayInfoMessage(string message, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                DisplayMessage(message, pfConsoleMessageType.Information, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error in DisplayMessage" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Output message of specified type.
        /// </summary>
        /// <param name="message">Text of message to output.</param>
        /// <param name="messageType">Use pfConsoleMessageType enumeration to specify type of message to output.</param>
        public static void DisplayMessage(string message, pfConsoleMessageType messageType)
        {
            DisplayMessage(message, messageType, false, false);
        }

        /// <summary>
        /// Output message of specified type.
        /// </summary>
        /// <param name="message">Text of message to output.</param>
        /// <param name="messageType">Use pfConsoleMessageType enumeration to specify type of message to output.</param>
        /// <param name="writeToAppLog">If true, then message will be copied to the app log file.</param>
        public static void DisplayMessage(string message, pfConsoleMessageType messageType, bool writeToAppLog)
        {
            DisplayMessage(message, messageType, writeToAppLog, false);
        }

        /// <summary>
        /// Outputs message to console.
        /// </summary>
        /// <param name="message">Text of message to output.</param>
        /// <param name="messageType">Message Level: Information, Warning, Error, Alert.</param>
        /// <param name="writeToAppLog">Copy messaage to the app log.</param>
        /// <param name="writeToEventLog">Copy message to the windows application event log.</param>
        public static void DisplayMessage(string message, pfConsoleMessageType messageType, bool writeToAppLog, bool writeToEventLog)
        {
            string messagePrefix = string.Empty;
            if (messageType != pfConsoleMessageType.Information)
                messagePrefix = messageType.ToString() + ": ";

            try
            {
                if (writeToAppLog)
                {
                    WriteToAppLog(message, messageType);
                }
                if (writeToEventLog)
                {
                    EventLogEntryType eventLogMessageType;
                    switch (messageType)
                    {
                        case pfConsoleMessageType.Error:
                            eventLogMessageType = EventLogEntryType.Error;
                            break;
                        case pfConsoleMessageType.Warning:
                            eventLogMessageType = EventLogEntryType.Warning;
                            break;
                        default:
                            eventLogMessageType = EventLogEntryType.Information;
                            break;

                    }
                    WriteToEventLog(message, eventLogMessageType);
                }
                Console.WriteLine(messagePrefix + message);
            }
            catch (Exception ex)
            {
               Console.WriteLine( "Unexpected Error in DisplayMessage" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Output warning type message.
        /// </summary>
        /// <param name="message">Text of message to output</param>
        public static void DisplayWarningMessage(string message)
        {
            DisplayWarningMessage(message, false, false);
        }

        /// <summary>
        /// Output warning type message.
        /// </summary>
        /// <param name="message">Text of message to output</param>
        /// <param name="writeToAppLog">If true, then message will be copied to the app log file.</param>
        public static void DisplayWarningMessage(string message, bool writeToAppLog)
        {
            DisplayWarningMessage(message, writeToAppLog, false);
        }

        /// <summary>
        /// Output warning type message.
        /// </summary>
        /// <param name="message">Text of message to output</param>
        /// <param name="writeToAppLog">If true, then message will be copied to the app log file.</param>
        /// <param name="writeToEventLog">If true, then message will be copied to the windows application event log.</param>
        public static void DisplayWarningMessage(string message, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                DisplayMessage(message, pfConsoleMessageType.Warning, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error in DisplayMessage" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Routine to format the contents of an exception object to formatted text message.
        /// </summary>
        /// <param name="pex">Exception object to format.</param>
        /// <returns>Formatted text containing exception information.</returns>
        public static string FormatErrorMessage(Exception pex)
        {
            StringBuilder errMsg = new StringBuilder();
            try
            {
                errMsg.Append("Source: ");
                errMsg.Append(pex.Source);
                errMsg.Append(": \r\n");
                errMsg.Append(pex.Message);
                Exception iex = pex.InnerException;
                while (iex != null)
                {
                    errMsg.Append("\r\n\r\nInner Exception:\r\nSource: ");
                    errMsg.Append(iex.Source);
                    errMsg.Append(": \r\n");
                    errMsg.Append(iex.Message);
                    iex = iex.InnerException;
                }
            }
            catch (Exception ex)
            {
                errMsg.Append("\r\n*** UNEXPECTED ERROR: ***\r\nUnable to format error message.\n");
                errMsg.Append("Source: ");
                errMsg.Append(ex.Source);
                errMsg.Append(": \r\n");
                errMsg.Append(ex.Message);
            }
            return errMsg.ToString();
        }

        /// <summary>
        /// Routine to format the contents of an exception object and the stack trace to a formatted text message.
        /// </summary>
        /// <param name="pex">Exception object to format.</param>
        /// <returns>Formatted text containing exception information and stack trace.</returns>
        public static string FormatErrorMessageWithStackTrace(Exception pex)
        {
            StringBuilder errMsg = new StringBuilder();
            try
            {
                errMsg.Append("Source: ");
                errMsg.Append(pex.Source);
                errMsg.Append(": \r\n");
                errMsg.Append(pex.Message);
                errMsg.Append(": \r\n");
                errMsg.Append(pex.StackTrace);
                Exception iex = pex.InnerException;
                while (iex != null)
                {
                    errMsg.Append("\r\n\r\nInner Exception:\r\nSource: ");
                    errMsg.Append(iex.Source);
                    errMsg.Append(": \r\n");
                    errMsg.Append(iex.Message);
                    errMsg.Append(": \r\n");
                    errMsg.Append(iex.StackTrace);
                    iex = iex.InnerException;
                }
            }
            catch (Exception ex)
            {
                errMsg.Append("\r\n*** UNEXPECTED ERROR: ***\r\nUnable to format error message.\n");
                errMsg.Append("Source: ");
                errMsg.Append(ex.Source);
                errMsg.Append(": \r\n");
                errMsg.Append(ex.Message);
            }
            return errMsg.ToString();

        }

        /// <summary>
        /// Writes messeage to the app log file.
        /// </summary>
        /// <param name="message">Text of the message to output.</param>
        public static void WriteToAppLog(string message)
        {
            WriteToAppLog(message, pfConsoleMessageType.Information);
        }

        /// <summary>
        /// Writes messeage to the app log file.
        /// </summary>
        /// <param name="message">Text of the message to output.</param>
        /// <param name="messageType">Use pfConsoleMessageType enumeration to specify type of message to output.</param>
        public static void WriteToAppLog(string message, pfConsoleMessageType messageType)
        {
            string appLogFilename = "";
            string separator = new string('-', 50);
            string msgType = "";
            try
            {
                if (_appLogFilename.Length > 0)
                {
                    if (Path.GetDirectoryName(_appLogFilename).Length == 0)
                        appLogFilename = Environment.CurrentDirectory  + @"\" + _appLogFilename;
                    else
                        appLogFilename = _appLogFilename;
                }
                else
                {
                    appLogFilename = Environment.CurrentDirectory + @"\" + _prodName + "_log.txt";
                }
                StreamWriter log = new StreamWriter(appLogFilename, true);

                switch (messageType)
                {
                    case pfConsoleMessageType.Error:
                        msgType = "Error:\n";
                        break;
                    case pfConsoleMessageType.Warning:
                        msgType = "Warning:\n";
                        break;
                    default:
                        msgType = string.Empty;
                        break;
                }

                if (_copyConsoleOutputToAppLog)
                {
                    //console output likely being copied to app log to simulate redirecting of output
                    //output redirection does not work when runas administrator processing being done by some apps
                    if(messageType == pfConsoleMessageType.Warning || messageType == pfConsoleMessageType.Error)
                        log.WriteLine(messageType.ToString() + ": " + message);
                    else
                        log.WriteLine(message);
                }
                else
                {
                    log.WriteLine(separator);
                    log.WriteLine(msgType + DateTime.Now.ToString());
                    log.WriteLine(separator);
                    log.WriteLine(message);
                    log.WriteLine("");
                }
                log.Flush();
                log.Close();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace;
                Console.WriteLine(errorMessage);
            }
        }

        /// <summary>
        /// Outputs specified message text to the windows application event log.
        /// </summary>
        /// <param name="message">Text of message to output.</param>
        /// <param name="messageType">Use EventLogEntryType enumeration to specified the type of the message (Information, Warning, Error, etc.)</param>
        public static void WriteToEventLog(string message, EventLogEntryType messageType)
        {
            EventLog log = new EventLog("Application", ".", _prodName);
            try
            {
                log.WriteEntry(message, messageType);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace;
                Console.WriteLine(errorMessage);
            }
            finally
            {
                log.Close();
            }
        }

    }//end class




}//end namespace
