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
using PFXmlObjects;

namespace PFFileSystemObjects
{
    /// <summary>
    /// Class for retrieving and manipulating file information. Class includes support for outputting the contents of the class field to text and XML.
    /// </summary>
    public class PFFileEx
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();


        //private variables for properties
        FileInfo _fileInfoObject = null;
        private DateTime _statsAsOfDate = DateTime.Now;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        internal PFFileEx()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFFileEx(string filePath)
        {
            _fileInfoObject = new FileInfo(filePath);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileInfoObject"></param>
        public PFFileEx(FileInfo fileInfoObject)
        {
            _fileInfoObject = fileInfoObject;
        }

        //properties
        /// <summary>
        /// Returns FileInfo object underlying this instance.
        /// </summary>
        public System.IO.FileInfo FileInfoObject
        {
            get
            {
                return _fileInfoObject;
            }
        }

        /// <summary>
        /// The file name.
        /// </summary>
        public string Name
        {
            get
            {
                return _fileInfoObject.Name;
            }
        }

        /// <summary>
        /// Full path of the file.
        /// </summary>
        public string FullName
        {
            get
            {
                return _fileInfoObject.FullName;
            }
        }

        /// <summary>
        /// File extension for the file.
        /// </summary>
        public string Extension
        {
            get
            {
                return _fileInfoObject.Extension;
            }
        }

        /// <summary>
        /// File's attributes. 
        /// </summary>
        public FileAttributes Attributes
        {
            get
            {
                return _fileInfoObject.Attributes;
            }
        }

        /// <summary>
        /// If true, the file is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return IsAttribute(FileAttributes.ReadOnly);
            }
        }

        /// <summary>
        /// If true, the file archive bit has been set.
        /// </summary>
        public bool IsReadyToArchive
        {
            get
            {
                return IsAttribute(FileAttributes.Archive);
            }
        }

        /// <summary>
        /// If true, the file has been encrypted so that only the user account used to encrypt the file can read the file.
        /// </summary>
        public bool IsEncrypted
        {
            get
            {
                return IsAttribute(FileAttributes.Encrypted);
            }
        }

        /// <summary>
        /// If true, the file has been compressed using Windows NTFS compression.
        /// </summary>
        public bool IsCompressed
        {
            get
            {
                return IsAttribute(FileAttributes.Compressed);
            }
        }

        /// <summary>
        /// If true, the file has been marked as temporary.
        /// </summary>
        public bool IsTemporary
        {
            get
            {
                return IsAttribute(FileAttributes.Temporary);
            }
        }

        /// <summary>
        /// If true, the file is offline. The data in the file is not immediately available.
        /// </summary>
        public bool IsOffline
        {
            get
            {
                return IsAttribute(FileAttributes.Offline);
            }
        }

        /// <summary>
        /// If true, the file is system file.
        /// </summary>
        public bool IsSystemFile
        {
            get
            {
                return IsAttribute(FileAttributes.System);
            }
        }

        /// <summary>
        /// If true, the file is a hidden file.
        /// </summary>
        public bool IsHidden
        {
            get
            {
                return IsAttribute(FileAttributes.Hidden);
            }
        }

        /// <summary>
        /// Size in bytes of file.
        /// </summary>
        public long Size
        {
            get
            {
                return _fileInfoObject.Length;
            }
        }

        /// <summary>
        /// Date and time file was created.
        /// </summary>
        public DateTime CreationTime
        {
            get
            {
                return _fileInfoObject.CreationTime;
            }
        }

        /// <summary>
        /// Date and time file was created in universal time format.
        /// </summary>
        public DateTime CreationTimeUtc
        {
            get
            {
                return _fileInfoObject.CreationTimeUtc;
            }
        }

        /// <summary>
        /// Date and time of last write to the file.
        /// </summary>
        public DateTime LastModifiedTime
        {
            get
            {
                return _fileInfoObject.LastWriteTime;
            }
        }

        /// <summary>
        /// Date and time of last write to the file in universal time format.
        /// </summary>
        public DateTime LastModifiedTimeUtc
        {
            get
            {
                return _fileInfoObject.LastWriteTimeUtc;
            }
        }

        /// <summary>
        /// Date and time of last access to the file.
        /// </summary>
        public DateTime LastAccessTime
        {
            get
            {
                return _fileInfoObject.LastAccessTime;
            }
        }

        /// <summary>
        /// Date and time of last access to the file in universal time format.
        /// </summary>
        public DateTime LastAccessTimeUtc
        {
            get
            {
                return _fileInfoObject.LastAccessTimeUtc;
            }
        }

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

        //methods

        /// <summary>
        /// Method to determine if file has specified attribute.
        /// </summary>
        /// <param name="attribute">Attribute to query for.</param>
        /// <returns>Returns true if the file has the specified attribute.</returns>
        public bool IsAttribute(FileAttributes attribute)
        {
            bool ret = false;

            if ((this.Attributes & attribute) == attribute)
                ret = true;

            return ret;
        }

        /// <summary>
        /// Encrypts a file so that only the account used to encrypt the file can decrypt it.
        /// </summary>
        public void Encrypt()
        {
            _fileInfoObject.Encrypt();
        }

        /// <summary>
        /// Decrypts a file that was encrypted by the current account using the Encrypt method.
        /// </summary>
        public void Decrypt()
        {
            _fileInfoObject.Decrypt();
        }

        /// <summary>
        /// Makes file read-only.
        /// </summary>
        public void SetReadOnlyAttribute()
        {
            SetAttributes(FileAttributes.ReadOnly);
        }

        /// <summary>
        /// Makes file read-write.
        /// </summary>
        public void RemoveReadOnlyAttribute()
        {
            if(this.IsReadOnly)
                RemoveAttributes(FileAttributes.ReadOnly);
        }

        /// <summary>
        /// Hides file.
        /// </summary>
        public void SetHiddenAttribute()
        {
            SetAttributes(FileAttributes.Hidden);
        }

        /// <summary>
        /// Unhides file.
        /// </summary>
        public void RemoveHiddenAttribute()
        {
            if(this.IsHidden)
                RemoveAttributes(FileAttributes.Hidden);
        }

        /// <summary>
        /// Sets file attributes to the specified attributes.
        /// </summary>
        /// <param name="attributesToSet">One or more attributes to set.</param>
        public void SetAttributes(FileAttributes attributesToSet)
        {
            File.SetAttributes(this.FullName, _fileInfoObject.Attributes | attributesToSet);
            string fullPath = this.FullName;
            _fileInfoObject = null;
            _fileInfoObject = new FileInfo(fullPath);
        }

        /// <summary>
        /// Removes the specified attributes from the file.
        /// </summary>
        /// <param name="attributesToRemove">One or more attributes to remove.</param>
        public void RemoveAttributes(FileAttributes attributesToRemove)
        {
            FileAttributes attributes = _fileInfoObject.Attributes & ~attributesToRemove;
            File.SetAttributes(this.FullName, attributes);
            string fullPath = this.FullName;
            _fileInfoObject = null;
            _fileInfoObject = new FileInfo(fullPath);
        }

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            //XmlSerializer ser = new XmlSerializer(typeof(PFFileEx));
            //TextWriter tex = new StreamWriter(filePath);
            //ser.Serialize(tex, this);
            //tex.Close();
            XmlDocument xmldoc = this.ToXmlDocument();
            xmldoc.Save(filePath);
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFFileEx object. If unable to create the instance, null is returned.</returns>
        public static PFFileEx LoadFromXmlFile(string filePath)
        {
            //XmlSerializer deserializer = new XmlSerializer(typeof(PFFileEx));
            //TextReader textReader = new StreamReader(filePath);
            //PFFileEx dirEx;
            //dirEx = (PFFileEx)deserializer.Deserialize(textReader);
            //textReader.Close();
            //return dirEx;

            PFFileEx pfFileExInstance=null;

            PFXmlDocument xmldoc = new PFXmlDocument();
            xmldoc.LoadFromFile(filePath);
            string searchPath = xmldoc.DocumentRootNodeName + "/" + "FullName";
            XmlElement fullNameNode = xmldoc.FindElement(searchPath);
            string PathForNewInstance = string.Empty;
            if(fullNameNode!=null)
            {
                PathForNewInstance=fullNameNode.InnerText;
            }
            if(PathForNewInstance.Length>0)
            {
                pfFileExInstance = new PFFileEx(PathForNewInstance);
            }

            return pfFileExInstance;


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
            Type t = this.GetType();
            PFXmlDocument xmldoc = new PFXmlDocument(t.Name);
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            
            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);

                xmldoc.AddNewElement(xmldoc.DocumentRootNode, prop.Name, val.ToString());
            }


            return xmldoc.OuterXml;

            //XmlSerializer ser = new XmlSerializer(typeof(PFFileEx));
            //StringWriter tex = new StringWriter();
            //ser.Serialize(tex, this);
            //return tex.ToString();
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
