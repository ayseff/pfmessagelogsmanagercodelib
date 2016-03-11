//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PFTextObjects
{
    /// <summary>
    /// Class for searching strings using a pattern mask. Mask uses wildcards like those used for Windows file spec masks. Pattern matches are case insensitive.
    /// </summary>
    public class PFSearchPattern : Regex 
    {

        /// <summary>
        /// Initializes a FileSpec with the given search pattern.
        /// </summary>
        /// <param name="pattern">The FileSpec pattern to match.</param>
        public PFSearchPattern(string pattern)
            : base(SearchPatternToRegex(pattern))
        {
        }

        /// <summary>
        /// Initializes a FileSpec with the given search pattern and options.
        /// </summary>
        /// <param name="pattern">The FileSpec pattern to match.</param>
        /// <param name="options">A combination of one or more System.Text.RegexOption.</param>
        public PFSearchPattern(string pattern, RegexOptions options)
            : base(SearchPatternToRegex(pattern), options)
        {
        }

        /// <summary>
        /// Converts a FileSpec to a regex.
        /// </summary>
        /// <param name="pattern">The FileSpec pattern to convert.</param>
        /// <returns>A regex equivalent of the given FileSpec.</returns>
        public static string SearchPatternToRegex(string pattern)
        {
            //return "^" + Regex.Escape(pattern).
            // Replace("\\*", ".*").
            // Replace("\\?", ".") + "$";
            StringBuilder sRegExFileSpec = new StringBuilder();
            string sPattern = pattern;

            //return "^" + pattern.Trim().Replace(".", @"\.").Replace("^",@"\^").Replace("*", ".*").Replace("?", @".?") + "$";

            sRegExFileSpec.Length = 0;
            sRegExFileSpec.Append("^(?i)");
            sPattern = sPattern.Replace(".", @"\.");
            sPattern = sPattern.Replace("^", @"\^");
            sPattern = sPattern.Replace("$", @"\$");
            sPattern = sPattern.Replace("*", @".*");
            sPattern = sPattern.Replace("?", @".?");
            sRegExFileSpec.Append(sPattern);
            sRegExFileSpec.Append("$");

            return sRegExFileSpec.ToString();
        }

    }//end class
}//end namespace
