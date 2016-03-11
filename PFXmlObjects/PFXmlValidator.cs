using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using AppGlobals;

namespace PFXmlObjects
{
    /// <summary>
    /// Class contains routine that will verify validity of an XmlDocument based on a specified Xml Schema file.
    /// </summary>
    public class XMLValidator
    {
        private StringBuilder _validationErrors = new StringBuilder();
        
        /// <summary>
        /// Returns all validation errors recorded by Validate method. Returns empty string if no errors.
        /// </summary>
        public string ValidationErrors
        {
            get
            {
                return _validationErrors.ToString();
            }
        }

        //XMLDocEx 

        /// <summary>
        /// Routine to verify whether or not XML in a file is well-formed.
        /// </summary>
        /// <param name="xmlFile">Path to Xml file.</param>
        /// <param name="xsdFile">Path to Xml Schema file that will be used to validate the data.</param>
        /// <returns>True if Xml is well-formed. Otherwise returns false.</returns>
        /// <remarks>Check ValidationErrors property for the detailed error messages returned by this routine if any Xml errors were encountered.</remarks>
        public bool Validate (string xmlFile, string xsdFile)
        {
            bool xmlIsValid = false;

            string revisedXmlDocument = string.Empty;
            string namespaceURI = string.Empty;

            PFXmlDocument xsdDoc = new PFXmlDocument();
            PFXmlDocument tempXmlDoc = new PFXmlDocument();

            try
            {
                xsdDoc.LoadFromFile(xsdFile);
                if (xsdDoc.DocumentRootNode.Attributes.Count > 0)
                {
                    namespaceURI = xsdDoc.GetAttributeValue(xsdDoc.DocumentRootNode, "xmlns", string.Empty);
                }

                tempXmlDoc.LoadFromFile(xmlFile);
                if (tempXmlDoc.DocumentRootNode.Attributes.Count == 0 && namespaceURI.Length > 0)
                    tempXmlDoc.AddNewAttribute((XmlElement)tempXmlDoc.DocumentRootNode, "xmlns", namespaceURI);
                revisedXmlDocument = tempXmlDoc.XmlDoc.OuterXml;
                tempXmlDoc = null;


                _validationErrors.Length = 0;

                PFXmlDocument xmlDoc = new PFXmlDocument();
                xmlDoc.XmlDoc.Schemas.Add(null, xsdFile);

                
                xmlDoc.LoadFromString(revisedXmlDocument);
                ValidationEventHandler oEventHandler = new ValidationEventHandler(XmlDocValidationEventHandler);
                xmlDoc.XmlDoc.Validate(oEventHandler);
            }
            catch (Exception ex)
            {
                _validationErrors.Append("Error: ");
                _validationErrors.Append(AppMessages.FormatErrorMessage(ex));
                _validationErrors.Append("\r\n");
            }

            if (_validationErrors.Length == 0)
                xmlIsValid = true;

            return xmlIsValid;
            
        }


        private void XmlDocValidationEventHandler(object sender, ValidationEventArgs e)
        {

            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    _validationErrors.Append("Error: ");
                    _validationErrors.Append(e.Message);
                    _validationErrors.Append("\r\n");
                    break;
                case XmlSeverityType.Warning:
                    _validationErrors.Append("Warning: ");
                    _validationErrors.Append(e.Message);
                    _validationErrors.Append("\r\n");
                    break;
            }

        }

    }//end class
}//end namespace
