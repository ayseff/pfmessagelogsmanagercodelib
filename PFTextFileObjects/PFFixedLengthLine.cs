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
using System.IO;

namespace PFTextFiles
{
    /// <summary>
    /// Contains definition for a fixed length text output line.
    /// </summary>
    public class PFFixedLengthDataLine
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _out = new StringBuilder();
        private int _minColLen = 1;

        //private varialbles for properties
        private bool _useLineTerminator = false;
        private bool _columnNamesOnFirstLine = false;
        private bool _allowDataTruncation = false;
        private string _lineTerminator = string.Empty;
        private int _numberOfColumns = 0;
        private int _maxColumnLengthOverride = 1024;
        private int _defaultStringColumnLength = 255;
        private PFColumnDefinitions _columnDefinitions = null;
        [NonSerialized]
        private PFColumnData _columnData = null;

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        internal PFFixedLengthDataLine()
        {
            ;
        }

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        public PFFixedLengthDataLine(int numberOfColumns)
        {
            this.NumberOfColumns = numberOfColumns;
            _columnDefinitions = new PFColumnDefinitions(numberOfColumns);
            _columnData = new PFColumnData(_columnDefinitions);
        }

        //properties

        /// <summary>
        /// Character(s) placed at end of line to force new line in output.
        /// </summary>
        /// <remarks>Uses .NET Environment.NewLine constant unless overriden later by setting the LineTerminator property.</remarks>
        public bool UseLineTerminator
        {
            get
            {
                return _useLineTerminator;
            }
            set
            {
                _useLineTerminator = value;
                if (_useLineTerminator)
                    _lineTerminator = Environment.NewLine;
                else
                    _lineTerminator = string.Empty;
            }
        }


        /// <summary>
        /// Specifies whether or not the first line of the output contains the column names.
        /// </summary>
        public bool ColumnNamesOnFirstLine
        {
            get
            {
                return _columnNamesOnFirstLine;
            }
            set
            {
                _columnNamesOnFirstLine = value;
            }
        }

        /// <summary>
        /// Determines whether or not a exception is thrown if the data for a column is longer than the column's defined length.
        /// </summary>
        /// <remarks>If false, an exception will be throw if data longer than extract column's length. Otherwise, data will be truncated before being included in the output.</remarks>
        public bool AllowDataTruncation
        {
            get
            {
                return _allowDataTruncation;
            }
            set
            {
                _allowDataTruncation = value;
            }
        }

        /// <summary>
        /// Sets the string to use as a line terminator. Must also specify UseLineTermnator property to True.
        /// </summary>
        public string LineTerminator
        {
            get
            {
                return _lineTerminator;
            }
            set
            {
                _lineTerminator = value;
            }
        }

        /// <summary>
        /// Specifies the number of columns into which the output will be separated.
        /// </summary>
        /// <remarks>Whenever this value is set, the existing ColumnDefinitions and ColumnData properties will be overwritten and set to the new number of columns. 
        ///  Any previously stored column definitions or data will be lost.
        /// </remarks>
        public int NumberOfColumns
        {
            get
            {
                return _numberOfColumns;
            }
            set
            {
                _numberOfColumns = value;
                _columnDefinitions = null;
                _columnDefinitions = new PFColumnDefinitions(_numberOfColumns);
                _columnData = null;
                _columnData = new PFColumnData(_columnDefinitions);
            }
        }

        /// <summary>
        /// Use this property to specify a maximum length for any column. This will override the maximum length contained in the data object if that length is greater than this override length.
        /// </summary>
        public int MaxColumnLengthOverride
        {
            get
            {
                return _maxColumnLengthOverride;
            }
            set
            {
                _maxColumnLengthOverride = value;
            }
        }

        /// <summary>
        /// Use this property to specify a length for any string column that has its length defined as less than 1.
        /// </summary>
        /// <remarks>This property only applies in cases where the length for a string in a data table is -1 and the calling routine has not overriden the -1 when creating the column definitions.</remarks>
        public int DefaultStringColumnLength
        {
            get
            {
                return _defaultStringColumnLength;
            }
            set
            {
                _defaultStringColumnLength = value;
            }
        }

