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
using System.Runtime.Serialization;
using System.IO;
using System.Data;
using System.Data.Common;
using PFRandomData;

namespace PFCollectionsObjects
{

    /// <summary>
    /// Class to manage list of sorted key/value pairs. List can be saved or written from and to XML.
    /// </summary>
    public class PFKeyValueListSorted<K, V> : SortedList<K, V>
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private int _eofInx = -1;
        //private varialbles for properties
        private int _currItemInx = -1;
        private bool _EOF = true;

        private readonly Type _typeKey = typeof(K);
        private readonly Type _typeValue = typeof(V);


        //private variables for properties

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFKeyValueListSorted()
        {
            ;
        }

        //properties

        /// <summary>
        /// EOF Property. Is true if current position is not pointing to any item in the collection.
        /// </summary>
        public bool EOF
        {
            get
            {
                return _EOF;
            }
        }

        /// <summary>
        /// Current item selected from collection by latest navigation method (FirstItem, LastItem, NextItem, PrevItem).
        /// </summary>
        public stKeyValuePair<K, V> CurrentItem
        {
            get
            {
                stKeyValuePair<K, V> ret = default(stKeyValuePair<K, V>);
               if (base.Count > 0)
                {
                    if (_currItemInx < base.Count && _currItemInx >= 0)
                    {
                        ret.Key = base.Keys[_currItemInx];
                        ret.Value = base.Values[_currItemInx];
                        _EOF = false;
                    }
                    else
                    {
                        _EOF = true;
                        _currItemInx = _eofInx;
                    }
                }
                else
                {
                    _EOF = true;
                    _currItemInx = _eofInx;
                }

                return ret;
            }
        }


        /// <summary>
        /// Selects first item in the collection. Returns null if no items in the collection. Returns null if there are no items in the collection.
        /// </summary>
        public stKeyValuePair<K, V> FirstItem
        {
            get
            {
                stKeyValuePair<K, V> ret = default(stKeyValuePair<K, V>);
                if (base.Count > 0)
                {
                    ret.Key = base.Keys[0];
                    ret.Value = base.Values[0];
                    _currItemInx = 0;
                    _EOF = false;
                }
                else
                {
                    _EOF = true;
                    _currItemInx = _eofInx;
                }

                return ret;
            }
        }

        /// <summary>
        /// Selects the last item in the collection. Returns null if there are no items in the collection.
        /// </summary>
        public stKeyValuePair<K, V> LastItem
        {
            get
            {
                stKeyValuePair<K, V> ret = default(stKeyValuePair<K, V>);
                if (base.Count > 0)
                {
                    ret.Key = base.Keys[base.Count - 1];
                    ret.Value = base.Values[base.Count - 1];
                    _currItemInx = base.Count - 1;
                    _EOF = false;
                }
                else
                {
                    _EOF = true;
                    _currItemInx = _eofInx;
                }

                return ret;
            }
        }

        /// <summary>
        /// Moves to and selects next item in the collection that follows the currently selected item. Returns null if navigation moves past the last item in the collection.
        /// </summary>
        public stKeyValuePair<K, V> NextItem
        {
            get
            {
                stKeyValuePair<K, V> ret = default(stKeyValuePair<K, V>);
                if (base.Count > 0)
                {
                    _currItemInx++;
                    if (_currItemInx < base.Count)
                    {
                        _EOF = false;
                        ret.Key = base.Keys[_currItemInx];
                        ret.Value = base.Values[_currItemInx];

                    }
                    else
                    {
                        _EOF = true;
                        _currItemInx = _eofInx;
                    }
                }
                else
                {
                    _EOF = true;
                    _currItemInx = _eofInx;
                }

                return ret;
            }
        }


