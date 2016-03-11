using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace AppGlobals
{
    /// <summary>
    /// Class for managing windows forms application message displays and logging.
    /// </summary>
    public class AppMessages
    {
        // Fields
        private static string _appLogFilename = "";
        private static string _defaultMessageBoxCaption = Application.ProductName;

        // Methods

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
                string caption = Application.ProductName + " Alert";
                DisplayMessage(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Unexpected Error in DisplayAlertMessage", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                string caption = Application.ProductName + " Error";
                DisplayMessage(FormatErrorMessage(pEx), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Unexpected Error in DisplayErrorMessage", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                string caption = Application.ProductName + " Error";
                errorMessage = message;
                DisplayMessage(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Hand, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Unexpected Error in DisplayErrorMessage", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                string caption = Application.ProductName + " Information";
                DisplayMessage(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Unexpected Error in DisplayInfoMessage", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
        public static DialogResult DisplayMessage(string message, string caption, MessageBoxButtons pButtonType, MessageBoxIcon pMessageType)
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
        public static DialogResult DisplayMessage(string message, string caption, MessageBoxButtons pButtonType, MessageBoxIcon pMessageType, bool writeToAppLog)
        {
            return  DisplayMessage(message, caption, pButtonType, pMessageType, writeToAppLog, false);
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
        public static DialogResult DisplayMessage(string message, string caption, MessageBoxButtons buttonType, MessageBoxIcon messageType, bool writeToAppLog, bool writeToEventLog)
        {
            DialogResult res = DialogResult.None;
            try
            {
                if (writeToAppLog)
                {
                    WriteToAppLog(message, messageType);
                }
                if (writeToEventLog)
                {
                    EventLogEntryType eventLogMessageType;
                    MessageBoxIcon icon = messageType;
                    switch (icon)
                    {
                        case MessageBoxIcon.Error:
                            eventLogMessageType = EventLogEntryType.Error;
                            break;
                        case MessageBoxIcon.Warning:
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
                MessageBox.Show(ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Unexpected Error in DisplayMessage", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                res = DialogResult.Ignore;
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
                string caption = Application.ProductName + " Warning";
                DisplayMessage(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, writeToAppLog, writeToEventLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, "Unexpected Error in DisplayWarningMessage", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            WriteToAppLog(message, MessageBoxIcon.Asterisk);
        }

        /// <summary>
        /// Writes a message to the application text-based log file.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="messageType">Type of message: Error, warning, or information.</param>
        public static void WriteToAppLog(string message, MessageBoxIcon messageType)
        {
            string appLogFilename = "";
            string separator = new string('-', 50);
            string msgType = "";
            try
            {
                appLogFilename = GetAppLogFilename();
                //if (_appLogFilename.Length > 0)
                //{
                //    if (Path.GetDirectoryName(_appLogFilename).Length == 0)
                //        //appLogFilename = Application.StartupPath + @"\" + _appLogFilename;
                //        appLogFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _appLogFilename);
                //    else
                //        appLogFilename = _appLogFilename;
                //}
                //else
                //{
                //    //appLogFilename = Application.StartupPath + @"\" + Application.ProductName + "_log.txt";
                //    appLogFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Application.ProductName + "_log.txt");
                //}
                StreamWriter log = new StreamWriter(appLogFilename, true);
                log.WriteLine(separator);

                switch (messageType)
                {
                    case MessageBoxIcon.Error:
                        msgType = "Error:\n";
                        break;
                    case MessageBoxIcon.Warning:
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
                string caption = Application.ProductName + " App Log Write Failure";
                MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// Get the full path to the current application log file.
        /// </summary>
        /// <returns>Full path to the application log file.</returns>
        public static string GetAppLogFilename()
        {
            string appLogFilename = "";
            if (_appLogFilename.Length > 0)
            {
                if (Path.GetDirectoryName(_appLogFilename).Length == 0)
                    //appLogFilename = Application.StartupPath + @"\" + _appLogFilename;
                    appLogFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _appLogFilename);
                else
                    appLogFilename = _appLogFilename;
            }
            else
            {
                //appLogFilename = Application.StartupPath + @"\" + Application.ProductName + "_log.txt";
                appLogFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Application.ProductName + "_log.txt");
            }
            return appLogFilename;
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
                if(EventLog.SourceExists(eventSourceName))
                    log.WriteEntry(message, messageType);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace;
                string caption = Application.ProductName + " Event Log Write Failure";
                MessageBox.Show(errorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                log.Close();
            }
        }

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
    }

    /// <summary>
    /// Class for handling all errors not processed by the application code.
    /// </summary>
    public class CAppGlobalErrorHandler
    {
        // Fields
        private static bool _cancelApplicationOnGlobalError = false;
        private static bool _writeToAppLog = true;
        private static bool _writeToEventLog = false;

        // Methods

        /// <summary>
        /// Global error handler that is automatically run if an error is not caught by any application try ... catch routines. You do not typically call this routine directly.
        /// </summary>
        /// <param name="sender">Parameter.</param>
        /// <param name="t">Parameter.</param>
        public static void GlobalErrorHandler(object sender, ThreadExceptionEventArgs t)
        {
            Exception ex = t.Exception;
            string caption = "Global Error Handler for " + Application.ProductName;
            string errMsg = string.Empty;
            if (_writeToAppLog)
            {
                errMsg = caption + "\r\n" + AppMessages.FormatErrorMessage(ex) + Environment.NewLine + "Check application log for more information: " + Environment.NewLine + AppMessages.GetAppLogFilename();
                AppMessages.WriteToAppLog(AppMessages.FormatErrorMessageWithStackTrace(ex));
            }
            else
            {
                errMsg = caption + "\r\n" + AppMessages.FormatErrorMessageWithStackTrace(ex);
            }
            AppMessages.DisplayMessage(errMsg, caption, MessageBoxButtons.OK, MessageBoxIcon.Hand, _writeToAppLog, _writeToEventLog);
            //if(_writeToAppLog)
            //    AppMessages.WriteToAppLog(AppMessages.FormatErrorMessageWithStackTrace(ex));
            if (_cancelApplicationOnGlobalError)
            {
                CAppForms.CloseAllForms();
                Application.Exit();
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
    }

    /// <summary>
    /// Class defines a collection used to keep track of the forms open in an application. 
    /// See <see cref="CAppForms"/> class for more information.
    /// </summary>
    public class CFormsCollection : CollectionBase
    {
        // Methods
        /// <summary>
        /// Adds a form to the list of forms.
        /// </summary>
        /// <param name="poFormObject">Form to add to the list.</param>
        /// <returns>Returns poFormObject.</returns>
        public Form Add(Form poFormObject)
        {
            base.List.Add(poFormObject);
            return poFormObject;
        }

        /// <summary>
        /// Removes a form from the collection of forms.
        /// </summary>
        /// <param name="poFormObject">For to remove.</param>
        public void Remove(Form poFormObject)
        {
            base.List.Remove(poFormObject);
        }
    }

    /// <summary>
    /// Class for managing open windows forms. Particular useful for MDI applications. 
    /// </summary>
    public class CAppForms
    {
        // Fields
        private static CFormsCollection _winForms = new CFormsCollection();

        // Methods
        /// <summary>
        /// Adds form to collection that contains this application's forms.
        /// </summary>
        /// <param name="winForm">Form object.</param>
        /// <returns>The winform parameter is returned.</returns>
        public static Form Add(Form winForm)
        {
            return _winForms.Add(winForm);
        }

        /// <summary>
        /// Close all the open forms.
        /// </summary>
        public static void CloseAllForms()
        {
            CloseAllForms(null);
        }

        /// <summary>
        /// Closes all the open forms except the form with the current focus.
        /// </summary>
        /// <param name="doNotCloseThisForm">Current form that should not be clowed.</param>
        public static void CloseAllForms(Form doNotCloseThisForm)
        {
            ArrayList list = new ArrayList();
            int num = 0;
            try
            {
                if (_winForms.Count > 0)
                {
                    IEnumerator enumerator;
                    enumerator = _winForms.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        Form current = (Form)enumerator.Current;
                        if (doNotCloseThisForm != null)
                        {
                            if (current.Name != doNotCloseThisForm.Name)
                            {
                                list.Add(current);
                            }
                        }
                        else
                        {
                            list.Add(current);
                        }
                    }
                }
                if (list.Count > 0)
                {
                    for (num = list.Count - 1; num >= 0; num += -1)
                    {
                        ((Form)list[num]).Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while attempting to close all forms. \r\n" + ex.Source + "\r\n" + ex.Message + "\r\n" + ex.StackTrace, Application.ProductName + " Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// Removes form from list of application forms.
        /// </summary>
        /// <param name="poForm">Form object to remove.</param>
        public static void Remove(Form poForm)
        {
            _winForms.Remove(poForm);
        }

        // Properties
        /// <summary>
        /// Collection containing the forms that are being tracked.
        /// </summary>
        public static CFormsCollection Forms
        {
            get
            {
                return _winForms;
            }
        }
    }



}