        /// <summary>
        /// Object containing the definitions for each column in the delimited line.
        /// </summary>
        public PFColumnDefinitions ColumnDefinitions
        {
            get
            {
                return _columnDefinitions;
            }
            set
            {
                _columnDefinitions = value;
            }
        }

        /// <summary>
        /// Object containg the data on the delimited line.
        /// </summary>
        /// <remarks>Use this object where writing out or reading in data.</remarks>
        [System.Xml.Serialization.XmlIgnore()]
        public PFColumnData ColumnData
        {
            get
            {
                return _columnData;
            }
            set
            {
                _columnData = value;
            }
        }

        /// <summary>
        /// Returns the width in bytes of the data row as defined by this instance of the class.
        /// </summary>
        public int LineLength
        {
            get
            {
                int numBytes = 0;
                for (int i = 0; i < this.ColumnDefinitions.ColumnDefinition.Length; i++)
                {
                    if (this.ColumnDefinitions.ColumnDefinition[i].ColumnName.Length > 0)
                        numBytes += this.ColumnDefinitions.ColumnDefinition[i].ColumnLength;
                }
                if (this.UseLineTerminator)
                {
                    numBytes += _lineTerminator.Length;
                }
                return numBytes;
            }
        }

        //methods

        /// <summary>
        /// Method to specify column definition for the current instance.
        /// </summary>
        /// <param name="columnIndex">0-based index of the column being defined.</param>
        /// <param name="columnName">Name to assign to the column. This will become the column header for the output.</param>
        /// <param name="columnLength">Length in bytes of the column. </param>
        /// <remarks>If columnName is longer than columnLength, the columnName value will be truncated to columnLength.
        ///  Default for alignment with be PFDataAlign.LeftJustify.</remarks>
        public void SetColumnDefinition(int columnIndex,
                                        string columnName,
                                        int columnLength)
        {
            SetColumnDefinition(columnIndex,
                                columnName,
                                columnLength,
                                PFDataAlign.LeftJustify);
        }

        /// <summary>
        /// Method to specify column definitions for the current instance.
        /// </summary>
        /// <param name="columnIndex">0-based index of the column being defined.</param>
        /// <param name="columnName">Name to assign to the column. This will become the column header for the output.</param>
        /// <param name="columnLength">Length in bytes of the column. </param>
        /// <param name="columnDataAlignment">See <see cref="PFDataAlign"/> enumeration.</param>
        /// <remarks>If columnName is longer than columnLength, the columnName value will be truncated to columnLength.</remarks>
        public void SetColumnDefinition(int columnIndex,
                                          string columnName,
                                          int columnLength,
                                          PFDataAlign columnDataAlignment)
        {
            string colName = columnName;
            string newColName = string.Empty;
            int colLen = columnLength>this.MaxColumnLengthOverride ? this.MaxColumnLengthOverride : columnLength;
            if (colLen < _minColLen)
                colLen = this.DefaultStringColumnLength;
            if (colName.Length > colLen)
            {
                //newColName = "F" + columnIndex.ToString() + new String(' ', colLen);
                //colName = newColName.Substring(0, colLen).Trim();
                if (this.AllowDataTruncation == true)
                {
                    colName = colName.Substring(0, colLen);
                }
                else
                {
                    newColName = "F" + columnIndex.ToString() + new String(' ', colLen);
                    colName = newColName.Substring(0, colLen).Trim();
                    ////data truncation not allowed
                    //_msg.Length = 0;
                    //_msg.Append("Truncation error for column heading ");
                    //_msg.Append(columnName);
                    //_msg.Append(". Heading has length of ");
                    //_msg.Append(colName.Length.ToString());
                    //_msg.Append(", which is longer than allowed length of ");
                    //_msg.Append(colLen);
                    //_msg.Append(".");
                    //throw new System.ArgumentException(_msg.ToString());
                }
            }

            this.ColumnDefinitions.ColumnDefinition[columnIndex].ColumnName = colName;
            this.ColumnDefinitions.ColumnDefinition[columnIndex].ColumnLength = colLen;
            this.ColumnDefinitions.ColumnDefinition[columnIndex].ColumnDataAlignment = columnDataAlignment;
        }

