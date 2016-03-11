using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PFFileSystemObjects
{
    /// <summary>
    /// Represents a FileSpec running on the
    /// <see cref="System.Text.RegularExpressions"/> engine.
    /// </summary>
    public class PFFileSpec : Regex
    {
        /// <summary>
        /// Initializes a FileSpec with the given search pattern.
        /// </summary>
        /// <param name="pattern">The FileSpec pattern to match.</param>
        public PFFileSpec(string pattern)
            : base(FileSpecToRegex(pattern))
        {
        }

        /// <summary>
        /// Initializes a FileSpec with the given search pattern and options.
        /// </summary>
        /// <param name="pattern">The FileSpec pattern to match.</param>
        /// <param name="options">A combination of one or more System.Text.RegexOption.</param>
        public PFFileSpec(string pattern, RegexOptions options)
            : base(FileSpecToRegex(pattern), options)
        {
        }

        /// <summary>
        /// Converts a FileSpec to a regex.
        /// </summary>
        /// <param name="pattern">The FileSpec pattern to convert.</param>
        /// <returns>A regex equivalent of the given FileSpec.</returns>
        public static string FileSpecToRegex(string pattern)
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
