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
using AppGlobals;
using PFFileSystemObjects;

namespace PFTextFiles
{

    /// <summary>
    /// Class to read and write data in a text file.
    /// </summary>
    /// <remarks>
    /// Open mode is one of the following:
    ///     DoNotOpenFile,
    ///     OpenFileForAppend
    ///     OpenFileToRead
    ///     OpenFileForWrite
    /// </remarks>
    /// <example>
    /// <code>
    /// PFTextFile textFile = new PFTextFile(fileName, PFFileOpenOperation.OpenFileForWrite);
    /// textFile.WriteLine("This is line 1.");
    /// textFile.WriteLine("This is line 2.");
    /// textFile.CloseFile();
    /// //Read the data just written
    /// textFile.OpenFile(fileName, PFFileOpenOperation.OpenFileToRead);
    /// while (textFile.Peek() >= 0)
    /// {
    ///     fileData = textFile.ReadLine();
    ///     Program._messageLog.WriteLine(fileData);
    /// }
    /// textFile.CloseFile();
    /// </code>
    /// </example>
        
    public class PFTextFile
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _formattedData = new StringBuilder();
        private StreamReader _sr = null;
        private StreamWriter _sw = null;
        private char[] _readBuffer = new char[2000];

        //private varialbles for properties
        private string _fileName = string.Empty;
        private int _readBufferSize = 2000;
        private bool _fileIsOpen = false;

        //constructors
        /// <summary>
        /// Constructor for PFTextClass. Specify path to file and type of i/o required.
        /// </summary>
        /// <param name="filePath">Full path to file to be opened in this instance.</param>
        /// <param name="fileOperation">Type of processing to be done on the file.</param>
        public PFTextFile(string filePath, PFFileOpenOperation fileOperation)
        {
            _fileName = filePath;
            if (fileOperation != PFFileOpenOperation.DoNotOpenFile)
                this.OpenFile(filePath, fileOperation);
        }

        /// <summary>
        /// Constructor for PFTextClass. Use OpenFile method to specify file and type of i/o.
        /// </summary>
        public PFTextFile()
        {
            ;
        }

        //properties
        /// <summary>
        /// Returns full path of file being processed by the instance.
        /// </summary>
        public string FileName
        {
            get
            {
                return this._fileName;
            }
        }

        /// <summary>
        /// Property to manually specify the read buffer for ReadData method.
        /// </summary>
        public int BufferSizeForReadData
        {
            get
            {
                return _readBufferSize;
            }
            set
            {
                _readBufferSize = value;
                _readBuffer = null;
                _readBuffer = new char[_readBufferSize];
            }
        }

        /// <summary>
        /// Reports whether or not the file is in an Open state.
        /// </summary>
        public bool FileIsOpen
        {
            get
            {
                return _fileIsOpen;
            }
            set
            {
                _fileIsOpen = value;
            }
        }


        /// <summary>
        /// Returns the number of bytes in the file being processed by the instance.
        /// </summary>
        public long Length
        {
            get
            {
                long numBytes = -1;
                if(this.FileName.Length>0)
                    numBytes = PFFile.GetFileSize(this.FileName);
                return numBytes;
            }
        }


        //methods

        /// <summary>
        /// Creates file and returns a PFTextFile instance for that file.
        /// </summary>
        /// <param name="filePath">Full pathname of file.</param>
        /// <returns>An instance of PFTextFile object.</returns>
        /// <remarks>If file already exists, it will be overwritten and left empty.</remarks>
        public static PFTextFile CreateFile(string filePath)
        {
            PFTextFile file;
            try
            {
                FileStream s = File.Create(filePath);
                s.Close();
                file = new PFTextFile(filePath, PFFileOpenOperation.DoNotOpenFile);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(AppMessages.FormatErrorMessage(ex));
            }

            return file;

        }

