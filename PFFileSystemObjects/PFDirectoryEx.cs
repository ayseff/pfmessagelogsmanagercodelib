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
using AppGlobals;
using PFXmlObjects;

namespace PFFileSystemObjects
{
    /// <summary>
    /// Class for retrieving and manipulating directory information. Class includes support for outputting the contents of the class field to text and XML.
    /// </summary>
    public class PFDirectoryEx
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();


        //private variables for properties
        DirectoryInfo _dirInfo = null;
        PFList<PFFileEx> _files = new PFList<PFFileEx>();
        PFList<PFDirectoryEx> _subdirectories = new PFList<PFDirectoryEx>();
        PFList<string> _warningMessages = new PFList<string>();
        long _numBytesInDirectory = -1;
        long _numBytesInDirectoryTree = -1;
        long _numFilesInDirectory = -1;
        long _numSubdirectoriesInDirectory = -1;
        long _totalNumFilesInDirectoryTree = -1;
        long _totalNumSubdirectoriesInDirectoryTree = -1;
        int _numErrors = 0;
        string _errorMessages = string.Empty;
        private DateTime _statsAsOfDate = DateTime.Now;
        
        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        internal PFDirectoryEx()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="directoryPath">Full path to directory this instance will represent.</param>
        public PFDirectoryEx(string directoryPath)
        {
            InitializeInstance(directoryPath, false);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="directoryPath">Full path to directory this instance will represent.</param>
        /// <param name="getDirectoryTree">If true, an internal list of all subdirectories and files will be built.</param>
        public PFDirectoryEx(string directoryPath, bool getDirectoryTree)
        {
            InitializeInstance(directoryPath, getDirectoryTree);
        }

        private void InitializeInstance(string directoryPath, bool getDirectoryTree)
        {
            string dirPath = directoryPath;
            FileInfo[] fileInfos;
            DirectoryInfo[] dirInfos;

            if (directoryPath.EndsWith(Path.DirectorySeparatorChar.ToString()) == false)
                dirPath = dirPath + Path.DirectorySeparatorChar;
            _dirInfo = new DirectoryInfo(dirPath);
            //fileInfos = _dirInfo.GetFiles();
            fileInfos = GetFiles(_warningMessages);
            if (fileInfos.Length > 0)
            {
                for (int i = 0; i < fileInfos.Length; i++)
                {
                    _files.Add(new PFFileEx(fileInfos[i]));
                }
            }
            GetDirectorySize();
            if (getDirectoryTree)
            {
                //dirInfos = _dirInfo.GetDirectories();
                dirInfos = GetSubdirectories(_warningMessages);
                if (dirInfos.Length > 0)
                {
                    for (int i = 0; i < dirInfos.Length; i++)
                    {
                        _subdirectories.Add(new PFDirectoryEx(dirInfos[i].FullName,true));
                    }
                }
            }
        }

        //properties

        /// <summary>
        /// Underlying DirectoryInfo instance.
        /// </summary>
        public DirectoryInfo DirectoryInfoObject
        {
            get
            {
                return _dirInfo;
            }
        }

        /// <summary>
        /// Returns list of PFFileEx objects representing the files in this directory.
        /// </summary>
        public PFList<PFFileEx> Files
        {
            get
            {
                return _files;
            }
        }

        /// <summary>
        /// Returns list of PFDirectoryEx objects representing the subdirectories in this directory.
        /// </summary>
        public PFList<PFDirectoryEx> Subdirectories
        {
            get
            {
                return _subdirectories;
            }
        }

        /// <summary>
        /// Collection of warning messages encountered during processing. Usually these will involve 
        /// </summary>
        public PFList<string> WarningMessages
        {
            get
            {
                return _warningMessages;
            }
            set
            {
                _warningMessages = value;
            }
        }


        /// <summary>
        /// Name of the directory.
        /// </summary>
        public string Name
        {
            get
            {
                return _dirInfo.Name;
            }
        }

        /// <summary>
        /// Full path for the directory.
        /// </summary>
        public string FullName
        {
            get
            {
                return _dirInfo.FullName;
            }
        }

        /// <summary>
        /// NumBytesInDirectory Property.
        /// </summary>
        public long NumBytesInDirectory
        {
            get
            {
                if (_numBytesInDirectory == -1)
                    this.Refresh(false);
                return _numBytesInDirectory;
            }
        }

        /// <summary>
        /// NumBytesInDirectoryTree Property.
        /// </summary>
        public long NumBytesInDirectoryTree
        {
            get
            {
                if (_numBytesInDirectoryTree == -1)
                    this.Refresh(true);
                return _numBytesInDirectoryTree;
            }
        }

        /// <summary>
        /// NumFilesInDirectory Property.
        /// </summary>
        public long NumFilesInDirectory
        {
            get
            {
                if (_numFilesInDirectory == -1)
                    this.Refresh(false);
                return _numFilesInDirectory;
            }
        }

        /// <summary>
        /// NumSubdirectoriesInDirectory Property.
        /// </summary>
        public long NumSubdirectoriesInDirectory
        {
            get
            {
                if (_numSubdirectoriesInDirectory == -1)
                    this.Refresh(false);
                return _numSubdirectoriesInDirectory;
            }
        }

        /// <summary>
        /// TotalNumFilesInDirectoryTree Property.
        /// </summary>
        public long TotalNumFilesInDirectoryTree
        {
            get
            {
                if (_totalNumFilesInDirectoryTree == -1)
                    this.Refresh(true);
                return _totalNumFilesInDirectoryTree;
            }
        }

        /// <summary>
        /// TotalNumSubdirectoriesInDirectoryTree Property.
        /// </summary>
        public long TotalNumSubdirectoriesInDirectoryTree
        {
            get
            {
                if (_totalNumSubdirectoriesInDirectoryTree == -1)
                    this.Refresh(true);
                return _totalNumSubdirectoriesInDirectoryTree;
            }
        }

        /// <summary>
        /// Number of errors encountered while getting directory and file information.
        /// </summary>
        public int NumErrors
        {
            get
            {
                return _numErrors;
            }
        }

        /// <summary>
        /// ErrorMessages recorded while getting directory and file information.
        /// </summary>
        public string ErrorMessages
        {
            get
            {
                return _errorMessages;
            }
        }


        /// <summary>
        /// Statistics for folder are current as of this date and time.
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
        /// File's attributes. 
        /// </summary>
        public FileAttributes Attributes
        {
            get
            {
                return _dirInfo.Attributes;
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


        //methods

        /// <summary>
        /// Method to determine if folder has specified attribute.
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

        private FileInfo[] GetFiles(PFList<string> warningMessages)
        {
            List<string> files = null;
            List<FileInfo> fileInfos = new List<FileInfo>();
            
            try
            {
                files = new List<string>(Directory.EnumerateFiles(this.FullName));
            }
            catch (UnauthorizedAccessException UAEx)
            {
                _msg.Length = 0;
                _msg.Append("Access denied: Unable to retrieve file information list. Permissions exception: ");
                _msg.Append(this.FullName);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(UAEx));
                _msg.Append("\r\n");
                warningMessages.Add(_msg.ToString());
            }
            catch (PathTooLongException PathEx)
            {
                _msg.Length = 0;
                _msg.Append("Access denied: Unable to retrieve file information list. Path too long exception: ");
                _msg.Append(this.FullName);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(PathEx));
                _msg.Append("\r\n");
                warningMessages.Add(_msg.ToString());
            }

            if (files != null)
            {
                foreach (var fileItem in files)
                {
                    try
                    {
                        FileInfo file = new FileInfo(fileItem);
                        fileInfos.Add(file);
                    }
                    catch (UnauthorizedAccessException UAEx)
                    {
                        _msg.Length = 0;
                        _msg.Append("Access denied: Permissions exception: ");
                        _msg.Append(fileItem);
                        _msg.Append(" Error message: ");
                        _msg.Append(AppMessages.FormatErrorMessage(UAEx));
                        _msg.Append("\r\n");
                        warningMessages.Add(_msg.ToString());
                    }
                    catch (PathTooLongException PathEx)
                    {
                        _msg.Length = 0;
                        _msg.Append("Access denied: Path too long exception: ");
                        _msg.Append(fileItem);
                        _msg.Append(" Error message: ");
                        _msg.Append(AppMessages.FormatErrorMessage(PathEx));
                        _msg.Append("\r\n");
                        warningMessages.Add(_msg.ToString());
                    }
                    catch (System.Exception ex)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to process: ");
                        _msg.Append(fileItem);
                        _msg.Append(" Error message: ");
                        _msg.Append(AppMessages.FormatErrorMessage(ex));
                        _msg.Append("\r\n");
                        warningMessages.Add(_msg.ToString());
                    }
                }
            }

            return fileInfos.ToArray();
        }

