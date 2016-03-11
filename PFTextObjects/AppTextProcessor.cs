using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace PFTextObjects
{
    /// <summary>
    /// Classes containing static routines for various string conversions and manipulations.
    /// </summary>
    public class PFTextProcessor
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
                case "t":
                    bValue = true;
                    break;
                case "yes":
                    bValue = true;
                    break;
                case "false":
                    bValue = false;
                    break;
                case "f":
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
        /// Converts string to Byte.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <returns>Byte value.</returns>
        /// <remarks>If string cannot be converted (e.g. it is not a number or is too large for a Byte) then 0 is returned.</remarks>
        public static Byte ConvertStringToByte(string psValue)
        {
            return ConvertStringToByte(psValue, 0);
        }

        /// <summary>
        /// Converts string to Byte.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="pnDefaultValue">Value to return if value is invalid.</param>
        /// <returns>Byte value.</returns>
        public static Byte ConvertStringToByte(string psValue, Byte pnDefaultValue)
        {
            Byte nValue = 0;
            try
            {
                nValue = Convert.ToByte(psValue);
            }
            catch
            {
                nValue = pnDefaultValue;
            }

            return nValue;
        }

        /// <summary>
        /// Converts string to SByte.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <returns>SByte value.</returns>
        /// <remarks>If string cannot be converted (e.g. it is not a number or is too large for a SByte) then 0 is returned.</remarks>
        public static SByte ConvertStringToSByte(string psValue)
        {
            return ConvertStringToSByte(psValue, 0);
        }

        /// <summary>
        /// Converts string to SByte.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="pnDefaultValue">Value to return if value is invalid.</param>
        /// <returns>SByte value.</returns>
        public static SByte ConvertStringToSByte(string psValue, SByte pnDefaultValue)
        {
            SByte nValue = 0;
            try
            {
                nValue = Convert.ToSByte(psValue);
            }
            catch
            {
                nValue = pnDefaultValue;
            }

            return nValue;
        }

        /// <summary>
        /// Converts string to short integer.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <returns>Short value.</returns>
        /// <remarks>If string cannot be converted (e.g. it is not a number or is too large for a Short) then 0 is returned.</remarks>
        public static short ConvertStringToShort(string psValue)
        {
            return ConvertStringToShort(psValue, 0);
        }

        /// <summary>
        /// Converts string to short integer.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="pnDefaultValue">Value to return if value is invalid.</param>
        /// <returns>Short value.</returns>
        public static short ConvertStringToShort(string psValue, short pnDefaultValue)
        {
            short nValue = 0;
            try
            {
                nValue = Convert.ToInt16(psValue);
            }
            catch
            {
                nValue = pnDefaultValue;
            }

            return nValue;
        }

        /// <summary>
        /// Converts string to unsigned short integer.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <returns>ushort value.</returns>
        /// <remarks>If string cannot be converted (e.g. it is not a number or is too large for a ushort) then 0 is returned.</remarks>
        public static ushort ConvertStringToushort(string psValue)
        {
            return ConvertStringToushort(psValue, 0);
        }

        /// <summary>
        /// Converts string to unsigned short integer.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="pnDefaultValue">Value to return if value is invalid.</param>
        /// <returns>ushort value.</returns>
        public static ushort ConvertStringToushort(string psValue, ushort pnDefaultValue)
        {
            ushort nValue = 0;
            try
            {
                nValue = Convert.ToUInt16(psValue);
            }
            catch
            {
                nValue = pnDefaultValue;
            }

            return nValue;
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
        /// Converts string to unsigned integer.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <returns>uint value.</returns>
        /// <remarks>If string cannot be converted (e.g. it is not a number) then 0 is returned.</remarks>
        public static uint ConvertStringToUint(string psValue)
        {
            return ConvertStringToUint(psValue, 0);
        }

        /// <summary>
        /// Converts string to unsigned integer.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="pnDefaultValue">Value to return if value is not a number.</param>
        /// <returns>uint value.</returns>
        public static uint ConvertStringToUint(string psValue, uint pnDefaultValue)
        {
            uint nValue = 0;
            try
            {
                nValue = Convert.ToUInt32(psValue);
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
        /// <returns>int value.</returns>
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
        /// Converts string to unsigned long.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <returns>ulong value.</returns>
        /// <remarks>If string cannot be converted (e.g. it is not a number) then 0 is returned.</remarks>
        public static ulong ConvertStringToUlong(string psValue)
        {
            return ConvertStringToUlong(psValue, 0);
        }

        /// <summary>
        /// Converts string to unsigned long.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="pnDefaultValue">Value to return if value is not a number.</param>
        /// <returns>int value.</returns>
        public static ulong ConvertStringToUlong(string psValue, ulong pnDefaultValue)
        {
            ulong nValue = 0;
            try
            {
                nValue = Convert.ToUInt64(psValue);
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
        /// Converts string to Decimal.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <returns>Decimal value.</returns>
        /// <remarks>If string cannot be converted (e.g. it is not a number or is too large for a Decimal) then 0 is returned.</remarks>
        public static Decimal ConvertStringToDecimal(string psValue)
        {
            return ConvertStringToDecimal(psValue, 0);
        }

        /// <summary>
        /// Converts string to Decimal.
        /// </summary>
        /// <param name="psValue">Value to convert.</param>
        /// <param name="pnDefaultValue">Value to return if value is invalid.</param>
        /// <returns>Decimal value.</returns>
        public static Decimal ConvertStringToDecimal(string psValue, Decimal pnDefaultValue)
        {
            Decimal nValue = 0;
            try
            {
                nValue = Convert.ToDecimal(psValue);
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
        /// Routine to capitalize the first letter of a string.
        /// </summary>
        /// <param name="psValue">String value to be formatted as a title.</param>
        /// <returns>String with first letter capitalized and all subsequent letters in lower case.</returns>
        public static string ConvertStringToTitleFormat(string psValue)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(psValue.ToLower());
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
        /// Converts byte array to a char array.
        /// </summary>
        /// <param name="bytes">Byte array to convert.</param>
        /// <returns>char[].</returns>
        public static char[] ConvertByteArrayToCharArray(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return chars;
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
        /// Converts byte array to an ascii string.
        /// </summary>
        /// <param name="bytes">Byte array to convert.</param>
        /// <returns>String.</returns>
        public static string ConvertByteArrayToAsciiString(byte[] bytes)
        {
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// Converts byte array to an ascii extended string.
        /// </summary>
        /// <param name="bytes">Byte array to convert.</param>
        /// <returns>String.</returns>
        public static string ConvertByteArrayToAsciiExtendedString(byte[] bytes)
        {
            return System.Text.Encoding.GetEncoding("iso-8859-1").GetString(bytes);
        }

        /// <summary>
        /// Converts byte array to an UTF8 string.
        /// </summary>
        /// <param name="bytes">Byte array to convert.</param>
        /// <returns>String.</returns>
        public static string ConvertByteArrayToUTF8String(byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Converts byte array to a string formatted as hexadecimal numbers.
        /// </summary>
        /// <param name="bytes">Byte array to convert.</param>
        /// <returns>String.</returns>
        public static string ConvertByteArrayToHexString(byte[] bytes)
        {
            //StringBuilder hex = new StringBuilder();
            //foreach (byte c in bytes)
            //{
            //    hex.AppendFormat("{0:x2}", c);
            //}
            //return hex.ToString();

            string hex = BitConverter.ToString(bytes);
            //return hex.Replace("-", "");
            return hex;
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
        /// Converts char array to a byte array.
        /// </summary>
        /// <param name="chars">Char array to convert.</param>
        /// <returns>byte[]</returns>
        public static byte[] ConvertCharArrayToByteArray(char[] chars)
        {
            string s = new String(chars);
            byte[] b = new byte[s.Length * sizeof(char)];
            System.Buffer.BlockCopy(s.ToCharArray(), 0, b, 0, b.Length);
            return b;

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
        /// Converts char array to a string formatted as hexadecimal numbers.
        /// </summary>
        /// <param name="chars">Char array to convert.</param>
        /// <returns>String.</returns>
        public static string ConvertCharArrayToHexString(char[] chars)
        {
            //StringBuilder hex = new StringBuilder();
            string s = ConvertCharArrayToString(chars);
            byte[] b = ConvertStringToByteArray(s);
            return ConvertByteArrayToHexString(b);

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

        /// <summary>
        /// Returns whether or not a specified string represents a number.
        /// </summary>
        /// <param name="str">String to be parsed for a valid number.</param>
        /// <returns>True if string represents a number; otherwise returns false.</returns>
        public static bool StringIsNumeric(string str)
        {
            bool isNumeric = false;
            double num;

            try
            {
                isNumeric = Double.TryParse(str, out num);
            }
            catch
            {
                isNumeric = false;
            }

            return isNumeric;
        }

        /// <summary>
        /// Returns whether or not a specified string represents an integer.
        /// </summary>
        /// <param name="str">String to be parsed for a valid integer.</param>
        /// <returns>True if string represents an integer; otherwise returns false.</returns>
        public static bool StringIsInt(string str)
        {
            bool isNumeric = false;
            int num;

            try
            {
                isNumeric = Int32.TryParse(str, out num);
            }
            catch
            {
                isNumeric = false;
            }

            return isNumeric;
        }

        /// <summary>
        /// Returns whether or not a specified string represents a long integer.
        /// </summary>
        /// <param name="str">String to be parsed for a valid long integer.</param>
        /// <returns>True if string represents a long integer; otherwise returns false.</returns>
        public static bool StringIsLong(string str)
        {
            bool isNumeric = false;
            long num;

            try
            {
                isNumeric = Int64.TryParse(str, out num);
            }
            catch
            {
                isNumeric = false;
            }

            return isNumeric;
        }

        /// <summary>
        /// Produces error message in format that includes difference parts of the message. Stack trace is not included in the result.
        /// </summary>
        /// <param name="pex">Exception object.</param>
        /// <returns>String with formatted error message.</returns>
        public static string FormatErrorMessage(Exception pex)
        {
            StringBuilder errMsg = new StringBuilder();
            try
            {
                errMsg.Append("Source: ");
                errMsg.Append(pex.Source);
                errMsg.Append(": \r\n");
                errMsg.Append(pex.Message);
                Exception iex = pex.InnerException;
                while (iex != null)
                {
                    errMsg.Append("\r\n\r\nInner Exception:\r\nSource: ");
                    errMsg.Append(iex.Source);
                    errMsg.Append(": \r\n");
                    errMsg.Append(iex.Message);
                    iex = iex.InnerException;
                }
            }
            catch (Exception ex)
            {
                errMsg.Append("\r\n*** UNEXPECTED ERROR: ***\r\nUnable to format error message.\n");
                errMsg.Append("Source: ");
                errMsg.Append(ex.Source);
                errMsg.Append(": \r\n");
                errMsg.Append(ex.Message);
            }
            return errMsg.ToString();
        }

        /// <summary>
        /// Produces error message in format that includes difference parts, including the stack trace, of the message.
        /// </summary>
        /// <param name="pex">Exception object.</param>
        /// <returns>String with formatted error message.</returns>
        public static string FormatErrorMessageWithStackTrace(Exception pex)
        {
            StringBuilder errMsg = new StringBuilder();
            try
            {
                errMsg.Append("Source: ");
                errMsg.Append(pex.Source);
                errMsg.Append(": \r\n");
                errMsg.Append(pex.Message);
                errMsg.Append(": \r\n");
                errMsg.Append(pex.StackTrace);
                Exception iex = pex.InnerException;
                while (iex != null)
                {
                    errMsg.Append("\r\n\r\nInner Exception:\r\nSource: ");
                    errMsg.Append(iex.Source);
                    errMsg.Append(": \r\n");
                    errMsg.Append(iex.Message);
                    errMsg.Append(": \r\n");
                    errMsg.Append(iex.StackTrace);
                    iex = iex.InnerException;
                }
            }
            catch (Exception ex)
            {
                errMsg.Append("\r\n*** UNEXPECTED ERROR: ***\r\nUnable to format error message.\n");
                errMsg.Append("Source: ");
                errMsg.Append(ex.Source);
                errMsg.Append(": \r\n");
                errMsg.Append(ex.Message);
            }
            return errMsg.ToString();

        }

        /// <summary>
        /// Calculates the TimeSpan between the start and end times and returns the parts of the TimeSpan formatted into a string.
        /// </summary>
        /// <param name="startTime">DateTime representing the start of the time interval.</param>
        /// <param name="endTime">DateTime representing the end of the time interval.</param>
        /// <returns>String formatted as n day(s) n hour(s) n minute(s) n second(s).</returns>
        /// <remarks>Parts of the TimeSpan only shown if they have a value. For example, if Days value is 0 then the n day(s) portion of the formatted output is suppressed.</remarks>
        public static string FormatElapsedTime(DateTime startTime, DateTime endTime)
        {
            return FormatElapsedTime(startTime, endTime, false);
        }

        /// <summary>
        /// Calculates the TimeSpan between the start and end times and returns the parts of the TimeSpan formatted into a string.
        /// </summary>
        /// <param name="startTime">DateTime representing the start of the time interval.</param>
        /// <param name="endTime">DateTime representing the end of the time interval.</param>
        /// <param name="showMilliseconds">If true, milliseconds will be included in the formatted output.</param>
        /// <returns>String formatted as n day(s) n hour(s) n minute(s) n second(s) n ms.</returns>
        /// <remarks>Parts of the TimeSpan only shown if they have a value. For example, if Days value is 0 then the n day(s) portion of the formatted output is suppressed.</remarks>
        public static string FormatElapsedTime(DateTime startTime, DateTime endTime, bool showMilliseconds)
        {
            if (startTime > endTime)
            {
                throw new System.Exception("End time must be greater than or equal to start time.");
            }

            TimeSpan datediff = endTime.Subtract(startTime);

            return FormatTimeSpan(datediff, showMilliseconds);
        }

        /// <summary>
        /// Returns the parts of the TimeSpan formatted into a string.
        /// </summary>
        /// <param name="ts">TimeSpan object.</param>
        /// <returns>String formatted as n day(s) n hour(s) n minute(s) n second(s).</returns>
        /// <remarks>Parts of the TimeSpan only shown if they have a value. For example, if Days value is 0 then the n day(s) portion of the formatted output is suppressed.</remarks>
        public static string FormatTimeSpan(TimeSpan ts)
        {
            return FormatTimeSpan(ts, false);
        }

        /// <summary>
        /// Returns the parts of the TimeSpan formatted into a string.
        /// </summary>
        /// <param name="ts">TimeSpan object.</param>
        /// <param name="showMilliseconds">If true, milliseconds will be included in the formatted output.</param>
        /// <returns>String formatted as n day(s) n hour(s) n minute(s) n second(s) n ms.</returns>
        /// <remarks>Parts of the TimeSpan only shown if they have a value. For example, if Days value is 0 then the n day(s) portion of the formatted output is suppressed.</remarks>
        public static string FormatTimeSpan(TimeSpan ts, bool showMilliseconds)
        {
            StringBuilder formattedTimeSpan = new StringBuilder();


            if (ts.Days > 0)
            {
                formattedTimeSpan.Append(ts.Days.ToString());
                if (ts.Days != 1)
                {
                    formattedTimeSpan.Append(" days ");
                }
                else
                {
                    formattedTimeSpan.Append(" day ");
                }
            }

            if (ts.Hours > 0)
            {
                formattedTimeSpan.Append(ts.Hours.ToString());
                if (ts.Hours != 1)
                {
                    formattedTimeSpan.Append(" hours ");
                }
                else
                {
                    formattedTimeSpan.Append(" hour ");
                }
            }

            if (ts.Minutes > 0)
            {
                formattedTimeSpan.Append(ts.Minutes.ToString());
                if (ts.Minutes != 1)
                {
                    formattedTimeSpan.Append(" minutes ");
                }
                else
                {
                    formattedTimeSpan.Append(" minute ");
                }
            }

            formattedTimeSpan.Append(ts.Seconds.ToString());
            if (ts.Seconds != 1)
            {
                formattedTimeSpan.Append(" seconds ");
            }
            else
            {
                formattedTimeSpan.Append(" second ");
            }

            if (showMilliseconds)
            {
                formattedTimeSpan.Append(ts.Milliseconds.ToString());
                formattedTimeSpan.Append(" ms ");
            }




            return formattedTimeSpan.ToString();
        }

        /// <summary>
        /// Converts a string array into a concatenated string.
        /// </summary>
        /// <param name="strArray">Array of strings.</param>
        /// <returns>String value.</returns>
        /// <remarks>Inserts a space after each element in the array when the element is appended to the concatenated result string.</remarks>
        public static string ConvertStringArrayToString(string[] strArray)
        {
            return ConvertStringArrayToString(strArray, " ");
        }

        /// <summary>
        /// Converts a string array into a concatenated string.
        /// </summary>
        /// <param name="strArray">Array of strings.</param>
        /// <param name="separatorAfterConversion">Separate to insert after each element in the array when the element is appended to the concatenated result string.</param>
        /// <returns>String value.</returns>
        public static string ConvertStringArrayToString(string[] strArray, string separatorAfterConversion)
        {
            string str = String.Join(separatorAfterConversion, strArray);
            return str;
        }

    }//end class
}//end namespace