        /// <summary>
        /// Deletes file represented by an instance of PFTextFile.
        /// </summary>
        /// <param name="textFile">Instance of PFTextFile. Is nulled after file represented by the instance is deleted.</param>
        public static void DeleteFile(PFTextFile textFile)
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
        /// Opens a file for processing by this instance of PFTextFile.
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
                _fileName = filePath;
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
            _fileIsOpen = true;
        }


        private void OpenFileForAppend(string filePath)
        {
            _sw = new StreamWriter(filePath, true);
            _fileIsOpen = true;
        }

        private void OpenFileForWrite(string filePath)
        {
            _sw = new StreamWriter(filePath, true);
            _fileIsOpen = true;
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
                _fileIsOpen = false;
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
                _sw=null;
                File.WriteAllText(this.FileName,string.Empty);
                _sw = new StreamWriter(this.FileName,true);
            }

            if (_sr != null)
            {
                _msg.Length = 0;
                _msg.Append("ClearFile not allowed for a file opened for reading.");
                throw new System.Exception(_msg.ToString());
            }

        }

        /// <summary>
        /// Reads all the data in a file and returns it to the caller in a string.
        /// </summary>
        /// <returns>String containing all the data in the file.</returns>
        public string ReadAllText()
        {
            string fileData = string.Empty;

            _sr.BaseStream.Position = 0;
            fileData = _sr.ReadToEnd();

            return fileData;
        }

        /// <summary>
        /// Reads specified number of bytes from the file.
        /// </summary>
        /// <param name="numBytesToRead">Number of bytes to read each time file is read.</param>
        /// <returns>Data that was retrieved from the file.</returns>
        /// <remarks>This method works best when data is structured in a fixed length format.
        /// if actual number of bytes read does not match the number specified by numBytesToRead, an exception is thrown.
        ///  If numBytesToRead does not match the current value of BufferSizeForReadData, the buffer size will be adjusted to match the numBytesToRead.</remarks>

        public string ReadData(int numBytesToRead)
        {
            return ReadData(numBytesToRead, true);
        }

        /// <summary>
        /// Reads specified number of bytes from the file.
        /// </summary>
        /// <param name="numBytesToRead">Data that was retrieved from the file.</param>
        /// <param name="errorIfNumBytesMismatch">True/False.</param>
        /// <returns>Data that was retrieved from the file.</returns>
        /// <remarks> If true is specified for errorIfNumBytesMismatch parameter, the method throws an exception if number of bytes read do not match the number specified by numBytesToRead.
        ///  If numBytesToRead does not match the current value of BufferSizeForReadData, the buffer size will be adjusted to match the numBytesToRead.</remarks>
        public string ReadData(int numBytesToRead, bool errorIfNumBytesMismatch)
        {
            string data = null;
            int numBytes = -1;

            if (numBytesToRead != _readBufferSize)
            {
                _readBuffer = null;
                _readBuffer = new char[numBytesToRead];
                _readBufferSize = numBytesToRead;
            }
            if (_sr.Peek() >= 0)
            {
                numBytes = _sr.Read(_readBuffer, (int)0, numBytesToRead);
                if (errorIfNumBytesMismatch)
                {
                    if(numBytes!=numBytesToRead)
                    {
                        throw new System.Exception("Number of bytes read do not match expected number of bytes.");
                    }
                }
                data = new string(_readBuffer);
            }
            else
                data = null;

            return data;
        }

        /// <summary>
        /// Checks to see if there is any more data in the file.
        /// </summary>
        /// <returns>-1 if no data; 0 or more if there is data.</returns>
        /// ///<remarks>This method is using the Peek method of the StreamReader class.</remarks>
        public int Peek()
        {
            return _sr.Peek();
        }

        /// <summary>
        /// Reads data one line at a time.
        /// </summary>
        /// <returns>String containing the line.</returns>
        /// <remarks>StreamReader is used to retrieve data. A line is defined as a sequence of characters followed by a line feed ("\n"), a carriage return ("\r"), or a carriage return immediately followed by a line feed ("\r\n"). The string that is returned does not contain the terminating carriage return or line feed. The returned value is null if the end of the input stream is reached. </remarks>
        
        public string ReadLine()
        {
            string line = string.Empty;

            if (_sr.Peek() >= 0)
                line = _sr.ReadLine();
            else
                line = null;

            return line;
        }


        /// <summary>
        /// Writes data as a line. 
        /// </summary>
        /// <param name="line">Data to be written.</param>
        /// <remarks>Line terminator is appended to the data. StreamWriter.WriteLine is used.</remarks>
        public void WriteLine(string line)
        {

            try
            {
                _sw.WriteLine(line);
                _sw.Flush();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(AppMessages.FormatErrorMessage(ex));
            }

        }

        /// <summary>
        /// Data is written as is to the file. No line terminators added.
        /// </summary>
        /// <param name="data">Data to be written.</param>
        public void WriteData(string data)
        {

            try
            {
                _sw.Write(data);
                _sw.Flush();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(AppMessages.FormatErrorMessage(ex));
            }

        }

        /// <summary>
        /// Writes a blank line that only contains a line terminator.
        /// </summary>
        public void WriteBlankLine()
        {
            WriteBlankLine((int)0);
        }

        /// <summary>
        /// Writes out the number spaces specified and then terminates then with a line terminator.
        /// </summary>
        /// <param name="numSpacesToInsert"></param>
        public void WriteBlankLine(int numSpacesToInsert)
        {
            string spacesToInsert = string.Empty;

            try
            {
                _formattedData.Length = 0;
                if(numSpacesToInsert>0)
                {
                    spacesToInsert = spacesToInsert.PadRight(numSpacesToInsert);
                    _formattedData.Append(spacesToInsert);
                }
                _sw.WriteLine(_formattedData.ToString());
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(AppMessages.FormatErrorMessage(ex)); 
            }

        }



    }//end class
}//end namespace
