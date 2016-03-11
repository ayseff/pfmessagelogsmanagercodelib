//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2013
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AppGlobals;
using PFFileSystemObjects;

namespace PFTextFiles
{
    /// <summary>
    /// Class to define and output delimited data in text format. Use this class for creating text based extract files.
    /// </summary>
    public class PFDelimitedTextFile
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _formattedData = new StringBuilder();
        private StreamReader _sr = null;
        private StreamWriter _sw = null;

        //private varialbles for properties
        private PFDelimitedTextFileType _fileType = PFDelimitedTextFileType.Default;
        private string _fileName = string.Empty;

        //constructors

        //constructors
        /// <summary>
        /// Constructor for PFDelimitedTextClass. Specify path to file and type of i/o required.
        /// </summary>
        /// <param name="filePath">Full path to file to be opened in this instance.</param>
        /// <param name="fileOperation">Type of processing to be done on the file.</param>
        public PFDelimitedTextFile(string filePath, PFFileOpenOperation fileOperation)
        {
            _fileName = filePath;
            if (fileOperation != PFFileOpenOperation.DoNotOpenFile)
                this.OpenFile(filePath, fileOperation);
        }

        /// <summary>
        /// Constructor for PFTextClass. Use OpenFile method to specify file and type of i/o.
        /// </summary>
        public PFDelimitedTextFile()
        {
            ;
        }

        //properties
        /// <summary>
        /// Defines the type of delimited text file to be produced. See <see cref="PFDelimitedTextFileType"/> for description of types of delimited text files.
        /// </summary>
        public PFDelimitedTextFileType FileType
        {
            get
            {
                return _fileType;
            }
            set
            {
                _fileType = value;
            }
        }

        /// <summary>
        /// Returns full path of file being processed by the instance.
        /// </summary>
        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        /// <summary>
        /// Returns the number of bytes in the file being processed by the instance.
        /// </summary>
        public long FileLength
        {
            get
            {
                long numBytes = -1;
                if (this.FileName.Length > 0)
                    numBytes = PFFile.GetFileSize(this.FileName);
                return numBytes;
            }
        }


        
        //methods

        /// <summary>
        /// Creates file and returns a PFDelimitedTextFile instance for that file.
        /// </summary>
        /// <param name="filePath">Full pathname of file.</param>
        /// <returns>An instance of PFDelimitedTextFile object.</returns>
        /// <remarks>If file already exists, it will be overwritten and left empty.</remarks>
        public static PFDelimitedTextFile CreateFile(string filePath)
        {
            PFDelimitedTextFile file;
            try
            {
                FileStream s = File.Create(filePath);
                s.Close();
                file = new PFDelimitedTextFile(filePath, PFFileOpenOperation.DoNotOpenFile);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(AppMessages.FormatErrorMessage(ex));
            }

            return file;

        }

        /// <summary>
        /// Deletes file represented by an instance of PFDelimitedTextFile.
        /// </summary>
        /// <param name="textFile">Instance of PFDelimitedTextFile. Is nulled after file represented by the instance is deleted.</param>
        public static void DeleteFile(PFDelimitedTextFile textFile)
        {
            try
            {
                if (textFile._sr != null || textFile._sw != null)
                {
                    textFile.CloseFile();
                }
                File.Delete(textFile.FileName);
                textFile = null;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(AppMessages.FormatErrorMessage(ex));
            }


        }

        /// <summary>
        /// Opens a file for processing by this instance of PFDelimitedTextFile.
        /// </summary>
        /// <param name="filePath">Full path to file to be opened.</param>
        /// <param name="fileOperation">Read/Write/Append/DoNotOpen</param>
        /// <remarks>Explicitly close any file opened by this instance before opening a new file. Exception will be thrown if instance is already processing a file.</remarks>
        public void OpenFile(string filePath, PFFileOpenOperation fileOperation)
        {


            try
            {
                if (_sr != null || _sw != null)
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid OpenFile request. Instance already processing a file: ");
                    _msg.Append(this.FileName);
                    _msg.Append(". Close current file before opening a new file.");
                    throw new System.Exception(_msg.ToString());
                }
                switch (fileOperation)
                {
                    case PFFileOpenOperation.OpenFileToRead:
                        OpenFileForRead(filePath);
                        break;
                    case PFFileOpenOperation.OpenFileForAppend:
                        OpenFileForAppend(filePath);
                        break;
                    case PFFileOpenOperation.OpenFileForWrite:
                        OpenFileForWrite(filePath);
                        break;
                    case PFFileOpenOperation.DoNotOpenFile:
                        //ignore
                        break;
                    default:
                        _msg.Length = 0;
                        _msg.Append("Invalid PFFileOpenOperatin value: ");
                        _msg.Append(fileOperation.ToString());
                        throw new System.Exception(_msg.ToString());
                    //break;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                ;
            }


        }

        private void OpenFileForRead(string filePath)
        {
            _sr = new StreamReader(filePath);
        }


        private void OpenFileForAppend(string filePath)
        {
            _sw = new StreamWriter(filePath, true);
        }

        private void OpenFileForWrite(string filePath)
        {
            _sw = new StreamWriter(filePath, true);
            this.ClearFile();
        }

        /// <summary>
        /// Closes the file represented by this instance.
        /// </summary>
        public void CloseFile()
        {

            try
            {
                if (_sr != null)
                {
                    _sr.Close();
                    _sr = null;
                }
                if (_sw != null)
                {
                    _sw.Close();
                    _sw = null;
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to close file: ");
                _msg.Append(this.FileName);
                _msg.Append("  ");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }


        }

        /// <summary>
        /// Deletes all data in the file. File is left open but has no data.
        /// </summary>
        public void ClearFile()
        {
            if (_sw != null)
            {
                _sw.Close();
                _sw = null;
                File.WriteAllText(this.FileName, string.Empty);
                _sw = new StreamWriter(this.FileName, true);
            }

            if (_sr != null)
            {
                _msg.Length = 0;
                _msg.Append("ClearFile not allowed for a file opened for reading.");
                throw new System.Exception(_msg.ToString());
            }

        }



    }//end class
}//end namespace
