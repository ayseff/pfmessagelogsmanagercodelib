using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;


namespace PFTextObjects
{
    /// <summary>
    /// Class to split a string into a defined set of components. Useful for parsing command lines.
    /// </summary>
    public class PFParseString
    {

        //public enums
        /// <summary>
        /// Specifies how the arguments are grouped in a string.
        /// </summary>
        public enum PFArgumentKeyType
        {
            /// <summary>
            /// Arguments do have a key.
            /// </summary>
            NoKey = 0,
            /// <summary>
            /// One character key (e.g. /d parm1)
            /// </summary>
            CharKey = 1,
            /// <summary>
            /// Example: /server MyServer
            /// </summary>
            NamedKey = 2
        }

#pragma warning disable 1591
        public enum PFScroll
        {
            First = 0,
            Next = 1,
            Previous = 2,
            Last =3
        }
#pragma warning restore 1591

        /// <summary>
        /// Used for storing keys and value pairs found in a string.
        /// </summary>
        public struct PFKeyValuePair
        {
#pragma warning disable 1591
            public string Key;
            public string Value;
#pragma warning restore 1591
        }


        //work variables
        private StringCollection _parsedString = new StringCollection();
        private StringDictionary _keyValuePairs = new StringDictionary(); 
        private int _curPos = 0;
        private List<string> _keyList = new List<string>();
        private int _curKeyListPos = 0;

        //fields for properties
        private string _stringToParse = "";
        private string _delimiters = "-/";
        private bool _quotedValues = false;
        private int _numItems = 0;
        private PFArgumentKeyType _keyType = PFArgumentKeyType.NoKey;
        private bool _EOL = true;

        //constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public PFParseString()
        {
            ;
        }

        //properties
        /// <summary>
        /// One or more delimiters used to separate arguments on a line.
        /// </summary>
        public string Delimiters
        {
            get
            {
                return _delimiters;
            }
            set
            {
                _delimiters = value;
            }
        }

        /// <summary>
        /// If true, phrases with embedded spaces can be surrounded by quotes so they will be processed as one argument.
        /// </summary>
        public bool QuotedValues
        {
            get
            {
                return _quotedValues;
            }
            set
            {
                _quotedValues = value;
            }
        }

        /// <summary>
        /// Returns number of items (arguments) found in a string.
        /// </summary>
        public int NumItems
        {
            get
            {
                return _numItems;
            }
        }

        /// <summary>
        /// Gets or sets the type of key to look for in the string being parsed.
        /// </summary>
        public PFArgumentKeyType KeyType
        {
            get
            {
                return _keyType;
            }
            set
            {
                _keyType = value;
            }
        }

        /// <summary>
        /// True if end of line has been reached.
        /// </summary>
        public bool EOL
        {
            get
            {
                return _EOL;
            }
        }

        /// <summary>
        /// String that is to be parsed.
        /// </summary>
        /// <remarks>Setting this property automatically parses the string.</remarks>
        public string StringToParse
        {
            get
            {
                return _stringToParse;
            }
            set
            {

                _stringToParse = value;
                if (_quotedValues)
                {
                    ParseStringWithQuotes();
                }
                else
                {
                    ParseString();
                }

            }
        }

        /// <summary>
        /// Number of keys found in the parsed string.
        /// </summary>
        public int NumKeys
        {
            get
            {
                return _keyList.Count();
            }
        }


        //methods
        /// <summary>
        /// Retrieves the argument specified by the index parameter.
        /// </summary>
        /// <param name="rnIndex">0 based index into the list of arguments found in the parsed string.</param>
        /// <returns>Value at the specified index.</returns>
        public string Get(int rnIndex)
        {
            string value = "";
            try
            {
                value = (string)(_parsedString[rnIndex]);

            }
            catch
            {
                value = "";
            }

            return value;

        }

        /// <summary>
        /// Retrieves the value of the argument specified by the key parameter.
        /// </summary>
        /// <param name="rsKey">Key to lookup.</param>
        /// <returns>Value associated with the key.</returns>
        public string Get(string rsKey)
        {

            string value = "";

            try
            {
                value = (string)(_keyValuePairs[rsKey]);
            }
            catch
            {
                value = "";
            }

            return value;

        }

