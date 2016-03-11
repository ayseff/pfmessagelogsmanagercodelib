namespace PFTextFiles
{
    /// <summary>
    /// Open modes for PFTextFile instances.
    /// </summary>
    public enum PFFileOpenOperation
    {
        /// <summary>
        /// Useful if instance will only be used to determine file size or some other attribute of the file.
        /// </summary>
        DoNotOpenFile=1,
        /// <summary>
        /// File is opened for output and all current data is retained. New data will be appended to the end of the file.
        /// </summary>
        OpenFileForAppend=2,
        /// <summary>
        /// File is opened for reading only.
        /// </summary>
        OpenFileToRead=3,
        /// <summary>
        /// File is opened for writing. Any existing data is overwritten by the new data.
        /// </summary>
        OpenFileForWrite=4
    }

    /// <summary>
    /// Used to specity how to place data in text fields.
    /// </summary>
    public enum PFTextAlign
    {
#pragma warning disable 1591
        General = 0,
        LeftJustify=1,
        RightJustify=2,
        Center=3
#pragma warning restore 1591
    }

    /// <summary>
    /// Used to specity how to place data in columns in extract files.
    /// </summary>
    public enum PFDataAlign
    {
#pragma warning disable 1591
        LeftJustify = -1,
        RightJustify = 1,
#pragma warning restore 1591
    }

    /// <summary>
    /// Describes the type of delimited text file to produce.
    /// </summary>
    public enum PFDelimitedTextFileType
    {
        /// <summary>
        /// Calling program has complete control over formatting. Caller decides when to write a new line and when to append data to an existing line. Any internal formatting for the line is manually created by code in the calling program.
        /// </summary>
        /// <remarks>Data will be written out as a stream of bytes. Calling program must implement any formatting, separators or terminators.</remarks>
        Default=0,
        /// <summary>
        /// Column separators and/or line terminators are defined for each line.
        /// </summary>
        /// <remarks>Use delimited for CSV and Tab delimited data extract files. Custom delimiters can be defined.</remarks>
        Delimited=1,
        /// <summary>
        /// One or more columns are defined for each line, each column having a fixed length that must be preserved in the output.
        /// Optionally a line terminator may be defined.
        /// </summary>
        /// <remarks>Use to produce fixed length data extract files. If line terminator is defined, file is divided into equally sized lines. 
        /// If there is no line terminator, the data streams into one big blob. With or without a line terminator, the program reading a fixed length file has to have the column and row definitions in order to correctly parse the data.
        /// </remarks>
        FixedLength=2
    }


}//end namespace
