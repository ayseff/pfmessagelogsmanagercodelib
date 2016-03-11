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
    /// Class for generating byte arrays containing random characters.
    /// </summary>
    public class RandomBytes
    {
        //private work variables
        //private StringBuilder _msg = new StringBuilder();
        private Random _random = new Random();

        //private varialbles for properties

        //constructors
        /// <summary>
        /// Constructor for the class. Does nothing.
        /// </summary>
        public RandomBytes()
        {
            ;
        }

        //properties

        //methods
        /// <summary>
        /// Method for generating a byte array containing random values.
        /// </summary>
        /// <param name="numBytesToGenerate">Number of random bytes to produce.</param>
        /// <returns>Byte array.</returns>
        public byte[] GenerateRandomBytes(int numBytesToGenerate)
        {
            Byte[] b = new Byte[numBytesToGenerate];
            _random.NextBytes(b);
            return b;
        }

        /// <summary>
        /// Method for generating a char array containing random values.
        /// </summary>
        /// <param name="numCharsToGenerate">Number of char values to produce.</param>
        /// <returns>Char array.</returns>
        public char[] GenerateRandomChars(int numCharsToGenerate)
        {
            byte[] b = GenerateRandomBytes(numCharsToGenerate);
            char[] c = new char[numCharsToGenerate];
            int inx = 0;
            for (inx = 0; inx < numCharsToGenerate; inx++)
            {
                c[inx] = (char)b[inx];
            }
            return c;
        }

    }//end class
}//end namespace
