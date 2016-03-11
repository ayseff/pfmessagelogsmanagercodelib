using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppGlobals
{
    /// <summary>
    /// Class for various string manipulations.
    /// </summary>
    public class AppTextGlobals
    {
        /// <summary>
        /// Converts a string to a bool value.
        /// </summary>
        /// <param name="psYesNo">String to be converted.</param>
        /// <returns>Bool value.</returns>
        /// <remarks>Yes or No string value returns true; all other values return false.</remarks>
        public static bool ConvertYesNoToTrueFalse(string psYesNo)
        {
            bool bTrueFalse = false;
            switch (psYesNo.ToLower())
            {
                case "yes":
                    bTrueFalse = true;
                    break;
                case "true":
                    bTrueFalse = true;
                    break;
                default:
                    bTrueFalse = false;
                    break;
            }
            return bTrueFalse;
        }

        /// <summary>
        /// Converts string to a bool value.
        /// </summary>
        /// <param name="psValue">Value to be converted.</param>
        /// <returns>Boolean value.</returns>
        /// <remarks>Default conversion value if value is not one of following (yes, true, no, false) is false.</remarks>
        public static bool ConvertStringToBoolean(string psValue)
        {

            return ConvertStringToBoolean(psValue, "False");

        }

        /// <summary>
        /// Converts string to a bool value.
        /// </summary>
        /// <param name="psValue">Value to be converted.</param>
        /// <param name="psDefaultValue">Default conversion value if value is not one of following: yes, true, no, false.</param>
        /// <returns>Boolean value.</returns>
        public static bool ConvertStringToBoolean(string psValue, string psDefaultValue)
        {
            bool bValue = false;
            bool bDefaultValue = false;

            if (psDefaultValue != null)
            {
                if (psDefaultValue.ToUpper() == "TRUE")
                {
                    bDefaultValue = true;
                }
                else
                {
                    bDefaultValue = false;
                }
            }

            switch (psValue.ToLower())
            {
                case "true":
                    bValue = true;
                    break;
                case "yes":
                    bValue = true;
                    break;
                case "false":
                    bValue = false;
                    break;
                case "no":
                    bValue = false;
                    break;
                default:
                    bValue = bDefaultValue;
                    break;
            }

            return bValue;
        }

        /// <summary>
        /// Converts string to integer.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <returns>int value.</returns>
        /// <remarks>If string cannot be converted (e.g. it is not a number) then 0 is returned.</remarks>
        public static int ConvertStringToInt(string psValue)
        {
            return ConvertStringToInt(psValue, 0);
        }

        /// <summary>
        /// Converts string to integer.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="pnDefaultValue">Value to return if value is not a number.</param>
        /// <returns>int value.</returns>
        public static int ConvertStringToInt(string psValue, int pnDefaultValue)
        {
            int nValue = 0;
            try
            {
                nValue = Convert.ToInt32(psValue);
            }
            catch
            {
                nValue = pnDefaultValue;
            }

            return nValue;
        }

        /// <summary>
        /// Converts string to long.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <returns>long value.</returns>
        /// <remarks>If string cannot be converted (e.g. it is not a number) then 0 is returned.</remarks>
        public static long ConvertStringToLong(string psValue)
        {
            return ConvertStringToLong(psValue, 0);
        }

        /// <summary>
        /// Converts string to long.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="pnDefaultValue">Value to return if value is not a number.</param>
        /// <returns>long value.</returns>
        public static long ConvertStringToLong(string psValue, long pnDefaultValue)
        {
            long nValue = 0;
            try
            {
                nValue = Convert.ToInt64(psValue);
            }
            catch
            {
                nValue = pnDefaultValue;
            }
            return nValue;
        }

        /// <summary>
        /// Converts string to float.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <returns>float value.</returns>
        /// <remarks>If string cannot be converted (e.g. it is not a number) then 0 is returned.</remarks>
        public static float ConvertStringToFloat(string psValue)
        {
            return ConvertStringToFloat(psValue, (float)0.0);
        }

        /// <summary>
        /// Converts string to float.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="pnDefaultValue">Value to return if value is not a number.</param>
        /// <returns>float value.</returns>
        public static float ConvertStringToFloat(string psValue, float pnDefaultValue)
        {
            float nValue = (float)0.0;
            try
            {
                nValue = Convert.ToSingle(psValue);
            }
            catch
            {
                nValue = pnDefaultValue;
            }
            return nValue;
        }

        /// <summary>
        /// Converts string to double.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <returns>double value.</returns>
        /// <remarks>If string cannot be converted (e.g. it is not a number) then 0 is returned.</remarks>
        public static double ConvertStringToDouble(string psValue)
        {
            return ConvertStringToDouble(psValue, (double)0.0);
        }

        /// <summary>
        /// Converts string to double.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="pnDefaultValue">Value to return if value is not a number.</param>
        /// <returns>double value.</returns>
        public static double ConvertStringToDouble(string psValue, double pnDefaultValue)
        {
            double nValue = (double)0.0;
            try
            {
                nValue = Convert.ToDouble(psValue);
            }
            catch
            {
                nValue = pnDefaultValue;
            }
            return nValue;
        }


        /// <summary>
        /// Converts string to DateTime value.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="psDefaultValue">Value to return if value is not a Date/Time.</param>
        /// <returns>DateTime value.</returns>
        public static DateTime ConvertStringToDateTime(string psValue, string psDefaultValue)
        {
            DateTime dValue;
            DateTime dDefaultValue;
            try
            {
                if (DateTime.TryParse(psDefaultValue, out dDefaultValue) == false)
                    dDefaultValue = DateTime.MinValue;
            }
            catch
            {
                dDefaultValue = DateTime.MinValue;
            }

            try
            {
                if (DateTime.TryParse(psValue, out dValue) == false)
                    dValue = dDefaultValue;
            }
            catch
            {
                dValue = dDefaultValue;
            }

            return dValue;
        }

        /// <summary>
        /// Converts string to DateTime value.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="pdDefaultValue">Value to return if value is not a Date/Time.</param>
        /// <returns>DateTime value.</returns>
        public static DateTime ConvertStringToDateTime(string psValue, DateTime pdDefaultValue)
        {
            DateTime dValue;
               
            try
            {
                if (DateTime.TryParse(psValue, out dValue) == false)
                    dValue = pdDefaultValue;
            }
            catch
            {
                dValue = pdDefaultValue;
            }

            return dValue;
        }


        /// <summary>
        /// Converts string to TimeSpan value.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="psDefaultValue">Value to return if value is not a TimeSpan.</param>
        /// <returns>TimeSpan value.</returns>
        public static TimeSpan ConvertStringToTimeSpan(string psValue, string psDefaultValue)
        {
            TimeSpan tsValue;
            TimeSpan tsDefaultValue;
            try
            {
                if (TimeSpan.TryParse(psDefaultValue, out tsDefaultValue) == false)
                    tsDefaultValue = TimeSpan.MinValue;
            }
            catch
            {
                tsDefaultValue = TimeSpan.MinValue;
            }

            try
            {
                if (TimeSpan.TryParse(psValue, out tsValue) == false)
                    tsValue = tsDefaultValue;
            }
            catch
            {
                tsValue = tsDefaultValue;
            }

            return tsValue;
        }

        /// <summary>
        /// Converts string to TimeSpan value.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="tsDefaultValue">Value to return if value is not a TimeSpan.</param>
        /// <returns>TimeSpan value.</returns>
        public static TimeSpan ConvertStringToTimeSpan(string psValue, TimeSpan tsDefaultValue)
        {
            TimeSpan tsValue;

            try
            {
                if (TimeSpan.TryParse(psValue, out tsValue) == false)
                    tsValue = tsDefaultValue;
            }
            catch
            {
                tsValue = tsDefaultValue;
            }

            return tsValue;
        }


        /// <summary>
        /// Gets length of a string.
        /// </summary>
        /// <param name="psStringValue">String to measure.</param>
        /// <returns>Length of string.</returns>
        /// <remarks>Use this is value could be null or empty string.</remarks>
        public static int StringLength(string psStringValue)
        {
            int nLen = 0;
            if (String.IsNullOrEmpty(psStringValue))
                nLen = 0;
            else
                nLen = psStringValue.Length;
            return nLen;
        }

        /// <summary>
        /// Reverses a string.
        /// </summary>
        /// <param name="psStringValue">String to reverse.</param>
        /// <returns>Reversed string.</returns>
        public static string ReverseString(string psStringValue)
        {

            char[] rev = psStringValue.ToCharArray();
            Array.Reverse(rev);
            return (new string(rev));

        }

        /// <summary>
        /// Converts string to an array of bytes.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <returns>Byte array.</returns>
        public static byte[] ConvertStringToByteArray(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Converts byte array to a string.
        /// </summary>
        /// <param name="bytes">Byte array to convert.</param>
        /// <returns>String.</returns>
        public static string ConvertByteArrayToString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }


        /// <summary>
        /// Returns string containing a character repeated the specified number of times.
        /// </summary>
        /// <param name="c">Char value to repeat.</param>
        /// <param name="count">Number of times to repeat the char value.</param>
        /// <returns>String containing the repeated char value.</returns>
        public static string RepeatChar(char c, int count)
        {
            return new string(c, count);
        }

        /// <summary>
        /// Converts char array to a string.
        /// </summary>
        /// <param name="chars">char array to convert.</param>
        /// <returns>String.</returns>
        public static string ConvertCharArrayToString(char[] chars)
        {
            return new String(chars);
        }

        /// <summary>
        /// Convert a char value to a string value.
        /// </summary>
        /// <param name="ch">char to convert.</param>
        /// <returns>String.</returns>
        public static string ConvertCharToString(char ch)
        {
            return new String(ch, 1);
        }

    }//end class
}//end namespace
