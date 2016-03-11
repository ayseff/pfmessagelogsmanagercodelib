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

#pragma warning disable 1591
    [Serializable]
    public struct stKeyValuePair<K, V>
    {
        public K Key;
        public V Value;

        public stKeyValuePair(K pKey, V pValue)
        {
            this.Key = pKey;
            this.Value = pValue;
        }
    }
#pragma warning restore 1591

    /// <summary>
    /// Class to manage list of key/value pairs. List can be save to either XML or to a database.
    /// </summary>
    public class PFKeyValueList<K, V> : List<stKeyValuePair<K, V>>
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private int _eofInx = -1;
        //private varialbles for properties
        private int _currItemInx = -1;
        private bool _EOF = true;


        //private variables for properties

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFKeyValueList()
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
                        ret = base[_currItemInx];
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
                    ret = base[0];
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
                    ret = base[base.Count - 1];
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
                        ret = base[_currItemInx];
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
                        ret = base[_currItemInx];
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
        /// Converts currentg instance to another PFKeyValueList object.
        /// </summary>
        /// <returns>PFKeyValueList object.</returns>
        public PFKeyValueList<K, V> CopyThisToPFKeyValueList()
        {
            PFKeyValueList<K, V> kvlist = new PFKeyValueList<K, V>();
            stKeyValuePair<K, V> kvp = default(stKeyValuePair<K, V>);

            if (this.Count > 0)
            {
                this.SetToBOF();
                kvp = this.FirstItem;
                while (this.EOF == false)
                {
                    kvlist.Add(new stKeyValuePair<K, V>(kvp.Key, kvp.Value));
                    kvp = this.NextItem;
                }
            }

            return kvlist;
        }

        /// <summary>
        /// Converts current instance of a PFKeyValueList object to PFList object.
        /// </summary>
        /// <returns>PFList object.</returns>
        public PFList<stKeyValuePair<K, V>> ConvertThisToPFList()
        {
            PFList<stKeyValuePair<K, V>> list = new PFList<stKeyValuePair<K, V>>();
            stKeyValuePair<K, V> kvp = default(stKeyValuePair<K, V>);

            if (this.Count > 0)
            {
                this.SetToBOF();
                kvp = this.FirstItem;
                while (this.EOF == false)
                {
                    list.Add(new stKeyValuePair<K, V>(kvp.Key, kvp.Value));
                    kvp = this.NextItem;
                }
            }

            return list;
        }

        /// <summary>
        /// Searches for and returns key/value pair that has matching key.
        /// </summary>
        /// <param name="keyToFind">Key to search for.</param>
        /// <returns>Object containing key and value. Returns empty key/val object if not found.</returns>
        public stKeyValuePair<K, V> Find(string keyToFind)
        {
            stKeyValuePair<K, V> retval = default(stKeyValuePair<K, V>);
            foreach (stKeyValuePair<K, V> kvp in this)
            {
                if (kvp.Key.ToString() == keyToFind)
                {
                    retval = kvp;
                }
            }
            return retval;
        }

        /// <summary>
        /// Determines if specified key exists in the list.
        /// </summary>
        /// <param name="keyToFind">Key to search for.</param>
        /// <returns>Returns true if found.</returns>
        public bool Exists(string keyToFind)
        {
            bool keyExists = false;

            foreach (stKeyValuePair<K, V> kvp in this)
            {
                if (kvp.Key.ToString() == keyToFind)
                {
                    keyExists = true;
                    break;
                }
            }

            return keyExists;

        }

        /// <summary>
        /// Removes the item identified by the specified key from the list.
        /// </summary>
        /// <param name="keyToRemove">Key to search for and remove.</param>
        public void Remove(string keyToRemove)
        {
            stKeyValuePair<K, V> keyval = this.Find(keyToRemove);
            this.Remove(keyval);
        }

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFKeyValueList<K, V>));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFKeyValueList.</returns>
        public static PFKeyValueList<K, V> LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFKeyValueList<K, V>));
            TextReader textReader = new StreamReader(filePath);
            PFKeyValueList<K, V> objectInstance;
            objectInstance = (PFKeyValueList<K, V>)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
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
            StringBuilder temp = new StringBuilder();
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
                //object val = null;
                //try
                //{
                //    val = prop.GetValue(this, null);
                //}
                //catch
                //{
                //    val = "Unable to retrieve value.";
                //}

                object val = null;
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
                        temp.Append("  \r\n");
                    }
                    val = temp.ToString();
                }
                else
                {
                    //this is an indexed property
                    temp.Length = 0;
                    temp.Append("Num indexes for property: ");
                    temp.Append(prop.GetIndexParameters().Length.ToString());
                    temp.Append("  \r\n");
                    val = temp.ToString();
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
            XmlSerializer ser = new XmlSerializer(typeof(PFKeyValueList<K, V>));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFKeyValueList.</returns>
        public static PFKeyValueList<K, V> LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFKeyValueList<K, V>));
            StringReader strReader = new StringReader(xmlString);
            PFKeyValueList<K, V> objectInstance;
            objectInstance = (PFKeyValueList<K, V>)deserializer.Deserialize(strReader);
            strReader.Close();
            return objectInstance;
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
        /// Routine that concatenates two or more lists into one list.
        /// </summary>
        /// <param name="lists">Array of list objects to be concatenated.</param>
        /// <returns>Concatenated list.</returns>
        public static PFKeyValueList<K, V> ConcatenateLists(PFKeyValueList<K, V>[] lists)
        {
            PFKeyValueList<K, V> concatenatedList = new PFKeyValueList<K, V>();

            if (lists == null)
            {
                return concatenatedList;
            }

            for (int listInx = 0; listInx < lists.Length; listInx++)
            {
                PFKeyValueList<K, V> tempList = lists[listInx];
                if (tempList != null)
                {
                    foreach (stKeyValuePair<K,V> item in tempList)
                    {
                        concatenatedList.Add(item);
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
        public static PFKeyValueList<K, V> ConcatenateLists(PFList<PFKeyValueList<K, V>> lists)
        {
            PFKeyValueList<K, V> concatenatedList = new PFKeyValueList<K, V>();

            if (lists == null)
            {
                return concatenatedList;
            }

            for (int listInx = 0; listInx < lists.Count; listInx++)
            {
                PFKeyValueList<K, V> tempList = lists[listInx];
                if (tempList != null)
                {
                    foreach (stKeyValuePair<K, V> item in tempList)
                    {
                        concatenatedList.Add(item);
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
        public PFKeyValueList<K, V> Merge(PFKeyValueList<K, V> list)
        {
            PFKeyValueList<K, V> mergedList = new PFKeyValueList<K, V>();

            if (list == null)
            {
                return mergedList;
            }

            foreach (stKeyValuePair<K, V> item in this)
            {
                mergedList.Add(item);
            }

            foreach (stKeyValuePair<K, V> item in list)
            {
                mergedList.Add(item);
            }

            return mergedList;
        }

        /// <summary>
        /// Merges current list with the array of lists specified in the parameter.
        /// </summary>
        /// <param name="lists">Lists to merge with.</param>
        /// <returns>Merged list.</returns>
        public PFKeyValueList<K, V> Merge(PFKeyValueList<K, V>[] lists)
        {
            PFKeyValueList<K, V> mergedList = new PFKeyValueList<K, V>();

            if (lists == null)
            {
                return mergedList;
            }

            foreach (stKeyValuePair<K, V> item in this)
            {
                mergedList.Add(item);
            }

            for (int listInx = 0; listInx < lists.Length; listInx++)
            {
                PFKeyValueList<K, V> tempList = lists[listInx];
                if (tempList != null)
                {
                    foreach (stKeyValuePair<K, V> item in tempList)
                    {
                        mergedList.Add(item);
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
        public PFKeyValueList<K, V> Merge(PFList<PFKeyValueList<K, V>> lists)
        {
            PFKeyValueList<K, V> mergedList = new PFKeyValueList<K, V>();

            if (lists == null)
            {
                return mergedList;
            }

            foreach (stKeyValuePair<K, V> item in this)
            {
                mergedList.Add(item);
            }

            for (int listInx = 0; listInx < lists.Count; listInx++)
            {
                PFKeyValueList<K, V> tempList = lists[listInx];
                if (tempList != null)
                {
                    foreach (stKeyValuePair<K, V> item in tempList)
                    {
                        mergedList.Add(item);
                    }
                }
            }

            return mergedList;
        }


        /// <summary>
        /// Copies current list to a new list.
        /// </summary>
        /// <returns>Copy of list.</returns>
        public PFKeyValueList<K, V> Copy()
        {
            PFKeyValueList<K, V> newList = new PFKeyValueList<K, V>();

            foreach (stKeyValuePair<K, V> item in this)
            {
                newList.Add(item);
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

            for (int i = 0; i < this.Count; i++)
            {
                stKeyValuePair<K, V> item = this[i];
                int key = rnd.GenerateRandomNumber(min, max);
                sortList.Add(key, item);
            }

            IEnumerator<KeyValuePair<int, stKeyValuePair<K, V>>> enumerator = sortList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Get current key value pair 
                stKeyValuePair<int, stKeyValuePair<K, V>> keyValuePair = new stKeyValuePair<int, stKeyValuePair<K, V>>(enumerator.Current.Key, enumerator.Current.Value);
                randomizedList.Add(keyValuePair.Value);
            }



            return randomizedList;
        }



    }//end class


}//end namespace