        /// <summary>
        /// Navigates to and selects the previous item in the collection. Return null if navigation moves past the first item in the collection.
        /// </summary>
        public stKeyValuePair<K, V> PrevItem
        {
            get
            {
                stKeyValuePair<K, V> ret = default(stKeyValuePair<K, V>);
                if (base.Count > 0)
                {
                    _currItemInx--;
                    if (_currItemInx > _eofInx)
                    {
                        ret.Key = base.Keys[_currItemInx];
                        ret.Value = base.Values[_currItemInx];
                        _EOF = false;
                    }
                    else
                    {
                        _EOF = true;
                        _currItemInx = base.Count;
                    }
                }
                else
                {
                    _EOF = true;
                    _currItemInx = _eofInx;
                }

                return ret;
            }
        }



        //methods

        /// <summary>
        /// Used to setup a while(myList.NextItem) loop. NextItem will return first element in the collection after this method is run.
        /// </summary>
        public void SetToBOF()
        {
            _currItemInx = _eofInx;
            _EOF = false;
        }

        /// <summary>
        /// Converts PFKeyValueListSorted object to PFKeyValueList object.
        /// </summary>
        /// <returns>PFKeyValueList object.</returns>
        public PFKeyValueList<K, V> ConvertThisToPFKeyValueList()
        {
            PFKeyValueList<K, V> kvlist = new PFKeyValueList<K, V>();

            IEnumerator<KeyValuePair<K, V>> enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Get current key value pair 
                stKeyValuePair<K, V> keyValuePair = new stKeyValuePair<K, V>(enumerator.Current.Key, enumerator.Current.Value);
                kvlist.Add(keyValuePair);
            }

