//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2013
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFListObjects
{
    /// <summary>
    /// Set of methods for allowing the sorting of key/value lists.
    /// </summary>

    public class PFKeyValueListExComparers
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFKeyValueListExComparers()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Compares two keys that are int values.
        /// </summary>
        /// <param name="x">First key/value pair to compare.</param>
        /// <param name="y">Second key/value pair to compare.</param>
        /// <returns>Less than zero: x precedes y in the sort order.  Zero: x occurs in the same position in the sort order as y. Greater than zero: x follows y in the sort order. </returns>
        public static int CompareKeyValueListIntString(pfKeyValuePair<int, string> x, pfKeyValuePair<int, string> y)
        {
            int ret = 0;

            if (x.Key < y.Key)
                ret = -1;
            else if (x.Key > y.Key)
                ret = 1;
            else
                ret = 0;

            return ret;
        }


        /// <summary>
        /// Compares two keys that are string values.
        /// </summary>
        /// <param name="x">First key/value pair to compare.</param>
        /// <param name="y">Second key/value pair to compare.</param>
        /// <returns>Less than zero: x precedes y in the sort order.  Zero: x occurs in the same position in the sort order as y. Greater than zero: x follows y in the sort order. </returns>
        public static int CompareKeyValueListStringString(pfKeyValuePair<string, string> x, pfKeyValuePair<string, string> y)
        {
            int ret = 0;

            if (x.Key != null && y.Key != null)
            {
                ret = x.Key.CompareTo(y.Key);
            }
            else
            {
                if (x.Key != null)
                    ret = 1;
                else if (y.Key != null)
                    ret = -1;
                else
                    ret = 0;
            }

            return ret;
        }

    }//end class
}//end namespace
