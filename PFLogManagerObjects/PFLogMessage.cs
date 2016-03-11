//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;

namespace PFLogManagerObjects
{
    /// <summary>
    /// Contains definition for a log message that will be written by an instance of the PFLogManager class.
    /// </summary>
    public class PFLogMessage
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();


        //private variables for properties
        private DateTime _logMessageDateTime = DateTime.MinValue;
        private enLogMessageType _logMessageType = enLogMessageType.Information;
        private bool _showDatetime = false;
        private bool _showMessageType = false;         //Show message type on line
        private bool _showErrorWarningTypes = false;   //Only show error and warning messages
        private bool _showApplicationName = false;
        private bool _showMachineName = false;
        private bool _showUsername = false;
        private string _applicationName = Assembly.GetEntryAssembly().GetName().Name;
        private string _machineName = Environment.MachineName;
        private string _username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        private string _dateTimeFormat = "MM/dd/yyyy HH:mm:ss";
        private string _messageText = string.Empty;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFLogMessage()
        {
            ;            
        }

        //properties

        /// <summary>
        /// Date/time log message was written.
        /// </summary>
        public DateTime LogMessageDateTime
        {
            get
            {
                return _logMessageDateTime;
            }
            set
            {
                _logMessageDateTime = value;
            }
        }

