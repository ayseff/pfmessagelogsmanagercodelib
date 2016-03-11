using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.IO;
using System.Reflection;

namespace PFXmlObjects
{

    /// <summary>
    /// Class for the processing of XML documents.
    /// </summary>
    public class PFXmlDocument
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _errMsg = new StringBuilder();
        
        private XmlDocument _xmlDoc = new XmlDocument();
        private string _documentFileName = string.Empty;
        private KeyValuePair<string, string>[] _defaultNamespaces = new KeyValuePair<string, string>[2] {new KeyValuePair<string,string>("xmlns:xsi","http://www.w3.org/2001/XMLSchema-instance"),
                                                                                                       new KeyValuePair<string,string>("xmlns:xsd","http://www.w3.org/2001/XMLSchema")};        

        //properties
        /// <summary>
        /// Returns instance of XmlDocument class that is represented by this class. Use this property to get at all the methods and properties associated with an XmlDocument object.
        /// </summary>
        public XmlDocument XmlDoc
        {
            get
            {
                return _xmlDoc;
            }
        }

        /// <summary>
        /// DocumentElement object of XmlDocument instance.
        /// </summary>
        public XmlNode DocumentRootNode
        {
            get
            {
                return _xmlDoc.DocumentElement;
            }
        }

        /// <summary>
        /// DocumentElement Name.
        /// </summary>
        public string DocumentRootNodeName
        {
            get
            {
                return _xmlDoc.DocumentElement.Name;
            }
        }

        /// <summary>
        /// Path to file containing the Xml data.
        /// </summary>
        public string DocumentFileName
        {
            get
            {
                return _documentFileName;
            }
        }

