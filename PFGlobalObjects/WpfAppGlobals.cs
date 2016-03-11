//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2014
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Configuration;
using System.Threading;
using System.Diagnostics;  
using System.IO;

namespace AppGlobals
{
    /// <summary>
    /// Class containing global message routines for WPF applications.
    /// </summary>
    public class WpfAppMessages
    {
        //private work variables
        private static StringBuilder _msg = new StringBuilder();
        private static string _productName = AppInfo.AssemblyProduct.Length > 0 ? AppInfo.AssemblyProduct : AppInfo.AssemblyName.Length > 0 ? AppInfo.AssemblyName : Environment.MachineName;

        //private variables for properties
        private static string _appLogFilename = "";
        private static string _defaultMessageBoxCaption = AppInfo.AssemblyProduct.Length > 0 ? AppInfo.AssemblyProduct : AppInfo.AssemblyName.Length>0?AppInfo.AssemblyName:Environment.MachineName;

        //constructors

        //properties
        // Properties
        /// <summary>
        /// File name for the application  log.
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
        /// Caption to display on message boxes.
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

        //methods

        /// <summary>
        /// Displays alert message on popup form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        public static void DisplayAlertMessage(string message)
        {
            DisplayAlertMessage(message, false, false);
        }

        /// <summary>
        /// Displays alert message on popup form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="writeToAppLog">If true, message is written to the application log file.</param>
        public static void DisplayAlertMessage(string message, bool writeToAppLog)
        {
            DisplayAlertMessage(message, writeToAppLog, false);
        }