        /// <summary>
        /// Specifies message level: Information, Alert, Warning or Error.
        /// </summary>
        public enLogMessageType LogMessageType
        {
            get
            {
                return _logMessageType;
            }
            set
            {
                _logMessageType = value;
            }
        }

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
                if (_machineName.Trim().Length > 0)
                    _showMachineName = true;
            }
        }

        /// <summary>
        /// Logged on username.
        /// </summary>
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                if (_username.Trim().Length > 0)
                    _showUsername = true;
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

        /// <summary>
        /// Specifies whether or not to show the application name in front of the message in text output.
        /// </summary>
        public bool ShowApplicationName
        {
            get
            {
                return _showApplicationName;
            }
            set
            {
                _showApplicationName = value;
            }
        }

        /// <summary>
        /// Specifies whether or not to show the computer name in front of the message in text output.
        /// </summary>
        public bool ShowMachineName
        {
            get
            {
                return _showMachineName;
            }
            set
            {
                _showMachineName = value;
            }
        }

        /// <summary>
        /// Specifies whether or not to show the logon username in front of the message in text output.
        /// </summary>
        public bool ShowUsername
        {
            get
            {
                return _showUsername;
            }
            set
            {
                _showUsername = value;
            }
        }


        /// <summary>
        /// Message that is to be logged.
        /// </summary>
        public string MessageText
        {
            get
            {
                return _messageText;
            }
            set
            {
                _messageText = value;
            }
        }

        /// <summary>
        /// Log message text that includes such information at message severity level, date/time, application name and/or machine name.
        /// </summary>
        public string FormattedMessageText
        {
            get
            {
                return FormatLogMessageText();
            }
        }


        //methods

        /// <summary>
        /// Routine to create a formatted message that includes such information at message severity level, date/time, application name and/or machine name.
        /// </summary>
        /// <returns>String value containing formatted message.</returns>
        private string FormatLogMessageText()
        {
            string formattedMessage = string.Empty;
            string messageDateAndType = string.Empty;
            string messagePrefix = string.Empty;
            string machineName = string.Empty;
            string username = string.Empty;

            if (this.ShowDatetime)
            {
                messageDateAndType = DateTime.Now.ToString(this.DateTimeFormat);
            }
            else
            {
                messageDateAndType = "";
            }

            if (this.ShowMessageType)
            {
                if (messageDateAndType.Length > 0)
                    messageDateAndType += " ";
                messageDateAndType += this.LogMessageType.ToString().ToUpper() + ": ";
            }

            if (this.ShowErrorWarningTypes == true && this.ShowMessageType == false)
            {
                if (this.LogMessageType == enLogMessageType.Warning || this.LogMessageType == enLogMessageType.Error)
                {
                    if (messageDateAndType.Length > 0)
                        messageDateAndType += " ";
                    messageDateAndType += this.LogMessageType.ToString().ToUpper() + ": ";
                }
            }


            if (this.MachineName.Length > 0 && this.ShowMachineName)
            {
                if (this.ApplicationName.Length > 0 && this.ShowApplicationName)
                {
                    machineName = " on " + this.MachineName;
                }
                else
                {
                    machineName = this.MachineName;
                }
            }
            else
            {
                machineName = "";
            }

            if (this.Username.Length > 0 && this.ShowUsername)
            {
                username = this.Username;
            }
            else
            {
                username = "";
            }

            
            if ((this.ApplicationName.Length > 0 && this.ShowApplicationName) || (machineName.Length > 0 && this.ShowMachineName))
            {
                messagePrefix = (messageDateAndType + " <" + (this.ApplicationName + machineName).Trim() + "> ").Trim();
            }
            else
            {
                messagePrefix = messageDateAndType.Trim();
            }

            if (username.Length > 0 && messagePrefix.Length > 0)
            {
                messagePrefix += " [" + username + "]";
            }
            else if (username.Length > 0 && messagePrefix.Length == 0)
            {
                messagePrefix += "[" + username + "]";
            }
            else
            {
                messagePrefix += "";
            }

            if (messagePrefix.Length > 0)
            {
                formattedMessage = messagePrefix + " " + this.MessageText;
            }
            else
            {
                formattedMessage = this.MessageText;
            }




            return formattedMessage;
        }




        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFLogMessage));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFLogMessage.</returns>
        public static PFLogMessage LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFLogMessage));
            TextReader textReader = new StreamReader(filePath);
            PFLogMessage objectInstance;
            objectInstance = (PFLogMessage)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
        }


        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            data.Append(PropertiesToString());
            data.Append("\r\n");
            data.Append(FieldsToString());
            data.Append("\r\n");


            return data.ToString();
        }


        /// <summary>
        /// Routine outputs name and value for all properties.
        /// </summary>
        /// <returns></returns>
        public string PropertiesToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("\r\nClass properties for");
            data.Append(t.FullName);
            data.Append("\r\n");


            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);

                /*
                //****************************************************************************************
                //use the following code if you class has an indexer or is derived from an indexed class
                //****************************************************************************************
                object val = null;
                StringBuilder temp = new StringBuilder();
                if (prop.GetIndexParameters().Length == 0)
                {
                    val = prop.GetValue(this, null);
                }
                else if (prop.GetIndexParameters().Length == 1)
                {
                    temp.Length = 0;
                    for (int i = 0; i < this.Count; i++)
                    {
                        temp.Append("Index ");
                        temp.Append(i.ToString());
                        temp.Append(" = ");
                        temp.Append(val = prop.GetValue(this, new object[] { i }));
                        temp.Append("  ");
                    }
                    val = temp.ToString();
                }
                else
                {
                    //this is an indexed property
                    temp.Length = 0;
                    temp.Append("Num indexes for property: ");
                    temp.Append(prop.GetIndexParameters().Length.ToString());
                    temp.Append("  ");
                    val = temp.ToString();
                }
                //****************************************************************************************
                // end code for indexed property
                //****************************************************************************************
                */

                if (prop.GetGetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetGetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetGetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetGetMethod(true).IsPublic && !prop.GetGetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetGetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" get ");
                    data.Append("> ");
                }
                if (prop.GetSetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetSetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetSetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetSetMethod(true).IsPublic && !prop.GetSetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetSetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" set ");
                    data.Append("> ");
                }
                data.Append(" ");
                data.Append(prop.PropertyType.FullName);
                data.Append(" ");

                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (prop.PropertyType.IsArray)
                {
                    System.Collections.IList valueList = (System.Collections.IList)prop.GetValue(this, null);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        /// <summary>
        /// Routine outputs name and value for all fields.
        /// </summary>
        /// <returns></returns>
        public string FieldsToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            FieldInfo[] finfos = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            bool typeHasFieldsToStringMethod = false;

            data.Append("\r\nClass fields for ");
            data.Append(t.FullName);
            data.Append("\r\n");

            int inx = 0;
            int maxInx = finfos.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                FieldInfo fld = finfos[inx];
                object val = fld.GetValue(this);
                if (fld.IsPublic)
                    data.Append(" public ");
                if (fld.IsPrivate)
                    data.Append(" private ");
                if (!fld.IsPublic && !fld.IsPrivate)
                    data.Append(" internal ");
                if (fld.IsStatic)
                    data.Append(" static ");
                data.Append(" ");
                data.Append(fld.FieldType.FullName);
                data.Append(" ");
                data.Append(fld.Name);
                data.Append(": ");
                typeHasFieldsToStringMethod = UseFieldsToString(fld.FieldType);
                if (val != null)
                    if (typeHasFieldsToStringMethod)
                        data.Append(GetFieldValues(val));
                    else
                        data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (fld.FieldType.IsArray)
                //if (fld.Name == "TestStringArray" || "_testStringArray")
                {
                    System.Collections.IList valueList = (System.Collections.IList)fld.GetValue(this);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        private bool UseFieldsToString(Type pType)
        {
            bool retval = false;

            //avoid have this type calling its own FieldsToString and going into an infinite loop
            if (pType.FullName != this.GetType().FullName)
            {
                MethodInfo[] methods = pType.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == "FieldsToString")
                    {
                        retval = true;
                        break;
                    }
                }
            }

            return retval;
        }

        private string GetFieldValues(object typeInstance)
        {
            Type typ = typeInstance.GetType();
            MethodInfo methodInfo = typ.GetMethod("FieldsToString");
            Object retval = methodInfo.Invoke(typeInstance, null);


            return (string)retval;
        }


        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFLogMessage));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFLogMessage.</returns>
        public static PFLogMessage LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFLogMessage));
            StringReader strReader = new StringReader(xmlString);
            PFLogMessage objectInstance;
            objectInstance = (PFLogMessage)deserializer.Deserialize(strReader);
            strReader.Close();
            return objectInstance;
        }


        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ToXmlString());
            return doc;
        }


    }//end class
}//end namespace
