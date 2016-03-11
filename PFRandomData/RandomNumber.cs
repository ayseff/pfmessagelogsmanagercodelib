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
    /// Generates random numeric values for various .NET numeric types.
    /// </summary>
    public class RandomNumber
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private Random _random = new Random();

        //private varialbles for properties

        //constructors
        /// <summary>
        /// Constructor for the class. Does nothing.
        /// </summary>
        public RandomNumber()
        {
            ;
        }

        //properties

        //methods
        private double GenerateRandomInteger(double min, double max)
        {
            return (_random.NextDouble() * (max - min + 1)) + min;
        }

        private double GenerateRandomDouble(double min, double max)
        {
            return (_random.NextDouble() * (max - min)) + min;
        }
        
        /// <summary>
        /// Generates an int32 number in the specified range.
        /// </summary>
        /// <param name="min">Output will be equal to or greater than this number.</param>
        /// <param name="max">Output will be less than this number.</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to min and less than or equal to max
        /// </returns>
        /// <remarks>Uses Next method of the .NET Framework Random class.</remarks>
        public int GenerateRandomInt(int min, int max)
        {
            return _random.Next(min, max+1);
        }
        /// <summary>
        /// Generates random integer with a value within min to max range.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>Returns random int32.</returns>
        /// <remarks>Method first generates a random double which it converts to an integer value.</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        public int GenerateRandomNumber(int min, int max)
        {
            //return (int)GenerateRandomNumber((double)min, (double)max);
            return (int)GenerateRandomInteger((double)min, (double)max);
        }
        /// <summary>
        /// Generates random double.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>Double</returns>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        /// 
        public double GenerateRandomNumber(double min, double max)
        {
            //return (_random.NextDouble() * (max - min + 1)) + min;
            return GenerateRandomDouble(min, max);
        }

        /// <summary>
        /// Generates random float.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>float</returns>
        public float GenerateRandomNumber(float min, float max)
        {
            return (float)GenerateRandomDouble((double)min, (double)max);
        }

        /// <summary>
        /// Generates random unsigned integer.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>uint</returns>
        public uint GenerateRandomNumber(uint min, uint max)
        {
            return (uint)GenerateRandomInteger((double)min, (double)max);
        }

        /// <summary>
        /// Generates random long integer.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>long</returns>
        public long GenerateRandomNumber(long min, long max)
        {
            return (long)GenerateRandomInteger((double)min, (double)max);
        }

        /// <summary>
        /// Generates random unsigned long integer.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>ulong</returns>
        public ulong GenerateRandomNumber(ulong min, ulong max)
        {
            return (ulong)GenerateRandomInteger((double)min, (double)max);
        }

        /// <summary>
        /// Generates random short integer.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>short</returns>
        public short GenerateRandomNumber(short min, short max)
        {
            return (short)GenerateRandomInteger((double)min, (double)max);
        }

        /// <summary>
        /// Generates random unsigned short integer.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>ushort</returns>
        public ushort GenerateRandomNumber(ushort min, ushort max)
        {
            return (ushort)GenerateRandomInteger((double)min, (double)max);
        }

        /// <summary>
        /// Generates random decimal number.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>decimal</returns>
        public decimal GenerateRandomNumber(decimal min, decimal max)
        {
            return (decimal)GenerateRandomDouble((double)min, (double)max);
        }

        /// <summary>
        /// Generates random byte.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>byte</returns>
        public byte GenerateRandomNumber(byte min, byte max)
        {
            return (byte)GenerateRandomInteger((double)min, (double)max);
        }

        /// <summary>
        /// Generates random signed byte.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>sbyte</returns>
        public sbyte GenerateRandomNumber(sbyte min, sbyte max)
        {
            return (sbyte)GenerateRandomInteger((double)min, (double)max);
        }

        //************************************************************************************************
        //Functions to generate arrays of random numbers defined next
        //************************************************************************************************

        /// <summary>
        /// Method generates an array of random int values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>
        /// An array of 32-bit signed integers, each greater than or equal to min and less than or equal to max.
        /// </returns>
        /// <remarks>Uses Next method of the .NET Framework Random class.</remarks>
        public int[] GenerateRandomInt(int min, int max, int arraySize)
        {
            int[] randomNumbers = new int[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = _random.Next(min, max + 1);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random integer values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>int[] array</returns>
        /// <remarks>Method first generates a random double which it converts to an integer value.</remarks>
        public int[] GenerateRandomNumber(int min, int max, int arraySize)
        {
            int[] randomNumbers = new int[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (int)GenerateRandomInteger((double)min, (double)max); 
            }

            return randomNumbers;
        }


        /// <summary>
        /// Method generates an array of random double values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>double[] array</returns>
        public double[] GenerateRandomNumber(double min, double max, int arraySize)
        {
            double[] randomNumbers = new double[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = GenerateRandomDouble(min, max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random float values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>float[] array</returns>
        public float[] GenerateRandomNumber(float min, float max, int arraySize)
        {
            float[] randomNumbers = new float[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (float)GenerateRandomDouble((double)min, (double)max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random unsigned integer values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>uint[] array</returns>
        public uint[] GenerateRandomNumber(uint min, uint max, int arraySize)
        {
            uint[] randomNumbers = new uint[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (uint)GenerateRandomInteger((double)min, (double)max); 
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random long integer values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>long[] array</returns>
        public long[] GenerateRandomNumber(long min, long max, int arraySize)
        {
            long[] randomNumbers = new long[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (long)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random unsigned long integer values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>ulong[] array</returns>
        public ulong[] GenerateRandomNumber(ulong min, ulong max, int arraySize)
        {
            ulong[] randomNumbers = new ulong[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (ulong)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random short integer values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>short[] array</returns>
        public short[] GenerateRandomNumber(short min, short max, int arraySize)
        {
            short[] randomNumbers = new short[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (short)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random unsigned short integer values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>ushort[] array</returns>
        public ushort[] GenerateRandomNumber(ushort min, ushort max, int arraySize)
        {
            ushort[] randomNumbers = new ushort[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (ushort)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }


        /// <summary>
        /// Method generates an array of random unsigned decimal values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>decimal[] array</returns>
        public decimal[] GenerateRandomNumber(decimal min, decimal max, int arraySize)
        {
            decimal[] randomNumbers = new decimal[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (decimal)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random byte values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>byte[] array</returns>
        public byte[] GenerateRandomNumber(byte min, byte max, int arraySize)
        {
            byte[] randomNumbers = new byte[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (byte)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random signed byte values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>sbyte[] array</returns>
        public sbyte[] GenerateRandomNumber(sbyte min, sbyte max, int arraySize)
        {
            sbyte[] randomNumbers = new sbyte[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (sbyte)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }


    }//end class
}//end namespace
