using System;
namespace PFFileSystemObjects
{
#pragma warning disable 1591

    public struct DirSizeInfo
    {
        public long NumFolders;
        public long NumFiles;
        public long NumBytes;
        public int NumErrors;
        public string errorMessages;
    }

    public struct XCopyInfo
    {
        public long NumFolders;
        public long NumFiles;
        public long NumBytes;
        public long NumFolderOverwrites;
        public long NumFileOverwrites;
        public int NumErrors;
        public string errorMessages;
    }

    public struct XDeleteInfo
    {
        public long NumFolders;
        public long NumFiles;
        public long NumBytes;
        public int NumErrors;
        public long NumFoldersNotDeleted;
        public long NumFilesNotDeleted;
        public string errorMessages;
    }

    public enum FileSystemObjectType
    {
        Unknown = 0,
        Directory = 1,
        File = 2
    }

    public struct SourceAndDestinationInfo
    {
        public string sourcePath;
        public string destinationPath;
        public FileSystemObjectType objectType;
        public long sourceBytes;
        public long destinationBytes;
    }

    public struct SourceInfo
    {
        public string sourcePath;
        public FileSystemObjectType objectType;
        public long sourceBytes;
    }


#pragma warning restore 1591

}//end namespace