//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppGlobals
{
    /// <summary>
    /// Enumerates various byte count descriptions.
    /// </summary>
    public enum ByteUnits
    {
#pragma warning disable 1591
        Bytes = 1,
        Kilobytes = 2,
        Megabytes = 3,
        Gigabytes = 4,
        Terabytes = 5,
        Petabytes = 6,
        Exabytes = 7
#pragma warning restore 1591
    }
    /// <summary>
    /// Class contains various methods for converted numbers between different formats.
    /// </summary>
    public class AppNumberGlobals
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private static double[] ByteUnitMultipliers = new double[7] {(double)1.0,                      //byte
                                                                     (double)1024.0,                   //kilobyte 
                                                                     (double)1048576.0,                //megabyte
                                                                     (double)1073741824.0,             //gigabyte 
                                                                     (double)1099511627776.0,          //terabyte
                                                                     (double)1125899906842620.0,       //petabyte
                                                                     (double)1152921504606850000.0};   //exabyte

        //private variables for properties;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public AppNumberGlobals()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Use this to convert a number from one unit of byte measuremen to another. For example, convert from bytes to kilobytes or vice versa.
        ///  For example, double result = ConvertByteUnits(2048.0, ByteUnits.Bytes, ByteUnits.Kilobytes) returns 2.0 (i.e. 2KB).
        /// </summary>
        /// <param name="fromNumber">Number to convert.</param>
        /// <param name="fromByteUnits">Current unit of measurement.</param>
        /// <param name="toByteUnits">Desired unit of measurement.</param>
        /// <returns>Revised number in new unit of measurement.</returns>
        public static double ConvertByteUnits (double fromNumber, ByteUnits fromByteUnits, ByteUnits toByteUnits)
        {
            double retval = -1.0;

            retval = (fromNumber * ByteUnitMultipliers[(int)fromByteUnits]) / ByteUnitMultipliers[(int)toByteUnits];

            return retval;
        }

        /// <summary>
        /// Function returns a dictionary list containing all the elements of the specified et (enum type).
        /// </summary>
        /// <param name="et">The enum type to process.</param>
        /// <returns>Dictionary list containing an int representing the integer value of the enum element and string representing the descriptive name of the enum element.</returns>

        public static Dictionary<int, string> GetEnumValues(System.Type et)
        {
            Dictionary<int, string> enumValues = new Dictionary<int, string>();

            foreach (var e in Enum.GetValues(et))
            {
                enumValues.Add((int)e, e.ToString()); 
            }

            return enumValues;
        }



    }//end class
}//end namespace
