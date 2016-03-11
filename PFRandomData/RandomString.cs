//#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFRandomData
{
    /// <summary>
    /// Class has methods to generate random strings.
    /// </summary>
    public class RandomString
    {
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

        char[] LC = new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
        
        char[] UC = new char[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

        char[] DEC = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

        char[] HEX = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                'A', 'B', 'C', 'D', 'E', 'F'};
        char[] LCCON = new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };

        char[] UCCON = new char[] { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };

        char[] LCVOW = new char[] { 'a', 'e', 'i', 'o', 'u', 'y' };

        char[] UCVOW = new char[] { 'A', 'E', 'I', 'O', 'U', 'Y' };

        string[] SYLLABLE_PATTERNS = new string[] {"CV", "VC", "CVC", "CVV", "CVVC", "VVC", "VCV", "VVCV", "VVCC", "VVCCV", "CCV", "CCVC" };


        
        //methods
        /// <summary>
        /// Generates random string using characters between ascii 32 and 127 (\ excluded).
        /// </summary>
        /// <param name="size">Number of characters in random string.</param>
        /// <returns>String of random characters.</returns>
        /// <remarks>Backslash is excluded to make sure the resulting string can be stored correctly in a string variable.</remarks>
        /// <example>
        /// <code>
        /// int stringLength=100;
        /// string retval = string.Empty;
        /// RandomString rs = new RandomString();
        /// retval = rs.GetString(stringLength);
        /// </code>
        /// </example>
        public string GetString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                //ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));  //returns capital letters
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(95 * rand.NextDouble() + 32)));
                if (ch == '\\' || ch == '"')
                    ch = '*';
                builder.Append(ch);
            }

            return builder.ToString();
        }

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
            for (int i = 0; i < size; i++)
            {
                ch = AN[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
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
            for (int i = 0; i < size; i++)
            {
                ch = ANUC[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
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
            for (int i = 0; i < size; i++)
            {
                ch = ANLC[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
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
            for (int i = 0; i < size; i++)
            {
                ch = ANX[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
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
            for (int i = 0; i < size; i++)
            {
                ch = AL[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
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
            for (int i = 0; i < size; i++)
            {
                ch = LC[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
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
            for (int i = 0; i < size; i++)
            {
                ch = UC[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
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
            for (int i = 0; i < size; i++)
            {
                ch = DEC[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
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
            for (int i = 0; i < size; i++)
            {
                ch = HEX[Convert.ToInt32(Math.Floor(numPossibleRandomChars * rand.NextDouble()))];
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Retrieves a random string that can include backslash (\) and double-quote characters ("). 
        /// Ascii characters between decimal 32 and 127 can be included in the return string.
        /// </summary>
        /// <param name="size">Size of random string.</param>
        /// <returns>String of random characters.</returns>
        public string GetStringIncludingBackslashAndDoubleQuotes(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(95 * rand.NextDouble() + 32)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Retrieves string of random ascii characters from decimal 33 to 127.
        /// Space, backslash and double-quote characters are excluded from inclusion in random string output.
        /// </summary>
        /// <param name="size">Size of the random string.</param>
        /// <returns>String of random characters.</returns>
        public string GetStringNoSpaces(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                //ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));  //returns capital letters
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(94 * rand.NextDouble() + 33)));
                if (ch == '\\' || ch == '"')
                    ch = '*';
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates random syllables in lower case.
        /// </summary>
        /// <param name="size">Number of syllables to generate.</param>
        /// <returns>String containing the random syllables.</returns>
        public string GetRandomSyllablesLC(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            double maxPatterns = SYLLABLE_PATTERNS.Length;
            int maxCON = LCCON.Length;
            int maxVOW = LCVOW.Length;
            for (int i = 0; i < size; i++)
            {
                string pat = SYLLABLE_PATTERNS[Convert.ToInt32(Math.Floor(maxPatterns * rand.NextDouble()))];
                for (int j = 0; j < pat.Length; j++)
                {
                    if (pat.Substring(j, 1) == "C")
                    {
                        ch = LCCON[Convert.ToInt32(Math.Floor(maxCON * rand.NextDouble()))];
                        builder.Append(ch);
                    }
                    else //==V
                    {
                        ch = LCVOW[Convert.ToInt32(Math.Floor(maxVOW * rand.NextDouble()))];
                        builder.Append(ch);
                    }
                }
            }

            return builder.ToString();
        }


        /// <summary>
        /// Generates random syllables in upper case.
        /// </summary>
        /// <param name="size">Number of syllables to generate.</param>
        /// <returns>String containing the random syllables.</returns>
        public string GetRandomSyllablesUC(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            double maxPatterns = SYLLABLE_PATTERNS.Length;
            int maxCON = UCCON.Length;
            int maxVOW = UCVOW.Length;
            for (int i = 0; i < size; i++)
            {
                string pat = SYLLABLE_PATTERNS[Convert.ToInt32(Math.Floor(maxPatterns * rand.NextDouble()))];
                for (int j = 0; j < pat.Length; j++)
                {
                    if (pat.Substring(j, 1) == "C")
                    {
                        ch = UCCON[Convert.ToInt32(Math.Floor(maxCON * rand.NextDouble()))];
                        builder.Append(ch);
                    }
                    else //==V
                    {
                        ch = UCVOW[Convert.ToInt32(Math.Floor(maxVOW * rand.NextDouble()))];
                        builder.Append(ch);
                    }
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates random syllables with first letter of first syllable in upper case and all othrs in lower case.
        /// </summary>
        /// <param name="size">Number of syllables to generate.</param>
        /// <returns>String containing the random syllables.</returns>
        public string GetRandomSyllablesUCLC(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            double maxPatterns = SYLLABLE_PATTERNS.Length;
            int maxCON = LCCON.Length;
            int maxVOW = LCVOW.Length;
            int maxUCCON = UCCON.Length;
            int maxUCVOW = UCVOW.Length;
            for (int i = 0; i < size; i++)
            {
                string pat = SYLLABLE_PATTERNS[Convert.ToInt32(Math.Floor(maxPatterns * rand.NextDouble()))];
                for (int j = 0; j < pat.Length; j++)
                {
                    if (pat.Substring(j, 1) == "C")
                    {
                        if (i == 0 && j == 0)
                        {
                            ch = UCCON[Convert.ToInt32(Math.Floor(maxUCCON * rand.NextDouble()))];
                        }
                        else
                        {
                            ch = LCCON[Convert.ToInt32(Math.Floor(maxCON * rand.NextDouble()))];
                        }
                        builder.Append(ch);
                    }
                    else //==V
                    {
                        if (i == 0 && j == 0)
                        {
                            ch = UCVOW[Convert.ToInt32(Math.Floor(maxUCVOW * rand.NextDouble()))];
                        }
                        else
                        {
                            ch = LCVOW[Convert.ToInt32(Math.Floor(maxVOW * rand.NextDouble()))];
                        }
                        builder.Append(ch);
                    }
                }
            }

            return builder.ToString();
        }



    
    }//end class
}//end namespace
//#pragma warning restore 1591
