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
using System.Data;

namespace PFTextFiles
{
    /// <summary>
    /// Class to hold definitions for all columns to be included in a line of data.
    /// </summary>
    public class PFColumnDefinitions
    {
        private PFColDef[] _columnDefinition = null;
        private int _numColumns = -1;

        /// <summary>
        /// Consructor for serialization operations.
        /// </summary>
        internal PFColumnDefinitions()
        {
            ;
        }
        //Constructor
        /// <summary>
        /// Constructor for class. Must specify number of columns delimited line will contain.
        /// </summary>
        /// <param name="numberOfColumns"></param>
        public PFColumnDefinitions(int numberOfColumns)
        {
            _columnDefinition = new PFColDef[numberOfColumns];
            _numColumns = numberOfColumns;
            for (int inx = 0; inx < numberOfColumns; inx++)
            {
                _columnDefinition[inx] = new PFColDef();
            }
        }

        //properties

        /// <summary>
        /// Array of column definitions.
        /// </summary>
        public PFColDef[] ColumnDefinition
        {
            get
            {
                return _columnDefinition;
            }
            set
            {
                _columnDefinition = value;
            }
        }

        /// <summary>
        /// Number of columns for this instance.
        /// </summary>
        public int NumberOfColumns
        {
            get
            {
                return _numColumns;
            }
            set
            {
                _numColumns = value;
            }
        }

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFColumnDefinitions));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFDelimitedLine.</returns>
        public static PFColumnDefinitions LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFColumnDefinitions));
            TextReader textReader = new StreamReader(filePath);
            PFColumnDefinitions columnDefinitions;
            columnDefinitions = (PFColumnDefinitions)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }

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

            if (this.ColumnDefinition.Length > 0)
            {
                data.Append(" Column information: ");
                for (int i = 0; i < ColumnDefinition.Length; i++)
                {
                    data.Append(ColumnDefinition[i].ColumnName);
                    data.Append(",");
                    data.Append(ColumnDefinition[i].ColumnLength.ToString());
                    data.Append(",");
                    data.Append(ColumnDefinition[i].ColumnDataAlignment.ToString());
                    data.Append("  ");
                }
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
            XmlSerializer ser = new XmlSerializer(typeof(PFColumnDefinitions));
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

    }//end class

    /// <summary>
    /// Class to hold data for each column defined for a line of data.
    /// </summary>
    public class PFColumnData
    {
        private StringBuilder _str = new StringBuilder();
        PFColumnDefinitions _columnDefinitions = null;
        private PFColData[] _columnDataValue = null;

        //Constructor

        /// <summary>
        /// Constructor for serialization operations.
        /// </summary>
        internal PFColumnData()
        {
            ;
        }
        /// <summary>
        /// Constructor for class. Will create objects to hold data values for each of the columns defined in the colDefs parameter.
        /// </summary>
        /// <param name="colDefs">Objects containing column definitions.</param>
        public PFColumnData(PFColumnDefinitions colDefs)
        {
            _columnDefinitions = colDefs;
            _columnDataValue = new PFColData[colDefs.ColumnDefinition.Length];
            for (int i = 0; i < colDefs.ColumnDefinition.Length; i++)
            {
                ColumnDataValue[i] = new PFColData();
            }
        }

        //properties
        /// <summary>
        /// Array of data values for columns defined for this instance.
        /// </summary>
        public PFColData[] ColumnDataValue
        {
            get
            {
                return _columnDataValue;
            }
            set
            {
                _columnDataValue = value;
            }
        }

        //methods
        /// <summary>
        /// Override that outputs the values of the class' data properties.
        /// </summary>
        /// <returns>String containing column names and data values for current instance of class.</returns>
        public override string ToString()
        {
            _str.Length = 0;
            for (int i = 0; i < _columnDataValue.Length; i++)
            {
                _str.Append(_columnDefinitions.ColumnDefinition[i].ColumnName);
                _str.Append(" = ");
                _str.Append(_columnDataValue[i].Data);
                _str.Append("  ");
            }
            return _str.ToString();
        }


    }//end class

    /// <summary>
    /// Class for defining a column that will be included in a delimited or fixed length file.
    /// </summary>
    public class PFColDef
    {
        //private variables for properties

        private string _columnName = string.Empty;
        private int _columnLength = -1;
        private PFDataAlign _columnDataAlignment = PFDataAlign.LeftJustify;

        //properties

        /// <summary>
        /// Column name that will appear in output if name output is requested.
        /// </summary>
        public string ColumnName
        {
            get
            {
                return _columnName;
            }
            set
            {
                _columnName = value;
            }
        }

        /// <summary>
        /// Length of the data for the column. Only valid in case of fixed length data.
        /// </summary>
        public int ColumnLength
        {
            get
            {
                return _columnLength;
            }
            set
            {
                _columnLength = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        public PFDataAlign ColumnDataAlignment
        {
            get
            {
                return _columnDataAlignment;
            }
            set
            {
                _columnDataAlignment = value;
            }
        }



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
        /// <returns></returns>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFColDef));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }



    }//end PFColDef

    //-------------------------------------------------------------------------------------------------------
    // Routines that also save source column name follow
    //-------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Class to hold definitions for all columns to be included in a line of data.
    /// </summary>
    /// <remarks>Ext version of class includes information on the source column name.</remarks>
    public class PFColumnDefinitionsExt
    {
        private PFColDefExt[] _columnDefinitionExt = null;
        private int _numColumns = -1;

        /// <summary>
        /// Consructor for serialization operations.
        /// </summary>
        internal PFColumnDefinitionsExt()
        {
            ;
        }
        //Constructor
        /// <summary>
        /// Constructor for class. Must specify number of columns delimited line will contain.
        /// </summary>
        /// <param name="numberOfColumns"></param>
        public PFColumnDefinitionsExt(int numberOfColumns)
        {
            _columnDefinitionExt = new PFColDefExt[numberOfColumns];
            _numColumns = numberOfColumns;
            for (int inx = 0; inx < numberOfColumns; inx++)
            {
                _columnDefinitionExt[inx] = new PFColDefExt();
            }
        }

        //properties

        /// <summary>
        /// Array of column definitions.
        /// </summary>
        public PFColDefExt[] ColumnDefinition
        {
            get
            {
                return _columnDefinitionExt;
            }
            set
            {
                _columnDefinitionExt = value;
            }
        }

        /// <summary>
        /// Number of columns for this instance.
        /// </summary>
        public int NumberOfColumns
        {
            get
            {
                return _numColumns;
            }
            set
            {
                _numColumns = value;
            }
        }

        //methods

        /// <summary>
        /// Routine to return a list of columns
        /// </summary>
        /// <param name="dt">DataTable object containing columns.</param>
        /// <returns>Object containing column definitions. Definitions include both source and output column names.</returns>
        public static PFColumnDefinitionsExt GetColumnDefinitionsExt(DataTable dt)
        {
            if (dt.Columns == null)
            {
                return new PFColumnDefinitionsExt();
            }
            if (dt.Columns.Count < 1)
            {
                return new PFColumnDefinitionsExt();
            }

            PFColumnDefinitionsExt columnDefinitionsExt = new PFColumnDefinitionsExt(dt.Columns.Count);
            int colInx = 0;
            int maxColInx = dt.Columns.Count - 1;
            int colLen = -1;
            DataColumnCollection cols = dt.Columns;

            for (colInx = 0; colInx <= maxColInx; colInx++)
            {
                if (PFFixedLengthDataLine.DataTypeIsNumeric(cols[colInx].DataType))
                {
                    colLen = PFFixedLengthDataLine.GetNumericTypeMaxExtractLength(cols[colInx].DataType);
                }
                else if (PFFixedLengthDataLine.DataTypeIsDateTime(cols[colInx].DataType))
                {
                    colLen = PFFixedLengthDataLine.GetDateTimeTypeMaxExtractLength(cols[colInx].DataType);
                }
                else
                {
                    colLen = cols[colInx].MaxLength;
                }

                SetColumnDefinitionExt(columnDefinitionsExt, colInx, cols[colInx].ColumnName, colLen, PFDataAlign.LeftJustify);
            }


            return columnDefinitionsExt;
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
        /// Method to specify column definitions for the current instance.
        /// </summary>
        /// <param name="columnDefinitionsExt">Object containing the array of column definitions.</param>
        /// <param name="columnIndex">0-based index of the column being defined.</param>
        /// <param name="sourceColumnName">Name to assign to the column. This will become the column header for the output.</param>
        /// <param name="columnLength">Length in bytes of the column. </param>
        /// <param name="columnDataAlignment">See <see cref="PFDataAlign"/> enumeration.</param>
        /// <remarks>If sourceColumnName is longer than columnLength, the sourceColumnName value will be truncated to columnLength.</remarks>
        public static void SetColumnDefinitionExt(PFColumnDefinitionsExt columnDefinitionsExt,
                                                  int columnIndex,
                                                  string sourceColumnName,
                                                  int columnLength,
                                                  PFDataAlign columnDataAlignment)
        {
            int minColLen = 1;
            int maxColumnLengthOverride = 1024;
            int defaultStringColumnLength = 255;

            string colName = sourceColumnName;
            string newColName = string.Empty;
            int colLen = columnLength > maxColumnLengthOverride ? maxColumnLengthOverride : columnLength;
            if (colLen < minColLen)
                colLen = defaultStringColumnLength;
            if (colName.Length > colLen)
            {
                colName = colName.Substring(0, colLen);
            }

            columnDefinitionsExt.ColumnDefinition[columnIndex].SourceColumnName = sourceColumnName;
            columnDefinitionsExt.ColumnDefinition[columnIndex].OutputColumnName = colName;
            columnDefinitionsExt.ColumnDefinition[columnIndex].OutputColumnLength = colLen;
            columnDefinitionsExt.ColumnDefinition[columnIndex].OutputColumnDataAlignment = columnDataAlignment;
        }

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFColumnDefinitionsExt));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFColumnDefinitionsExt.</returns>
        public static PFColumnDefinitionsExt LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFColumnDefinitionsExt));
            TextReader textReader = new StreamReader(filePath);
            PFColumnDefinitionsExt columnDefinitions;
            columnDefinitions = (PFColumnDefinitionsExt)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from an XML formatted string.
        /// </summary>
        /// <param name="xmlString">String containing data in XML format.</param>
        /// <returns>An instance of PFColumnDefinitionsExt.</returns>
        public static PFColumnDefinitionsExt LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFColumnDefinitionsExt));
            StringReader strReader = new StringReader(xmlString);
            PFColumnDefinitionsExt columnDefinitions;
            columnDefinitions = (PFColumnDefinitionsExt)deserializer.Deserialize(strReader);
            strReader.Close();
            return columnDefinitions;
        }


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

            if (this.ColumnDefinition.Length > 0)
            {
                data.Append(" Column information: ");
                for (int i = 0; i < ColumnDefinition.Length; i++)
                {
                    data.Append(ColumnDefinition[i].SourceColumnName);
                    data.Append(" as ");
                    data.Append(ColumnDefinition[i].OutputColumnName);
                    data.Append(",");
                    data.Append(ColumnDefinition[i].OutputColumnLength.ToString());
                    data.Append(",");
                    data.Append(ColumnDefinition[i].OutputColumnDataAlignment.ToString());
                    data.Append("  ");
                }
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
            XmlSerializer ser = new XmlSerializer(typeof(PFColumnDefinitionsExt));
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

        /// <summary>
        /// Makes a deep copy of the current instance to a new instance.
        /// </summary>
        /// <returns>New instance of PFColumnDefinitionsExt object.</returns>
        public PFColumnDefinitionsExt Copy()
        {
            string xmlString = this.ToXmlString();
            PFColumnDefinitionsExt newInstance = PFColumnDefinitionsExt.LoadFromXmlString(xmlString);
            return newInstance;
        }



    }//end class

    /// <summary>
    /// Class to hold data for each column defined for a line of data.
    /// </summary>
    public class PFColumnDataExt
    {
        private StringBuilder _str = new StringBuilder();
        PFColumnDefinitionsExt _columnDefinitionsExt = null;
        private PFColData[] _columnDataValue = null;

        //Constructor

        /// <summary>
        /// Constructor for serialization operations.
        /// </summary>
        internal PFColumnDataExt()
        {
            ;
        }
        /// <summary>
        /// Constructor for class. Will create objects to hold data values for each of the columns defined in the colDefs parameter.
        /// </summary>
        /// <param name="colDefs">Objects containing column definitions.</param>
        public PFColumnDataExt(PFColumnDefinitionsExt colDefs)
        {
            _columnDefinitionsExt = colDefs;
            _columnDataValue = new PFColData[colDefs.ColumnDefinition.Length];
            for (int i = 0; i < colDefs.ColumnDefinition.Length; i++)
            {
                ColumnDataValue[i] = new PFColData();
            }
        }

        //properties
        /// <summary>
        /// Array of data values for columns defined for this instance.
        /// </summary>
        public PFColData[] ColumnDataValue
        {
            get
            {
                return _columnDataValue;
            }
            set
            {
                _columnDataValue = value;
            }
        }

        //methods
        /// <summary>
        /// Override that outputs the values of the class' data properties.
        /// </summary>
        /// <returns>String containing column names and data values for current instance of class.</returns>
        public override string ToString()
        {
            _str.Length = 0;
            for (int i = 0; i < _columnDataValue.Length; i++)
            {
                _str.Append(_columnDefinitionsExt.ColumnDefinition[i].SourceColumnName);
                _str.Append(" as ");
                _str.Append(_columnDefinitionsExt.ColumnDefinition[i].OutputColumnName);
                _str.Append(" = ");
                _str.Append(_columnDataValue[i].Data);
                _str.Append("  ");
            }
            return _str.ToString();
        }


    }//end class

    /// <summary>
    /// Class for defining a column that will be included in a delimited or fixed length file.
    /// </summary>
    public class PFColDefExt
    {
        //private variables for properties

        private string _sourceColumnName = string.Empty;
        private string _outputColumnName = string.Empty;
        private int _outputColumnLength = -1;
        private PFDataAlign _outputColumnDataAlignment = PFDataAlign.LeftJustify;

        //properties

        /// <summary>
        /// Column name from the source data. It may be different from the OutputColumnName.
        /// </summary>
        public string SourceColumnName
        {
            get
            {
                return _sourceColumnName;
            }
            set
            {
                _sourceColumnName = value;
            }
        }

        /// <summary>
        /// Column name that will appear in output if name output is requested.
        /// </summary>
        public string OutputColumnName
        {
            get
            {
                return _outputColumnName;
            }
            set
            {
                _outputColumnName = value;
            }
        }

        /// <summary>
        /// Length of the data for the column. Only valid in case of fixed length data.
        /// </summary>
        public int OutputColumnLength
        {
            get
            {
                return _outputColumnLength;
            }
            set
            {
                _outputColumnLength = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        public PFDataAlign OutputColumnDataAlignment
        {
            get
            {
                return _outputColumnDataAlignment;
            }
            set
            {
                _outputColumnDataAlignment = value;
            }
        }



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
        /// <returns></returns>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFColDefExt));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }



    }//end PFColDefExt


    //-------------------------------------------------------------------------------------------------------
    // Routines that store column data follow.
    //-------------------------------------------------------------------------------------------------------
    
    /// <summary>
    /// Class to encapsulate the data on a text file output line.
    /// </summary>
    public class PFColData
    {
        //private variables for properties
        private string _data = string.Empty;

        /// <summary>
        /// Data to output.
        /// </summary>
        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }


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
        /// <returns></returns>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFColData));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }


    }//end class





}//end namespace
