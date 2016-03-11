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

namespace PFFileSystemObjects
{
    /// <summary>
    /// Contains set of statistics for an individual file.
    /// </summary>
    public class PFFileStats
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        FileInfo _fileInfoObject = null;
        private FileAttributes _attributes = default(FileAttributes);

        //private variables for properties
        private DateTime _statsAsOfDate = DateTime.Now;
        private string _name = string.Empty;
        private string _fullName = string.Empty;
        private string _extension = string.Empty;
        private long _size = -1;
        private DateTime _creationTime = DateTime.MinValue;
        private DateTime _creationTimeUtc = DateTime.MinValue;
        private DateTime _lastModifiedTime = DateTime.MinValue;
        private DateTime _lastModifiedTimeUtc = DateTime.MinValue;
        private DateTime _lastAccessTime = DateTime.MinValue;
        private DateTime _lastAccessTimeUtc = DateTime.MinValue;
        private bool _isReadOnly = false;
        private bool _isReadyToArchive = false;
        private bool _isEncrypted = false;
        private bool _isCompressed = false;
        private bool _isTemporary = false;
        private bool _isOffline = false;
        private bool _isSystemFile = false;
        private bool _isHidden = false;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFFileStats()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFFileStats(string filePath)
        {
            _fileInfoObject = new FileInfo(filePath);
            InitializeFileStatsObject();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileInfoObject"></param>
        public PFFileStats(FileInfo fileInfoObject)
        {
            _fileInfoObject = fileInfoObject;
            InitializeFileStatsObject();
        }


        private void InitializeFileStatsObject()
        {
            _statsAsOfDate = DateTime.Now;
            _name = _fileInfoObject.Name;
            _fullName = _fileInfoObject.FullName;
            _extension = _fileInfoObject.Extension;
            _size = _fileInfoObject.Length;
            _creationTime = _fileInfoObject.CreationTime;
            _creationTimeUtc = _fileInfoObject.CreationTimeUtc;
            _lastModifiedTime = _fileInfoObject.LastWriteTime;
            _lastModifiedTimeUtc = _fileInfoObject.LastWriteTimeUtc;
            _lastAccessTime = _fileInfoObject.LastAccessTime;
            _lastAccessTimeUtc = _fileInfoObject.LastAccessTimeUtc;
            _attributes = _fileInfoObject.Attributes;
            _isReadOnly = IsAttribute(FileAttributes.ReadOnly);
            _isReadyToArchive = IsAttribute(FileAttributes.Archive);
            _isEncrypted = IsAttribute(FileAttributes.Encrypted);
            _isCompressed = IsAttribute(FileAttributes.Compressed);
            _isTemporary = IsAttribute(FileAttributes.Temporary);
            _isOffline = IsAttribute(FileAttributes.Offline);
            _isSystemFile = IsAttribute(FileAttributes.System);
            _isHidden = IsAttribute(FileAttributes.Hidden);
        }

        //properties

        /// <summary>
        /// Statistics for file are current as of this date and time.
        /// </summary>
        public DateTime StatsAsOfDate
        {
            get
            {
                return _statsAsOfDate;
            }
            set
            {
                _statsAsOfDate = value;
            }
        }

        /// <summary>
        /// Name Property.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// FullName Property.
        /// </summary>
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }

        /// <summary>
        /// Extension Property.
        /// </summary>
        public string Extension
        {
            get
            {
                return _extension;
            }
            set
            {
                _extension = value;
            }
        }

        /// <summary>
        /// Size Property.
        /// </summary>
        public long Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        /// <summary>
        /// CreationTime Property.
        /// </summary>
        public DateTime CreationTime
        {
            get
            {
                return _creationTime;
            }
            set
            {
                _creationTime = value;
            }
        }

        /// <summary>
        /// CreationTimeUtc Property.
        /// </summary>
        public DateTime CreationTimeUtc
        {
            get
            {
                return _creationTimeUtc;
            }
            set
            {
                _creationTimeUtc = value;
            }
        }

