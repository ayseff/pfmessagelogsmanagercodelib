//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management;
using AppGlobals;

namespace PFFileSystemObjects
{
    /// <summary>
    /// Class for working with Windows directories (folders).
    /// </summary>
    public class PFDirectory
    {
        //private work variables
        private static StringBuilder _msg = new StringBuilder();

        private static string _defaultXCopyFileSearchPattern = "*.*";
        private static bool _defaultXCopyOverwriteDestination = true;
        private static bool _defaultXCopySubdirCopy = true;
        private static bool _defaultXCopyOnErrorContinue = true;
        private static int _defaultXCopyMaxNumErrors = 5;

        //private varialbles for properties
        //private string _path;

        //events
#pragma warning disable 1591
        public delegate void XCopyResultDelegate(SourceAndDestinationInfo objectInfo, XCopyInfo runningTotals);
        public static event XCopyResultDelegate returnXCopyResult;
        public delegate void XDeleteResultDelegate(SourceInfo objecttInfo, XDeleteInfo runningTotals);
        public static event XDeleteResultDelegate returnXDeleteResult;
        public delegate void DeleteSubfolderResultDelegate(FileSystemObjectType deletedObjectType, string deletedObjectPath);
        public static event DeleteSubfolderResultDelegate returnDeleteSubfolderResult;
        public delegate void DeleteFilesResultDelegate(string deletedFilePath);
        public static event DeleteFilesResultDelegate returnDeleteFilesResult;
        public delegate void ErrorMessageDelegate(string errorMessage);
        public static event ErrorMessageDelegate returnErrorMessage;
#pragma warning restore 1591

        //constructors

        //properties

        //methods