        /// <summary>
        /// Method for scrolling through the list of key/value pairs found in a parsed string.
        /// </summary>
        /// <param name="scroll">Identifies which key/value pair to retrieve.</param>
        /// <returns>Returns key and its associated value at the position on line specified by the PFScroll parameter.</returns>
        public PFKeyValuePair GetKeyValuePair(PFScroll scroll)
        {
            PFKeyValuePair kv = new PFKeyValuePair();
            string key = string.Empty;
            string value = string.Empty;
            int maxKeyListPos = _keyList.Count - 1;

            switch (scroll)
            {
                case PFScroll.First:
                    _curKeyListPos = 0;
                    break;
                case PFScroll.Next:
                    _curKeyListPos++;
                    break;
                case PFScroll.Previous:
                    _curKeyListPos--;
                    break;
                case PFScroll.Last:
                    _curKeyListPos=maxKeyListPos;
                    break;
                default:
                    _curKeyListPos = 0;
                    break;

            }


            if (_keyList.Count > 0)
            {
                if (_curKeyListPos <= maxKeyListPos
                    && _curKeyListPos >= 0)
                {
                    key = _keyList[_curKeyListPos];
                    value = this.Get(key);
                }
            }

            kv.Key = key;
            kv.Value = value;
            return kv;
        }

        /// <summary>
        /// Method to retrieve values in parsed string by scrolling through the list of items found.
        /// </summary>
        /// <param name="scroll">Direction to scroll.</param>
        /// <returns>Value found at the position specified by the scroll.</returns>
        public string Get(PFScroll scroll)
        {
            string value = string.Empty;
            int maxPos = _parsedString.Count - 1;

            switch (scroll)
            {
                case PFScroll.First:
                    _curPos = 0;
                    break;
                case PFScroll.Next:
                    _curPos++;
                    break;
                case PFScroll.Previous:
                    _curPos--;
                    break;
                case PFScroll.Last:
                    _curPos = maxPos;
                    break;
                default:
                    _curPos = 0;
                    break;

            }

            if (_parsedString.Count > 0)
            {
                if (_curPos <= maxPos
                    && _curPos >= 0)
                {
                    value = _parsedString[_curPos];
                    _EOL = false;
                }
                else
                {
                    _EOL = true;
                }
            }
            else
            {
                _EOL = true;
            }


            return value;
        }

        /// <summary>
        /// Retrieves first value found in parsed string.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetFirst()
        {

            return (Get(PFScroll.First));

        }

        /// <summary>
        /// Retrieves last value found in parsed string.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLast()
        {

            return (Get(PFScroll.Last));

        }

        /// <summary>
        /// Retrieves next value found in parsed string.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetNext()
        {

            return (Get(PFScroll.Next));

        }

        /// <summary>
        /// Retrieves previous value found in parsed string.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetPrev()
        {

            return (Get(PFScroll.Previous));

        }
        
        /// <summary>
        /// Gets first key/value pair in parsed string.
        /// </summary>
        /// <returns>Key/Value pair.</returns>
        public PFKeyValuePair GetFirstKeyValue()
        {

            return (GetKeyValuePair(PFScroll.First));

        }

        /// <summary>
        /// Gets last key/value pair in parsed string.
        /// </summary>
        /// <returns>Key/Value pair.</returns>
        public PFKeyValuePair GetLastKeyValue()
        {

            return (GetKeyValuePair(PFScroll.Last));

        }

        /// <summary>
        /// Gets next key/value pair in parsed string.
        /// </summary>
        /// <returns>Key/Value pair.</returns>
        public PFKeyValuePair GetNextKeyValue()
        {

            return (GetKeyValuePair(PFScroll.Next));

        }

        /// <summary>
        /// Gets previous key/value pair in parsed string.
        /// </summary>
        /// <returns>Key/Value pair.</returns>
        public PFKeyValuePair GetPrevKeyValue()
        {

            return (GetKeyValuePair(PFScroll.Previous));

        }

