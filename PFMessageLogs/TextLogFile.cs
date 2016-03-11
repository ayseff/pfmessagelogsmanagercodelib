using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace PFMessageLogs
{
    /// <summary>
    /// Class for routines to write messages to a text log file. No User Interface output window is provided by this class.
    /// </summary>
    public class TextLogFile
    {
        // Fields
        private bool _showDatetime;
        private bool _showMessageType = false;         //Show message type on line
        private bool _showErrorWarningTypes = false;   //Only show error and warning messages
        private string _applicationName;
        private string _dateTimeFormat;
        private string _fileName;
        private string _machineName;

        /// <summary>
        /// Enumerates different types of messages that can appear in the log.
        /// </summary>
        public enum LogMessageType
        {
#pragma warning disable 1591
            Error = 1,
            Warning = 2,
            Information = 4,
#pragma warning restore 1591
        }


        // Methods
        /// <summary>
        /// Constructor.
        /// </summary>
        public TextLogFile()
        {
            this._showDatetime = false;
            this._showMessageType = false;
            this._fileName = "";
            this._applicationName = "";
            this._machineName = "";
            this._dateTimeFormat = "MM/dd/yyyy HH:mm:ss";
            string text = (Environment.CurrentDirectory + @"\applog.txt").Replace(@"\\", @"\");
            this._fileName = text;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileName">Name of file messages will be stored in.</param>
        public TextLogFile(string fileName)
        {
            this._showDatetime = false;
            this._fileName = "";
            this._applicationName = "";
            this._machineName = "";
            this._dateTimeFormat = "MM/dd/yyyy HH:mm:ss";
            this._fileName = fileName;
        }

        /// <summary>
        /// Sets file size to 0 bytes. Erases any existing messages in the log.
        /// </summary>
        public void TruncateFile()
        {
            if (File.Exists(this.FileName))
            {
                FileStream fs = new FileStream(this.FileName, FileMode.Truncate);
                fs.Close();
            }
        }

        /// <summary>
        /// Writes message line to text log file. This is considered to be an Information message.
        /// </summary>
        /// <param name="message">Text to output.</param>
        public void WriteLine(string message)
        {
            WriteLine(message, LogMessageType.Information);
        }

        /// <summary>
        /// Writes message line to text log file. Type of message is also specified.
        /// </summary>
        /// <param name="message">Text to output.</param>
        /// <param name="logMessageType">Use LogMessageType enumeration to specify the type of the message: e.g. Information, warning, error.</param>
        public void WriteLine(string message, LogMessageType logMessageType)
        {
            string messageDateAndType = "";
            string formattedMessage = "";
            string messagePrefix = "";
            string machineName = "";
            try
            {
                if (this._showDatetime)
                {
                    messageDateAndType = DateTime.Now.ToString(this._dateTimeFormat);
                }
                else
                {
                    messageDateAndType = "";
                }

                if (this._showMessageType)
                {
                    if (messageDateAndType.Length > 0)
                        messageDateAndType += " ";
                    messageDateAndType += logMessageType.ToString().ToUpper() + ": ";
                }

                if (this.ShowErrorWarningTypes == true && this.ShowMessageType == false)
                {
                    if (logMessageType == LogMessageType.Warning || logMessageType == LogMessageType.Error)
                    {
                        if (messageDateAndType.Length > 0)
                            messageDateAndType += " ";
                        messageDateAndType += logMessageType.ToString().ToUpper() + ": ";
                    }
                }


                if (this._machineName.Length > 0)
                {
                    if (this._applicationName.Length > 0)
                    {
                        machineName = " on " + this._machineName;
                    }
                    else
                    {
                        machineName = this._machineName;
                    }
                }
                else
                {
                    machineName = "";
                }
                if ((this._applicationName.Length > 0) | (machineName.Length > 0))
                {
                    messagePrefix = (messageDateAndType + " <" + (this._applicationName + machineName).Trim() + "> ").Trim();
                }
                else
                {
                    messagePrefix = messageDateAndType.Trim();
                }
                if (messagePrefix.Length > 0)
                {
                    formattedMessage = messagePrefix + " " + message;
                }
                else
                {
                    formattedMessage = message;
                }
                StreamWriter writer = File.AppendText(this._fileName);
                writer.WriteLine(formattedMessage);
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Writes empty string to text log file.
        /// </summary>
        public void WriteBlankLine()
        {
            try
            {
                StreamWriter writer = File.AppendText(this._fileName);
                writer.WriteLine(string.Empty);
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // Properties
        /// <summary>
        /// Name of application writing to the log. This will be shown as part of each message. Leave blank to not include application name in the message output. 
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return this._applicationName;
            }
            set
            {
                this._applicationName = value;
            }
        }

        /// <summary>
        /// .NET format string to use for display of date/time values.
        /// </summary>
        public string DateTimeFormat
        {
            get
            {
                return this._dateTimeFormat;
            }
            set
            {
                this._dateTimeFormat = value;
            }
        }

        /// <summary>
        /// Full path to log file.
        /// </summary>
        public string FileName
        {
            get
            {
                return this._fileName;
            }
            set
            {
                this._fileName = value;
            }
        }

        // Properties
        /// <summary>
        /// Name of PC on which log writes taking place. This name will be shown as part of each message. Leave blank to not include machine name in the message output. 
        /// </summary>
        public string MachineName
        {
            get
            {
                return this._machineName;
            }
            set
            {
                this._machineName = value;
            }
        }

        /// <summary>
        /// Specifies whether or not to show date/time with each message.
        /// </summary>
        public bool ShowDatetime
        {
            get
            {
                return this._showDatetime;
            }
            set
            {
                this._showDatetime = value;
            }
        }

        /// <summary>
        /// Specifies whether or not to include the message type with the message. 
        /// When true, all message types are identified as part of the message output.
        /// </summary>
        public bool ShowMessageType
        {
            get
            {
                return this._showMessageType;
            }
            set
            {
                this._showMessageType = value;
            }
        }

        /// <summary>
        /// Specifies whether or not to include Error and Warning message types with the message. 
        /// When true, only the Error and Warning message type identifications are included as part of the message output.
        /// </summary>
        public bool ShowErrorWarningTypes
        {
            get
            {
                return this._showErrorWarningTypes;
            }
            set
            {
                this._showErrorWarningTypes = value;
            }
        }



    }//end class
}//end namespace
