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
using PFCollectionsObjects;
using PFTextFiles;

namespace PFLogManagerObjects
{
    /// <summary>
    /// Class to manage messages to an application log. Has features that save messages when a write fails. Manager will write the message to the log later using messages stored on a retry queue.
    /// </summary>
    public class PFLogManager
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private string _applicationName = Assembly.GetEntryAssembly().GetName().Name;

        private string _defaultTextLogFileConnectionString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "applog.txt");
        private string _defaultXmlLogFileConnectionString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "applog.xml");

        private string _defaultXmlLogRetryQueueConnectionString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "applog_retry_queue.xml");


        //private variables for properties
        private enLogFileStorageType _logFileStorageType = enLogFileStorageType.TextFile;
        private enLogRetryQueueStorageType _logRetryQueueStorageType = enLogRetryQueueStorageType.XmlFile;
        private string _logFileConnectionString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "applog.txt");
        private string _logRetryQueueConnectionString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "applog_retry_queue.xml");
        private PFList<PFLogMessage> _logRetryQueue = new PFList<PFLogMessage>();

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFLogManager()
        {
            enLogFileStorageType logFileStorageType = enLogFileStorageType.TextFile;
            string logFileConnectionString = _defaultTextLogFileConnectionString;
            enLogRetryQueueStorageType logRetryQueueStorageType = enLogRetryQueueStorageType.XmlFile;
            string logRetryQueueConnectionString = _defaultXmlLogRetryQueueConnectionString;
            InitInstance(logFileStorageType, logFileConnectionString, logRetryQueueStorageType, logRetryQueueConnectionString);
}

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFLogManager(enLogFileStorageType logFileStorageType, string logFileConnectionString)
        {
            enLogRetryQueueStorageType logRetryQueueStorageType =  enLogRetryQueueStorageType.XmlFile;
            string logRetryQueueConnectionString = _defaultXmlLogRetryQueueConnectionString;
            InitInstance(logFileStorageType, logFileConnectionString, logRetryQueueStorageType, logRetryQueueConnectionString);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFLogManager(enLogFileStorageType logFileStorageType, string logFileConnectionString,
                            enLogRetryQueueStorageType logRetryQueueStorageType, string logRetryQueueConnectionString)
        {
            InitInstance(logFileStorageType, logFileConnectionString, logRetryQueueStorageType, logRetryQueueConnectionString);
        }


        private void InitInstance(enLogFileStorageType logFileStorageType, string logFileConnectionString,
                                  enLogRetryQueueStorageType logRetryQueueStorageType, string logRetryQueueConnectionString)
        {
            _logFileStorageType = logFileStorageType;
            _logFileConnectionString = logFileConnectionString;
            _logRetryQueueStorageType = logRetryQueueStorageType;
            _logRetryQueueConnectionString = logRetryQueueConnectionString;

            if (File.Exists(_logFileConnectionString) == false)
            {
                FileStream fs = File.Create(_logFileConnectionString);
                fs.Close();

            }

            if (File.Exists(_logRetryQueueConnectionString) == false)
            {
                //create the retry queue
                _logRetryQueue.SaveToXmlFile(_logRetryQueueConnectionString);
            }

            _logRetryQueue = PFList<PFLogMessage>.LoadFromXmlFile(_logRetryQueueConnectionString);


        }

        //properties

        /// <summary>
        /// Specifies the type of data store in which the log messages will be written.
        /// </summary>
        public enLogFileStorageType LogFileStorageType
        {
            get
            {
                return _logFileStorageType;
            }
            //set
            //{
            //    _logFileStorageType = value;
            //}
        }

        /// <summary>
        /// Specifies how the retry queue for log messages will be saved to external storage.
        /// </summary>
        /// <remarks>Log retry queue contains messages where the write operation failed. The manager will use this queue to try to resend the message at a later time.</remarks>
        public enLogRetryQueueStorageType LogRetryQueueStorageType
        {
            get
            {
                return _logRetryQueueStorageType;
            }
            //set
            //{
            //    _logRetryQueueStorageType = value;
            //}
        }

        /// <summary>
        /// Specifies the file path if log is formatted as a text or xml file.
        /// </summary>
        public string LogFileConnectionString
        {
            get
            {
                return _logFileConnectionString;
            }
            set
            {
                _logFileConnectionString = value;
            }
        }

        /// <summary>
        /// Specifies the file path if the retry queue is formatted as an xml file.
        /// </summary>
        public string LogRetryQueueConnectionString
        {
            get
            {
                return _logRetryQueueConnectionString;
            }
            set
            {
                _logRetryQueueConnectionString = value;
            }
        }





        //methods

        /// <summary>
        /// Writes message to message log encapsulated by this instance of PFLogManager.
        /// </summary>
        /// <param name="logMessage">Text of message to be written to log.</param>
        public void WriteMessageToLog(PFLogMessage logMessage)
        {
            WriteMessageToTextLog(logMessage);
        }

        private bool WriteMessageToTextLog(PFLogMessage logMessage)
        {
            return WriteMessageToTextLog(logMessage, true);
        }

        private bool WriteMessageToTextLog(PFLogMessage logMessage, bool saveFailedWriteToRetryQueue)
        {
            PFTextFile logfile = new PFTextFile(_logFileConnectionString, PFFileOpenOperation.OpenFileForAppend);
            bool logWriteSucceeded = false;

            try
            {
                logfile.WriteLine(logMessage.FormattedMessageText);
                logWriteSucceeded = true;
            }
            catch
            {
                logWriteSucceeded = false;
            }
            finally
            {
                if (logfile != null)
                    if (logfile.FileIsOpen)
                        logfile.CloseFile();
                logfile = null;

            }

            if (!logWriteSucceeded && saveFailedWriteToRetryQueue)
            {
                SaveLogMessageToRetryQueue(logMessage);
            }

            return logWriteSucceeded;

        }

        /// <summary>
        /// Used by unit test routines to fill up a retry queue with dummy messages.
        /// </summary>
        /// <param name="logMessage">Text of message to be logged.</param>
        /// <returns></returns>
        public bool  WriteMessageToLogRetryQueue(PFLogMessage logMessage)
        {
            bool logWriteSucceeded = false;

            logWriteSucceeded = WriteMessageToTextRetryQueue(logMessage);

            return logWriteSucceeded;


        }

        private bool WriteMessageToTextRetryQueue(PFLogMessage logMessage)
        {
            PFTextFile logfile = new PFTextFile(_logFileConnectionString, PFFileOpenOperation.OpenFileForAppend);
            bool logWriteSucceeded = false;

            try
            {
                logWriteSucceeded = false;
            }
            catch
            {
                logWriteSucceeded = false;
            }
            finally
            {
                if (logfile != null)
                    if (logfile.FileIsOpen)
                        logfile.CloseFile();
                logfile = null;

            }

            if (!logWriteSucceeded)
            {
                SaveLogMessageToRetryQueue(logMessage);
            }

            return logWriteSucceeded;
        }


        private void SaveLogMessageToRetryQueue(PFLogMessage logMessage)
        {

            _logRetryQueue.Add(logMessage);
            
            SaveRetryQueue();
        }

        private void SaveRetryQueue()
        {
            _logRetryQueue.SaveToXmlFile(_logRetryQueueConnectionString);
        }

        /// <summary>
        /// Write a log message that was stored on the retry queue.
        /// </summary>
        /// <returns></returns>
        public int WriteLogMessagesOnRetryQueue()
        {
            bool writeSucceeded = false;
            int numMessagesSent = 0;

            for (int i = 0; i < _logRetryQueue.Count; i++)
            {
                writeSucceeded = WriteMessageToTextLog(_logRetryQueue[i], false);
                if (writeSucceeded)
                {
                    _logRetryQueue.RemoveAt(i);
                    i--;
                    numMessagesSent++;
                }
            }

            SaveRetryQueue();

            return numMessagesSent;
        }

        /// <summary>
        /// Test routine for writing all messages on the retry queue.
        /// </summary>
        /// <returns>Number of messages sent.</returns>
        public int TestWriteLogMessagesOnRetryQueue()
        {
            bool writeSucceeded = false;
            int numMessagesSent = 0;
            int numMessagesChecked = 0;

            for (int i = 0; i < _logRetryQueue.Count; i++)
            {
                numMessagesChecked++;
                if ((numMessagesChecked % 2) != 0)
                {
                    writeSucceeded = WriteMessageToTextLog(_logRetryQueue[i], false);
                    if (writeSucceeded)
                    {
                        _logRetryQueue.RemoveAt(i);
                        i--;
                        numMessagesSent++;
                    }
                }
            }

            SaveRetryQueue();

            return numMessagesSent;
        }

        //********************************************************************************************************************
        //Following code loops through queue backwards, starting at last index and working its way down to the first index
        //********************************************************************************************************************
        //private void WriteLogMessagesOnRetryQueue()
        //{
        //    int maxInx = _logRetryQueue.Count - 1;
        //    int minInx = 0;
        //    bool writeSucceeded = false;

        //    for (int i = maxInx; i >= minInx; i--)
        //    {
        //        writeSucceeded = false;
        //        PFLogMessage logMsg = _logRetryQueue[i];
        //        try
        //        {
        //            WriteMessageToTextLog(logMsg, false);
        //            writeSucceeded = true;
        //        }
        //        catch
        //        {
        //            writeSucceeded = false;
        //        }
        //        if (writeSucceeded)
        //        {
        //            _logRetryQueue.RemoveAt(i);
        //        }
        //    }

        //}






        //helper methods to load and save the object represented by this instance

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFLogManager));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFLogManager.</returns>
        public static PFLogManager LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFLogManager));
            TextReader textReader = new StreamReader(filePath);
            PFLogManager objectInstance;
            objectInstance = (PFLogManager)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(PFLogManager));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFLogManager.</returns>
        public static PFLogManager LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFLogManager));
            StringReader strReader = new StringReader(xmlString);
            PFLogManager objectInstance;
            objectInstance = (PFLogManager)deserializer.Deserialize(strReader);
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
