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

namespace PFFileSystemObjects
{
    /// <summary>
    /// Class for creating and managing temporary files.
    /// </summary>
    public class PFTempFile : IDisposable 
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private string _tempFileName = string.Empty;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFTempFile()
        {
            _tempFileName = Path.GetTempFileName();
        }

        //properties

        /// <summary>
        /// Retrieves file name of temp file encapsulated by this instance of the class.
        /// </summary>
        public string TempFileName
        {
            get
            {
                return _tempFileName;
            }
        }

        //methods

        /// <summary>
        /// Deletes the temp file.
        /// </summary>
        public void DeleteFile()
        {
            File.Delete(_tempFileName);
        }

        /// <summary>
        /// Dispose method for the class. Will delete the temp file.
        /// </summary>
        public void Dispose()
        {
            File.Delete(_tempFileName); 
        }

    
    }//end class
}//end namespace
