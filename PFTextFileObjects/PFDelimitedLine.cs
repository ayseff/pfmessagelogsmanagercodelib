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
    /// Class for containing definition and data for a text file line where data is separated by delimiters.
    /// </summary>
    public class PFDelimitedDataLine
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _out = new StringBuilder();
        private long _numLinesInOutput = 0;

        private delegate void ParseLineDelegate(string line);
        private ParseLineDelegate[] _parseLine = new ParseLineDelegate[2];
        private int _parseLineNoQuotesInx = 0;
        private int _parseLineWithQuotesInx = 1;
        private int _parseLineInx = 0;

        private delegate void SetColumnDataDelegate(int columnInx, string value);
        private SetColumnDataDelegate[] _setColumnData = new SetColumnDataDelegate[2];
        private int _setColumnDataNoQuotesInx = 0;
        private int _setColumnDataWithQuotesInx = 1;
        private int _setColumnDataInx = 0;

        private delegate void SetColumnDataArrayDelegate(string[] values);
        private SetColumnDataArrayDelegate[] _setColumnDataArray = new SetColumnDataArrayDelegate[2];
        private int _setColumnDataArrayNoQuotesInx = 0;
        private int _setColumnDataArrayWithQuotesInx = 1;
        private int _setColumnDataArrayInx = 0;

        //private varialbles for properties
        private string _lineTerminator = "\r\n";
        private bool _columnNamesOnFirstLine = false;
        private string[] _columnSeparator = new string[1] { "," };
        private int _numberOfColumns = 0;
        private bool _stringValuesSurroundedWithQuotationMarks = false;
        PFColumnDefinitions _columnDefinitions = null;
        [NonSerialized]
        PFColumnData _columnData = null;

        //constructors

        /// <summary>
        /// Constructor used for serialization operations.
        /// </summary>
        internal PFDelimitedDataLine()
        {
            ;
        }

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        public PFDelimitedDataLine(int numberOfColumns)
        {
            this.NumberOfColumns = numberOfColumns;
            _columnDefinitions = new PFColumnDefinitions(numberOfColumns);
            _columnData = new PFColumnData(_columnDefinitions);
            SetParseDelegates();
        }

        private void SetParseDelegates()
        {
            _parseLine[_parseLineNoQuotesInx] = new ParseLineDelegate(ParseDataNoDoubleQuotes);
            _parseLine[_parseLineWithQuotesInx] = new ParseLineDelegate(ParseDataContainingDoubleQuotes);
            if (this.StringValuesSurroundedWithQuotationMarks)
                _parseLineInx = _parseLineWithQuotesInx;
            else
                _parseLineInx = _parseLineNoQuotesInx;

            _setColumnData[_setColumnDataNoQuotesInx] = new SetColumnDataDelegate(SetColumnDataNoQuotes);
            _setColumnData[_setColumnDataWithQuotesInx] = new SetColumnDataDelegate(SetColumnDataWithQuotes);
            if (this.StringValuesSurroundedWithQuotationMarks)
                _setColumnDataInx = _setColumnDataWithQuotesInx;
            else
                _setColumnDataInx = _setColumnDataNoQuotesInx;

            _setColumnDataArray[_setColumnDataArrayNoQuotesInx] = new SetColumnDataArrayDelegate(SetColumnDataNoQuotes);
            _setColumnDataArray[_setColumnDataArrayWithQuotesInx] = new SetColumnDataArrayDelegate(SetColumnDataWithQuotes);
            if (this.StringValuesSurroundedWithQuotationMarks)
                _setColumnDataArrayInx = _setColumnDataArrayWithQuotesInx;
            else
                _setColumnDataArrayInx = _setColumnDataArrayNoQuotesInx;

        }

        //properties

        /// <summary>
        /// Character(s) placed at end of line to force new line in output.
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
        /// One or more characters serving as a separator between columns of data in the output.
        /// </summary>
        public string ColumnSeparator
        {
            get
            {
                return _columnSeparator[0];
            }
            set
            {
                _columnSeparator[0] = value;
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
        /// If true, then all string values on the line are enclosed within double quotes.
        /// </summary>
        public bool StringValuesSurroundedWithQuotationMarks
        {
            get
            {
                return _stringValuesSurroundedWithQuotationMarks;
            }
            set
            {
                _stringValuesSurroundedWithQuotationMarks = value;
                SetParseDelegates();
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



        //methods

        /// <summary>
        /// Method to specify column definitions for the current instance.
        /// </summary>
        /// <param name="columnIndex">0-based index of the column being defined.</param>
        /// <param name="columnName">Name to assign to the column. This will become the column header for the output.</param>
        /// <remarks>There is no limit on the size of the column name or the size of the data.</remarks>
        public void SetColumnDefinition(int columnIndex,
                                        string columnName)
        {
            SetColumnDefinition(columnIndex,
                                columnName,
                                (int)9999999);
        }

        /// <summary>
        /// Method to specify column definitions for the current instance.
        /// </summary>
        /// <param name="columnIndex">0-based index of the column being defined.</param>
        /// <param name="columnName">Name to assign to the column. This will become the column header for the output.</param>
        /// <param name="maxColumnLength">Maximum number of bytes this the data in this column can have. Size of the data can be from 0 to maxColumnLength. There is no limit on the size of the column name.</param>
        public void SetColumnDefinition(int columnIndex,
                                          string columnName,
                                          int maxColumnLength)
        {
            this.ColumnDefinitions.ColumnDefinition[columnIndex].ColumnName=columnName;
            this.ColumnDefinitions.ColumnDefinition[columnIndex].ColumnLength = maxColumnLength;
            this.ColumnDefinitions.ColumnDefinition[columnIndex].ColumnDataAlignment = PFDataAlign.LeftJustify;
        }

        /// <summary>
        /// Sets the value of the column at the specified column index.
        /// </summary>
        /// <param name="columnInx"> 0-based index.</param>
        /// <param name="value">Data value for column at specified index.</param>
        public void SetColumnData(int columnInx, string value)
        {
            _setColumnData[_setColumnDataInx](columnInx, value);

            //this.ColumnData.ColumnDataValue[columnInx].Data = value;
        }

        /// <summary>
        /// Sets the value of the column at the specified column index.
        /// </summary>
        /// <param name="columnInx"> 0-based index.</param>
        /// <param name="value">Data value for column at specified index.</param>
        public void SetColumnDataNoQuotes(int columnInx, string value)
        {
            this.ColumnData.ColumnDataValue[columnInx].Data = value;
        }

        /// <summary>
        /// Sets the value of the column at the specified column index.
        /// </summary>
        /// <param name="columnInx"> 0-based index.</param>
        /// <param name="value">Data value for column at specified index.</param>
        public void SetColumnDataWithQuotes(int columnInx, string value)
        {
            this.ColumnData.ColumnDataValue[columnInx].Data = "\"" + value + "\"";
        }

        /// <summary>
        /// Array of values for the current line.
        /// </summary>
        /// <param name="values">String array. Length of array must match number of columns defined for this instance of the line.</param>
        public void SetColumnData(string[] values)
        {
            _setColumnDataArray[_setColumnDataArrayInx](values);

            //for (int i = 0; i < this.NumberOfColumns; i++)
            //{
            //    this.ColumnData.ColumnDataValue[i].Data = values[i];
            //}
        }

        /// <summary>
        /// Array of values for the current line.
        /// </summary>
        /// <param name="values">String array. Length of array must match number of columns defined for this instance of the line.</param>
        public void SetColumnDataNoQuotes(string[] values)
        {
            for (int i = 0; i < this.NumberOfColumns; i++)
            {
                this.ColumnData.ColumnDataValue[i].Data = values[i];
            }
        }

        /// <summary>
        /// Array of values for the current line.
        /// </summary>
        /// <param name="values">String array. Length of array must match number of columns defined for this instance of the line.</param>
        public void SetColumnDataWithQuotes(string[] values)
        {
            for (int i = 0; i < this.NumberOfColumns; i++)
            {
                this.ColumnData.ColumnDataValue[i].Data = "\"" + values[i] + "\"";
            }
        }


        //write data

        /// <summary>
        /// Creates data line containing column names. 
        /// </summary>
        /// <returns>String containg delimited line of column names.</returns>
        /// <remarks>Use this to produce formatted delimited column names for output.</remarks>
        public string OutputColumnNames()
        {
            _out.Length = 0;

            _out.Append(_columnDefinitions.ColumnDefinition[0].ColumnName);
            for (int inx = 1; inx < _numberOfColumns; inx++)
            {
                _out.Append(_columnSeparator[0]);
                _out.Append(_columnDefinitions.ColumnDefinition[inx].ColumnName);
            }
            _out.Append(_lineTerminator);

            _numLinesInOutput++;
            if (_numLinesInOutput == 1)
                _columnNamesOnFirstLine = true;

            return _out.ToString();
        }

        /// <summary>
        /// Creates data line containing the data for this instance.
        /// </summary>
        /// <returns>String containing delimited data line.</returns>
        /// <remarks>Use this method to produced formatted delimited data for output.</remarks>
        public string OutputColumnData()
        {

            _out.Length = 0;

            _out.Append(_columnData.ColumnDataValue[0].Data);
            for (int inx = 1; inx < _numberOfColumns; inx++)
            {
                _out.Append(_columnSeparator[0]);
                _out.Append(_columnData.ColumnDataValue[inx].Data);
            }
            _out.Append(_lineTerminator);

            return _out.ToString();

        }

        /// <summary>
        /// Creates data line containing the data for this instance.
        /// </summary>
        /// <param name="enforceMaxLength">If true, method throws exception if data length is greater than maximum length defined for the column.</param>
        /// <returns>String containing delimited data line.</returns>
        /// <remarks>Use this method to check for data length errors when producing formatted delimited data for output.</remarks>
        public string OutputColumnData(bool enforceMaxLength)
        {
            if (enforceMaxLength)
            {
                for (int inx = 1; inx < _numberOfColumns; inx++)
                {
                    if (_columnData.ColumnDataValue[inx].Data.Length > _columnDefinitions.ColumnDefinition[inx].ColumnLength)
                    {
                        _msg.Length = 0;
                        _msg.Append("Invalid column data length. Actual length: ");
                        _msg.Append(_columnData.ColumnDataValue[inx].Data.Length.ToString());
                        _msg.Append(", Maximum allowed length: ");
                        _msg.Append(_columnDefinitions.ColumnDefinition[inx].ColumnLength.ToString());
                        _msg.Append(". Column name: ");
                        _msg.Append(_columnDefinitions.ColumnDefinition[inx].ColumnName);
                        _msg.Append(" = ");
                        _msg.Append(_columnData.ColumnDataValue[inx].Data);
                        _msg.Append(".");
                        throw new System.Exception(_msg.ToString());
                    }
                }
            }
            return OutputColumnData();
        }
        
        /// <summary>
        /// Method to verify that line of input contains valid column names for this instance.
        /// </summary>
        /// <param name="input">String of data read from input file.</param>
        public void VerifyColumnNames(string input)
        {
           if (input != null)
            {
                string[] colNames = input.Split(_columnSeparator, StringSplitOptions.None);
                if (colNames.Length != this.ColumnDefinitions.ColumnDefinition.Length)
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid number of column names. ");
                    _msg.Append(colNames.Length.ToString());
                    _msg.Append(" found instead of expected ");
                    _msg.Append(this.ColumnData.ColumnDataValue.Length.ToString());
                    _msg.Append(".");
                    throw new System.Exception(_msg.ToString());
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
        /// Method to break up line of input based on column separator defined for this instance. Data is stored in ColumnData property.
        /// </summary>
        /// <param name="input"></param>
        public void ParseData(string input)
        {
            _parseLine[_parseLineInx](input);

            //if (input != null)
            //{
            //    string[] colData = input.Split(_columnSeparator, StringSplitOptions.None);
            //    if(colData.Length != this.ColumnData.ColumnDataValue.Length)
            //    {
            //        _msg.Length=0;
            //        _msg.Append("Invalid number of data columns. ");
            //        _msg.Append(colData.Length.ToString());
            //        _msg.Append(" found instead of expected ");
            //        _msg.Append(this.ColumnData.ColumnDataValue.Length.ToString());
            //        _msg.Append(".");
            //        throw new System.Exception(_msg.ToString());
            //    }
            //    for (int i = 0; i < colData.Length; i++)
            //    {
            //        if (i < this.ColumnDefinitions.ColumnDefinition.Length)
            //        {
            //            this.ColumnData.ColumnDataValue[i].Data = colData[i];
            //        }
            //    }
            //}

        }


        /// <summary>
        /// Method to break up line of input based on column separator defined for this instance. Data is stored in ColumnData property.
        /// </summary>
        /// <param name="input"></param>
        public void ParseDataNoDoubleQuotes(string input)
        {
            if (input != null)
            {
                string[] colData = input.Split(_columnSeparator, StringSplitOptions.None);
                if (colData.Length != this.ColumnData.ColumnDataValue.Length)
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid number of data columns. ");
                    _msg.Append(colData.Length.ToString());
                    _msg.Append(" found instead of expected ");
                    _msg.Append(this.ColumnData.ColumnDataValue.Length.ToString());
                    _msg.Append(".");
                    throw new System.Exception(_msg.ToString());
                }
                for (int i = 0; i < colData.Length; i++)
                {
                    if (i < this.ColumnDefinitions.ColumnDefinition.Length)
                    {
                        this.ColumnData.ColumnDataValue[i].Data = colData[i];
                    }
                }
            }

        }

        /// <summary>
        /// Method to break up line of input based on column separator defined for this instance. Data is stored in ColumnData property.
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>This routine removes leading and trailing double quotes from values.</remarks>
        public void ParseDataContainingDoubleQuotes(string input)
        {
            if (input != null)
            {
                string[] colData = input.Split(_columnSeparator, StringSplitOptions.None);
                if (colData.Length != this.ColumnData.ColumnDataValue.Length)
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid number of data columns. ");
                    _msg.Append(colData.Length.ToString());
                    _msg.Append(" found instead of expected ");
                    _msg.Append(this.ColumnData.ColumnDataValue.Length.ToString());
                    _msg.Append(".");
                    throw new System.Exception(_msg.ToString());
                }
                for (int i = 0; i < colData.Length; i++)
                {
                    if (i < this.ColumnDefinitions.ColumnDefinition.Length)
                    {
                        this.ColumnData.ColumnDataValue[i].Data = colData[i].TrimStart('"').TrimEnd('"');
                    }
                }
            }

        }

        //save definitions to file
        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFDelimitedDataLine));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFFixedLengthLine.</returns>
        public static PFDelimitedDataLine LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFDelimitedDataLine));
            TextReader textReader = new StreamReader(filePath);
            PFDelimitedDataLine columnDefinitions;
            columnDefinitions = (PFDelimitedDataLine)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }



        //class helpers

        /// <summary>
        /// Override that outputs the values of the class' public properties.
        /// </summary>
        /// <returns>String containing property names and values of the class' public properties.</returns>
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


        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFDelimitedDataLine));
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



    }//end PFDelimitedLine class


}//end namespace
