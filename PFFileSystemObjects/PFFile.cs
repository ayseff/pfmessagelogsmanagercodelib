using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Management;
using System.Text.RegularExpressions;

namespace PFFileSystemObjects
{
#pragma warning disable 1591
    public struct FilePathParts
    {
        private string pathRoot;
        private string folderPath;
        private string fileName;
        private string fileExtension;

        public string PathRoot
        {
            get
            {
                return pathRoot;
            }
            set
            {
                pathRoot = value;
            }
        }

        public string FolderPath
        {
            get
            {
                return folderPath;
            }
            set
            {
                folderPath = value;
            }
        }

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        public string FileExtension
        {
            get
            {
                return fileExtension;
            }
            set
            {
                fileExtension = value;
            }
        }

        public FilePathParts(string root, string folder, string fileName, string fileExt)
        {
            this.pathRoot=root;
            this.folderPath = folder;
            this.fileName = fileName;
            this.fileExtension = fileExt;
        }

        /// <summary>
        /// Routine overrides default ToString method and outputs name and value for all public properties.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties();
            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);
                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");
            }

            return data.ToString();
        }


    }
#pragma warning restore 1591
    
    /// <summary>
    /// Class for retrieving file information.
    /// </summary>
    public class PFFile 
    {
#pragma warning disable 1591
        //private variables
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern bool CopyFileW(string lpExistingFileName, string lpNewFileName, bool bFailIfExists);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern int GetLastError();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetLongPathName(
                 [MarshalAs(UnmanagedType.LPTStr)]
                   string path,
                 [MarshalAs(UnmanagedType.LPTStr)]
                   StringBuilder longPath,
                 int longPathLength
                 );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(
                 [MarshalAs(UnmanagedType.LPTStr)]
                   string path,
                 [MarshalAs(UnmanagedType.LPTStr)]
                   StringBuilder shortPath,
                 int shortPathLength
                 );

#pragma warning restore 1591

        private static StringBuilder _msg = new StringBuilder();

        private const string longPathPrefix = @"\\?\";
        private const int MAX_PATH_LENGTH = 260;


        //private fields for properties
        //private string _fileName = string.Empty;

        //constructors
        /// <summary>
        /// Cosntructor.
        /// </summary>
        public PFFile()
        {
            ;
        }

        ///// <summary>
        ///// Constructor.
        ///// </summary>
        ///// <param name="fileName"></param>
        //public PFFile(string fileName)
        //{
        //    _fileName = fileName;
        //}
        
        //properties

        ///// <summary>
        ///// Path to the file represented by this instance of the class.
        ///// </summary>
        //public string FileName
        //{
        //    get
        //    {
        //        return _fileName;
        //    }
        //    set
        //    {
        //        _fileName = value;
        //    }
        //}


        //methods
        /// <summary>
        /// Method attempts to copy files whose paths are longer the default Windows maximum path length of 260.
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        public static void CopyFileLongPath(string sourcePath, string destinationPath)
        {
            CopyFileLongPath(sourcePath, destinationPath, false);
        }

        /// <summary>
        /// Method attempts to copy files whose paths are longer the default Windows maximum path length of 260.
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="failIfFileExists">If true, destination path must not exist.</param>
        public static void CopyFileLongPath(string sourcePath, string destinationPath, bool failIfFileExists)
        {
            StringBuilder sourcePathLong = new StringBuilder();
            StringBuilder destinationPathLong = new StringBuilder();

            sourcePathLong.Length = 0;
            sourcePathLong.Append(longPathPrefix);
            sourcePathLong.Append(sourcePath);

            destinationPathLong.Length = 0;
            destinationPathLong.Append(longPathPrefix);
            destinationPathLong.Append(destinationPath);

            bool result = false;
            int lastError = -999;

            result = CopyFileW(sourcePathLong.ToString(), destinationPathLong.ToString(), failIfFileExists);
            lastError = GetLastError();


        }

#pragma warning disable 1591

        public static void CopyFileLongPathT(string sourcePath, string destinationPath, bool failIfFileExists)
        {

            bool result = false;
            int lastError = -999;

            StringBuilder sourcePathLong = new StringBuilder();
            StringBuilder destinationPathLong = new StringBuilder();

            sourcePathLong.Length = 0;
            sourcePathLong.Append(longPathPrefix);
            sourcePathLong.Append(sourcePath);

            result = CopyFileW(sourcePathLong.ToString(), destinationPath, failIfFileExists);
            lastError = GetLastError();

        }
#pragma warning restore 1591

        /// <summary>
        /// Produces the Windows short names for the folder names in the path.
        /// </summary>
        /// <param name="longFilePath"></param>
        /// <returns>Windows short file path names.</returns>
        public static string GetShortFilePath(string longFilePath)
        {
            StringBuilder shortFilePath = new StringBuilder(MAX_PATH_LENGTH);
            //string shortFilePath = string.Empty;
            GetShortPathName(longFilePath, shortFilePath, shortFilePath.Capacity);

            return shortFilePath.ToString();
        }

        /// <summary>
        /// Produces the long names for the folder names in the shortFilePath value.
        /// </summary>
        /// <param name="shortFilePath"></param>
        /// <returns>Windows long file path names.</returns>
        public static string GetLongFilePath(string shortFilePath)
        {
            StringBuilder longFilePath = new StringBuilder(MAX_PATH_LENGTH);

            GetLongPathName(shortFilePath, longFilePath, longFilePath.Capacity);

            return longFilePath.ToString();
        }

        /// <summary>
        /// Takes the drive name and folderPath and combines them into a formatted path. Backslash will be appended to end of folderPath if none supplied with input parameter.
        /// </summary>
        /// <param name="drive">Drive name (e.g. C:\).</param>
        /// <param name="folderPath">Path (e.g. foldername\subfoldername\</param>
        /// <returns>String containing formatted path.</returns>
        public static string FormatDrivePlusFolderPath(string drive, string folderPath)
        {
            StringBuilder filePath = new StringBuilder();

            filePath.Length = 0;
            filePath.Append(drive);
            if (folderPath.EndsWith(@"\") == false)
                filePath.Append(@"\");
            filePath.Append(folderPath);
            if (folderPath.EndsWith(@"\") == false)
                filePath.Append(@"\");

            return filePath.ToString();
        }

        /// <summary>
        /// Creates full path that includes folder names and file name.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        /// <returns>Formatted path (e.g. testdir\subdir1\file1.dat</returns>
        public static string FormatFilePath(string folderPath, string fileName)
        {
            StringBuilder filePath = new StringBuilder();

            filePath.Length = 0;
            filePath.Append(folderPath);
            if (folderPath.EndsWith(@"\") == false)
                filePath.Append(@"\");
            filePath.Append(fileName);

            return filePath.ToString();
        }

        /// <summary>
        /// Pulls together separate folderPath, fileName and fileExtension into a valid path.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <returns>String containing path.</returns>
        public static string FormatFilePath(string folderPath, string fileName, string fileExtension)
        {
            StringBuilder filePath = new StringBuilder();

            filePath.Length = 0;
            filePath.Append(folderPath);
            if (folderPath.EndsWith(@"\") == false)
                filePath.Append(@"\");
            filePath.Append(fileName);
            if (fileName.EndsWith(".") == false && fileExtension.StartsWith(".") == false)
                filePath.Append(".");
            filePath.Append(fileExtension);

            return filePath.ToString();
        }

        /// <summary>
        /// Pulls together separate folderPath, fileName and fileExtension into a valid path. Optionally, a date/time string can be appended to the filename.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <param name="AppendDateTimeToFileName">Current date/time will be appended to the filename if this is set to true.</param>
        /// <returns>String containing formatted path.</returns>

        public static string FormatFilePath(string folderPath, string fileName, string fileExtension, bool AppendDateTimeToFileName)
        {
            return FormatFilePath(folderPath, fileName, fileExtension, AppendDateTimeToFileName, string.Empty, DateTime.Now);
        }

        /// <summary>
        /// Pulls together separate folderPath, fileName and fileExtension into a valid path. Optionally, a date/time string can be appended to the filename.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <param name="AppendDateTimeToFileName">Current date/time will be appended to the filename if this is set to true.</param>
        /// <param name="dateTimeFormat">Format for the date/time string. If left blank or null, a default format of _yyyyMMdd_HHmmss will be used.</param>
        /// <returns>String containing formatted path.</returns>
        public static string FormatFilePath(string folderPath, string fileName, string fileExtension, bool AppendDateTimeToFileName, string dateTimeFormat)
        {
            return FormatFilePath(folderPath, fileName, fileExtension, AppendDateTimeToFileName, dateTimeFormat, DateTime.Now);
        }

        /// <summary>
        /// Pulls together separate folderPath, fileName and fileExtension into a valid path. Optionally, a date/time string can be appended to the filename.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <param name="AppendDateTimeToFileName">Date/time specified by the dateTimeToAppend parameter will be appended to the filename if this is set to true.</param>
        /// <param name="dateTimeFormat">Format for the date/time string. If left blank or null, a default format of _yyyyMMdd_HHmmss will be used.</param>
        /// <param name="dateTimeToAppend"></param>
        /// <returns>String containing formatted path.</returns>
        public static string FormatFilePath(string folderPath, string fileName, string fileExtension, bool AppendDateTimeToFileName, string dateTimeFormat, DateTime dateTimeToAppend)
        {
            StringBuilder filePath = new StringBuilder();

            filePath.Length = 0;
            filePath.Append(folderPath);
            if (folderPath.EndsWith(@"\") == false)
                filePath.Append(@"\");
            filePath.Append(fileName);
            if (AppendDateTimeToFileName)
            {
                string fmat = dateTimeFormat;
                DateTime dt = dateTimeToAppend;
                if(dt==null)
                    dt=DateTime.Now;
                if(String.IsNullOrEmpty(fmat))
                    fmat="_yyyyMMdd_HHmmss";
                filePath.Append(dt.ToString(fmat));
            }
            if (fileName.EndsWith(".") == false && fileExtension.StartsWith(".") == false)
                filePath.Append(".");
            filePath.Append(fileExtension);

            return filePath.ToString();
        }

        /// <summary>
        /// Breaks up a file path string into its constituent parts.
        /// </summary>
        /// <param name="filePath">Path to parse.</param>
        /// <returns>FilePathParts struct containing the different parts of the path.</returns>
        public static FilePathParts ParsefilePath(string filePath)
        {
            FilePathParts parsedFilePath = new FilePathParts(string.Empty, string.Empty, string.Empty, string.Empty);

            parsedFilePath.PathRoot = Path.GetPathRoot(filePath);
            parsedFilePath.FolderPath = Path.GetDirectoryName(filePath);
            parsedFilePath.FileName = Path.GetFileNameWithoutExtension(filePath);
            parsedFilePath.FileExtension = Path.GetExtension(filePath);

            return parsedFilePath;
        }

        /// <summary>
        /// Gets size of file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Long value that represents size of file.</returns>
        public static long GetFileSize(string fileName)
        {
            long numBytes = -1;
            FileInfo fi = new FileInfo(fileName);
            numBytes = fi.Length;
            fi = null;
            return numBytes;
        }

        /// <summary>
        /// Compresses the file at the path represented by the specified file name. Windows compression is used.
        /// </summary>
        /// <param name="fileName">Path to file to be compressed.</param>
        public static void Compress(string fileName)
        {
            string destinationFile = fileName;
            string destFileWithForwardSlashes = destinationFile.Replace(@"\", "/");
            FileInfo fileInfo = new FileInfo(destinationFile);
            if ((fileInfo.Attributes & FileAttributes.Compressed) != FileAttributes.Compressed)
            {
                string objPath = "CIM_DataFile.Name=" + "\"" + destFileWithForwardSlashes + "\"";
                using (ManagementObject dirEntry = new ManagementObject(objPath))
                {
                    ManagementBaseObject outParams = dirEntry.InvokeMethod("Compress", null, null);
                    uint ret = (uint)(outParams.Properties["ReturnValue"].Value);
                }
            }
        }


        /// <summary>
        /// Uncompresses the file at the path represented by the specified fileName. Windows compression is used.
        /// </summary>
        public static void Uncompress(string fileName)
        {
            string destinationFile = fileName;
            string destFileWithForwardSlashes = destinationFile.Replace(@"\", "/");
            FileInfo fileInfo = new FileInfo(destinationFile);
            if ((fileInfo.Attributes & FileAttributes.Compressed) == FileAttributes.Compressed)
            {
                string objPath = "CIM_DataFile.Name=" + "\"" + destFileWithForwardSlashes + "\"";
                using (ManagementObject dirEntry = new ManagementObject(objPath))
                {
                    ManagementBaseObject outParams = dirEntry.InvokeMethod("Uncompress", null, null);
                    uint ret = (uint)(outParams.Properties["ReturnValue"].Value);
                }
            }
        }

        /// <summary>
        /// Deletes specified file.
        /// </summary>
        /// <param name="fileToDelete">Full path to file to be deleted.</param>
        /// <remarks>Read-only files will be deleted by this method. See overloaded method with overrideReadOnlyAttribute parameter. You can set the parameter to false to bypass read-only files.</remarks>
        public static void FileDelete(string fileToDelete)
        {
            FileDelete(fileToDelete, true);
        }

        /// <summary>
        /// Deletes specified file.
        /// </summary>
        /// <param name="fileToDelete">Full path to file to be deleted.</param>
        /// <param name="overrideReadOnlyAttribute">If true, file with read-only attribute will be deleted. If false, file delete will fail.</param>
        public static void FileDelete(string fileToDelete, bool overrideReadOnlyAttribute)
        {
            if(File.Exists(fileToDelete))
            {
                if(overrideReadOnlyAttribute)
                    File.SetAttributes(fileToDelete, FileAttributes.Normal);
                File.Delete(fileToDelete);
            }
        }

        /// <summary>
        /// Copies files over a local area network.
        /// </summary>
        /// <param name="sourceFile">File to be copied.</param>
        /// <param name="destinationFile">Name of file at copy destination.</param>
        /// <remarks>Copy fails if destinationFile already exists.</remarks>
        public static void FileCopy(string sourceFile, string destinationFile)
        {
            FileCopy(sourceFile, destinationFile, false);
        }

        /// <summary>
        /// Copies files over a local area network.
        /// </summary>
        /// <param name="sourceFile">File to be copied.</param>
        /// <param name="destinationFile">Name of file at copy destination.</param>
        /// <param name="overwriteDestination">If true and destination file exists, destination file will be deleted before sourceFile is copied.</param>
        public static void FileCopy(string sourceFile, string destinationFile, bool overwriteDestination)
        {
            if (File.Exists(sourceFile) == false)
            {
                _msg.Length = 0;
                _msg.Append("Unable to copy file ");
                _msg.Append(sourceFile);
                _msg.Append(". File not found. ");
                throw new FileNotFoundException(_msg.ToString());
            }
            if (sourceFile == destinationFile)
            {
                _msg.Length = 0;
                _msg.Append("Unable to copy file ");
                _msg.Append(sourceFile);
                _msg.Append(". Source and destination file paths are the same. ");
                throw new IOException(_msg.ToString());
            }
            if (File.Exists(destinationFile))
            {
                if (overwriteDestination == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to copy file ");
                    _msg.Append(sourceFile);
                    _msg.Append(". Destination file ");
                    _msg.Append(destinationFile);
                    _msg.Append(" exists and option to overwrite destination is false.");
                    throw new IOException(_msg.ToString());
                }
            }

            File.Copy(sourceFile, destinationFile, overwriteDestination);
        }

        /// <summary>
        /// Moves files over a local area network.
        /// </summary>
        /// <param name="sourceFile">File to be moved.</param>
        /// <param name="destinationFile">Name of file at move destination.</param>
        /// <remarks>Move fails if destination file already exists. Original file is deleted after a successful move.</remarks>
        public static void FileMove(string sourceFile, string destinationFile)
        {
            FileMove(sourceFile, destinationFile, false);
        }

        /// <summary>
        /// Moves files over a local area network.
        /// </summary>
        /// <param name="sourceFile">File to be moved.</param>
        /// <param name="destinationFile">Name of file at move destination.</param>
        /// <param name="overwriteDestination">If true and destination file exists, destination file will be deleted before sourceFile is moved.</param>
        /// <remarks>Original file is deleted after the move.</remarks>
        public static void FileMove(string sourceFile, string destinationFile, bool overwriteDestination)
        {
            if (File.Exists(sourceFile) == false)
            {
                _msg.Length = 0;
                _msg.Append("Unable to move file ");
                _msg.Append(sourceFile);
                _msg.Append(". File not found. ");
                throw new FileNotFoundException(_msg.ToString());
            }
            if (sourceFile == destinationFile)
            {
                _msg.Length = 0;
                _msg.Append("Unable to move file ");
                _msg.Append(sourceFile);
                _msg.Append(". Source and destination file paths are the same. ");
                throw new IOException(_msg.ToString());
            }
            if (File.Exists(destinationFile))
            {
                if (overwriteDestination == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to move file ");
                    _msg.Append(sourceFile);
                    _msg.Append(". Destination file name ");
                    _msg.Append(destinationFile);
                    _msg.Append(" exists and option to overwrite destination is false.");
                    throw new IOException(_msg.ToString());
                }
                else
                {
                    //overwriteDestination = true and destination file exists: so delete it
                    File.Delete(destinationFile);
                }
            }

            File.Move(sourceFile, destinationFile);
        }


        /// <summary>
        /// Renames file. Renamed file exists in same directory as original file name.
        /// </summary>
        /// <param name="originalFile">Full path to original file.</param>
        /// <param name="newFileName">New file name.</param>
        /// <remarks>Rename will fail if file with new name already exists.</remarks>
        public static void FileRename(string originalFile, string newFileName)
        {
            FileRename(originalFile, newFileName, false);
        }

        /// <summary>
        /// Renames file. Renamed file exists in same directory as original file name.
        /// </summary>
        /// <param name="originalFile">Full path to original file.</param>
        /// <param name="newFileName">New file name.</param>
        /// <param name="overwriteDestination">If true and a file with same name as original exists, the file with same name will be deleted </param>
        public static void FileRename(string originalFile, string newFileName, bool overwriteDestination)
        {
            if (File.Exists(originalFile) == false)
            {
                _msg.Length = 0;
                _msg.Append("Unable to rename file ");
                _msg.Append(originalFile);
                _msg.Append(". File not found. ");
                throw new FileNotFoundException(_msg.ToString());
            }

            string originalDirectoryPath = Path.GetDirectoryName(originalFile);
            string originalFileName = Path.GetFileName(originalFile);
            string renameFileName = Path.GetFileName(newFileName);
            string renameFilePath = Path.Combine(originalDirectoryPath, renameFileName);

            if (File.Exists(renameFilePath))
            {
                if (overwriteDestination == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to rename file ");
                    _msg.Append(originalFile);
                    _msg.Append(". Destination file name ");
                    _msg.Append(renameFilePath);
                    _msg.Append(" exists and option to overwrite destination is false.");
                    throw new IOException(_msg.ToString());
                }
                else
                {
                    //overwriteDestination = true and destination file exists: so delete it
                    File.Delete(renameFilePath);
                }
            }

            File.Move(originalFileName, renameFilePath);

        }

        /// <summary>
        /// Checks if file name is a valid Windows file name.
        /// </summary>
        /// <param name="fileName">File name plus extension, if any.</param>
        /// <returns>True if file name is valid.</returns>
        /// <remarks>Do not specify a folder path for this method. The folder separators (\ or /) will be considered an error.</remarks>
        public static bool IsValidFileName(string fileName)
        {
            bool ret = true;

            string regexString = "[" + Regex.Escape(new String(Path.GetInvalidFileNameChars())) + "]";
            Regex containsABadCharacter = new Regex(regexString);
            string forbiddenFileNameCharacters = @"*[/\|<>:*?""]*";

            if (containsABadCharacter.IsMatch(fileName)
                || fileName.IndexOfAny(forbiddenFileNameCharacters.ToCharArray()) != -1)
            {
                ret = false;
            }
            

            return ret;
        }

        /// <summary>
        /// Determines whether or not the specified path contains legal Windows file path characters.
        /// </summary>
        /// <param name="pathName">Path to be validated.</param>
        /// <returns>True if path is a valid Windows path.</returns>
        public static bool IsValidPath(string pathName)
        {
            bool ret = true;

            string regexString = "[" + Regex.Escape(new String(Path.GetInvalidPathChars())) + "]";
            Regex containsABadCharacter = new Regex(regexString);
            string forbiddenPathNameCharacters = @"*[|<>*?""]*";
            char[] dirSeparators = {'\\','/'};


            if (containsABadCharacter.IsMatch(pathName)
                || pathName.IndexOfAny(forbiddenPathNameCharacters.ToCharArray()) != -1)
            {
                ret = false;
            }

            string[] dirs = pathName.Split(dirSeparators);
            if (ret == true)
            {
                if (dirs.Length > 0)
                {
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        if ((i > 0) || (i == 0 && dirs[i].Contains(':') == false))
                        {
                            //ignore 1st entry if it is a drive (e.g. c:
                            if (PFFile.IsValidFileName(dirs[i]) == false)
                            {
                                ret = false;
                                break;
                            }
                        }
                    }
                }
            }

            return ret;
        }

    }//end class
}//end namespace