            return kvlist;
        }

        /// <summary>
        /// Converts PFKeyValueList object to PFKeyValueListSorted object.
        /// </summary>
        /// <param name="kvlist"></param>
        /// <returns>PFKeyValueListSorted object.</returns>
        public static PFKeyValueListSorted<K, V> ConvertPFKeyValueListToSortedList(PFKeyValueList<K, V> kvlist)
        {
            PFKeyValueListSorted<K, V> kvlistSorted = new PFKeyValueListSorted<K, V>();
            kvlist.SetToBOF();
            stKeyValuePair<K, V> stKeyValuePair = kvlist.FirstItem;
            while (!kvlist.EOF)
            {
                kvlistSorted.Add(stKeyValuePair.Key, stKeyValuePair.Value);
                stKeyValuePair = kvlist.NextItem;
            }
            return kvlistSorted;
        }

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            PFKeyValueList<K, V> kvlist = ConvertThisToPFKeyValueList();
            kvlist.SaveToXmlFile(filePath);
        }


        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFKeyValueList.</returns>
        public static PFKeyValueListSorted<K, V> LoadFromXmlFile(string filePath)
        {
            PFKeyValueList<K, V> kvlist = PFKeyValueList<K, V>.LoadFromXmlFile(filePath);
            return ConvertPFKeyValueListToSortedList(kvlist);
        }



        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            data.Append(PropertiesToString());
            data.Append("\r\n");
            data.Append(FieldsToString());
            data.Append("\r\n");


            return data.ToString();
        }


        /// <summary>
        /// Routine outputs name and value for all properties.
        /// </summary>
        /// <returns></returns>
        public string PropertiesToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("\r\nClass properties for");
            data.Append(t.FullName);
            data.Append("\r\n");


            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = null;
                try
                {
                    val = prop.GetValue(this, null);
                }
                catch
                {
                    val = "Unable to retrieve value.";
                }

                /*
                //****************************************************************************************
                //use the following code if you class has an indexer or is derived from an indexed class
                //****************************************************************************************
                object val = null;
                StringBuilder temp = new StringBuilder();
                if (prop.GetIndexParameters().Length == 0)
                {
                    val = prop.GetValue(this, null);
                }
                else if (prop.GetIndexParameters().Length == 1)
                {
                    temp.Length = 0;
                    for (int i = 0; i < this.Count; i++)
                    {
                        temp.Append("Index ");
                        temp.Append(i.ToString());
                        temp.Append(" = ");
                        temp.Append(val = prop.GetValue(this, new object[] { i }));
                        temp.Append("  ");
                    }
                    val = temp.ToString();
                }
                else
                {
                    //this is an indexed property
                    temp.Length = 0;
                    temp.Append("Num indexes for property: ");
                    temp.Append(prop.GetIndexParameters().Length.ToString());
                    temp.Append("  ");
                    val = temp.ToString();
                }
                //****************************************************************************************
                // end code for indexed property
                //****************************************************************************************
                */

                if (prop.GetGetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetGetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetGetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetGetMethod(true).IsPublic && !prop.GetGetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetGetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" get ");
                    data.Append("> ");
                }
                if (prop.GetSetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetSetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetSetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetSetMethod(true).IsPublic && !prop.GetSetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetSetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" set ");
                    data.Append("> ");
                }
                data.Append(" ");
                data.Append(prop.PropertyType.FullName);
                data.Append(" ");

                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (prop.PropertyType.IsArray)
                {
                    System.Collections.IList valueList = (System.Collections.IList)prop.GetValue(this, null);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        /// <summary>
        /// Routine outputs name and value for all fields.
        /// </summary>
        /// <returns></returns>
        public string FieldsToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            FieldInfo[] finfos = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            bool typeHasFieldsToStringMethod = false;

            data.Append("\r\nClass fields for ");
            data.Append(t.FullName);
            data.Append("\r\n");

            int inx = 0;
            int maxInx = finfos.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                FieldInfo fld = finfos[inx];
                object val = fld.GetValue(this);
                if (fld.IsPublic)
                    data.Append(" public ");
                if (fld.IsPrivate)
                    data.Append(" private ");
                if (!fld.IsPublic && !fld.IsPrivate)
                    data.Append(" internal ");
                if (fld.IsStatic)
                    data.Append(" static ");
                data.Append(" ");
                data.Append(fld.FieldType.FullName);
                data.Append(" ");
                data.Append(fld.Name);
                data.Append(": ");
                typeHasFieldsToStringMethod = UseFieldsToString(fld.FieldType);
                if (val != null)
                    if (typeHasFieldsToStringMethod)
                        data.Append(GetFieldValues(val));
                    else
                        data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (fld.FieldType.IsArray)
                //if (fld.Name == "TestStringArray" || "_testStringArray")
                {
                    System.Collections.IList valueList = (System.Collections.IList)fld.GetValue(this);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        private bool UseFieldsToString(Type pType)
        {
            bool retval = false;

            //avoid have this type calling its own FieldsToString and going into an infinite loop
            if (pType.FullName != this.GetType().FullName)
            {
                MethodInfo[] methods = pType.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == "FieldsToString")
                    {
                        retval = true;
                        break;
                    }
                }
            }

            return retval;
        }

        private string GetFieldValues(object typeInstance)
        {
            Type typ = typeInstance.GetType();
            MethodInfo methodInfo = typ.GetMethod("FieldsToString");
            Object retval = methodInfo.Invoke(typeInstance, null);


            return (string)retval;
        }


        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            PFKeyValueList<K, V> kvlist = ConvertThisToPFKeyValueList();
            return kvlist.ToXmlString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFKeyValueList.</returns>
        public static PFKeyValueListSorted<K, V> LoadFromXmlString(string xmlString)
        {
            PFKeyValueList<K, V> kvlist = default(PFKeyValueList<K,V>);
            try
            {
                kvlist = PFKeyValueList<K, V>.LoadFromXmlString(xmlString);
            }
            catch (System.Exception ex)
            {
                StringBuilder errMsg = new StringBuilder();
                errMsg.Length = 0;
                errMsg.Append("Error while loading xml string:");
                errMsg.Append(Environment.NewLine);
                errMsg.Append(ex.Message);
                errMsg.Append(Environment.NewLine);
                errMsg.Append("XML must contain definition for a PFKeyValueList object. That object will be converted into a PFKeyValueListSorted object.");
                throw new System.Exception(errMsg.ToString());
            }
            return ConvertPFKeyValueListToSortedList(kvlist);
        }


        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument()
        {
            PFKeyValueList<K, V> kvlist = ConvertThisToPFKeyValueList();
            return kvlist.ToXmlDocument();
        }

        /// <summary>
        /// Routine that concatenates two or more lists into one list.
        /// </summary>
        /// <param name="lists">Array of list objects to be concatenated.</param>
        /// <returns>Concatenated list.</returns>
        public static PFKeyValueListSorted<K, V> ConcatenateLists(PFKeyValueListSorted<K, V>[] lists)
        {
            PFKeyValueListSorted<K, V> concatenatedList = new PFKeyValueListSorted<K, V>();

            if (lists == null)
            {
                return concatenatedList;
            }

            for (int listInx = 0; listInx < lists.Length; listInx++)
            {
                PFKeyValueListSorted<K, V> tempList = lists[listInx];
                if (tempList != null)
                {
                    IEnumerator<KeyValuePair<K, V>> enumerator = tempList.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        // Get current key value pair 
                        stKeyValuePair<K, V> keyValuePair = new stKeyValuePair<K, V>(enumerator.Current.Key, enumerator.Current.Value);
                        concatenatedList.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                }
            }

            return concatenatedList;
        }

        /// <summary>
        /// Routine that concatenates two or more lists into one list.
        /// </summary>
        /// <param name="lists">List of list objects to be concatenated.</param>
        /// <returns>Concatenated list.</returns>
        public static PFKeyValueListSorted<K, V> ConcatenateLists(PFList<PFKeyValueListSorted<K, V>> lists)
        {
            PFKeyValueListSorted<K, V> concatenatedList = new PFKeyValueListSorted<K, V>();

            if (lists == null)
            {
                return concatenatedList;
            }

            for (int listInx = 0; listInx < lists.Count; listInx++)
            {
                PFKeyValueListSorted<K, V> tempList = lists[listInx];
                if (tempList != null)
                {
                    IEnumerator<KeyValuePair<K, V>> enumerator = tempList.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        // Get current key value pair 
                        stKeyValuePair<K, V> keyValuePair = new stKeyValuePair<K, V>(enumerator.Current.Key, enumerator.Current.Value);
                        concatenatedList.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                }
            }

            return concatenatedList;
        }

        /// <summary>
        /// Merges current list with the list specified in the parameter.
        /// </summary>
        /// <param name="list">List to merge with.</param>
        /// <returns>Merged list.</returns>
        public PFKeyValueListSorted<K, V> Merge(PFKeyValueListSorted<K, V> list)
        {
            PFKeyValueListSorted<K, V> mergedList = new PFKeyValueListSorted<K, V>();

            if (list == null)
            {
                return mergedList;
            }

            IEnumerator<KeyValuePair<K, V>> enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Get current key value pair 
                stKeyValuePair<K, V> keyValuePair = new stKeyValuePair<K, V>(enumerator.Current.Key, enumerator.Current.Value);
                mergedList.Add(keyValuePair.Key, keyValuePair.Value);
            }


            IEnumerator<KeyValuePair<K, V>> enumList = list.GetEnumerator();
            while (enumList.MoveNext())
            {
                // Get current key value pair 
                stKeyValuePair<K, V> keyValuePair = new stKeyValuePair<K, V>(enumList.Current.Key, enumList.Current.Value);
                mergedList.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return mergedList;
        }

        /// <summary>
        /// Merges current list with the array of lists specified in the parameter.
        /// </summary>
        /// <param name="lists">Lists to merge with.</param>
        /// <returns>Merged list.</returns>
        public PFKeyValueListSorted<K, V> Merge(PFKeyValueListSorted<K, V>[] lists)
        {
            PFKeyValueListSorted<K, V> mergedList = new PFKeyValueListSorted<K, V>();

            if (lists == null)
            {
                return mergedList;
            }

            IEnumerator<KeyValuePair<K, V>> enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Get current key value pair 
                stKeyValuePair<K, V> keyValuePair = new stKeyValuePair<K, V>(enumerator.Current.Key, enumerator.Current.Value);
                mergedList.Add(keyValuePair.Key, keyValuePair.Value);
            }


            for (int listInx = 0; listInx < lists.Length; listInx++)
            {
                PFKeyValueListSorted<K, V> tempList = lists[listInx];
                if (tempList != null)
                {
                    IEnumerator<KeyValuePair<K, V>> enumTempList = tempList.GetEnumerator();
                    while (enumTempList.MoveNext())
                    {
                        // Get current key value pair 
                        stKeyValuePair<K, V> keyValuePair = new stKeyValuePair<K, V>(enumTempList.Current.Key, enumTempList.Current.Value);
                        mergedList.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                }
            }

            return mergedList;
        }

        /// <summary>
        /// Merges current list with the list of lists specified in the parameter.
        /// </summary>
        /// <param name="lists">Lists to merge with.</param>
        /// <returns>Merged list.</returns>
        public PFKeyValueListSorted<K, V> Merge(PFList<PFKeyValueListSorted<K, V>> lists)
        {
            PFKeyValueListSorted<K, V> mergedList = new PFKeyValueListSorted<K, V>();

            if (lists == null)
            {
                return mergedList;
            }

            IEnumerator<KeyValuePair<K, V>> enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Get current key value pair 
                stKeyValuePair<K, V> keyValuePair = new stKeyValuePair<K, V>(enumerator.Current.Key, enumerator.Current.Value);
                mergedList.Add(keyValuePair.Key, keyValuePair.Value);
            }


            for (int listInx = 0; listInx < lists.Count; listInx++)
            {
                PFKeyValueListSorted<K, V> tempList = lists[listInx];
                if (tempList != null)
                {
                    IEnumerator<KeyValuePair<K, V>> enumTempList = tempList.GetEnumerator();
                    while (enumTempList.MoveNext())
                    {
                        // Get current key value pair 
                        stKeyValuePair<K, V> keyValuePair = new stKeyValuePair<K, V>(enumTempList.Current.Key, enumTempList.Current.Value);
                        mergedList.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                }
            }

            return mergedList;
        }

        /// <summary>
        /// Copies current list to a new list.
        /// </summary>
        /// <returns>Copy of list.</returns>
        public PFKeyValueListSorted<K, V> Copy()
        {
            PFKeyValueListSorted<K, V> newList = new PFKeyValueListSorted<K, V>();

            IEnumerator<KeyValuePair<K, V>> enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Get current key value pair 
                stKeyValuePair<K, V> keyValuePair = new stKeyValuePair<K, V>(enumerator.Current.Key, enumerator.Current.Value);
                newList.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return newList;
        }


        /// <summary>
        /// Produces a copy of this list in which the item order has been randomized.
        /// </summary>
        /// <returns>List containing items in random order.</returns>
        public PFKeyValueList<K, V> Randomize()
        {
            PFKeyValueList<K, V> randomizedList = new PFKeyValueList<K, V>();
            PFKeyValueListSorted<int, stKeyValuePair<K, V>> sortList = new PFKeyValueListSorted<int, stKeyValuePair<K, V>>();
            RandomNumber rnd = new RandomNumber();
            int min = 0;
            int max = 200000000;

            IEnumerator<KeyValuePair<K, V>> enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Get current key value pair 
                stKeyValuePair<K, V> keyValuePair = new stKeyValuePair<K, V>(enumerator.Current.Key, enumerator.Current.Value);
                int key = rnd.GenerateRandomNumber(min, max);
                sortList.Add(key, keyValuePair);
            }

            IEnumerator<KeyValuePair<int, stKeyValuePair<K, V>>> enumSortList = sortList.GetEnumerator();
            while (enumSortList.MoveNext())
            {
                // Get current key value pair 
                stKeyValuePair<int, stKeyValuePair<K, V>> keyValuePair = new stKeyValuePair<int, stKeyValuePair<K, V>>(enumSortList.Current.Key, enumSortList.Current.Value);
                randomizedList.Add(new stKeyValuePair<K, V>(keyValuePair.Value.Key, keyValuePair.Value.Value));
            }



            return randomizedList;
        }



    }//end class


}//end namespace