        private void ParseString()
        {

            int inx;
            int maxInx;
            string str;
            string ch;


            //remove any items left over from a previous parse
            RemoveAllItemsFromList();
            inx = 0;
            maxInx = _stringToParse.Length - 1;

            //parse the string based on delimiters
            str = "";
            ch = "";
            for (inx = 0; inx <= maxInx; inx++)
            {
                ch = _stringToParse.Substring(inx, 1);
                if (_delimiters.IndexOf(ch) != -1)
                {
                    if (str != "")
                    {
                        AddStringToList(str);
                        str = "";
                    }
                }
                else
                {
                    str = str + ch;
                }

            }
            //get the final substring, if any, into the list
            if (str != "")
            {
                AddStringToList(str);
                str = "";
            }
            _numItems = System.Convert.ToInt32(_parsedString.Count);


        }

        private void ParseStringWithQuotes()
        {

            int inx;
            int maxInx;
            string str;
            string ch;
            bool insideQuotes;
            bool keepChar;
            const string QUOTATION_MARK = "\"";

            //remove any items left over from a previous parse
            RemoveAllItemsFromList();
            inx = 0;
            maxInx = _stringToParse.Length - 1;
            insideQuotes = false;

            //parse the string based on delimiters
            str = "";
            ch = "";
            for (inx = 0; inx <= maxInx; inx++)
            {
                keepChar = true;
                ch = _stringToParse.Substring(inx, 1);
                if (ch == QUOTATION_MARK)
                {
                    if (insideQuotes == false)
                    {
                        insideQuotes = true;
                        keepChar = false;
                    }
                    else
                    {
                        if (inx < maxInx)
                        {
                            if (_stringToParse.Substring(inx + 1, 1) == QUOTATION_MARK)
                            {
                                //quote will be stored
                                //push index past the second quote in the pair
                                inx++;
                            }
                            else
                            {
                                //quote signifies end of quotation
                                insideQuotes = false;
                                keepChar = false;
                            }
                        }
                        else
                        {
                            //quote is at end of string
                            //treat this as end of quotation
                            insideQuotes = false;
                            keepChar = false;
                        }
                    }
                }
                if (_delimiters.IndexOf(ch) != -1 && (insideQuotes == false))
                {
                    if (str != "")
                    {
                        AddStringToList(str);
                        str = "";
                    }
                }
                else
                {
                    if (keepChar)
                    {
                        str = str + ch;
                    }
                }

            }
            if (str != "")
            {
                AddStringToList(str);
                str = "";
            }
            _numItems = System.Convert.ToInt32(_parsedString.Count);

        }

        private void RemoveAllItemsFromList()
        {
            _parsedString.Clear();
            _keyValuePairs.Clear();
            _keyList.Clear();
        }

        private void AddStringToList(string pStr)
        {
            //moParsedString.Add(rsStr)
            string str;
            string item;
            string key;
            int len;
            int itemPos;

            str = pStr.Trim();
            item = "";
            key = "";

            len = str.Length;
            if (len > 0)
            {
                //do not add null strings to the collection
                _parsedString.Add(str);
                //determine if key/value pair needs to be stored
                switch (_keyType)
                {
                    case PFArgumentKeyType.CharKey:
                        key = str.Substring(0, 1);
                        if (len > 1)
                        {
                            item = str.Substring(1, len - 1);
                        }
                        else
                        {
                            item = "";
                        }
                        break;
                    case PFArgumentKeyType.NamedKey:
                        //nItemPos = InStr(1, sStr, "=", vbBinaryCompare)
                        itemPos = str.IndexOf("=");
                        if (itemPos != -1)
                        {
                            //sItem = Mid$(sStr, nItemPos + 1, nLen - nItemPos)
                            //sKey = Mid$(sStr, 1, nItemPos - 1)\
                            item = str.Substring(itemPos + 1, len - (itemPos + 1));
                            key = str.Substring(0, itemPos);
                        }
                        break;
                }

                if (key != "")
                {
                    if (_keyValuePairs.ContainsKey(key) == false)
                    {
                        _keyValuePairs.Add(key, item);
                        _keyList.Add(key);
                    }
                }
            }

        }



    }//end class

}//end namespace