        /// <summary>
        /// Sets the value of the column at the specified column index.
        /// </summary>
        /// <param name="columnInx"> 0-based index.</param>
        /// <param name="value">Data value for column at specified index.</param>
        public void SetColumnData(int columnInx, string value)
        {
            string val = value;
            if (value.Length > this.ColumnDefinitions.ColumnDefinition[columnInx].ColumnLength)
            {
                if (this.AllowDataTruncation == true)
                {
                    val = value.Substring(0, this.ColumnDefinitions.ColumnDefinition[columnInx].ColumnLength);
                }
                else
                {
                    //trucation error
                    _msg.Length = 0;
                    _msg.Append("Truncation error for ");
                    _msg.Append(this.ColumnDefinitions.ColumnDefinition[columnInx].ColumnName);
                    _msg.Append(". Value ");
                    _msg.Append(value);
                    _msg.Append(" has length of ");
                    _msg.Append(value.Length.ToString());
                    _msg.Append(", which is longer than allowed length of ");
                    _msg.Append(this.ColumnDefinitions.ColumnDefinition[columnInx].ColumnLength.ToString());
                    _msg.Append(".");
                    throw new System.ArgumentException(_msg.ToString());
                }
            }
            this.ColumnData.ColumnDataValue[columnInx].Data = val;
        }

        /// <summary>
        /// Array of values for the current line.
        /// </summary>
        /// <param name="values">String array. Length of array must match number of columns defined for this instance of the line.</param>
        public void SetColumnData(string[] values)
        {
            string val = string.Empty;

            for (int i = 0; i < this.NumberOfColumns; i++)
            {
                val = values[i];
                if (values[i].Length > this.ColumnDefinitions.ColumnDefinition[i].ColumnLength)
                {
                    if (this.AllowDataTruncation == true)
                    {
                        val = values[i].Substring(0, this.ColumnDefinitions.ColumnDefinition[i].ColumnLength);
                    }
                    else
                    {
                        //truncation error
                        _msg.Length = 0;
                        _msg.Append("Truncation error for ");
                        _msg.Append(this.ColumnDefinitions.ColumnDefinition[i].ColumnName);
                        _msg.Append(". Value ");
                        _msg.Append(values[i]);
                        _msg.Append(" has length of ");
                        _msg.Append(values[i].Length.ToString());
                        _msg.Append(", which is longer than allowed length of ");
                        _msg.Append(this.ColumnDefinitions.ColumnDefinition[i].ColumnLength.ToString());
                        _msg.Append(".");
                        throw new System.ArgumentException(_msg.ToString());
                    }
                }
                this.ColumnData.ColumnDataValue[i].Data = val;
            }
        }

        /// <summary>
        /// Method checks in input data matches the list of column names for this instance of the data row.
        /// </summary>
        /// <param name="input">Data line containing values.</param>
        /// <remarks>Thows exception if line values do not match the column names defined for this instance.</remarks>
        public void VerifyColumnNames(string input)
        {
            if (input != null)
            {
                VerifyLineLength(input);

                string[] colNames = new string[this.ColumnDefinitions.ColumnDefinition.Length];
                int start = 0;
                int len = 0;
                for (int i = 0; i < colNames.Length; i++)
                {
                    len = this.ColumnDefinitions.ColumnDefinition[i].ColumnLength;
                    colNames[i] = input.Substring(start, len).Trim();
                    start += len;
                }

                for (int i = 0; i < colNames.Length; i++)
                {
                    if (i < this.ColumnDefinitions.ColumnDefinition.Length)
                    {
                        if (this.ColumnDefinitions.ColumnDefinition[i].ColumnName != colNames[i])
                        {
                            _msg.Length = 0;
                            _msg.Append("Invalid column name: ");
                            _msg.Append(colNames[i]);
                            _msg.Append(" found instead of expected ");
                            _msg.Append(this.ColumnDefinitions.ColumnDefinition[i].ColumnName);
                            _msg.Append(".");
                            throw new System.Exception(_msg.ToString());
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Verifies length of the data in the input parameter is exactly the same as the LineLength property.
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>Exception thrown if input length does not match LineLength.</remarks>
        public void VerifyLineLength(string input)
        {

            if (input.Length != this.LineLength)
            {
                _msg.Length = 0;
                _msg.Append("Unexpected or invalid line length. Actual length: ");
                _msg.Append(input.Length.ToString());
                _msg.Append(" Expected length: ");
                _msg.Append(this.LineLength.ToString());
                _msg.Append(".");
                throw new System.Exception(_msg.ToString());
            }

        }

        /// <summary>
        /// Method to break up a line data into each column defned for this instance.
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>Data can be accessed using ColumnData property after it is parsed.</remarks>
        public void ParseData(string input)
        {
            if (input != null)
            {
                VerifyLineLength(input);

                int start = 0;
                int len = 0;
                for (int i = 0; i < this.ColumnDefinitions.ColumnDefinition.Length; i++)
                {
                    len = this.ColumnDefinitions.ColumnDefinition[i].ColumnLength;
                    this.ColumnData.ColumnDataValue[i].Data = input.Substring(start, len);
                    start += len;
                }
            }

        }


        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFFixedLengthDataLine));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFFixedLengthLine.</returns>
        public static PFFixedLengthDataLine LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFFixedLengthDataLine));
            TextReader textReader = new StreamReader(filePath);
            PFFixedLengthDataLine columnDefinitions;
            columnDefinitions = (PFFixedLengthDataLine)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }


