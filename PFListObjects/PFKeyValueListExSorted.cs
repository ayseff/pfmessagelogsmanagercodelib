//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2013
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
//using PFDataAccessObjects;
using PFEncryptionObjects;

namespace PFListObjects
{

    /// <summary>
    /// Class to manage list of key/value pairs. List can be save to either XML or to a database.
    /// </summary>
    public class PFKeyValueListExSorted<K, V> : SortedList<K, V>
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
        public PFKeyValueListExSorted()
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
        public pfKeyValuePair<K, V> CurrentItem
        {
            get
            {
                pfKeyValuePair<K, V> ret = default(pfKeyValuePair<K, V>);
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
        public pfKeyValuePair<K, V> FirstItem
        {
            get
            {
                pfKeyValuePair<K, V> ret = default(pfKeyValuePair<K, V>);
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
        public pfKeyValuePair<K, V> LastItem
        {
            get
            {
                pfKeyValuePair<K, V> ret = default(pfKeyValuePair<K, V>);
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
        public pfKeyValuePair<K, V> NextItem
        {
            get
            {
                pfKeyValuePair<K, V> ret = default(pfKeyValuePair<K, V>);
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
        public pfKeyValuePair<K, V> PrevItem
        {
            get
            {
                pfKeyValuePair<K, V> ret = default(pfKeyValuePair<K, V>);
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
        /// Converts PFKeyValueListExSorted object to PFKeyValueListEx object.
        /// </summary>
        /// <returns>PFKeyValueListEx object.</returns>
        public PFKeyValueListEx<K, V> ConvertThisToPFKeyValueListEx()
        {
            PFKeyValueListEx<K, V> kvlist = new PFKeyValueListEx<K, V>();

            IEnumerator<KeyValuePair<K, V>> enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Get current key value pair 
                pfKeyValuePair<K, V> keyValuePair = new pfKeyValuePair<K, V>(enumerator.Current.Key, enumerator.Current.Value);
                kvlist.Add(keyValuePair);
            }

            return kvlist;
        }

        /// <summary>
        /// Converts PFKeyValueListEx object to PFKeyValueListExSorted object.
        /// </summary>
        /// <param name="kvlist"></param>
        /// <returns>PFKeyValueListExSorted object.</returns>
        public static PFKeyValueListExSorted<K, V> ConvertPFKeyValueListExToSortedList(PFKeyValueListEx<K, V> kvlist)
        {
            PFKeyValueListExSorted<K, V> kvlistSorted = new PFKeyValueListExSorted<K, V>();
            kvlist.SetToBOF();
            pfKeyValuePair<K, V> pfKeyValuePair = kvlist.FirstItem;
            while (!kvlist.EOF)
            {
                kvlistSorted.Add(pfKeyValuePair.Key, pfKeyValuePair.Value);
                pfKeyValuePair = kvlist.NextItem;
            }
            return kvlistSorted;
        }

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            PFKeyValueListEx<K, V> kvlist = ConvertThisToPFKeyValueListEx();
            kvlist.SaveToXmlFile(filePath);
        }

        /// <summary>
        /// Saves the public property values contained in the current instance to an encrypted file. Key and IV parameters are used
        /// to specify an AES encryption. Serialization is used to get the object values into an XML string that will be encrypted and
        /// written to the output file.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        /// <param name="key">AES encryption key.</param>
        /// <param name="iv">AES IV value.</param>
        /// <remarks>Only AES encryption is supported by this routine.</remarks>
        public void SaveToXmlFile(string filePath, string key, string iv)
        {
            pfEncryptionAlgorithm alg = pfEncryptionAlgorithm.AES;
            IStringEncryptor enc2 = PFEncryption.GetStringEncryptor(alg);
            //store xml data in a string
            string xmldata = this.ToXmlString();

            //encrypt the xml data string
            enc2.Key = key;
            enc2.IV = iv;
            string encryptedXML = enc2.Encrypt(xmldata);

            //save encrypted xml to file
            File.WriteAllText(filePath, encryptedXML);
        }


        /// <summary>
        /// Saves the public property values contained in the current instance to the database specified by the connection string.
        /// </summary>
        /// <param name="connectionString">Contains information needed to open the database.</param>
        /// <param name="listName">Name to give list in the database.</param>
        public void SaveToDatabase(string connectionString, string listName)
        {
            PFKeyValueListEx<K, V> kvlist = ConvertThisToPFKeyValueListEx();
            kvlist.SaveToDatabase(connectionString, listName);
        }


        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFKeyValueListEx.</returns>
        public static PFKeyValueListExSorted<K, V> LoadFromXmlFile(string filePath)
        {
            PFKeyValueListEx<K, V> kvlist = PFKeyValueListEx<K, V>.LoadFromXmlFile(filePath);
            return ConvertPFKeyValueListExToSortedList(kvlist);
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored in an encrypted file.
        /// The file is first decrypted into an XML string and the XML string is then used to create an instance of the class.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <param name="key">AES encryption key.</param>
        /// <param name="iv">AES IV value.</param>
        /// <returns>An instance of PFKeyValueListEx.</returns>
        /// <remarks>Only AES encryption is supported by this routine.</remarks>
        public static PFKeyValueListExSorted<K, V> LoadFromXmlFile(string filePath, string key, string iv)
        {
            pfEncryptionAlgorithm alg = pfEncryptionAlgorithm.AES;
            IStringEncryptor enc2 = PFEncryption.GetStringEncryptor(alg);
            PFKeyValueListExSorted<K, V> listElements;

            //first: load the encrypted data from the specified file
            string encryptedXML = File.ReadAllText(filePath);
            //next: decrypt the encrypted data
            enc2.Key = key;
            enc2.IV = iv;
            string xmldata = enc2.Decrypt(encryptedXML);
            //step 3: create a PFLIcense object from the decrypted string
            listElements = LoadFromXmlString(xmldata);
            //finally: return to caller 
            return listElements;
        }


        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a database record.
        /// </summary>
        /// <param name="connectionString">Connection parameters for the database.</param>
        /// <param name="listName">Name of the list in the database.</param>
        /// <returns>PFListEx object.</returns>
        public static PFKeyValueListExSorted<K, V> LoadFromDatabase(string connectionString, string listName)
        {
            PFKeyValueListEx<K, V> kvlist = PFKeyValueListEx<K, V>.LoadFromDatabase(connectionString, listName);
            return ConvertPFKeyValueListExToSortedList(kvlist);
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
            PFKeyValueListEx<K, V> kvlist = ConvertThisToPFKeyValueListEx();
            return kvlist.ToXmlString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFKeyValueListEx.</returns>
        public static PFKeyValueListExSorted<K, V> LoadFromXmlString(string xmlString)
        {
            PFKeyValueListEx<K, V> kvlist = default(PFKeyValueListEx<K,V>);
            try
            {
                kvlist = PFKeyValueListEx<K, V>.LoadFromXmlString(xmlString);
            }
            catch (System.Exception ex)
            {
                StringBuilder errMsg = new StringBuilder();
                errMsg.Length = 0;
                errMsg.Append("Error while loading xml string:");
                errMsg.Append(Environment.NewLine);
                errMsg.Append(ex.Message);
                errMsg.Append(Environment.NewLine);
                errMsg.Append("XML must contain definition for a PFKeyValueListEx object. That object will be converted into a PFKeyValueListExSorted object.");
                throw new System.Exception(errMsg.ToString());
            }
            return ConvertPFKeyValueListExToSortedList(kvlist);
        }


        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument()
        {
            PFKeyValueListEx<K, V> kvlist = ConvertThisToPFKeyValueListEx();
            return kvlist.ToXmlDocument();
        }


    }//end class


}//end namespace