        /// <summary>
        /// Returns InnerXml of the XmlDocument object represented by this class.
        /// </summary>
        public string InnerXml
        {
            get
            {
                if (_xmlDoc != null)
                    return _xmlDoc.InnerXml;
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Returns InnerText of the XmlDocument object represented by this class.
        /// </summary>
        public string InnerText
        {
            get
            {
                if (_xmlDoc != null)
                    return _xmlDoc.InnerText;
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Returns OuterXml of the XmlDocument object represented by this class.
        /// </summary>
        public string OuterXml
        {
            get
            {
                if (_xmlDoc != null)
                    return _xmlDoc.OuterXml;
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Returns a list of all the child nodes for the XmlDocument object represented by this class.
        /// </summary>
        public XmlNodeList ChildNodes
        {
            get
            {
                if (_xmlDoc != null)
                    return _xmlDoc.ChildNodes;
                else
                    return null;
            }
        }

        //constructor

        /// <summary>
        /// Class constructor. Will created an Xml document with a DocumentElement name of "Root".
        /// </summary>
        public PFXmlDocument()
        {
            CreateRootNode("Root", "utf-16", _defaultNamespaces);
        }

        /// <summary>
        /// Class constructor. Will created an Xml document with a DocumentElement name as specified by parameter.
        /// </summary>
        /// <param name="rootNodeTagName">Name of DocumentElement.</param>
        public PFXmlDocument(string rootNodeTagName)
        {
            CreateRootNode(rootNodeTagName, "utf-16", _defaultNamespaces);
        }

        /// <summary>
        /// Class constructor. Will created an Xml document with a DocumentElement name as specified by parameter.
        /// </summary>
        /// <param name="rootNodeTagName">Name of DocumentElement.</param>
        /// <param name="encoding">Encoding to use with this document.</param>
        public PFXmlDocument(string rootNodeTagName, string encoding)
        {
            CreateRootNode(rootNodeTagName, encoding, _defaultNamespaces);
        }

        /// <summary>
        /// Class constructor. Will created an Xml document with a DocumentElement name as specified by parameter.
        /// </summary>
        /// <param name="rootNodeTagName">Name of DocumentElement.</param>
        /// <param name="namespaces">One of more key/value pairs that identify the namespaces for the root element.</param>
        public PFXmlDocument(string rootNodeTagName, KeyValuePair<string, string>[] namespaces)
        {
            CreateRootNode(rootNodeTagName, "utf-16", namespaces);
        }

        /// <summary>
        /// Class constructor. Will created an Xml document with a DocumentElement name as specified by parameter.
        /// </summary>
        /// <param name="rootNodeTagName">Name of DocumentElement.</param>
        /// <param name="encoding">Encoding to use with this document.</param>
        /// <param name="namespaces">One of more key/value pairs that identify the namespaces for the root element.</param>
        public PFXmlDocument(string rootNodeTagName, string encoding, KeyValuePair<string, string>[] namespaces)
        {
            CreateRootNode(rootNodeTagName, encoding, namespaces);
        }

        private void CreateRootNode(string psRootNodeTagName, string encoding, KeyValuePair<string, string>[] namespaces)
        {
            XmlDeclaration xmldec = _xmlDoc.CreateXmlDeclaration("1.0", encoding, null);
            XmlNode oRootNode = _xmlDoc.CreateElement(psRootNodeTagName);
            _xmlDoc.InsertAfter(oRootNode, null);
            _xmlDoc.InsertBefore(xmldec, oRootNode);
            if(namespaces !=null)
                if(namespaces.Length > 0)
                {
                    for(int inx = 0; inx < namespaces.Length; inx++)
                    {
                        _xmlDoc.DocumentElement.SetAttribute(namespaces[inx].Key, namespaces[inx].Value);
                    }
                }
        }


        //methods
        /// <summary>
        /// Loads contents of specified file into the XmlDocument represented by this class.
        /// </summary>
        /// <param name="filename">Path to file containing Xml data.</param>
        /// <remarks>Will throw an XmlException if the Xml is not well formed. Also throws exception if the file cannot be found.</remarks>
        public void LoadFromFile(string filename)
        {

            if(File.Exists(filename))
            {
                try
                {
                    _documentFileName = filename;
                    _xmlDoc.Load(filename);
                }
                catch (XmlException xmlex)
                {
                    _msg.Append("Unable to load specified file due to XML Exception. Filename: ");
                    _msg.Append(filename);
                    _msg.Append("\r\n");
                    throw new System.Exception(_msg.ToString(), xmlex);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                _msg.Append("Unable to find the specified file: ");
                _msg.Append(filename);
                throw new System.Exception(_msg.ToString());
            }
        }

        /// <summary>
        /// Loads Xml contained in specified string to the XmlDocument represented by this class.
        /// </summary>
        /// <param name="xmlString">String containing the Xml data.</param>
        /// <remarks>Will throw an XmlException if the Xml is not well formed.</remarks>
        public void LoadFromString(string xmlString)
        {

            try
            {
                _xmlDoc.LoadXml(xmlString);
            }
            catch (XmlException xmlex)
            {
                _msg.Append("Unable to load specified string due to XML Exception. String contents: ");
                _msg.Append(xmlString);
                _msg.Append("\r\n");
                throw new System.Exception(_msg.ToString(), xmlex);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Saves the Xml contents of the XmlDocument represented by this class to the specified file.
        /// </summary>
        /// <param name="filename">Path to output file.</param>
        public void SaveToFile(string filename)
        {
            if (_xmlDoc == null)
            {
                //there is no document open yet
                throw new System.Exception("No document exists for this instance of PFXmlDocument");
            }
            try
            {
                _documentFileName = filename;
                _xmlDoc.Save(filename);
            }
            catch (Exception ex)
            {
                _msg.Append("Unable to save xml document to ");
                _msg.Append(filename);
                _msg.Append("\r\n");
                throw new System.Exception(_msg.ToString(), ex);
            }
        }


        /// <summary>
        /// Adds new XmlElement to specified parent XmlNode.
        /// </summary>
        /// <param name="parentNode">XmlNode under which new element is to be placed.</param>
        /// <param name="nodeName">Name of new node.</param>
        /// <returns>XmlElement object for the new node.</returns>
        public XmlElement AddNewElement(XmlNode parentNode,
                                         string nodeName)
        {
            return this.AddNewElement(parentNode, nodeName, null);
        }

        /// <summary>
        /// Adds new XmlElement to specified parent XmlNode.
        /// </summary>
        /// <param name="parentNode">XmlNode under which new element is to be placed.</param>
        /// <param name="nodeName">Name of new node.</param>
        /// <param name="innerText">Inner text for the new node.</param>
        /// <returns>XmlElement object for the new node.</returns>
        public XmlElement AddNewElement(XmlNode parentNode,
                                 string nodeName,
                                 string innerText)
        {
            // Create a new XmlElement object,and set the return value.
            XmlElement oXMLEL = parentNode.OwnerDocument.CreateElement(nodeName);
            // Set its inner text if provided.
            if (innerText != null)
            {
                oXMLEL.InnerText = innerText;
            }
            // Make it a child of its parent node.
            parentNode.AppendChild(oXMLEL);
            // Return the new node to the caller.
            return oXMLEL;
        }

        /// <summary>
        /// Sets attribute for specified XmlElement.
        /// </summary>
        /// <param name="xmlElement">XmlElement object to be modified.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="attributeValue">Value for the specified attribute.</param>
        public void AddNewAttribute(XmlElement xmlElement,
                                     String attributeName,
                                     String attributeValue)
        {
            xmlElement.SetAttribute(attributeName, attributeValue);
        }

        /// <summary>
        /// Deletes all the nodes in a document. Only root node remains after operation completes.
        /// </summary>
        public void DeleteAllNodesInDocument()
        {
            _xmlDoc.DocumentElement.RemoveAll();
        }

        /// <summary>
        /// Removes all specified attributes and children of the current node. Default attributes are not removed.
        /// </summary>
        /// <param name="xmlElement">XmlElement from which the attributes and children are to be removed.</param>
        public void DeleteAll(XmlElement xmlElement)
        {
            xmlElement.RemoveAll();
        }

        /// <summary>
        /// Deletes all child nodes for specified XmlElement.
        /// </summary>
        /// <param name="xmlElement">XmlElement from which the nodes are to be removed.</param>
        public void DeleteChildNodes(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.NodeType != XmlNodeType.Text)
                {
                    xmlNode.RemoveAll();
                    xmlNode.ParentNode.RemoveChild(xmlNode);
                    //recursion will end when all non-text children have been removed
                    DeleteChildNodes(xmlElement);
                }
            }
        }

        /// <summary>
        /// Deletes specified element plus all its children.
        /// </summary>
        /// <param name="xmlElement">Element to be deleted.</param>
        public void DeleteNode(XmlElement xmlElement)
        {
            XmlNode parentNode;

            xmlElement.RemoveAll();
            parentNode = xmlElement.ParentNode;
            parentNode.RemoveChild(xmlElement);
        }


        /// <summary>
        /// Routine to find the element specified by the xpathQuery.
        /// </summary>
        /// <param name="xpathQuery">Query specifying the element to be found.</param>
        /// <returns>Returns XmlElement if found; otherwise null.</returns>
        /// <remarks>Throws an exception if the xpath query is malformed.</remarks>
        public XmlElement FindElement(String xpathQuery)
        {
            XmlElement xmlElement;

            try
            {
                xmlElement = (XmlElement)_xmlDoc.SelectSingleNode(xpathQuery);
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error while trying to find XML element. Check XPath Query. Make sure XPath syntax is correct: " + xpathQuery, ex);
            }

            return xmlElement;
        }

        /// <summary>
        /// Returns list of all nodes in the XmlDocument represented by this instance that are found by the specified xpathQuery.
        /// </summary>
        /// <param name="xpathQuery">Query specifying the element(s) to be found.</param>
        /// <returns>List of nodes found as result of applying the xpath query.</returns>
        public XmlNodeList GetMultipleNodes(String xpathQuery)
        {
            XmlNodeList nodes = null;

            try
            {
                nodes = (XmlNodeList)_xmlDoc.SelectNodes(xpathQuery);
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error while trying to find XML node list. Check XPath Query. Make sure XPath syntax is correct: " + xpathQuery, ex);
            }

            return nodes;
        }

        /// <summary>
        ///  Returns list of all nodes in the within the specified XmlNode that are found by the specified xpathQuery.
        /// </summary>
        /// <param name="node">Node for which search is to be conducted.</param>
        /// <param name="xpathQuery">Query specifying the element(s) to be found.</param>
        /// <returns>List of nodes found as result of applying the xpath query.</returns>
        public XmlNodeList GetMultipleNodes(XmlNode node, String xpathQuery)
        {
            XmlNodeList nodes = null;

            try
            {
                nodes = (XmlNodeList)node.SelectNodes(xpathQuery);
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error while trying to find XML node list. Check XPath Query. Make sure XPath syntax is correct: " + xpathQuery, ex);
            }

            return nodes;
        }


        /// <summary>
        /// Finds inner text for the XmlElement found by the xpathQuery.
        /// </summary>
        /// <param name="xpathQuery">Query specifying the element to be found.</param>
        /// <returns>String containing the element's inner text.</returns>
        public string GetInnerText(string xpathQuery)
        {

            XmlElement xmlElement;
            string innerText = string.Empty;

            try
            {
                xmlElement = FindElement(xpathQuery);
                if (xmlElement != null)
                    if (xmlElement.InnerText.Length == 0)
                        if (xmlElement.HasAttribute("xsi:nil"))
                            if (xmlElement.Attributes["xsi:nil"].Value.ToUpper() == "TRUE")
                                innerText=null;
                            else
                                innerText=String.Empty;
                        else
                            innerText = string.Empty;
                    else
                        innerText = xmlElement.InnerText;
                else
                    innerText = null;
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error while trying to find inner text of XML element. " + xpathQuery, ex);
            }

            return innerText;

        }

        /// <summary>
        /// Gets the value for the specified attribute of the specified node.
        /// </summary>
        /// <param name="xmlNode">XmlNode containing the attribute.</param>
        /// <param name="attributeName">Attribute containing the value to be returned.</param>
        /// <returns>String containing the value for the specified attribute at the specified node.</returns>
        public string GetAttributeValue(XmlNode xmlNode,
                                        string attributeName)
        {
            return GetAttributeValue(xmlNode, attributeName, null);
        }


        /// <summary>
        /// Gets the value for the specified attribute of the specified node. Returns defaultValue if attribute not found.
        /// </summary>
        /// <param name="xmlNode">XmlNode containing the attribute.</param>
        /// <param name="attributeName">Attribute containing the value to be returned.</param>
        /// <param name="defaultValue">Value to return if the attribute is not found.</param>
        /// <returns>String containing the value for the specified attribute at the specified node. Default value is returned if attribute not found.</returns>
        public string GetAttributeValue(XmlNode xmlNode,
                                        string attributeName,
                                        string defaultValue)
        {
            string attributeValue = null;

            try
            {
                XmlNode attribute = xmlNode.Attributes.GetNamedItem(attributeName);
                attributeValue = attribute.Value.ToString();
            }
            catch (Exception ex)
            {
                if (defaultValue != null)
                    attributeValue = defaultValue;
                else
                {
                    _errMsg.Length = 0;
                    _errMsg.Append("Unable to retrieve requested attribute: ");
                    _errMsg.Append(attributeName);
                    _errMsg.Append("\r\n");
                    _errMsg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                    throw new System.Exception(_errMsg.ToString());
                }
            }
            return attributeValue;
        }

        /// <summary>
        /// Modifies the value of the attribute for the specified XmlElement.
        /// </summary>
        /// <param name="xmlElement">Element containing the attribute to update.</param>
        /// <param name="attributeName">Name of attribute to update.</param>
        /// <param name="attributeValue">New value to apply to the attribute.</param>
        public void UpdateAttribute(XmlElement xmlElement,
                                    String attributeName,
                                    String attributeValue)
        {
            xmlElement.SetAttribute(attributeName, attributeValue);
        }

        /// <summary>
        /// Modified the inner text for the specified XmlElement.
        /// </summary>
        /// <param name="xmlElement">Element to modify.</param>
        /// <param name="newInnerText">Inner text to apply to the specified XmlElement.</param>
        public void UpdateElement(XmlElement xmlElement,
                                  string newInnerText)
        {
            xmlElement.InnerText = newInnerText;
        }


        ///// <summary>
        ///// Routine overrides default ToString method and outputs name and value for all public properties.
        ///// </summary>
        ///// <returns></returns>
        //public override string ToString()
        //{
        //    StringBuilder data = new StringBuilder();
        //    Type t = this.GetType();
        //    PropertyInfo[] props = t.GetProperties();
        //    int inx = 0;
        //    int maxInx = props.Length - 1;

        //    for (inx = 0; inx <= maxInx; inx++)
        //    {
        //        PropertyInfo prop = props[inx];
        //        object val = prop.GetValue(this, null);
        //        data.Append(prop.Name);
        //        data.Append(": ");
        //        if (val != null)
        //            data.Append(val.ToString());
        //        else
        //            data.Append("<null value>");
        //        data.Append("  ");
        //    }

        //    return data.ToString();
        //}


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
                object val = prop.GetValue(this, null);

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


    }//end class

}//end namespace
