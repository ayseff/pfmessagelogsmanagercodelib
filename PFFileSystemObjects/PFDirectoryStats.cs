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

namespace PFFileSystemObjects
{
    /// <summary>
    /// Class to organize collections of statistics for a directory and optionally for the subdirectories of the directory.
    /// </summary>
    public class PFDirectoryStats
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private DirectoryInfo _dirInfo = null;
        private FileAttributes _attributes = default(FileAttributes);

        //private variables for properties
        private DateTime _statsAsOfDate = DateTime.Now;
        private PFList<PFFileStats> _files = new PFList<PFFileStats>();
        private PFList<PFDirectoryStats> _subdirectories = new PFList<PFDirectoryStats>();
        private PFList<string> _warningMessages = new PFList<string>();
        private string _name = string.Empty;
        private string _fullName = string.Empty;
        private string _parentDirectory = string.Empty;
        private string _pathRoot = string.Empty;
        private string _searchRoot = string.Empty;
        private long _numBytesInDirectory = -1;
        private long _numBytesInDirectoryTree = -1;
        private long _numFilesInDirectory = -1;
        private long _numSubdirectoriesInDirectory = -1;
        private long _totalNumFilesInDirectoryTree = -1;
        private long _totalNumSubdirectoriesInDirectoryTree = -1;
        private int _numErrors = 0;
        private string _errorMessages = string.Empty;
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
        public PFDirectoryStats()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFDirectoryStats(string folderPath)
        {
            InitializeInstance(folderPath, false);
            Refresh(false);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFDirectoryStats(string folderPath, bool getDirectoryTree)
        {
            InitializeInstance(folderPath, getDirectoryTree);
            Refresh(getDirectoryTree);
        }

        private void InitializeInstance(string directoryPath, bool getDirectoryTree)
        {
            string dirPath = directoryPath;
            FileInfo[] fileInfos;
            DirectoryInfo[] dirInfos;

            _searchRoot = directoryPath;

            if (directoryPath.EndsWith(Path.DirectorySeparatorChar.ToString()) == false)
                dirPath = dirPath + Path.DirectorySeparatorChar;
            _dirInfo = new DirectoryInfo(dirPath);
            GetDirectoryInfo();
            fileInfos = GetFiles(_warningMessages);
            if (fileInfos.Length > 0)
            {
                for (int i = 0; i < fileInfos.Length; i++)
                {
                    _files.Add(new PFFileStats(fileInfos[i]));
                }
            }
            GetDirectorySize();
            if (getDirectoryTree)
            {
                dirInfos = GetSubdirectories(_warningMessages);
                if (dirInfos.Length > 0)
                {
                    for (int i = 0; i < dirInfos.Length; i++)
                    {
                        _subdirectories.Add(new PFDirectoryStats(dirInfos[i].FullName, true));
                    }
                }
            }
        }



        //properties

        /// <summary>
        /// Files Property.
        /// </summary>
        public PFList<PFFileStats> Files
        {
            get
            {
                return _files;
            }
            set
            {
                _files = value;
            }
        }

        /// <summary>
        /// Subdirectories Property.
        /// </summary>
        public PFList<PFDirectoryStats> Subdirectories
        {
            get
            {
                return _subdirectories;
            }
            set
            {
                _subdirectories = value;
            }
        }

        /// <summary>
        /// WarningMessages Property.
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
        /// ParentDirectory Property.
        /// </summary>
        public string ParentDirectory
        {
            get
            {
                return _parentDirectory;
            }
            set
            {
                _parentDirectory = value;
            }
        }

        /// <summary>
        /// PathRoot: The root directory for the current path encompassed in the FullName.
        /// </summary>
        public string PathRoot
        {
            get
            {
                return _pathRoot;
            }
            set
            {
                _pathRoot = value;
            }
        }

        /// <summary>
        /// Root folder: the folder which started the directory info search.
        /// </summary>
        public string SearchRoot
        {
            get
            {
                return _searchRoot;
            }
            set
            {
                _searchRoot = value;
            }
        }



        /// <summary>
        /// NumBytesInDirectory Property.
        /// </summary>
        public long NumBytesInDirectory
        {
            get
            {
                return _numBytesInDirectory;
            }
            set
            {
                _numBytesInDirectory = value;
            }
        }

        /// <summary>
        /// NumBytesInDirectoryTree Property.
        /// </summary>
        public long NumBytesInDirectoryTree
        {
            get
            {
                return _numBytesInDirectoryTree;
            }
            set
            {
                _numBytesInDirectoryTree = value;
            }
        }

        /// <summary>
        /// NumFilesInDirectory Property.
        /// </summary>
        public long NumFilesInDirectory
        {
            get
            {
                return _numFilesInDirectory;
            }
            set
            {
                _numFilesInDirectory = value;
            }
        }

        /// <summary>
        /// NumSubdirectoriesInDirectory Property.
        /// </summary>
        public long NumSubdirectoriesInDirectory
        {
            get
            {
                return _numSubdirectoriesInDirectory;
            }
            set
            {
                _numSubdirectoriesInDirectory = value;
            }
        }

        /// <summary>
        /// TotalNumFilesInDirectoryTree Property.
        /// </summary>
        public long TotalNumFilesInDirectoryTree
        {
            get
            {
                return _totalNumFilesInDirectoryTree;
            }
            set
            {
                _totalNumFilesInDirectoryTree = value;
            }
        }

        /// <summary>
        /// TotalNumSubdirectoriesInDirectoryTree Property.
        /// </summary>
        public long TotalNumSubdirectoriesInDirectoryTree
        {
            get
            {
                return _totalNumSubdirectoriesInDirectoryTree;
            }
            set
            {
                _totalNumSubdirectoriesInDirectoryTree = value;
            }
        }

        /// <summary>
        /// NumErrors Property.
        /// </summary>
        public int NumErrors
        {
            get
            {
                return _numErrors;
            }
            set
            {
                _numErrors = value;
            }
        }

        /// <summary>
        /// ErrorMessages Property.
        /// </summary>
        public string ErrorMessages
        {
            get
            {
                return _errorMessages;
            }
            set
            {
                _errorMessages = value;
            }
        }

        /// <summary>
        /// StatsAsOfDate Property.
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
        /// Method to get a new PFDirectoryStats instance.
        /// </summary>
        /// <param name="folderPath">Path to folder for which statistics to be retrieved.</param>
        /// <param name="getDirectoryTree">If true, all subfolders will be included in the statistics.</param>
        /// <returns>PFDirectoryStats object.</returns>
        public static PFDirectoryStats GetDirectoryStats(string folderPath, bool getDirectoryTree)
        {
            PFDirectoryStats dirStats = new PFDirectoryStats(folderPath, getDirectoryTree);
            return dirStats;
        }

        /// <summary>
        /// Method to determine if folder has specified attribute.
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
        /// Gets name, date/time and attributes values for the directory referred to by this instance.
        /// </summary>
        public void GetDirectoryInfo()
        {
            _statsAsOfDate = DateTime.Now;
            _name = _dirInfo.Name; ;
            _fullName = _dirInfo.FullName;
            if (_dirInfo.Parent != null)
                _parentDirectory = _dirInfo.Parent.FullName;
            else
                _parentDirectory = string.Empty;
            _pathRoot = _dirInfo.Root.FullName; 
            _creationTime = _dirInfo.CreationTime;
            _creationTimeUtc = _dirInfo.CreationTimeUtc;
            _lastModifiedTime = _dirInfo.LastWriteTime;
            _lastModifiedTimeUtc = _dirInfo.LastWriteTimeUtc;
            _lastAccessTime = _dirInfo.LastAccessTime;
            _lastAccessTimeUtc = _dirInfo.LastAccessTimeUtc;
            _attributes = _dirInfo.Attributes;
            _isReadOnly = IsAttribute(FileAttributes.ReadOnly);
            _isReadyToArchive = IsAttribute(FileAttributes.Archive);
            _isEncrypted = IsAttribute(FileAttributes.Encrypted);
            _isCompressed = IsAttribute(FileAttributes.Compressed);
            _isTemporary = IsAttribute(FileAttributes.Temporary);
            _isOffline = IsAttribute(FileAttributes.Offline);
            _isSystemFile = IsAttribute(FileAttributes.System);
            _isHidden = IsAttribute(FileAttributes.Hidden);
        }

        /// <summary>
        /// Gets number of bytes contained in the objects in this directory.
        /// </summary>
        private void GetDirectorySize()
        {
            DirSizeInfo directorySizeInfo;

            directorySizeInfo = PFDirectory.GetDirectorySize(this.FullName, false);
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
            _numBytesInDirectoryTree = directorySizeInfo.NumBytes;
            _totalNumFilesInDirectoryTree = directorySizeInfo.NumFiles;
            _totalNumSubdirectoriesInDirectoryTree = directorySizeInfo.NumFolders;
            _numErrors = directorySizeInfo.NumErrors;
            _errorMessages = directorySizeInfo.errorMessages;
        }
        

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFDirectoryStats));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFDirectoryStats.</returns>
        public static PFDirectoryStats LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFDirectoryStats));
            TextReader textReader = new StreamReader(filePath);
            PFDirectoryStats objectInstance;
            objectInstance = (PFDirectoryStats)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(PFDirectoryStats));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFDirectoryStats.</returns>
        public static PFDirectoryStats LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFDirectoryStats));
            StringReader strReader = new StringReader(xmlString);
            PFDirectoryStats objectInstance;
            objectInstance = (PFDirectoryStats)deserializer.Deserialize(strReader);
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
