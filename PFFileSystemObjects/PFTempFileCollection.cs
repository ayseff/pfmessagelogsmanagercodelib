//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.IO;

namespace PFFileSystemObjects
{
    /// <summary>
    /// Class is a thin wrapper for the TempFileCollection class of System.CodeDom.Compiler class.
    /// </summary>
    public class PFTempFileCollection
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private TempFileCollection _tempFileCollection = null;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFTempFileCollection()
        {
            _tempFileCollection = new TempFileCollection();
            _tempFileCollection.KeepFiles = false;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tempDirectory">A path to the temporary directory to use for storing the temporary files.</param>
        public PFTempFileCollection(string tempDirectory)
        {
            _tempFileCollection = new TempFileCollection(tempDirectory);
            _tempFileCollection.KeepFiles = false;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tempDirectory">A path to the temporary directory to use for storing the temporary files.</param>
        /// <param name="keepFiles">true if the temporary files should be kept after use; false if the temporary files should be deleted. </param>
        public PFTempFileCollection(string tempDirectory, bool keepFiles)
        {
            _tempFileCollection = new TempFileCollection(tempDirectory, keepFiles);
            _tempFileCollection.KeepFiles = keepFiles;
        }

        //properties

        /// <summary>
        /// Set to false to have temp files deleted when this instance is disposed.
        /// </summary>
        public bool KeepFiles
        {
            get
            {
                return _tempFileCollection.KeepFiles;
            }
            set
            {
                _tempFileCollection.KeepFiles = value;
            }
        }

        /// <summary>
        /// Number of files in this instance of the temp file collection.
        /// </summary>
        public int Count
        {
            get
            {
                return _tempFileCollection.Count;
            }
        }

        /// <summary>
        /// Gets the temporary directory to store the temporary files in.
        /// </summary>
        public string TempDirectory
        {
            get
            {
                return _tempFileCollection.TempDir;
            }
        }

        /// <summary>
        /// Gets the full path to the base file name, without a file name extension, on the temporary directory path, that is used to generate temporary file names for the collection.
        /// </summary>
        public string BasePath
        {
            get
            {
                return _tempFileCollection.BasePath;
            }
        }

        //methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public string AddExtension(string ext)
        {
            return _tempFileCollection.AddExtension(ext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="keepFile"></param>
        /// <returns></returns>
        public string AddExtension(string ext, bool keepFile)
        {
            return _tempFileCollection.AddExtension(ext, keepFile);
        }

        /// <summary>
        /// Adds the specified file to the collection, using the specified value indicating whether to keep the file after the collection is disposed or when the Delete method is called.
        /// </summary>
        /// <param name="fileName">The name of the file to add to the collection. </param>
        /// <param name="keepFile">true if the file should be kept after use; false if the file should be deleted.</param>
        public void AddFile(string fileName, bool keepFile)
        {
            _tempFileCollection.AddFile(fileName, keepFile);
        }
        /// <summary>
        /// The Delete method examines each file in the collection to determine, on an individual basis, whether the file is to be kept or deleted.
        ///  Files can be explicitly marked to be kept when added to the collection using add methods that take a keepFile parameter.
        ///  When adding a file to the collection using the AddExtension overload that does not have a keepFile parameter the value of the KeepFiles property is used as the default keep file indicator. 
        /// </summary>
        public void DeleteFiles()
        {
            _tempFileCollection.Delete();
        }

        /// <summary>
        /// Dispose method for the class.
        /// </summary>
        public void Dispose()
        {
            _tempFileCollection.Delete();
        }


    }//end class
}//end namespace