        /// <summary>
        /// Calculates size in bytes of the directory.
        /// </summary>
        /// <param name="directoryPath">Path to directory to be sized.</param>
        /// <param name="includeSubfolders">True if you wish to include all subdirectories in the size calculation.</param>
        /// <returns></returns>
        public static DirSizeInfo GetDirectorySize(string directoryPath, bool includeSubfolders)
        {
            DirSizeInfo sizeInfo;
            DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
            sizeInfo.NumBytes = 0;
            sizeInfo.NumFiles = 0;
            sizeInfo.NumFolders = 0;
            sizeInfo.NumErrors = 0;
            sizeInfo.errorMessages = string.Empty;

            //List<string> files = new List<string>(Directory.EnumerateFiles(directoryPath));
            //foreach (FileInfo fileInfo in dirInfo.GetFiles())
            //{
            //    sizeInfo.NumBytes += fileInfo.Length;
            //    sizeInfo.NumFiles++;
            //}

            List<string> files = null;
            //List<FileInfo> fileInfos = new List<FileInfo>();

            try
            {
                files = new List<string>(Directory.EnumerateFiles(directoryPath));
            }
            catch (UnauthorizedAccessException UAEx)
            {
                _msg.Length = 0;
                _msg.Append("Access denied: Unable to retrieve file information list. Permissions exception: ");
                _msg.Append(directoryPath);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(UAEx));
                _msg.Append("\r\n");
                
            }
            catch (PathTooLongException PathEx)
            {
                _msg.Length = 0;
                _msg.Append("Access denied: Unable to retrieve file information list. Path too long exception: ");
                _msg.Append(directoryPath);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(PathEx));
                _msg.Append("\r\n");
            }
            catch (DirectoryNotFoundException NotFoundEx)
            {
                _msg.Length = 0;
                _msg.Append("Directory does not exist: ");
                _msg.Append(directoryPath);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(NotFoundEx));
                _msg.Append("\r\n");
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to process: ");
                _msg.Append(directoryPath);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                _msg.Append("\r\n");
            }

            if (files != null)
            {
                foreach (var fileItem in files)
                {
                    try
                    {
                        FileInfo file = new FileInfo(fileItem);
                        sizeInfo.NumBytes += file.Length;
                        sizeInfo.NumFiles++;
                    }
                    catch (UnauthorizedAccessException UAEx)
                    {
                        _msg.Length = 0;
                        _msg.Append("Access denied: Permissions exception: ");
                        _msg.Append(fileItem);
                        _msg.Append(" Error message: ");
                        _msg.Append(AppMessages.FormatErrorMessage(UAEx));
                        _msg.Append("\r\n");
                        sizeInfo.NumErrors += 1;
                        sizeInfo.errorMessages += _msg.ToString();
                    }
                    catch (PathTooLongException PathEx)
                    {
                        _msg.Length = 0;
                        _msg.Append("Access denied: Path too long exception: ");
                        _msg.Append(fileItem);
                        _msg.Append(" Error message: ");
                        _msg.Append(AppMessages.FormatErrorMessage(PathEx));
                        _msg.Append("\r\n");
                        sizeInfo.NumErrors += 1;
                        sizeInfo.errorMessages += _msg.ToString();
                    }
                    catch (System.Exception ex)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to process: ");
                        _msg.Append(fileItem);
                        _msg.Append(" Error message: ");
                        _msg.Append(AppMessages.FormatErrorMessage(ex));
                        _msg.Append("\r\n");
                    }
                }
            }


            //List<string> dirs = new List<string>(Directory.EnumerateDirectories(directoryPath));

            List<string> dirs = null;
            
            try
            {
                dirs = new List<string>(Directory.EnumerateDirectories(directoryPath));
            }
            catch (UnauthorizedAccessException UAEx)
            {
                _msg.Length = 0;
                _msg.Append("Access denied: Unable to get directory information. Permissions exception: ");
                _msg.Append(directoryPath);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(UAEx));
                _msg.Append("\r\n");
                sizeInfo.NumErrors += 1;
                sizeInfo.errorMessages += _msg.ToString();
            }
            catch (PathTooLongException PathEx)
            {
                _msg.Length = 0;
                _msg.Append("Access denied: Unable to get directory information. Path too long exception: ");
                _msg.Append(directoryPath);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(PathEx));
                _msg.Append("\r\n");
                sizeInfo.NumErrors += 1;
                sizeInfo.errorMessages += _msg.ToString();
            }
            catch (DirectoryNotFoundException NotFoundEx)
            {
                _msg.Length = 0;
                _msg.Append("Directory does not exist: ");
                _msg.Append(directoryPath);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(NotFoundEx));
                _msg.Append("\r\n");
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to process: ");
                _msg.Append(directoryPath);
                _msg.Append(" Error message: ");
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                _msg.Append("\r\n");
            }

            if (dirs != null)
            {
                foreach (var subdirItem in dirs)
                {
                    try
                    {
                        sizeInfo.NumFolders++;
                        if (includeSubfolders)
                        {
                            try
                            {
                                DirectoryInfo subdir = new DirectoryInfo(subdirItem);
                                DirSizeInfo subdirSizeInfo = GetDirectorySize(subdir.FullName, includeSubfolders);
                                sizeInfo.NumBytes += subdirSizeInfo.NumBytes;
                                sizeInfo.NumFiles += subdirSizeInfo.NumFiles;
                                sizeInfo.NumFolders += subdirSizeInfo.NumFolders;
                                sizeInfo.NumErrors += subdirSizeInfo.NumErrors;
                                sizeInfo.errorMessages += subdirSizeInfo.errorMessages;
                            }
                            catch (UnauthorizedAccessException UAEx)
                            {
                                _msg.Length = 0;
                                _msg.Append("Access denied: Unable to get directory information. Permissions exception: ");
                                _msg.Append(subdirItem);
                                _msg.Append(" Error message: ");
                                _msg.Append(AppMessages.FormatErrorMessage(UAEx));
                                _msg.Append("\r\n");
                                sizeInfo.NumErrors += 1;
                                sizeInfo.errorMessages += _msg.ToString();
                            }
                            catch (PathTooLongException PathEx)
                            {
                                _msg.Length = 0;
                                _msg.Append("Access denied: Unable to get directory information. Path too long exception: ");
                                _msg.Append(subdirItem);
                                _msg.Append(" Error message: ");
                                _msg.Append(AppMessages.FormatErrorMessage(PathEx));
                                _msg.Append("\r\n");
                                sizeInfo.NumErrors += 1;
                                sizeInfo.errorMessages += _msg.ToString();
                            }
                            catch (System.Exception ex)
                            {
                                _msg.Length = 0;
                                _msg.Append("Unable to process: ");
                                _msg.Append(subdirItem);
                                _msg.Append(" Error message: ");
                                _msg.Append(AppMessages.FormatErrorMessage(ex));
                                _msg.Append("\r\n");
                            }

                        }
                    }
                    catch (UnauthorizedAccessException UAEx)
                    {
                        sizeInfo.NumErrors++;
                        _msg.Length = 0;
                        _msg.Append("Directory sizer failed due to permissions exception: ");
                        _msg.Append(subdirItem);
                        _msg.Append(" Error message: ");
                        _msg.Append(AppMessages.FormatErrorMessage(UAEx));
                        _msg.Append("\r\n");
                        sizeInfo.errorMessages += _msg.ToString();
                    }
                    catch (PathTooLongException PathEx)
                    {
                        sizeInfo.NumErrors++;
                        _msg.Length = 0;
                        _msg.Append("Directory sizer failed due to path too long exception: ");
                        _msg.Append(subdirItem);
                        _msg.Append(" Error message: ");
                        _msg.Append(AppMessages.FormatErrorMessage(PathEx));
                        _msg.Append("\r\n");
                        sizeInfo.errorMessages += _msg.ToString();
                    }
                    catch (System.Exception ex)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to process: ");
                        _msg.Append(subdirItem);
                        _msg.Append(" Error message: ");
                        _msg.Append(AppMessages.FormatErrorMessage(ex));
                        _msg.Append("\r\n");
                    }
                }
            }//end if


             return sizeInfo;
        }


        /// <summary>
        /// Compresses the folder at the path represented by the current instance of the class. Windows compression is used.
        /// </summary>
        /// <remarks>Subdirectories are not included in the compression.</remarks>
        public static void Compress(string directoryName)
        {
            Compress(directoryName, false);
        }

        /// <summary>
        /// Compresses the folder at the path represented by the current instance of the class.  Windows compression is used.
        /// </summary>
        /// <param name="directoryName">Path to directory to be compressed.</param>
        /// <param name="compressSubdirectories">If true, the subdirectories for the directory are included in the compression.</param>
        public static void Compress(string directoryName, bool compressSubdirectories)
        {
            string destinationDir = directoryName;
            string destDirWithForwardSlashes = destinationDir.Replace(@"\", "/");
            DirectoryInfo directoryInfo = new DirectoryInfo(destinationDir);
            if ((directoryInfo.Attributes & FileAttributes.Compressed) != FileAttributes.Compressed)
            {
                string objPath = "Win32_Directory.Name=" + "\"" + destDirWithForwardSlashes + "\"";
                using (ManagementObject dir = new ManagementObject(objPath))
                {
                    ManagementBaseObject outParams = dir.InvokeMethod("Compress", null, null);
                    uint ret = (uint)(outParams.Properties["ReturnValue"].Value);
                }
            }
            if (compressSubdirectories)
            {
                if (directoryInfo.GetDirectories().Length > 0)
                {
                    foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
                    {
                        Compress(subdir.FullName,true);
                    }
                }
            }
        }

        /// <summary>
        /// Uncompresses the folder at the path represented by the specified directory name. Windows compression is used.
        /// </summary>
        /// <param name="directoryName">Path to directory to be uncompressed.</param>
        /// <remarks>Subdirectories are not included in the uncompression.</remarks>
        public static void Uncompress(string directoryName)
        {
            Uncompress(directoryName, false);
        }
        
        /// <summary>
        /// Uncompresses the folder at the path represented by the current instance of the class. Windows compression is used.
        /// </summary>
        /// <param name="directoryName">Path to directory to be uncompressed.</param>
        /// <param name="uncompressSubdirectories">If true, the subdirectories for the directory are included in the uncompression.</param>
        public static void Uncompress(string directoryName, bool uncompressSubdirectories)
        {
            string destinationDir = directoryName;
            string destDirWithForwardSlashes = destinationDir.Replace(@"\", "/");
            DirectoryInfo directoryInfo = new DirectoryInfo(destinationDir);
            if ((directoryInfo.Attributes & FileAttributes.Compressed) == FileAttributes.Compressed)
            {
                string objPath = "Win32_Directory.Name=" + "\"" + destDirWithForwardSlashes + "\"";
                using (ManagementObject dir = new ManagementObject(objPath))
                {
                    ManagementBaseObject outParams = dir.InvokeMethod("Uncompress", null, null);
                    uint ret = (uint)(outParams.Properties["ReturnValue"].Value);
                }
            }
            if (uncompressSubdirectories)
            {
                if (directoryInfo.GetDirectories().Length > 0)
                {
                    foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
                    {
                        Uncompress(subdir.FullName, true);
                    }
                }
            }
        }


        /// <summary>
        /// Routine to copy the contents of a folder to another location. 
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder to be copied.</param>
        /// <param name="destFolderPath">Full path of the destination folder for the copy operation. Folder will be created if it does not already exist.</param>
        /// <returns>XCopyInfo structure is returned. See <see cref="PFFileSystemObjects.XCopyInfo"/> for information on contents of the structure.</returns>
        public static XCopyInfo XCopy(string sourceFolderPath, string destFolderPath)
        {
            return XCopy(sourceFolderPath, destFolderPath, _defaultXCopyFileSearchPattern, _defaultXCopyOverwriteDestination, _defaultXCopySubdirCopy, false, _defaultXCopyOnErrorContinue, _defaultXCopyMaxNumErrors);
        }

        /// <summary>
        /// Routine to copy the contents of a folder to another location. 
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder to be copied.</param>
        /// <param name="destFolderPath">Full path of the destination folder for the copy operation. Folder will be created if it does not already exist.</param>
        /// <param name="copySubDirs">If true, the folder plus all its subfolders will be copied. If false, only the files in the source folder will be copied.</param>
        /// <returns>XCopyInfo structure is returned. See <see cref="PFFileSystemObjects.XCopyInfo"/> for information on contents of the structure.</returns>
        public static XCopyInfo XCopy(string sourceFolderPath, string destFolderPath, bool copySubDirs)
        {
            return XCopy(sourceFolderPath, destFolderPath, _defaultXCopyFileSearchPattern, true, copySubDirs, false, _defaultXCopyOnErrorContinue, _defaultXCopyMaxNumErrors);
        }

        /// <summary>
        /// Routine to copy the contents of a folder to another location. 
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder to be copied.</param>
        /// <param name="destFolderPath">Full path of the destination folder for the copy operation. Folder will be created if it does not already exist.</param>
        /// <param name="fileSearchPattern">You can specify a Windows style search pattern to limit which files are copied. For example, *.* search pattern means copy all files. Copy all is the default.</param>
        /// <returns>XCopyInfo structure is returned. See <see cref="PFFileSystemObjects.XCopyInfo"/> for information on contents of the structure.</returns>
        public static XCopyInfo XCopy(string sourceFolderPath, string destFolderPath, string fileSearchPattern)
        {
            return XCopy(sourceFolderPath, destFolderPath, fileSearchPattern, _defaultXCopyOverwriteDestination, _defaultXCopySubdirCopy, false, _defaultXCopyOnErrorContinue, _defaultXCopyMaxNumErrors);
        }

        /// <summary>
        /// Routine to copy the contents of a folder to another location. 
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder to be copied.</param>
        /// <param name="destFolderPath">Full path of the destination folder for the copy operation. Folder will be created if it does not already exist.</param>
        /// <param name="overwriteIfDestAreadyExists">If true, contents of output folder or file will be replaced. If false, copy will fail and be recorded as an error. See continueOnError and maxNumError parameters for information on how copy errors are handled.</param>
        /// <param name="copySubDirs">If true, the folder plus all its subfolders will be copied. If false, only the files in the source folder will be copied.</param>
        /// <returns>XCopyInfo structure is returned. See <see cref="PFFileSystemObjects.XCopyInfo"/> for information on contents of the structure.</returns>
        public static XCopyInfo XCopy(string sourceFolderPath, string destFolderPath, bool overwriteIfDestAreadyExists, bool copySubDirs)
        {
            return XCopy(sourceFolderPath, destFolderPath, _defaultXCopyFileSearchPattern, overwriteIfDestAreadyExists, copySubDirs, false, _defaultXCopyOnErrorContinue, _defaultXCopyMaxNumErrors);
        }


        /// <summary>
        /// Routine to copy the contents of a folder to another location. 
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder to be copied.</param>
        /// <param name="destFolderPath">Full path of the destination folder for the copy operation. Folder will be created if it does not already exist.</param>
        /// <param name="fileSearchPattern">You can specify a Windows style search pattern to limit which files are copied. For example, *.* search pattern means copy all files. Copy all is the default.</param>
        /// <param name="overwriteIfDestAreadyExists">If true, contents of output folder or file will be replaced. If false, copy will fail and be recorded as an error. See continueOnError and maxNumError parameters for information on how copy errors are handled.</param>
        /// <param name="copySubDirs">If true, the folder plus all its subfolders will be copied. If false, only the files in the source folder will be copied.</param>
        /// <returns>XCopyInfo structure is returned. See <see cref="PFFileSystemObjects.XCopyInfo"/> for information on contents of the structure.</returns>
        public static XCopyInfo XCopy(string sourceFolderPath, string destFolderPath, string fileSearchPattern, bool overwriteIfDestAreadyExists, bool copySubDirs)
        {
            return XCopy(sourceFolderPath, destFolderPath, fileSearchPattern, overwriteIfDestAreadyExists, copySubDirs, false, _defaultXCopyOnErrorContinue, _defaultXCopyMaxNumErrors);
        }

        /// <summary>
        /// Routine to copy the contents of a folder to another location. 
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder to be copied.</param>
        /// <param name="destFolderPath">Full path of the destination folder for the copy operation. Folder will be created if it does not already exist.</param>
        /// <param name="overwriteIfDestAreadyExists">If true, contents of output folder or file will be replaced. If false, copy will fail and be recorded as an error. See continueOnError and maxNumError parameters for information on how copy errors are handled.</param>
        /// <param name="copySubDirs">If true, the folder plus all its subfolders will be copied. If false, only the files in the source folder will be copied.</param>
        /// <param name="preserveTimestamps">If true then last write and create date of the destination files are set to corresponding values of the source files. If false, then default System.IO File.Copy rules apply (modified date is preserved while create date is set to date of file copy).</param>
        /// <returns>XCopyInfo structure is returned. See <see cref="PFFileSystemObjects.XCopyInfo"/> for information on contents of the structure.</returns>
        public static XCopyInfo XCopy(string sourceFolderPath, string destFolderPath, bool overwriteIfDestAreadyExists, bool copySubDirs, bool preserveTimestamps)
        {
            return XCopy(sourceFolderPath, destFolderPath, _defaultXCopyFileSearchPattern, overwriteIfDestAreadyExists, copySubDirs, preserveTimestamps, _defaultXCopyOnErrorContinue, _defaultXCopyMaxNumErrors);
        }


        /// <summary>
        /// Routine to copy the contents of a folder to another location. 
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder to be copied.</param>
        /// <param name="destFolderPath">Full path of the destination folder for the copy operation. Folder will be created if it does not already exist.</param>
        /// <param name="fileSearchPattern">You can specify a Windows style search pattern to limit which files are copied. For example, *.* search pattern means copy all files. Copy all is the default.</param>
        /// <param name="overwriteIfDestAreadyExists">If true, contents of output folder or file will be replaced. If false, copy will fail and be recorded as an error. See continueOnError and maxNumError parameters for information on how copy errors are handled.</param>
        /// <param name="copySubDirs">If true, the folder plus all its subfolders will be copied. If false, only the files in the source folder will be copied.</param>
        /// <param name="preserveTimestamps">If true then last write and create date of the destination files are set to corresponding values of the source files. If false, then default System.IO File.Copy rules apply (modified date is preserved while create date is set to date of file copy).</param>
        /// <returns>XCopyInfo structure is returned. See <see cref="PFFileSystemObjects.XCopyInfo"/> for information on contents of the structure.</returns>
        public static XCopyInfo XCopy(string sourceFolderPath, string destFolderPath, string fileSearchPattern, bool overwriteIfDestAreadyExists, bool copySubDirs, bool preserveTimestamps)
        {
            return XCopy(sourceFolderPath, destFolderPath, fileSearchPattern, overwriteIfDestAreadyExists, copySubDirs, preserveTimestamps, _defaultXCopyOnErrorContinue, _defaultXCopyMaxNumErrors);
        }

        /// <summary>
        /// Routine to copy the contents of a folder to another location. 
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder to be copied.</param>
        /// <param name="destFolderPath">Full path of the destination folder for the copy operation. Folder will be created if it does not already exist.</param>
        /// <param name="fileSearchPattern">You can specify a Windows style search pattern to limit which files are copied. For example, *.* search pattern means copy all files. Copy all is the default.</param>
        /// <param name="overwriteIfDestAreadyExists">If true, contents of output folder or file will be replaced. If false, copy will fail and be recorded as an error. See continueOnError and maxNumError parameters for information on how copy errors are handled.</param>
        /// <param name="copySubDirs">If true, the folder plus all its subfolders will be copied. If false, only the files in the source folder will be copied.</param>
        /// <param name="preserveTimestamps">If true then last write and create date of the destination files are set to corresponding values of the source files. If false, then default System.IO File.Copy rules apply (modified date is preserved while create date is set to date of file copy).</param>
        /// <param name="continueOnError">If true, the application will attempt to continue if a folder copy operation failed because the source folder or file was unavailable or the destination folder or file could not be created. Default is to continue on error.</param>
        /// <param name="maxNumErrors">Maximum number of copy errors before the routine will terminate and throw an error message. Default is 5.</param>
        /// <returns>XCopyInfo structure is returned. See <see cref="PFFileSystemObjects.XCopyInfo"/> for information on contents of the structure.</returns>
        public static XCopyInfo XCopy(string sourceFolderPath, string destFolderPath, string fileSearchPattern, bool overwriteIfDestAreadyExists, bool copySubDirs,
                                        bool preserveTimestamps, bool continueOnError, int maxNumErrors)
        {
            XCopyInfo sizeInfo = new XCopyInfo();


            try
            {
                if (Directory.Exists(sourceFolderPath))
                {
                    DirectoryCopy(sourceFolderPath, destFolderPath, fileSearchPattern, overwriteIfDestAreadyExists, copySubDirs, preserveTimestamps, continueOnError, maxNumErrors, ref sizeInfo);
                    if (preserveTimestamps)
                    {
                        SyncDirectoryTimestamps(sourceFolderPath, destFolderPath, copySubDirs);
                    }
                }
                else
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourceFolderPath);
                }
            }
            catch (DirectoryNotFoundException)
            {
                sizeInfo.NumErrors++;
                _msg.Length = 0;
                _msg.Append("Directory not found: ");
                _msg.Append(sourceFolderPath);
                _msg.Append(".");
                _msg.Append(Environment.NewLine);
                throw new System.Exception(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(DirectoryCopyErrorMessage(sourceFolderPath, destFolderPath));
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                if (returnErrorMessage != null)
                    returnErrorMessage(_msg.ToString());
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }
                 
        

            return sizeInfo;
        }

        private static void DirectoryCopy(string sourceFolderPath, string destFolderPath, string fileSearchPattern, bool overwriteIfDestAreadyExists, bool copySubDirs,
                                          bool preserveTimestamps, bool continueOnError, int maxNumErrors, ref XCopyInfo sizeInfo)
        {
            try
            {
                // Get the subdirectories for the specified directory.
                DirectoryInfo dir = new DirectoryInfo(sourceFolderPath);
                DirectoryInfo[] dirs = dir.GetDirectories();

                if (dir.Exists==false)
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourceFolderPath);
                }

                string saveSearchPattern = String.IsNullOrEmpty(fileSearchPattern) ? "*.*" : fileSearchPattern;
                PFFileSpec fileSpec = new PFFileSpec(saveSearchPattern);

                try
                {
                    // If the destination directory doesn't exist, create it. 
                    if (Directory.Exists(destFolderPath) == false)
                    {
                        Directory.CreateDirectory(destFolderPath);
                    }
                    else
                    {
                        sizeInfo.NumFolderOverwrites++;
                    }

                    sizeInfo.NumFolders++;

                    // Get the files in the directory and copy them to the new location.
                    FileInfo[] files = dir.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        if (fileSpec.IsMatch(file.Name) || saveSearchPattern == "*.*")
                        {
                            //test
                            //FileInfo testfi = new FileInfo(file.FullName);
                            //if (testfi.Extension.ToLower() == ".txt")
                            //{
                            //    _msg.Length = 0;
                            //    _msg.Append("Test DirectoryNotFoundException for ");
                            //    _msg.Append(testfi.FullName);
                            //    _msg.Append(Environment.NewLine);
                            //    throw new DirectoryNotFoundException(_msg.ToString());
                            //}
                            //end test
                            string temppath = Path.Combine(destFolderPath, file.Name);
                            if (File.Exists(temppath))
                            {
                                if (overwriteIfDestAreadyExists)
                                {
                                    sizeInfo.NumFileOverwrites++;
                                    File.SetAttributes(temppath, FileAttributes.Normal);
                                    File.Delete(temppath);
                                }
                            }
                            file.CopyTo(temppath, overwriteIfDestAreadyExists);
                            sizeInfo.NumFiles++;
                            sizeInfo.NumBytes += file.Length;
                            FileInfo destFile = new FileInfo(temppath);
                            if (preserveTimestamps)
                            {
                                //need to save attributes to take care of case where dest file is read-only
                                FileAttributes saveAttr = destFile.Attributes;
                                File.SetAttributes(destFile.FullName, FileAttributes.Normal);
                                destFile.CreationTimeUtc = file.CreationTimeUtc;
                                destFile.LastWriteTimeUtc = file.LastWriteTimeUtc;
                                File.SetAttributes(destFile.FullName, saveAttr);
                            }
                            if (returnXCopyResult != null)
                            {
                                SourceAndDestinationInfo objectInfo = new SourceAndDestinationInfo();
                                objectInfo.sourcePath = Path.Combine(file.FullName);
                                objectInfo.destinationPath = temppath;
                                objectInfo.objectType = FileSystemObjectType.File;
                                objectInfo.sourceBytes = file.Length;
                                objectInfo.destinationBytes = destFile.Length;
                                returnXCopyResult(objectInfo, sizeInfo);
                            }
                        }
                    }//end foreach


                }
                catch (DirectoryNotFoundException ex)
                {
                    sizeInfo.NumErrors++;
                    sizeInfo.errorMessages += DirectoryDeleteErrorMessage(sourceFolderPath) + ":" + ex.Message;
                    if (sizeInfo.NumErrors > maxNumErrors || continueOnError == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Copy failed for ");
                        _msg.Append(sourceFolderPath);
                        _msg.Append(" to ");
                        _msg.Append(".");
                        _msg.Append(destFolderPath);
                        _msg.Append(Environment.NewLine);
                        if (continueOnError == false)
                        {
                            _msg.Append(" ContinueOnError is set to false.");
                        }
                        else if (sizeInfo.NumErrors > maxNumErrors)
                        {
                            _msg.Append(" Maximum error count exceeded. Number of errors encountered: ");
                            _msg.Append(sizeInfo.NumErrors.ToString("#,##0"));
                        }
                        else
                        {
                            _msg.Append(" Source Directory Not Found Exception during copy operation: ");
                        }
                        _msg.Append(ex.Message);
                        _msg.Append(Environment.NewLine);
                        if (returnErrorMessage != null)
                            returnErrorMessage(_msg.ToString());
                        throw new System.Exception(_msg.ToString());
                    }
                    else
                    {
                        if(returnErrorMessage != null)
                            returnErrorMessage(DirectoryDeleteErrorMessage(sourceFolderPath) + ":" + ex.Message);
                    }
                }
                catch (System.Exception)
                {
                    throw;
                }

        
                // If copying subdirectories, copy them and their contents to new location. 
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(destFolderPath, subdir.Name);
                        DirectoryCopy(subdir.FullName, temppath, fileSearchPattern, overwriteIfDestAreadyExists, copySubDirs, preserveTimestamps, continueOnError, maxNumErrors, ref sizeInfo);
                    }
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(DirectoryCopyErrorMessage(sourceFolderPath, destFolderPath));
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }
                 
        }

        private static string DirectoryCopyErrorMessage(string sourceFolderPath, string destFolderPath)
        {
            _msg.Length = 0;
            _msg.Append("Error attempting to copy ");
            _msg.Append(sourceFolderPath);
            _msg.Append(" to ");
            _msg.Append(destFolderPath);
            _msg.Append(".");
            _msg.Append(Environment.NewLine);
            return _msg.ToString();
        }

        /// <summary>
        /// Routine to set the create and modified folder timestamps in the destination path to be the same as those in the source path.
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder contaning the timestamps to be used in the sync operation.</param>
        /// <param name="destFolderPath">Full path of the destination folder for the timestamp sync operation.</param>
        /// <param name="includeSubdirs">If true, the folder plus all its subfolders will have their timestamps synchronized. If false, only the files in the source folder will have their timestamsp synchronized.</param>
        public static void SyncDirectoryTimestamps(string sourceFolderPath, string destFolderPath, bool includeSubdirs)
        {

            try
            {
                DirectoryInfo sourceDir = new DirectoryInfo(sourceFolderPath);
                DirectoryInfo destDir = new DirectoryInfo(destFolderPath);
                DirectoryInfo[] dirs = sourceDir.GetDirectories();

                destDir.CreationTimeUtc = sourceDir.CreationTimeUtc;
                destDir.LastWriteTimeUtc = sourceDir.LastWriteTimeUtc;

                if (includeSubdirs)
                {
                    foreach (DirectoryInfo di in dirs)
                    {
                        string temppath = Path.Combine(destFolderPath, di.Name);
                        if(Directory.Exists(temppath))
                        {
                            SyncDirectoryTimestamps(di.FullName, temppath, includeSubdirs);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("SyncDirectoryTimestamps routine failed:\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }
        
        }

        /// <summary>
        /// Routine to set the create and modified timestamps for files in the destination path to be the same as those in the source path.
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder contaning file timestamps to be used in the sync operation.</param>
        /// <param name="destFolderPath">Full path of the destination folder for the file timestamp sync operation.</param>
        /// <param name="fileSearchPattern">You can specify a Windows style search pattern to limit which files will have their timestamps synchronized. For example, *.* search pattern means synchronize the timestamps for all files. Synchronize all file timestamps is the default.</param>
        /// <param name="includeSubdirs">If true, the folder plus all its subfolders will have their file timestamps synchronized. If false, only the files in the source folder will have their timestamsp synchronized.</param>
        public static void SyncFileTimestamps(string sourceFolderPath, string destFolderPath, string fileSearchPattern, bool includeSubdirs)
        {

            DirectoryInfo sourceDir = null;

            try
            {
                string saveSearchPattern = String.IsNullOrEmpty(fileSearchPattern) ? "*.*" : fileSearchPattern;
                PFFileSpec fileSpec = new PFFileSpec(saveSearchPattern);

                if (Directory.Exists(sourceFolderPath))
                {
                    sourceDir = new DirectoryInfo(sourceFolderPath);
                    FileInfo[] files = sourceDir.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        if (fileSpec.IsMatch(file.Name) || saveSearchPattern == "*.*")
                        {
                            string temppath = Path.Combine(destFolderPath, file.Name);
                            if (File.Exists(temppath))
                            {
                                FileInfo destFile = new FileInfo(temppath);
                                //need to save attributes to take care of case where dest file is read-only
                                FileAttributes saveAttr = destFile.Attributes;
                                File.SetAttributes(destFile.FullName, FileAttributes.Normal);
                                destFile.CreationTimeUtc = file.CreationTimeUtc;
                                destFile.LastWriteTimeUtc = file.LastWriteTimeUtc;
                                File.SetAttributes(destFile.FullName, saveAttr);
                            }
                        }
                    }//end foreach
                    if (includeSubdirs)
                    {
                        DirectoryInfo[] dirs = sourceDir.GetDirectories();
                        foreach (DirectoryInfo di in dirs)
                        {
                            string temppath = Path.Combine(destFolderPath, di.Name);
                            if (Directory.Exists(temppath))
                            {
                                SyncFileTimestamps(sourceFolderPath, destFolderPath, fileSearchPattern, includeSubdirs);
                            }
                        }
                    }

                }//end if directory exists
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("SyncFileTimestamps routine failed:\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }
        
        }//end method

        /// <summary>
        /// Routine to delete files. Will override attributes to delete read-only files. Can delete all files and subfolders in the source's directory tree.
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder to be deleted.</param>
        /// <param name="includeSubDirs">If true, the folder plus all its subfolders will be deleted. If false, only the files in the source folder will be deleted.</param>
        /// <returns>XDeleteInfo structure is returned. See <see cref="PFFileSystemObjects.XDeleteInfo"/> for information on contents of the structure.</returns>
        public static XDeleteInfo XDelete(string sourceFolderPath, bool includeSubDirs)
        {
            return XDelete(sourceFolderPath, "*.*", includeSubDirs, true, 3);
        }

        /// <summary>
        /// Routine to delete files. Will override attributes to delete read-only files. Can delete all files and subfolders in the source's directory tree.
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder to be deleted.</param>
        /// <param name="fileSearchPattern">You can specify a Windows style search pattern to limit which files are deleted. For example, *.* search pattern means delete all files. Delete all is the default.</param>
        /// <param name="includeSubDirs">If true, the folder plus all its subfolders will be deleted. If false, only the files in the source folder will be deleted.</param>
        /// <returns>XDeleteInfo structure is returned. See <see cref="PFFileSystemObjects.XDeleteInfo"/> for information on contents of the structure.</returns>
        public static XDeleteInfo XDelete(string sourceFolderPath, string fileSearchPattern, bool includeSubDirs)
        {
            return XDelete(sourceFolderPath, fileSearchPattern, includeSubDirs, true, 3);
        }

        /// <summary>
        /// Routine to delete files. Will override attributes to delete read-only files. Can delete all files and subfolders in the source's directory tree.
        /// </summary>
        /// <param name="sourceFolderPath">Full path of the folder to be deleted.</param>
        /// <param name="fileSearchPattern">You can specify a Windows style search pattern to limit which files are deleted. For example, *.* search pattern means delete all files. Delete all is the default.</param>
        /// <param name="includeSubDirs">If true, the folder plus all its subfolders will be deleted. If false, only the files in the source folder will be deleted.</param>
        /// <param name="continueOnError">If true, the application will attempt to continue if a folder delete operation failed because the source folder or file was unavailable. Default is to continue on error.</param>
        /// <param name="maxNumErrors">Maximum number of delete errors before the routine will terminate and throw an error message. Default is 5.</param>
        /// <returns>XDeleteInfo structure is returned. See <see cref="PFFileSystemObjects.XDeleteInfo"/> for information on contents of the structure.</returns>
        public static XDeleteInfo XDelete(string sourceFolderPath, string fileSearchPattern, bool includeSubDirs, bool continueOnError, int maxNumErrors)
        {
            XDeleteInfo sizeInfo = new XDeleteInfo();


            try
            {
                if (Directory.Exists(sourceFolderPath))
                {
                    DirectoryDelete(sourceFolderPath, fileSearchPattern, includeSubDirs, continueOnError, maxNumErrors, ref sizeInfo);
                }
                else
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourceFolderPath);
                }
            }
            catch (DirectoryNotFoundException)
            {
                sizeInfo.NumErrors++;
                _msg.Length = 0;
                _msg.Append("Directory not found: ");
                _msg.Append(sourceFolderPath);
                _msg.Append(".");
                _msg.Append(Environment.NewLine);
                throw new System.Exception(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(DirectoryDeleteErrorMessage(sourceFolderPath));
                _msg.Append(AppMessages.FormatErrorMessage(ex));
                if (returnErrorMessage != null)
                    returnErrorMessage(_msg.ToString());
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return sizeInfo;
        }

        private static string DirectoryDeleteErrorMessage(string sourceFolderPath)
        {
            _msg.Length = 0;
            _msg.Append("Error attempting to delete folder: ");
            _msg.Append(sourceFolderPath);
            _msg.Append(".");
            _msg.Append(Environment.NewLine);
            return _msg.ToString();
        }


        private static void DirectoryDelete(string sourceFolderPath, string fileSearchPattern, bool includeSubDirs, bool continueOnError, int maxNumErrors, ref XDeleteInfo sizeInfo)
        {
            try
            {
                // Get the subdirectories for the specified directory.
                DirectoryInfo dir = new DirectoryInfo(sourceFolderPath);
                DirectoryInfo[] dirs = dir.GetDirectories();

                if (dir.Exists == false)
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourceFolderPath);
                }

                string saveSearchPattern = String.IsNullOrEmpty(fileSearchPattern) ? "*.*" : fileSearchPattern;
                PFFileSpec fileSpec = new PFFileSpec(saveSearchPattern);

                try
                {
                    // Get the files in the directory and delete them
                    FileInfo[] files = dir.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        if (fileSpec.IsMatch(file.Name) || saveSearchPattern == "*.*")
                        {
                            File.SetAttributes(file.FullName, FileAttributes.Normal);
                            sizeInfo.NumFiles++;
                            sizeInfo.NumBytes += file.Length;
                            File.Delete(file.FullName);
                            //test
                            //FileInfo testfi = new FileInfo(file.FullName);
                            //if (testfi.Extension.ToLower() == ".tab")
                            //{
                            //    _msg.Length = 0;
                            //    _msg.Append("Test IOException for ");
                            //    _msg.Append(testfi.FullName);
                            //    _msg.Append(Environment.NewLine);
                            //    throw new IOException(_msg.ToString());
                            //}
                            //end test
                            if (returnXDeleteResult != null)
                            {
                                SourceInfo objectInfo = new SourceInfo();
                                objectInfo.sourcePath = file.FullName;
                                objectInfo.objectType = FileSystemObjectType.File;
                                objectInfo.sourceBytes = file.Length;
                                returnXDeleteResult(objectInfo, sizeInfo);
                            }
                        }
                        else
                        {
                            sizeInfo.NumFilesNotDeleted++;
                        }
                    }//end foreach



                    // If copying subdirectories, copy them and their contents to new location. 
                    if (includeSubDirs)
                    {
                        foreach (DirectoryInfo subdir in dirs)
                        {
                            DirectoryDelete(subdir.FullName, fileSearchPattern, includeSubDirs, continueOnError, maxNumErrors, ref sizeInfo);
                        }
                    }
                }
                catch (IOException ex)
                {
                    sizeInfo.NumErrors++;
                    sizeInfo.errorMessages += DirectoryDeleteErrorMessage(sourceFolderPath) + ":" + ex.Message;
                    if (sizeInfo.NumErrors > maxNumErrors || continueOnError == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Delete failed for ");
                        _msg.Append(sourceFolderPath);
                        _msg.Append(".");
                        _msg.Append(Environment.NewLine);
                        if (continueOnError == false)
                        {
                            _msg.Append(" ContinueOnError is set to false.");
                        }
                        else if (sizeInfo.NumErrors > maxNumErrors)
                        {
                            _msg.Append(" Maximum error count exceeded. Number of errors encountered: ");
                            _msg.Append(sizeInfo.NumErrors.ToString("#,##0"));
                        }
                        else
                        {
                            _msg.Append(" IO Exception during delete operation: ");
                        }
                        _msg.Append(ex.Message);
                        _msg.Append(Environment.NewLine);
                        if (returnErrorMessage != null)
                            returnErrorMessage(_msg.ToString());
                        throw new System.Exception(_msg.ToString());
                    }
                    else
                    {
                        if (returnErrorMessage != null)
                            returnErrorMessage( DirectoryDeleteErrorMessage(sourceFolderPath) + ":" + ex.Message);
                    }
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    ;
                }

                dir.Refresh();
                if (dir.GetFiles().Length == 0)
                {
                    try
                    {
                        Directory.Delete(sourceFolderPath);
                        sizeInfo.NumFolders++;
                    }
                    catch (IOException ex)
                    {
                        sizeInfo.NumErrors++;
                        _msg.Length = 0;
                        _msg.Append("Directory delete failed for ");
                        _msg.Append(sourceFolderPath);
                        _msg.Append(Environment.NewLine);
                        _msg.Append(AppMessages.FormatErrorMessage(ex));
                        sizeInfo.errorMessages += _msg.ToString();
                        if (sizeInfo.NumErrors > maxNumErrors || continueOnError == false)
                        {
                            _msg.Length = 0;
                            _msg.Append("Delete failed for ");
                            _msg.Append(sourceFolderPath);
                            _msg.Append(".");
                            _msg.Append(Environment.NewLine);
                            if (continueOnError == false)
                            {
                                _msg.Append(" ContinueOnError is set to false.");
                            }
                            else if (sizeInfo.NumErrors > maxNumErrors)
                            {
                                _msg.Append(" Maximum error count exceeded. Number of errors encountered: ");
                                _msg.Append(sizeInfo.NumErrors.ToString("#,##0"));
                            }
                            else
                            {
                                _msg.Append(" IO Exception during delete operation: ");
                            }
                            _msg.Append(ex.Message);
                            _msg.Append(Environment.NewLine);
                            if (returnErrorMessage != null)
                                returnErrorMessage(_msg.ToString());
                            throw new System.Exception(_msg.ToString());
                        }
                    }
                    catch (System.Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        ;
                    }
                }
                else
                {
                    sizeInfo.NumFoldersNotDeleted++;
                }

            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                ;
            }
        }//end method

        /// <summary>
        /// Method to delete files from the specified folder. All files in the folder are deleted. Subfolders and their files are not deleted.
        /// </summary>
        /// <param name="folderPath">Path of folder containing files to be deleted.</param>
        /// <returns>Total number of files that were deleted.</returns>
        public static int DeleteFiles(string folderPath)
        {
            return DeleteFiles(folderPath, "*.*", false);
        }
        
        /// <summary>
        /// Method to delete files from the specified folder. Subfolders are not searched.
        /// </summary>
        /// <param name="folderPath">Path of folder containing files to be deleted.</param>
        /// <param name="searchPattern">Mask to use when selecting which files to delete. Examples: *.* for all files; *.txt for files with .txt file extension.</param>
        /// <returns>Total number of files that were deleted.</returns>
        public static int DeleteFiles(string folderPath, string searchPattern)
        {
            return DeleteFiles(folderPath, searchPattern, false);
        }
        
        /// <summary>
        /// Method to delete files from the specified folder.
        /// </summary>
        /// <param name="folderPath">Path of folder containing files to be deleted.</param>
        /// <param name="searchPattern">Mask to use when selecting which files to delete. Examples: *.* for all files; *.txt for files with .txt file extension.</param>
        /// <param name="includeSubFolders">Recursively search the subfolder tree to delete files that match the searchPattern.</param>
        /// <returns>Total number of files that were deleted.</returns>
        public static int DeleteFiles(string folderPath, string searchPattern, bool includeSubFolders)
        {
            int numFilesDeleted = 0;
            string saveSearchPattern = String.IsNullOrEmpty(searchPattern) ? "*.*" : searchPattern;
            PFFileSpec fileSpec = new PFFileSpec(saveSearchPattern);


            try
            {
                if (String.IsNullOrEmpty(folderPath))
                {
                    throw new System.Exception("You must specify a folder path for PFDirectory DeleteFiles method.");
                }

                string[] filePaths = Directory.GetFiles(folderPath);
                if (filePaths.Length > 0)
                {
                    for (int inx = 0; inx < filePaths.Length; inx++)
                    {
                        if (fileSpec.IsMatch(filePaths[inx]) || saveSearchPattern == "*.*")
                        {
                            File.SetAttributes(filePaths[inx], FileAttributes.Normal);
                            File.Delete(filePaths[inx]);
                            numFilesDeleted++;
                            if (returnDeleteFilesResult != null)
                                returnDeleteFilesResult(filePaths[inx]);
                        }
                    }
                }

                if (includeSubFolders)
                {
                    string[] directoryPaths = Directory.GetDirectories(folderPath);
                    if (directoryPaths.Length > 0)
                    {
                        for (int inx = 0; inx < directoryPaths.Length; inx++)
                        {
                            numFilesDeleted += DeleteFiles(directoryPaths[inx], searchPattern, includeSubFolders);
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                if (returnErrorMessage != null)
                    returnErrorMessage(_msg.ToString());
                throw;
            }
            finally
            {
                ;
            }
        
            
            return numFilesDeleted;

        }


        /// <summary>
        /// Deletes all subfolders in the specified folderPath. Specified folderPath and any files in it will remain.
        /// </summary>
        /// <param name="folderPath">Path to folder to process.</param>
        /// <returns>Number of subfolders deleted.</returns>
        public static int DeleteSubFolders(string folderPath)
        {
            return DeleteSubFolders(folderPath, "*.*", false);
        }

        /// <summary>
        /// Deletes subfolders in the specified folderPath when subfolder name matches searchPattern mask.  Specified folderPath and any files in it will remain.
        /// </summary>
        /// <param name="folderPath">Path to folder to process.</param>
        /// <param name="searchPattern">Mask to specify which subfolders to delete. Specify *.* to specify all subfolders.</param>
        /// <returns>Number of directory entries deleted.</returns>
        public static int DeleteSubFolders(string folderPath, string searchPattern)
        {
            return DeleteSubFolders(folderPath, searchPattern, false);
        }

        /// <summary>
        /// Deletes subfolders in the specified folderPath. Specified folderPath will remain.
        /// </summary>
        /// <param name="folderPath">Path to folder to process.</param>
        /// ///<param name="includeFiles">If true, files in the specified folderPath are also deleted. If false, only the subfolders are deleted.</param>
        /// <returns>Number of directory entries deleted.</returns>
        public static int DeleteSubFolders(string folderPath, bool includeFiles)
        {
            return DeleteSubFolders(folderPath, "*.*", includeFiles);
        }
        
        /// <summary>
        /// Deletes subfolders in the specified folderPath when subfolder name matches searchPattern mask.  Specified folderPath will remain.
        /// </summary>
        /// <param name="folderPath">Path to folder to process.</param>
        /// <param name="searchPattern">Mask to specify which subfolders to delete. Specify *.* to specify all subfolders. Pattern is also used to determine which files to delete if includeFiles is set to true.</param>
        /// ///<param name="includeFiles">If true, files in the specified folderPath are also deleted. If false, only the subfolders are deleted.</param>
        /// <returns>Number of directory entries deleted.</returns>
        public static int DeleteSubFolders(string folderPath, string searchPattern, bool includeFiles)
        {
            int numEntriesDeleted = 0;
            string saveSearchPattern = String.IsNullOrEmpty(searchPattern) ? "*.*" : searchPattern;
            PFFileSpec fileSpec = new PFFileSpec(saveSearchPattern);


            try
            {
                if (String.IsNullOrEmpty(folderPath))
                {
                    throw new System.Exception("You must specify a folder path for PFDirectory DeleteFiles method.");
                }

                if (includeFiles)
                {
                    string[] filePaths = Directory.GetFiles(folderPath);
                    if (filePaths.Length > 0)
                    {
                        for (int inx = 0; inx < filePaths.Length; inx++)
                        {
                            if (fileSpec.IsMatch(filePaths[inx]) || saveSearchPattern == "*.*")
                            {
                                File.SetAttributes(filePaths[inx], FileAttributes.Normal);
                                File.Delete(filePaths[inx]);
                                numEntriesDeleted++;
                                if (returnDeleteSubfolderResult != null)
                                    returnDeleteSubfolderResult(FileSystemObjectType.File, filePaths[inx]);
                            }
                        }
                    }
                }

                string[] directoryPaths = Directory.GetDirectories(folderPath);
                if (directoryPaths.Length > 0)
                {
                    for (int inx = 0; inx < directoryPaths.Length; inx++)
                    {
                        numEntriesDeleted += DeleteSubFolders(directoryPaths[inx], searchPattern, includeFiles);
                    }
                }

                DirectoryInfo di = new DirectoryInfo(folderPath);
                if (di.GetFiles().Length == 0 && di.GetDirectories().Length == 0)
                {
                    Directory.Delete(folderPath, true);
                    numEntriesDeleted++;
                    if (returnDeleteSubfolderResult != null)
                        returnDeleteSubfolderResult(FileSystemObjectType.Directory, folderPath);
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                if (returnErrorMessage != null)
                    returnErrorMessage(_msg.ToString());
                throw;
            }
            finally
            {
                ;
            }
                 
        


            return numEntriesDeleted;
        }

        /// <summary>
        /// Determines whether or not the specified path contains legal Windows file path characters.
        /// </summary>
        /// <param name="pathName">Path to be validated.</param>
        /// <returns>True if path is a valid Windows path.</returns>

        public static bool IsValidPath(string pathName)
        {
            return PFFile.IsValidPath(pathName);
        }

        /// <summary>
        /// Method determines if two folder names are equal. If necessary, the method will append a backslash (\) to the folder names.
        /// </summary>
        /// <param name="folderName1">First folder name to compare.</param>
        /// <param name="folderName2">Second folder name to compare.</param>
        /// <returns>True if folder names are equal.</returns>
        /// <remarks>In general, the folder names should include the full paths to the folder.</remarks>
        public static bool FolderNamesAreEqual(string folderName1, string folderName2)
        {
            bool ret = false;
            string sFolderName1 = folderName1.EndsWith(@"\") ? folderName1 : folderName1 + @"\";
            string sFolderName2 = folderName2.EndsWith(@"\") ? folderName2 : folderName2 + @"\";

            if (sFolderName1 == sFolderName2)
                ret = true;

            return ret;
        }


    }//end class
}//end namespace