        private DirectoryInfo[] GetSubdirectories(PFList<string> warningMessages)
        {
            List<string> dirs = null;
            List<DirectoryInfo> dirInfos = new List<DirectoryInfo>();

            try
            {
                dirs = new List<string>(Directory.EnumerateDirectories(this.FullName));
            }
            catch (UnauthorizedAccessException UAEx)
            {
                _msg.Length = 0;
                _msg.Append("Access denied: Unable to retrieve directory information list. Permissions exception: ");
                _msg.Append(this.FullName);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(UAEx));
                _msg.Append("\r\n");
                warningMessages.Add(_msg.ToString());
            }
            catch (PathTooLongException PathEx)
            {
                _msg.Length = 0;
                _msg.Append("Access denied: Unable to retrieve directory information list. Path too long exception: ");
                _msg.Append(this.FullName);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(PathEx));
                _msg.Append("\r\n");
                warningMessages.Add(_msg.ToString());
            }
            catch (DirectoryNotFoundException NotFoundEx)
            {
                _msg.Length = 0;
                _msg.Append("Directory does not exist: ");
                _msg.Append(this.FullName);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(NotFoundEx));
                _msg.Append("\r\n");
                warningMessages.Add(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to process: ");
                _msg.Append(this.FullName);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                _msg.Append("\r\n");
                warningMessages.Add(_msg.ToString());
            }

            if (dirs != null)
            {
                foreach (var subdirItem in dirs)
                {
                    try
                    {
                        DirectoryInfo subdir = new DirectoryInfo(subdirItem);
                        dirInfos.Add(subdir);
                    }
                    catch (UnauthorizedAccessException UAEx)
                    {
                        _msg.Length = 0;
                        _msg.Append("Access denied: Permissions exception: ");
                        _msg.Append(subdirItem);
                        _msg.Append(" Error message: ");
                        _msg.Append(AppMessages.FormatErrorMessage(UAEx));
                        _msg.Append("\r\n");
                        warningMessages.Add(_msg.ToString());
                    }
                    catch (PathTooLongException PathEx)
                    {
                        _msg.Length = 0;
                        _msg.Append("Access denied: Path too long exception: ");
                        _msg.Append(subdirItem);
                        _msg.Append(" Error message: ");
                        _msg.Append(AppMessages.FormatErrorMessage(PathEx));
                        _msg.Append("\r\n");
                        warningMessages.Add(_msg.ToString());
                    }
                    catch (System.Exception ex)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to process: ");
                        _msg.Append(subdirItem);
                        _msg.Append(" Error message: ");
                        _msg.Append(AppMessages.FormatErrorMessage(ex));
                        _msg.Append("\r\n");
                        warningMessages.Add(_msg.ToString());
                    }
                }
            }

            return dirInfos.ToArray();
        }

        /// <summary>
        /// Recalculates the number of bytes, files and subdirectories in this instance.
        /// </summary>
        /// <param name="includeSubfolders">If true, size of all objects in directory subfolders are calculated.</param>
        public void Refresh(bool includeSubfolders)
        {
            GetDirectorySize();
            if (includeSubfolders)
                GetDirectoryTreeSize();
        }

        /// <summary>
        /// Gets number of bytes contained in the objects in this directory.
        /// </summary>
        private void GetDirectorySize()
        {
            DirSizeInfo directorySizeInfo;

            directorySizeInfo = PFDirectory.GetDirectorySize(this.FullName, false);
            //if (directorySizeInfo.NumErrors > 0)
            //{
            //    _msg.Length = 0;
            //    _msg.Append("Error while retrieving directory information: ");
            //    _msg.Append(directorySizeInfo.errorMessages.ToString());
            //    throw new System.Exception(_msg.ToString());
            //}
            _numBytesInDirectory = directorySizeInfo.NumBytes;
            _numFilesInDirectory = directorySizeInfo.NumFiles;
            _numSubdirectoriesInDirectory = directorySizeInfo.NumFolders;
            _numErrors = directorySizeInfo.NumErrors;
            _errorMessages = directorySizeInfo.errorMessages;
        }


        /// <summary>
        /// Gets number of bytes contained in the objects in this directory tree.
        /// </summary>
        private void GetDirectoryTreeSize()
        {
            DirSizeInfo directorySizeInfo;

            directorySizeInfo = PFDirectory.GetDirectorySize(this.FullName, true);
            //if (directorySizeInfo.NumErrors > 0)
            //{
            //    _msg.Length = 0;
            //    _msg.Append("Error while retrieving directory tree information: ");
            //    _msg.Append(directorySizeInfo.errorMessages.ToString());
            //    throw new System.Exception(_msg.ToString());
            //}
            _numBytesInDirectoryTree = directorySizeInfo.NumBytes;
            _totalNumFilesInDirectoryTree = directorySizeInfo.NumFiles;
            _totalNumSubdirectoriesInDirectoryTree = directorySizeInfo.NumFolders;
            _numErrors = directorySizeInfo.NumErrors;
            _errorMessages = directorySizeInfo.errorMessages;
        }
        
        
        
        //class helpers


        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        /// <param name="summaryOnly">If true, only very high level information from the object is stored. Otherwise, full detail of all values from objects and subojects in the instance are retrieved.</param>
        public void SaveToXmlFile(string filePath, bool summaryOnly)
        {
            //*************************************************************
            //Built-in serialization does not work on PFDirectoryEx class.
            //*************************************************************
            //XmlSerializer ser = new XmlSerializer(typeof(PFDirectoryEx));
            //TextWriter tex = new StreamWriter(filePath);
            //ser.Serialize(tex, this);
            //tex.Close();

            XmlDocument xmldoc = this.ToXmlDocument(summaryOnly);
            xmldoc.Save(filePath);
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFDirectoryEx.</returns>
        public static PFDirectoryEx LoadFromXmlFile(string filePath)
        {
            //*************************************************************
            //Built-in serialization does not work on PFDirectoryEx class.
            //*************************************************************
            //XmlSerializer deserializer = new XmlSerializer(typeof(PFDirectoryEx));
            //TextReader textReader = new StreamReader(filePath);
            //PFDirectoryEx dirEx;
            //dirEx = (PFDirectoryEx)deserializer.Deserialize(textReader);
            //textReader.Close();
            //return dirEx;

            PFDirectoryEx pfD‏irectoryExInstance = null;

            PFXmlDocument xmldoc = new PFXmlDocument();
            xmldoc.LoadFromFile(filePath);
            string searchPath = xmldoc.DocumentRootNodeName + "/" + "FullName";
            XmlElement fullNameNode = xmldoc.FindElement(searchPath);
            string PathForNewInstance = string.Empty;
            if (fullNameNode != null)
            {
                PathForNewInstance = fullNameNode.InnerText;
            }
            if (PathForNewInstance.Length > 0)
            {
                pfD‏irectoryExInstance = new PFDirectoryEx(PathForNewInstance, true);
            }

            return pfD‏irectoryExInstance;


        }


        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            //data.Append(PropertiesToString());
            //data.Append("\r\n");
            //data.Append(FieldsToString());
            //data.Append("\r\n");

            data.Append(PropertiesShortForm());
            data.Append("\r\n");

            return data.ToString();
        }

        private string PropertiesShortForm()
        {
            StringBuilder data = new StringBuilder();

            data.Append("Class type:");
            data.Append(this.GetType().FullName);
            data.Append("\r\nClass public properties for");
            data.Append(this.GetType().FullName);
            data.Append("\r\n");

            data.Append("Name = ");
            data.Append(this.Name);
            data.Append("\r\n");
            data.Append("FullName = ");
            data.Append(this.FullName);
            data.Append("\r\n");
            data.Append("NumBytesInDirectory = ");
            data.Append(this.NumBytesInDirectory.ToString("#,##0"));
            data.Append("\r\n");
            data.Append("NumFilesInDirectory = ");
            data.Append(this.NumFilesInDirectory.ToString("#,##0"));
            data.Append("\r\n");
            data.Append("NumSubdirs = ");
            data.Append(this.NumSubdirectoriesInDirectory.ToString("#,##0"));
            data.Append("\r\n");
            data.Append("Total NumBytesInDirectoryTree = ");
            data.Append(this.NumBytesInDirectoryTree.ToString("#,##0"));
            data.Append("\r\n");
            data.Append("Total NumFiles = ");
            data.Append(this.TotalNumFilesInDirectoryTree.ToString("#,##0"));
            data.Append("\r\n");
            data.Append("Total Subdirectories = ");
            data.Append(this.TotalNumSubdirectoriesInDirectoryTree.ToString("#,##0"));
            data.Append("\r\n");
            data.Append("Number of errors = ");
            data.Append(this.NumErrors.ToString("#,##0"));
            data.Append("\r\n");
            data.Append("Error messages:\r\n");
            data.Append(this.ErrorMessages);
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
        /// <param name="summaryOnly">If true, only very high level information from the object is stored. Otherwise, full detail of all values from objects and subojects in the instance are retrieved.</param>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString(bool summaryOnly)
        {

            string retval = string.Empty;
            if (summaryOnly)
                retval = ToXmlStringSummary();
            else
                retval = ToXmlStringDetail();

            return retval;

        }

        private string ToXmlStringSummary()
        {
            Type t = this.GetType();
            PFXmlDocument xmldoc = new PFXmlDocument(t.Name);

             xmldoc.AddNewElement(xmldoc.DocumentRootNode, "Name", this.Name);

             xmldoc.AddNewElement(xmldoc.DocumentRootNode, "FullName", this.FullName);

             xmldoc.AddNewElement(xmldoc.DocumentRootNode, "NumBytesInDirectory", this.NumBytesInDirectory.ToString("#,##0"));

             xmldoc.AddNewElement(xmldoc.DocumentRootNode, "NumFilesInDirectory", this.NumFilesInDirectory.ToString("#,##0"));

             xmldoc.AddNewElement(xmldoc.DocumentRootNode, "NumSubdirectoriesInDirectory", this.NumSubdirectoriesInDirectory.ToString("#,##0"));

             xmldoc.AddNewElement(xmldoc.DocumentRootNode, "NumBytesInDirectoryTree", this.NumBytesInDirectoryTree.ToString("#,##0"));

             xmldoc.AddNewElement(xmldoc.DocumentRootNode, "TotalNumFilesInDirectoryTree", this.TotalNumFilesInDirectoryTree.ToString("#,##0"));

             xmldoc.AddNewElement(xmldoc.DocumentRootNode, "TotalNumSubdirectoriesInDirectoryTree", this.TotalNumSubdirectoriesInDirectoryTree.ToString("#,##0"));

             xmldoc.AddNewElement(xmldoc.DocumentRootNode, "NumErrors", this.NumErrors.ToString("#,##0"));

             xmldoc.AddNewElement(xmldoc.DocumentRootNode, "ErrorMessages", this.ErrorMessages);



            return xmldoc.OuterXml;
        }

        private string ToXmlStringDetail()
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
        }

        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <param name="summaryOnly">If true, only very high level information from the object is stored. Otherwise, full detail of all values from objects and subojects in the instance are retrieved.</param>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument(bool summaryOnly)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ToXmlString(summaryOnly));
            return doc;
        }


    }//end class
}//end namespace