        /// <summary>
        /// Creates a string containing the column names for this instance.
        /// </summary>
        /// <returns>String containing formatted column names.</returns>
        /// <remarks>Use this method to format column names before writing them to an output file.</remarks>
        public string OutputColumnNames()
        {
            _out.Length = 0;

            for (int inx = 0; inx < _numberOfColumns; inx++)
            {
                if (_columnDefinitions.ColumnDefinition[inx].ColumnName.Length >= _columnDefinitions.ColumnDefinition[inx].ColumnLength)
                {
                    _out.Append(_columnDefinitions.ColumnDefinition[inx].ColumnName.Substring(0, _columnDefinitions.ColumnDefinition[inx].ColumnLength));
                }
                else
                {
                    //padding will be required
                    string fmat = "{0," + (_columnDefinitions.ColumnDefinition[inx].ColumnLength*(int)_columnDefinitions.ColumnDefinition[inx].ColumnDataAlignment).ToString() + "}";
                    _out.Append(string.Format(fmat, _columnDefinitions.ColumnDefinition[inx].ColumnName));
                }

            }

            if(this.UseLineTerminator)
                _out.Append(_lineTerminator);

            return _out.ToString();
        }

        /// <summary>
        /// Creates string containing data to output.
        /// </summary>
        /// <returns>String containing formatted data.</returns>
        /// <remarks>Use this method to format data for output one line at a time.</remarks>
        public string OutputColumnData()
        {

            _out.Length = 0;

            for (int inx = 0; inx < _numberOfColumns; inx++)
            {
                if (_columnData.ColumnDataValue[inx].Data.Length >= _columnDefinitions.ColumnDefinition[inx].ColumnLength)
                {
                    _out.Append(_columnData.ColumnDataValue[inx].Data.Substring(0, _columnDefinitions.ColumnDefinition[inx].ColumnLength));
                }
                else
                {
                    //padding will be required
                    string fmat = "{0," + (_columnDefinitions.ColumnDefinition[inx].ColumnLength * (int)_columnDefinitions.ColumnDefinition[inx].ColumnDataAlignment).ToString() + "}";
                    _out.Append(string.Format(fmat, _columnData.ColumnDataValue[inx].Data));
                }

                //_out.Append(_columnData.ColumnDataValue[inx].Data);
            }

            if(this.UseLineTerminator)
                _out.Append(_lineTerminator);

            return _out.ToString();

        }


        //class helpers

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