        /// <summary>
        /// Displays alert message on popup form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="writeToAppLog">If true, message is written to the application text log file.</param>
        /// <param name="writeToEventLog">If true, message is written to Windows Application Event Log.</param>
        public static void DisplayAlertMessage(string message, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                string caption = _productName + " Alert";
                DisplayMessage(message, caption, MessageBoxButton.OK, MessageBoxImage.Asterisk, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Unexpected Error in DisplayAlertMessage", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        /// <summary>
        /// Displays error message on popup form.
        /// </summary>
        /// <param name="ex">Exception object containing message to display.</param>
        public static void DisplayErrorMessage(Exception ex)
        {
            DisplayErrorMessage(ex, false, false);
        }

        /// <summary>
        /// Displays error message on popup form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        public static void DisplayErrorMessage(string message)
        {
            DisplayErrorMessage(message, false, false);
        }

        /// <summary>
        /// Displays error message on popup form.
        /// </summary>
        /// <param name="ex">Exception object containing message to display.</param>
        /// <param name="writeToAppLog">If true, message is written to the application text log file.</param>
        public static void DisplayErrorMessage(Exception ex, bool writeToAppLog)
        {
            DisplayErrorMessage(ex, writeToAppLog, false);
        }

        /// <summary>
        /// Displays error message on popup form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="writeToAppLog">If true, message is written to the application text log file.</param>
        public static void DisplayErrorMessage(string message, bool writeToAppLog)
        {
            DisplayErrorMessage(message, writeToAppLog, false);
        }

        /// <summary>
        /// Displays error message on popup form.
        /// </summary>
        /// <param name="pEx">Exception object containing message to display.</param>
        /// <param name="writeToAppLog">If true, message is written to the application text log file.</param>
        /// <param name="writeToEventLog">If true, message is written to Windows Application Event Log.</param>
        public static void DisplayErrorMessage(Exception pEx, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                string caption = _productName + " Error";
                DisplayMessage(FormatErrorMessage(pEx), caption, MessageBoxButton.OK, MessageBoxImage.Hand, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Unexpected Error in DisplayErrorMessage", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        /// <summary>
        /// Displays error message on popup form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="writeToAppLog">If true, message is written to the application text log file.</param>
        /// <param name="writeToEventLog">If true, message is written to Windows Application Event Log.</param>
        public static void DisplayErrorMessage(string message, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                string errorMessage = "";
                string caption = _productName + " Error";
                errorMessage = message;
                DisplayMessage(errorMessage, caption, MessageBoxButton.OK, MessageBoxImage.Hand, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Unexpected Error in DisplayErrorMessage", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        /// <summary>
        /// Displays information message on pop up form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        public static void DisplayInfoMessage(string message)
        {
            DisplayInfoMessage(message, false, false);
        }

        /// <summary>
        /// Displays information message on pop up form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="writeToAppLog">If true, message is written to the application text log file.</param>
        public static void DisplayInfoMessage(string message, bool writeToAppLog)
        {
            DisplayInfoMessage(message, writeToAppLog, false);
        }

        /// <summary>
        /// Displays information message on pop up form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="writeToAppLog">If true, message is written to the application text log file.</param>
        /// <param name="writeToEventLog">If true, message is written to Windows Application Event Log.</param>
        public static void DisplayInfoMessage(string message, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                string caption = _productName + " Information";
                DisplayMessage(message, caption, MessageBoxButton.OK, MessageBoxImage.Asterisk, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Unexpected Error in DisplayInfoMessage", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        /// <summary>
        /// Displays message on a popup form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="caption">Form caption.</param>
        /// <param name="pButtonType">Type of button.</param>
        /// <param name="pMessageType">Message type.</param>
        /// <returns>DialogResult value.</returns>
        public static MessageBoxResult DisplayMessage(string message, string caption, MessageBoxButton pButtonType, MessageBoxImage pMessageType)
        {
            return DisplayMessage(message, caption, pButtonType, pMessageType, false, false);
        }

        /// <summary>
        /// Displays message on a popup form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="caption">Form caption.</param>
        /// <param name="pButtonType">Type of button.</param>
        /// <param name="pMessageType">Message type.</param>
        /// <param name="writeToAppLog">If true, message is written to the application text log file.</param>
        /// <returns>DialogResult value.</returns>
        public static MessageBoxResult DisplayMessage(string message, string caption, MessageBoxButton pButtonType, MessageBoxImage pMessageType, bool writeToAppLog)
        {
            return DisplayMessage(message, caption, pButtonType, pMessageType, writeToAppLog, false);
        }

        /// <summary>
        /// Displays message on a popup form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="caption">Form caption.</param>
        /// <param name="buttonType">Type of button.</param>
        /// <param name="messageType">Message type.</param>
        /// <param name="writeToAppLog">If true, message is written to the application text log file.</param>
        /// <param name="writeToEventLog">If true, message is written to Windows Application Event Log.</param>
        /// <returns>DialogResult value.</returns>
        public static MessageBoxResult DisplayMessage(string message, string caption, MessageBoxButton buttonType, MessageBoxImage messageType, bool writeToAppLog, bool writeToEventLog)
        {
            MessageBoxResult res = MessageBoxResult.None;
            try
            {
                if (writeToAppLog)
                {
                    WriteToAppLog(message, messageType);
                }
                if (writeToEventLog)
                {
                    EventLogEntryType eventLogMessageType;
                    MessageBoxImage icon = messageType;
                    switch (icon)
                    {
                        case MessageBoxImage.Error:
                            eventLogMessageType = EventLogEntryType.Error;
                            break;
                        case MessageBoxImage.Warning:
                            eventLogMessageType = EventLogEntryType.Warning;
                            break;
                        default:
                            eventLogMessageType = EventLogEntryType.Information;
                            break;

                    }
                    WriteToEventLog(message, eventLogMessageType);
                }
                res = MessageBox.Show(message, caption, buttonType, messageType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Unexpected Error in DisplayMessage", MessageBoxButton.OK, MessageBoxImage.Hand);
                res = MessageBoxResult.None;
            }
            return res;
        }

        /// <summary>
        /// Display warning message on a popup form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        public static void DisplayWarningMessage(string message)
        {
            DisplayWarningMessage(message, false, false);
        }

        /// <summary>
        /// Display warning message on a popup form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="writeToAppLog">If true, message is written to the application text log file.</param>
        public static void DisplayWarningMessage(string message, bool writeToAppLog)
        {
            DisplayWarningMessage(message, writeToAppLog, false);
        }

        /// <summary>
        /// Display warning message on a popup form.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="writeToAppLog">If true, message is written to the application text log file.</param>
        /// <param name="writeToEventLog">If true, message is written to Windows Application Event Log.</param>
        public static void DisplayWarningMessage(string message, bool writeToAppLog, bool writeToEventLog)
        {
            try
            {
                string caption = _productName + " Warning";
                DisplayMessage(message, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Unexpected Error in DisplayWarningMessage", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        /// <summary>
        /// Routine to format contents of an exception object into a string representing the different parts of the error object, including the message.
        /// </summary>
        /// <param name="pex">Error object.</param>
        /// <returns>String with contents of error object formatted.</returns>
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
        /// Routine to format contents of an exception object into a string representing the different parts of the error object, including the message.
        ///  The message will include information on any InnerException objects represented by the Exception object in the the parameter.
        /// </summary>
        /// <param name="pex">Error object.</param>
        /// <returns>String with contents of error object formatted.</returns>
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
        /// Writes a message to the application text-based log file.
        /// </summary>
        /// <param name="message">Message to log.</param>
        public static void WriteToAppLog(string message)
        {
            WriteToAppLog(message, MessageBoxImage.Asterisk);
        }

        /// <summary>
        /// Writes a message to the application text-based log file.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="messageType">Type of message: Error, warning, or information.</param>
        public static void WriteToAppLog(string message, MessageBoxImage messageType)
        {
            string appLogFilename = "";
            string separator = new string('-', 50);
            string msgType = "";
            string startupPath = string.Empty;

            try
            {
                startupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                if (_appLogFilename.Length > 0)
                {
                    if (Path.GetDirectoryName(_appLogFilename).Length == 0)
                        appLogFilename = Path.Combine(startupPath, appLogFilename);
                    else
                        appLogFilename = _appLogFilename;
                }
                else
                {
                    appLogFilename = Path.Combine(startupPath, _productName + "_log.txt");
                }
                StreamWriter log = new StreamWriter(appLogFilename, true);
                log.WriteLine(separator);

                switch (messageType)
                {
                    case MessageBoxImage.Error:
                        msgType = "Error:\n";
                        break;
                    case MessageBoxImage.Warning:
                        msgType = "Warning:\n";
                        break;
                    default:
                        msgType = string.Empty;
                        break;
                }

                log.WriteLine(msgType + DateTime.Now.ToString());
                log.WriteLine(separator);
                log.WriteLine(message);
                log.WriteLine("");
                log.Flush();
                log.Close();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace;
                string caption = _productName + " App Log Write Failure";
                MessageBox.Show(errorMessage, caption, MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        /// <summary>
        /// Writes a message to the Windows application event log.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="messageType">Type of message: Error, warning, or information.</param>
        public static void WriteToEventLog(string message, EventLogEntryType messageType)
        {
            string logName = AppConfig.GetStringValueFromConfigFile("WindowsEventLog", "Application");
            string machineName = AppConfig.GetStringValueFromConfigFile("WindowsEventLogMachineName", ".");
            string eventSourceName = AppConfig.GetStringValueFromConfigFile("WindowsEventLogEventSource", "PFApps");

            EventLog log = new EventLog(logName, machineName, eventSourceName);
            try
            {
                if (EventLog.SourceExists(eventSourceName))
                    log.WriteEntry(message, messageType);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace;
                string caption = _productName + " Event Log Write Failure";
                MessageBox.Show(errorMessage, caption, MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            finally
            {
                log.Close();
            }
        }

        /// <summary>
        /// Simulates DoEvents for Wpf applications with a call to Application.Current.Dispatcher.Invoke method.
        /// </summary>
        public static void WpfDoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
        }

    }//end class

    /// <summary>
    /// Class for handling all errors not processed by the application code.
    /// </summary>
    public class CWpfAppGlobalErrorHandler
    {
        // Fields
        private static bool _cancelApplicationOnGlobalError = false;
        private static bool _writeToAppLog = false;
        private static bool _writeToEventLog = false;

        private static string _productName = AppInfo.AssemblyProduct.Length > 0 ? AppInfo.AssemblyProduct : AppInfo.AssemblyName.Length > 0 ? AppInfo.AssemblyName : Environment.MachineName;

        // Methods

        /// <summary>
        /// Global error handler that is automatically run if an error is not caught by any application try ... catch routines. You do not typically call this routine directly.
        /// </summary>
        /// <param name="sender">Parameter.</param>
        /// <param name="e">Parameter.</param>
        public static void GlobalErrorHandler(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            Exception ex = e.Exception;
            string caption = "Global Error Handler for " + _productName;
            string errMsg = "Unhandled application error has occurred.\r\n";
            errMsg += WpfAppMessages.FormatErrorMessage(ex);
            errMsg += "\n\nDo you want to continue?\n(if you click Yes you will continue with your work, if you click No the application will close";

            if(WriteToAppLog)
                AppGlobals.WpfAppMessages.WriteToAppLog("Unhandled application error has occurred.\r\n" + WpfAppMessages.FormatErrorMessageWithStackTrace(ex));

            if (WriteToEventLog)
                AppGlobals.WpfAppMessages.WriteToEventLog("Unhandled application error has occurred.\r\n" + WpfAppMessages.FormatErrorMessage(ex), EventLogEntryType.Error);

            WpfAppMessages.DisplayErrorMessage(ex);

            if (CancelApplicationOnGlobalError)
            {
                Application.Current.Shutdown();
            }

        }



        // Properties

        /// <summary>
        /// Determines whether or not to cancel the application in the event the global error handler is fired by some unexpected error in the app code.
        /// </summary>
        public static bool CancelApplicationOnGlobalError
        {
            get
            {
                return _cancelApplicationOnGlobalError;
            }
            set
            {
                _cancelApplicationOnGlobalError = value;
            }
        }

        /// <summary>
        /// If true, then messages will be written to the app log.
        /// </summary>
        public static bool WriteToAppLog
        {
            get
            {
                return _writeToAppLog;
            }
            set
            {
                _writeToAppLog = value;
            }
        }

        /// <summary>
        /// If true, then messages will be written to the Windows event log.
        /// </summary>
        public static bool WriteToEventLog
        {
            get
            {
                return _writeToEventLog;
            }
            set
            {
                _writeToEventLog = value;
            }
        }
    }//end class




}//end namespace
