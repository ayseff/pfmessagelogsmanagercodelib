//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFCollectionsObjects
{
    /// <summary>
    /// Set of methods for allowing the sorting of key lists.
    /// </summary>
    public class PFListComparers
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public PFListComparers()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Compares two strings.
        /// </summary>
        /// <param name="x">First string to compare.</param>
        /// <param name="y">Second string to compare.</param>
        /// <returns>Less than zero: x precedes y in the sort order.  Zero: x occurs in the same position in the sort order as y. Greater than zero: x follows y in the sort order. </returns>
        public static int CompareListString(string x, string y)
        {
            int ret = 0;

            if (x != null && y != null)
            {
                ret = x.CompareTo(y);
            }
            else
            {
                if (x != null)
                    ret = 1;
                else if (y != null)
                    ret = -1;
                else
                    ret = 0;
            }

            return ret;
        }

        /// <summary>
        /// Compares two int values.
        /// </summary>
        /// <param name="x">First int to compare.</param>
        /// <param name="y">Second int to compare.</param>
        /// <returns>Less than zero: x precedes y in the sort order.  Zero: x occurs in the same position in the sort order as y. Greater than zero: x follows y in the sort order. </returns>
        public static int CompareListInt(int x, int y)
        {
            int ret = 0;

            ret = x.CompareTo(y);

            return ret;
        }

        /// <summary>
        /// Compares two long values.
        /// </summary>
        /// <param name="x">First long to compare.</param>
        /// <param name="y">Second long to compare.</param>
        /// <returns>Less than zero: x precedes y in the sort order.  Zero: x occurs in the same position in the sort order as y. Greater than zero: x follows y in the sort order. </returns>
        public static int CompareListLong(long x, long y)
        {
            int ret = 0;

            ret = x.CompareTo(y);

            return ret;
        }

        /// <summary>
        /// Compares two double values.
        /// </summary>
        /// <param name="x">First double to compare.</param>
        /// <param name="y">Second double to compare.</param>
        /// <returns>Less than zero: x precedes y in the sort order.  Zero: x occurs in the same position in the sort order as y. Greater than zero: x follows y in the sort order. </returns>
        public static int CompareListDouble(double x, double y)
        {
            int ret = 0;

            ret = x.CompareTo(y);

            return ret;
        }

        /// <summary>
        /// Compares two decimal values.
        /// </summary>
        /// <param name="x">First decimal to compare.</param>
        /// <param name="y">Second decimal to compare.</param>
        /// <returns>Less than zero: x precedes y in the sort order.  Zero: x occurs in the same position in the sort order as y. Greater than zero: x follows y in the sort order. </returns>
        public static int CompareListDecimal(decimal x, decimal y)
        {
            int ret = 0;

            ret = x.CompareTo(y);

            return ret;
        }

        /// <summary>
        /// Compares two DateTime values.
        /// </summary>
        /// <param name="x">First DateTime to compare.</param>
        /// <param name="y">Second DateTime to compare.</param>
        /// <returns>Less than zero: x precedes y in the sort order.  Zero: x occurs in the same position in the sort order as y. Greater than zero: x follows y in the sort order. </returns>
        public static int CompareListDateTime(DateTime x, DateTime y)
        {
            int ret = 0;

            ret = x.CompareTo(y);

            return ret;
        }



    }//end class
}//end namespace