        /// <summary>
        /// LastModifiedTime Property.
        /// </summary>
        public DateTime LastModifiedTime
        {
            get
            {
                return _lastModifiedTime;
            }
            set
            {
                _lastModifiedTime = value;
            }
        }

        /// <summary>
        /// LastModifiedTimeUtc Property.
        /// </summary>
        public DateTime LastModifiedTimeUtc
        {
            get
            {
                return _lastModifiedTimeUtc;
            }
            set
            {
                _lastModifiedTimeUtc = value;
            }
        }

        /// <summary>
        /// LastAccessTime Property.
        /// </summary>
        public DateTime LastAccessTime
        {
            get
            {
                return _lastAccessTime;
            }
            set
            {
                _lastAccessTime = value;
            }
        }

        /// <summary>
        /// LastAccessTimeUtc Property.
        /// </summary>
        public DateTime LastAccessTimeUtc
        {
            get
            {
                return _lastAccessTimeUtc;
            }
            set
            {
                _lastAccessTimeUtc = value;
            }
        }

        /// <summary>
        /// IsReadOnly Property.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                _isReadOnly = value;
            }
        }

        /// <summary>
        /// IsReadyToArchive Property.
        /// </summary>
        public bool IsReadyToArchive
        {
            get
            {
                return _isReadyToArchive;
            }
            set
            {
                _isReadyToArchive = value;
            }
        }

        /// <summary>
        /// IsEncrypted Property.
        /// </summary>
        public bool IsEncrypted
        {
            get
            {
                return _isEncrypted;
            }
            set
            {
                _isEncrypted = value;
            }
        }

        /// <summary>
        /// IsCompressed Property.
        /// </summary>
        public bool IsCompressed
        {
            get
            {
                return _isCompressed;
            }
            set
            {
                _isCompressed = value;
            }
        }

        /// <summary>
        /// IsTemporary Property.
        /// </summary>
        public bool IsTemporary
        {
            get
            {
                return _isTemporary;
            }
            set
            {
                _isTemporary = value;
            }
        }

        /// <summary>
        /// IsOffline Property.
        /// </summary>
        public bool IsOffline
        {
            get
            {
                return _isOffline;
            }
            set
            {
                _isOffline = value;
            }
        }

        /// <summary>
        /// IsSystemFile Property.
        /// </summary>
        public bool IsSystemFile
        {
            get
            {
                return _isSystemFile;
            }
            set
            {
                _isSystemFile = value;
            }
        }

        /// <summary>
        /// IsHidden Property.
        /// </summary>
        public bool IsHidden
        {
            get
            {
                return _isHidden;
            }
            set
            {
                _isHidden = value;
            }
        }




        //methods

        /// <summary>
        /// Method to determine if file has specified attribute.
        /// </summary>
        /// <param name="attribute">Attribute to query for.</param>
        /// <returns>Returns true if the file has the specified attribute.</returns>
        public bool IsAttribute(FileAttributes attribute)
        {
            bool ret = false;

            if ((_attributes & attribute) == attribute)
                ret = true;

            return ret;
        }

        /// <summary>
        /// Static method for obtaining a PFFileStats object instance.
        /// </summary>
        /// <param name="filePath">Path to file for which stats will be retrieved.</param>
        /// <returns>PFFileStats object.</returns>
        public static PFFileStats GetFileStats(string filePath)
        {
            PFFileStats fileStats = new PFFileStats(filePath);
            return fileStats;
        }


        
        
        //class helpers

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFFileStats));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFFileStats.</returns>
        public static PFFileStats LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFFileStats));
            TextReader textReader = new StreamReader(filePath);
            PFFileStats objectInstance;
            objectInstance = (PFFileStats)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
        }


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
            XmlSerializer ser = new XmlSerializer(typeof(PFFileStats));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFFileStats.</returns>
        public static PFFileStats LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFFileStats));
            StringReader strReader = new StringReader(xmlString);
            PFFileStats objectInstance;
            objectInstance = (PFFileStats)deserializer.Deserialize(strReader);
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
