//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFRandomData
{
    /// <summary>
    /// Class for producing repeated random characters. (For example, a line consisting of nothing but the letter A repeated x number of times.
    /// </summary>
    public class RandomSingleCharacterRepeated
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private Random rand = new Random((int)DateTime.Now.Ticks);
        char[] AN = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

        char[] ANUC = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                  'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

        char[] ANLC = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};

        char[] ANX = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                                 '!','@','#','$','%','^','&','*','(',')','_','+',':',';','?','>','<',
                                 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

        char[] AL = new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

        char[] LC = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        char[] UC = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        char[] DEC = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        char[] HEX = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                'A', 'B', 'C', 'D', 'E', 'F'};

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public RandomSingleCharacterRepeated()
        {
            ;
        }

        //properties

        //methods
        /// <summary>
        /// Generates alpha numeric random string (a-z, A-Z, 0-9)
        /// </summary>
        /// <param name="size">Number of characters in random string.</param>
        /// <returns>String of random characters.</returns>
        public string GetStringAN(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            double numPossibleRandomChars = (double)AN.Length;
            ch = AN[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
            for (int i = 0; i < size; i++)
            {
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates alpha numeric random string (A-Z, 0-9)
        /// </summary>
        /// <param name="size">Number of characters in random string.</param>
        /// <returns>String of random characters.</returns>
        public string GetStringANUC(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            double numPossibleRandomChars = (double)ANUC.Length;
            ch = ANUC[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
            for (int i = 0; i < size; i++)
            {
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates alpha numeric random string (a-z, 0-9)
        /// </summary>
        /// <param name="size">Number of characters in random string.</param>
        /// <returns>String of random characters.</returns>
        public string GetStringANLC(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            double numPossibleRandomChars = (double)ANLC.Length;
            ch = ANLC[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
            for (int i = 0; i < size; i++)
            {
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates alpha numeric random string (a-z, A-Z, 0-9) or random punctuation marks from among the following: !   @   #   $   %   ^  &amp;  *   (   )   _   +   :   ?   ;  &gt; &lt;
        /// </summary>
        /// <param name="size">Number of characters in random string</param>
        /// <returns></returns>
        public string GetStringANX(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            double numPossibleRandomChars = (double)ANX.Length;
            ch = ANX[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
            for (int i = 0; i < size; i++)
            {
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates upper and lower case letters random string (a-z, A-Z).
        /// </summary>
        /// <param name="size">Number of characters in random string</param>
        /// <returns></returns>
        public string GetStringAL(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            double numPossibleRandomChars = (double)AL.Length;
            ch = AL[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
            for (int i = 0; i < size; i++)
            {
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates alpha random string of lower case letters (a-z).
        /// </summary>
        /// <param name="size">Number of characters in random string</param>
        /// <returns></returns>
        public string GetStringLC(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            double numPossibleRandomChars = (double)LC.Length;
            ch = LC[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
            for (int i = 0; i < size; i++)
            {
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates alpha random string of Upper case letters (A-Z).
        /// </summary>
        /// <param name="size">Number of characters in random string</param>
        /// <returns></returns>
        public string GetStringUC(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            double numPossibleRandomChars = (double)UC.Length;
            ch = UC[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
            for (int i = 0; i < size; i++)
            {
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates alpha random string of numeric characters (0-9).
        /// </summary>
        /// <param name="size">Number of characters in random string</param>
        /// <returns></returns>
        public string GetStringDEC(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            double numPossibleRandomChars = (double)DEC.Length;
            ch = DEC[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
            for (int i = 0; i < size; i++)
            {
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates alpha random string of hexadecimal numeric characters (0-9, A-F).
        /// </summary>
        /// <param name="size">Number of characters in random string</param>
        /// <returns></returns>
        public string GetStringHEX(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            double numPossibleRandomChars = (double)HEX.Length;
            ch = HEX[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
            for (int i = 0; i < size; i++)
            {
                builder.Append(ch);
            }

            return builder.ToString();
        }



    }//end class
}//end namespace
