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
    /// Class for managing objects to encapsulate disk drive information.
    /// </summary>
    public class PFDiskDrive
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private varialbles for properties
        DriveInfo _driveInfo = null;

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFDiskDrive()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="drivePath">A valid drive path or drive letter. Can be either uppercase or lowercase4, 'a' to 'z'. Null value is not valid.</param>
        public PFDiskDrive(string drivePath)
        {
            _driveInfo = new DriveInfo(drivePath);
        }

        //properties

        /// <summary>
        /// DriveInfo object that represents the data for the drive that is represented by an instance of this class.
        /// </summary>
        public DriveInfo DriveInfoObject
        {
            get
            {
                return _driveInfo;
            }
            set
            {
                _driveInfo = value;
            }
        }

        /// <summary>
        /// Name of the disk drive represented by an instance of this class.
        /// </summary>
        public string DriveName
        {
            get
            {
                string retval = string.Empty;
                if (_driveInfo != null)
                    retval = _driveInfo.Name;
                return retval;
            }
        }

        /// <summary>
        /// Returns root directory path for drive represented by current instance of the class.
        /// </summary>
        public string DriveRootDirectoryPath
        {
            get
            {
                string retval = string.Empty;
                if (_driveInfo != null)
                    retval = _driveInfo.RootDirectory.FullName;
                return retval;
            }
        }

        /// <summary>
        /// Returns available free space for drive represented by current instance of the class.
        /// </summary>
        public long DriveAvailableFreeSpace
        {
            get
            {
                long retval = -1;
                if (_driveInfo != null)
                    retval = _driveInfo.AvailableFreeSpace;
                return retval;
            }
        }

        /// <summary>
        /// Returns total free space for drive represented by current instance of the class.
        /// </summary>
        public long DriveTotalFreeSpace
        {
            get
            {
                long retval = -1;
                if (_driveInfo != null)
                    retval = _driveInfo.TotalFreeSpace;
                return retval;
            }
        }

        /// <summary>
        /// Returns total space for drive represented by current instance of the class.
        /// </summary>
        public long DriveTotalSpace
        {
            get
            {
                long retval = -1;
                if (_driveInfo != null)
                    retval = _driveInfo.TotalFreeSpace;
                return retval;
            }
        }

        /// <summary>
        /// Returns status for drive represented by current instance of the class.
        /// </summary>
        public bool DriveIsReady
        {
            get
            {
                bool retval = false;
                if (_driveInfo != null)
                    retval = _driveInfo.IsReady;
                return retval;
            }
        }

        /// <summary>
        /// Returns disk drive format for drive represented by current instance of the class.
        /// </summary>
        public string DiskFormat
        {
            get
            {
                string retval = string.Empty;
                if (_driveInfo != null)
                    retval = _driveInfo.DriveFormat;
                return retval;
            }
        }

        /// <summary>
        /// Returns type of drive represented by current instance of the class.
        /// </summary>
        public string DiskType
        {
            get
            {
                string retval = string.Empty;
                if (_driveInfo != null)
                    retval = _driveInfo.DriveType.ToString();
                return retval;
            }
        }

        /// <summary>
        /// Returns volume label for drive represented by current instance of the class.
        /// </summary>
        public string DiskVolumeLabel
        {
            get
            {
                string retval = string.Empty;
                if (_driveInfo != null)
                    retval = _driveInfo.VolumeLabel;
                return retval;
            }
        }



        //methods

        /// <summary>
        /// Returns an array of DriveInfo objects representing all the disk drives defined for the computer.
        /// </summary>
        /// <returns></returns>
        public static DriveInfo[] GetDiskDrives()
        {
            return DriveInfo.GetDrives();
        }

        /// <summary>
        /// Returns list of root directory paths for the computer.
        /// </summary>
        /// <returns>List of strings representing root directory full paths.</returns>
        public static List<string> GetDiskDrivesRootDirectories()
        {
            return GetDiskDrivesRootDirectories(true);
        }

        /// <summary>
        /// Returns list of root directory paths for the computer.
        /// </summary>
        /// <param name="excludeNotReadyDevices">Set to true if you wish to exclude any not ready devices from the return list.</param>
        /// <returns>List of strings representing root directory full paths.</returns>
        public static List<string> GetDiskDrivesRootDirectories(bool excludeNotReadyDevices)
        {
            List<string> rootDirectories = new List<string>();
            DriveInfo[] dis = DriveInfo.GetDrives();

            foreach (DriveInfo di in dis)
            {
                if(excludeNotReadyDevices==false || di.IsReady == true)
                    rootDirectories.Add(di.RootDirectory.FullName);
            }

            return rootDirectories;
        }

        //class helpers

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFDiskDrive));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFDiskDrive.</returns>
        public static PFDiskDrive LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFDiskDrive));
            TextReader textReader = new StreamReader(filePath);
            PFDiskDrive columnDefinitions;
            columnDefinitions = (PFDiskDrive)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
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
            XmlSerializer ser = new XmlSerializer(typeof(PFDiskDrive));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
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
