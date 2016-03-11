//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace PFMessageLogs
{
    /// <summary>
    /// Class encapsulates functionality to write to the Windows event log.
    /// </summary>
    /// <remarks>Caller must have elevated security permissions (e.g. administrator permissions) to create and delete event sources and event logs.</remarks>
    public class WindowsLog
    {
        /// <summary>
        /// Enumeration of windows event log names.
        /// </summary>
        public enum EventLogName
        {
#pragma warning disable 1591
            Unknown = 0,
            Application = 1,
            System = 2,
            Security = 3,
            Setup = 4
#pragma warning restore 1591
        }

        /// <summary>
        /// Enumeration of valid message types for windows event logs.
        /// </summary>
        public enum WindowsEventLogEntryType
        {
#pragma warning disable 1591
            Unknown = 0,
            Error = EventLogEntryType.Error,
            Warning = EventLogEntryType.Warning,
            Information = EventLogEntryType.Information,
            SuccessAudit = EventLogEntryType.SuccessAudit,
            FailureAudit = EventLogEntryType.FailureAudit
#pragma warning restore 1591
        }

        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        private EventLog _eventLog = null;
        private string _logName = "Application";
        private string _machineName = ".";
        private string _eventSource = "PFApps";             //event source must already exist
        private static string _eventSourceInitializer = WindowsLog.GetStringValueFromConfigFile("EventSourceInitializer", string.Empty);
        private static string _currentWorkingDirectory = Environment.CurrentDirectory;
        private static string _outputMessages = string.Empty;
        private static string _errorMessages = string.Empty;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public WindowsLog()
        {
            InitEventLog(string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logName">Name of the log to write to.</param>
        public WindowsLog(EventLogName logName)
        {
            InitEventLog(logName.ToString(), string.Empty, string.Empty);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logName">Name of the log to write to.</param>
        /// <param name="machineName">Name of machine where log is located.</param>
        public WindowsLog(EventLogName logName, string machineName)
        {
            InitEventLog(logName.ToString(), machineName, string.Empty);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logName">Name of the log to write to.</param>
        /// <param name="machineName">Name of machine where log is located.</param>
        /// <param name="eventSource">Source of the event log entries.</param>
        public WindowsLog(EventLogName logName, string machineName, string eventSource)
        {
            InitEventLog(logName.ToString(), machineName, eventSource);
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logName">Name of the log to write to.</param>
        public WindowsLog(string logName)
        {
            InitEventLog(logName, string.Empty, string.Empty);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logName">Name of the log to write to.</param>
        /// <param name="machineName">Name of machine where log is located.</param>
        public WindowsLog(string logName, string machineName)
        {
            InitEventLog(logName, machineName, string.Empty);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logName">Name of the log to write to.</param>
        /// <param name="machineName">Name of machine where log is located.</param>
        /// <param name="eventSource">Source of the event log entries.</param>
        public WindowsLog(string logName, string machineName, string eventSource)
        {
            InitEventLog(logName, machineName, eventSource);
        }

        // this routine assumes that the eventSource already exists.
        private void InitEventLog(string logName, string machineName, string eventSource)
        {
            if (logName == "Unknown" || String.IsNullOrEmpty(logName))
                _logName = WindowsLog.GetStringValueFromConfigFile("WindowsEventLog", "Application");
            else
                _logName = logName;

            if (String.IsNullOrEmpty(machineName))
                _machineName = WindowsLog.GetStringValueFromConfigFile("WindowsEventLogMachineName", ".");
            else
                _machineName = machineName;

            if (String.IsNullOrEmpty(eventSource))
                _eventSource = WindowsLog.GetStringValueFromConfigFile("WindowsEventLogEventSource", "PFApps");
            else
                _eventSource = eventSource;


            _eventLog = new EventLog(_logName, _machineName, _eventSource);

        }

        //properties

        /// <summary>
        /// EventLog Property.
        /// </summary>
        public System.Diagnostics.EventLog EventLog
        {
            get
            {
                return _eventLog;
            }
        }

        /// <summary>
        /// LogName Property.
        /// </summary>
        public string LogName
        {
            get
            {
                return _logName;
            }
            set
            {
                _logName = value;
            }
        }

        /// <summary>
        /// MachineName Property.
        /// </summary>
        public string MachineName
        {
            get
            {
                return _machineName;
            }
            set
            {
                _machineName = value;
            }
        }

        /// <summary>
        /// EventSource Property.
        /// </summary>
        public string EventSource
        {
            get
            {
                return _eventSource;
            }
            set
            {
                _eventSource = value;
            }
        }

        /// <summary>
        /// Full path to the executable that can initialize an event source. 
        /// </summary>
        /// <remarks>Value can be specified in an application's config file by setting the value for the EventSourceInitializer key.</remarks>
        public static string EventSourceInitializer
        {
            get
            {
                return _eventSourceInitializer;
            }
            set
            {
                _eventSourceInitializer = value;
            }
        }

        /// <summary>
        /// CurrentWorkingDirectory Property.
        /// </summary>
        public static string CurrentWorkingDirectory
        {
            get
            {
                return _currentWorkingDirectory;
            }
            set
            {
                _currentWorkingDirectory = value;
            }
        }

        /// <summary>
        /// Returns output messages, if any, generated by the process that registers the event source name.
        /// </summary>
        public static string OutputMessages
        {
            get
            {
                return _outputMessages;
            }
        }

        /// <summary>
        /// Returns error messages, if any, generated by the process that registers the event source name.
        /// </summary>
        public static string ErrorMessages
        {
            get
            {
                return _errorMessages;
            }
        }




        //methods

        /// <summary>
        /// Routine to write messages to an event log.
        /// </summary>
        /// <param name="message">Text of the message to be written.</param>
        /// <param name="entryType">Type of message: Error, Warning, Information, SuccessAudit or FailureAudit.</param>
        public void WriteEntry (string message, WindowsEventLogEntryType entryType)
        {
            if(EventLog.SourceExists(_eventSource))
                _eventLog.WriteEntry(message, (EventLogEntryType)entryType);
        }

        /// <summary>
        /// Determines whether or not an event source is registered on the local computer.
        /// </summary>
        /// <param name="eventSourceName">Name of the event source.</param>
        /// <returns></returns>
        public static bool SourceExists(string eventSourceName)
        {
            return EventLog.SourceExists(eventSourceName);
        }

        /// <summary>
        /// Determines whether or not an event source is registered on a specified computer.
        /// </summary>
        /// <param name="eventSourceName">Name of the event source.</param>
        /// <param name="machineName">Name of the computer on which to look, or "." for the local computer.</param>
        /// <returns></returns>
        public static bool SourceExists(string eventSourceName, string machineName)
        {
            return EventLog.SourceExists(eventSourceName, machineName);
        }

        ///// <summary>
        ///// Method for registering a Windows Event Log event source.
        ///// </summary>
        ///// <param name="logName">Name of log to which messages will be written. Usually is the Application log.</param>
        ///// <param name="machineName">Name of the machine on which the log resides. Specify . or localhost for local machine.</param>
        ///// <param name="eventSourceName">Name of the event source. Usually will be PFApps for ProFast applications.</param>
        ///// <returns></returns>
        //public static int RegisterEventSource(string logName, string machineName, string eventSourceName)
        //{
        //    StringBuilder msg = new StringBuilder();

        //    if (WindowsEventLog.EventSourceInitializer == string.Empty)
        //    {
        //        msg.Length = 0;
        //        msg.Append("You must specify the EventSourceInitializer key in the configuration file or specify the EventSourceInitializer property of this object in order to register the event source.");
        //        throw new System.Exception(msg.ToString());
        //    }

        //    string currentFolder = Environment.CurrentDirectory;
        //    Process initializeEventSourceNameProcess = new Process();
        //    string processMessages = string.Empty;
        //    int processExitCode = -1;


        //    initializeEventSourceNameProcess.StartInfo.WorkingDirectory = WindowsEventLog.CurrentWorkingDirectory;
        //    initializeEventSourceNameProcess.StartInfo.FileName = WindowsEventLog.EventSourceInitializer;
        //    initializeEventSourceNameProcess.StartInfo.Arguments = BuildRegisterArguments(logName, machineName, eventSourceName);
        //    initializeEventSourceNameProcess.StartInfo.CreateNoWindow = true;
        //    initializeEventSourceNameProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    initializeEventSourceNameProcess.StartInfo.UseShellExecute = false;
        //    initializeEventSourceNameProcess.StartInfo.RedirectStandardOutput = true;
        //    initializeEventSourceNameProcess.StartInfo.RedirectStandardError = true;


        //    initializeEventSourceNameProcess.Start();
        //    initializeEventSourceNameProcess.WaitForExit();
        //    processExitCode = initializeEventSourceNameProcess.ExitCode;

        //    _outputMessages = initializeEventSourceNameProcess.StandardOutput.ReadToEnd();
        //    _errorMessages = initializeEventSourceNameProcess.StandardError.ReadToEnd();

        //    return processExitCode;
        
        //}

        //private static string BuildRegisterArguments(string logName, string machineName, string eventSourceName)
        //{
        //    StringBuilder arguments = new StringBuilder();

        //    arguments.Length = 0;

        //    arguments.Append(logName);
        //    arguments.Append(" ");
        //    arguments.Append(machineName);
        //    arguments.Append(" ");
        //    arguments.Append(eventSourceName);

        //    return arguments.ToString();

        //}

        /// <summary>
        /// Establishes a valid event source for writing event messages to the application log on the local computer.
        /// </summary>
        /// <param name="eventSourceName">The source by which the application is registered on the local computer. </param>
        public static void CreateEventSource(string eventSourceName)
        {
            EventLog.CreateEventSource(eventSourceName, EventLogName.Application.ToString());
        }

        /// <summary>
        /// Establishes the specified source name as a valid event source for writing entries to a log on the local computer. This method can also create a new custom log on the local computer.
        /// </summary>
        /// <param name="eventSourceName">The source by which the application is registered on the specified computer. </param>
        /// <param name="logName">The name of the log the source's entries are written to. Possible values include Application, System, or a custom event log. If you do not specify a value, logName defaults to Application. </param>
        public static void CreateEventSource(string eventSourceName, string logName)
        {
            EventLog.CreateEventSource(eventSourceName, logName);
        }

        /// <summary>
        /// Establishes the specified source name as a valid event source for writing entries to a log on the specified computer. This method can also be used to create a new custom log on the specified computer.
        /// </summary>
        /// <param name="eventSourceName">The source by which the application is registered on the specified computer. </param>
        /// <param name="logName">The name of the log the source's entries are written to. Possible values include Application, System, or a custom event log. If you do not specify a value, logName defaults to Application. </param>
        /// <param name="machineName">The name of the computer to register this event source with, or "." for the local computer. </param>
        public static void CreateEventSource(string eventSourceName, string logName, string machineName)
        {
            EventSourceCreationData eventSourceData = new EventSourceCreationData(eventSourceName, logName);
            eventSourceData.MachineName = machineName;
            EventLog.CreateEventSource(eventSourceData);
        }

        /// <summary>
        /// Removes the event source registration from the event log of the local computer.
        /// </summary>
        /// <param name="eventSourceName">The name by which the application is registered in the event log system. </param>
        public static void DeleteEventSource(string eventSourceName)
        {
            EventLog.DeleteEventSource(eventSourceName);
        }

        /// <summary>
        /// Removes the application's event source registration from the specified computer.
        /// </summary>
        /// <param name="eventSourceName">The name by which the application is registered in the event log system. </param>
        /// <param name="machineName">The name of the computer to remove the registration from, or "." for the local computer. </param>
        public static void DeleteEventSource(string eventSourceName, string machineName)
        {
            EventLog.DeleteEventSource(eventSourceName, machineName);
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <param name="psDefaultValue">Value to return if key not found.</param>
        /// <returns>String value if key found; otherwise an psDefaultValue is returned.</returns>
        public static string GetStringValueFromConfigFile(string psKey, string psDefaultValue)
        {
            string sKey = psKey;
            if (sKey == null)
                sKey = string.Empty;
            string sConfigValue = GetConfigValue(sKey);
            if (sConfigValue.Length == 0)
                sConfigValue = psDefaultValue;
            return sConfigValue;
        }

        /// <summary>
        /// Retrieves application configuration value for specified key.
        /// </summary>
        /// <param name="psKey">Configuration key to search for.</param>
        /// <returns>String value if key found; otherwise an empty string is returned.</returns>
        public static string GetConfigValue(string psKey)
        {
            string sConfigValue = string.Empty;
            try
            {
                sConfigValue = string.Empty;
                sConfigValue = System.Configuration.ConfigurationManager.AppSettings[psKey];
            }
            catch
            {
                sConfigValue = string.Empty;
            }

            if (sConfigValue == null)
                sConfigValue = string.Empty;

            return sConfigValue;

        }



    }//end class
}//end namespace