                //if (prop.Name == "TestStringArray")
                //{
                //    for (int i = 0; i < this.TestStringArray.Length; i++)
                //    {
                //        data.Append("Index ");
                //        data.Append(i.ToString("#,##0"));
                //        data.Append(": ");
                //        data.Append(this.TestStringArray[i].ToString());
                //        data.Append("  ");
                //    }
                //}

            }

            return data.ToString();
        }

        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFFixedLengthDataLine));
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

        //Methods for determining maximum lengths to allow for numeric types

        /// <summary>
        /// Routine to determine maximum length to allow for a numeric data type.
        /// </summary>
        /// <param name="sysType">System.Type to be evaluated.</param>
        /// <returns>Maximum length.</returns>
        /// <remarks>This routien is intended to get maximum lengths for numeric data types. Return value is -1 for all other types.</remarks>
        public static int GetNumericTypeMaxExtractLength(System.Type sysType)
        {
            int typeExtractLength = -1;

            if (sysType.FullName == "System.Int32")
                typeExtractLength = 11;
            else if (sysType.FullName == "System.UInt32")
                typeExtractLength = 10;
            else if (sysType.FullName == "System.Int64")
                typeExtractLength = 20;
            else if (sysType.FullName == "System.UInt64")
                typeExtractLength = 20;
            else if (sysType.FullName == "System.Int16")
                typeExtractLength = 6;
            else if (sysType.FullName == "System.UInt16")
                typeExtractLength = 5;
            else if (sysType.FullName == "System.Double")
                typeExtractLength = 22;
            else if (sysType.FullName == "System.Single")
                typeExtractLength = 13;
            else if (sysType.FullName == "System.Decimal")
                typeExtractLength = 30;
            else if (sysType.FullName == "System.Char")
                typeExtractLength = 1;
            else if (sysType.FullName == "System.Byte")
                typeExtractLength = 3;
            else if (sysType.FullName == "System.SByte")
                typeExtractLength = 4;
            else if (sysType.FullName == "System.Boolean")
                typeExtractLength = 5;
            else
                typeExtractLength = -1;

            return typeExtractLength;
        }

        /// <summary>
        /// Routine to determine maximum length to allow for a DateTime data type.
        /// </summary>
        /// <param name="sysType">System.Type to be evaluated.</param>
        /// <returns>Maximum length.</returns>
        /// <remarks>This routien is intended to get maximum lengths for DateTime data type. Return value is -1 for all other types.</remarks>
        public static int GetDateTimeTypeMaxExtractLength(System.Type sysType)
        {
            int typeExtractLength = -1;

            if (sysType.FullName == "System.DateTime")
                typeExtractLength = 26;
            else
                typeExtractLength = -1;

            return typeExtractLength;
        }

        /// <summary>
        /// Routine to determine maximum length of numeric datatypes.
        /// </summary>
        /// <param name="obj">A object of the type to be evaluated.</param>
        /// <returns>Maximum length.</returns>
        /// <remak>This routine is primarily designed to get maximum lengths for objects that are numeric. Routine returns length of the string if object is a string. If not numeric and not a string (e.g. a struct or class), -1 is returned for the maximum length.</remak>
        public static int GetObjectMaxExtractLength(object obj)
        {
            int objExtractLength = -1;

            if (obj.GetType().FullName == "System.String")
            {
                string str = (string)obj;
                objExtractLength = str.Length;
            }
            else if (obj.GetType().FullName == "System.DateTime")
            {
                objExtractLength = GetDateTimeTypeMaxExtractLength(obj.GetType());
            }
            else if (obj.GetType().FullName.StartsWith("System."))
            {
                objExtractLength = GetNumericTypeMaxExtractLength(obj.GetType());
            }
            else
            {
                objExtractLength = -1;
            }



            return objExtractLength;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is numeric.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a numeric data type; otherwise false.</returns>
        public static bool DataTypeIsNumeric(System.Type sysType)
        {
            bool typeIsNumeric = false;

            if (sysType.FullName == "System.Int32")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.UInt32")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.Int64")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.UInt64")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.Int16")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.UInt16")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.Double")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.Single")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.Decimal")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.Char")
                typeIsNumeric = false;
            else if (sysType.FullName == "System.Byte")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.SByte")
                typeIsNumeric = true;
            else if (sysType.FullName == "System.Boolean")
                typeIsNumeric = false;
            else
                typeIsNumeric = false;
             
           return typeIsNumeric;
        }

        /// <summary>
        /// Evaluates whether or not the specified system type is a DateTime.
        /// </summary>
        /// <param name="sysType">Type to be evaluated.</param>
        /// <returns>Returns true if the specified type is a DateTime data type; otherwise false.</returns>
        public static bool DataTypeIsDateTime(System.Type sysType)
        {
            bool typeIsDateTime = false;

            if (sysType.FullName == "System.DateTime")
                typeIsDateTime = true;
            else
                typeIsDateTime = false;

            return typeIsDateTime;
        }
    }//end class
}//end namespace
