using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Globalization;

namespace AppGlobals
{
    /// <summary>
    /// Class contains properties that retrieve information on the currently executing assembly.
    /// </summary>
    public class AppInfo
    {
        // Fields
        private static Assembly _asm = Assembly.GetEntryAssembly();
        //private static AssemblyName _assemblyNameAttribute;
        private static AssemblyCompanyAttribute _companyAttribute;
        private static AssemblyCopyrightAttribute _copyrightAttribute;
        //private static AssemblyCultureAttribute _cultureAttribute;
        private static AssemblyDescriptionAttribute _descriptionAttribute;
        private static AssemblyProductAttribute _productAttribute;
        private static AssemblyTitleAttribute _titleAttribute;
        private static AssemblyTrademarkAttribute _trademarkAttribute;
        //private static AssemblyVersionAttribute _versionAttribute;

        // Properties
        /// <summary>
        /// Gets the company name custom attribute from the assembly manifest.
        /// </summary>
        public static string AssemblyCompany
        {
            get
            {
                _companyAttribute = (AssemblyCompanyAttribute)_asm.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0];
                return _companyAttribute.Company;
            }
        }
        /// <summary>
        /// Get the copyright custom attribute from the assembly manifest.
        /// </summary>
        public static string AssemblyCopyright
        {
            get
            {
                _copyrightAttribute = (AssemblyCopyrightAttribute)_asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0];
                return _copyrightAttribute.Copyright;
            }
        }

        /// <summary>
        /// Specifies which culture the assembly supports.
        /// </summary>
        public static string AssemblyCulture
        {
            get
            {
                return CultureInfo.CurrentCulture.ToString();
            }
        }

        /// <summary>
        /// Gets the text description for the assembly.
        /// </summary>
        public static string AssemblyDescription
        {
            get
            {
                _descriptionAttribute = (AssemblyDescriptionAttribute)_asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0];
                return _descriptionAttribute.Description;
            }
        }

        /// <summary>
        /// Describes an assembly's unique identity in full.
        /// </summary>
        public static string AssemblyName
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Name;
            }
        }

        /// <summary>
        /// Gets the product name custom attribute from the assembly manifest.
        /// </summary>
        public static string AssemblyProduct
        {
            get
            {
                _productAttribute = (AssemblyProductAttribute)_asm.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0];
                return _productAttribute.Product;
            }
        }

        /// <summary>
        /// Gets the description for the assembly.
        /// </summary>
        public static string AssemblyTitle
        {
            get
            {
                _titleAttribute = (AssemblyTitleAttribute)_asm.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
                return _titleAttribute.Title;
            }
        }

        /// <summary>
        /// Gets the trademark custom attribute from the assembly manifest.
        /// </summary>
        public static string AssemblyTrademark
        {
            get
            {
                _trademarkAttribute = (AssemblyTrademarkAttribute)_asm.GetCustomAttributes(typeof(AssemblyTrademarkAttribute), false)[0];
                return _trademarkAttribute.Trademark;
            }
        }

        /// <summary>
        /// Gets the version in string format of the assembly from the manifest.
        /// </summary>
        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Gets the version as a Version object of the assembly from the manifest.
        /// </summary>
        public static Version AssemblyVersionEx
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Version;
            }
        }

        /// <summary>
        /// Retrieves local directory path of the currently executing assembly.
        /// </summary>
        public static string CurrentExecutingAssemblyDirectory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            }
        }

        /// <summary>
        /// Retrieves local path of the currently executing assembly.
        /// </summary>
        public static string CurrentExecutingAssembly
        {
            get
            {
                return new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            }
        }

        /// <summary>
        /// Retrieves local directory path of the currently entry assembly.
        /// </summary>
        public static string CurrentEntryAssemblyDirectory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase).LocalPath);
            }
        }

        /// <summary>
        /// Retrieves local path of the currently entry assembly.
        /// </summary>
        public static string CurrentEntryAssembly
        {
            get
            {
                return new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase).LocalPath;
            }
        }


    }//end class
}//end namespace
